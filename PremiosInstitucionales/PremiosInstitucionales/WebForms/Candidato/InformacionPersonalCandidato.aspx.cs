using PremiosInstitucionales.DBServices.InformacionPersonalCandidato;
using PremiosInstitucionales.Values;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace PremiosInstitucionales.WebForms
{
    public partial class InformacionPersonalCandidato : System.Web.UI.Page
    {
        MP_Global MasterPage = new MP_Global();
        protected void Page_Load(object sender, EventArgs e)
        {
            MasterPage = (MP_Global)Page.Master;
            if (!IsPostBack)
            {
                // revisar la primera vez que se carga la pagina que se haya iniciado sesion con cuenta de candidato
                if (Session[StringValues.RolSesion] != null)
                {
                    if (Session[StringValues.RolSesion].ToString() != StringValues.RolCandidato)
                        // si no es candidato, redireccionar a login
                        Response.Redirect("~/WebForms/Login.aspx", false);
                }
                else
                {
                    Response.Redirect("~/WebForms/Login.aspx", false);
                }

                ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:getProfileReferences(); ", true);
                MostrarCampos();
                ResetFields();
            }

            CheckPrivacy();

            var status = Request.QueryString["s"];
            if (status == "success")
            {
                MasterPage.ShowMessage("Aviso", "Cambios realizados con éxito.");
            }
        }

        private void CheckPrivacy()
        {
            var candidato = InformacionPersonalCandidatoService.GetCandidatoByCorreo(Session[StringValues.CorreoSesion].ToString());
            if (!candidato.FechaPrivacidadDatos.HasValue)
            {
                guardarCambiosBtn.Style.Add("display", "none");
                avisoPrivacidad.Style.Add("display", "inline-block");
            }
            else
            {
                guardarCambiosBtn.Style.Add("display", "inline-block");
                avisoPrivacidad.Style.Add("display", "none");
            }
        }

        private void ResetFields()
        {
            currentPwdTextBox.Text = "";
            newPwdTextBox.Text = "";
            confirmNewPwdTextBox.Text = "";
            CorreoTextBox.Text = Session[StringValues.CorreoSesion].ToString();
        }

        private void MostrarCampos()
        {
            var candidato = InformacionPersonalCandidatoService.GetCandidatoByCorreo(Session[StringValues.CorreoSesion].ToString());
            NombresTextBox.Text = candidato.Nombre;
            ApellidosTextBox.Text = candidato.Apellido;
            DomicilioTextBox.Text = candidato.Direccion;
            RFCTextBox.Text = candidato.RFC;
            TelefonoTextBox.Text = candidato.Telefono;
            NacionalidadTextBox.Text = candidato.Nacionalidad;

            if (candidato.NombreImagen != null && candidato.NombreImagen.Length > 0)
            {
                if (File.Exists(Server.MapPath("~/ProfilePictures/" + candidato.NombreImagen)))
                {
                    avatarImage.Attributes.Add("style", "background-image: url(/ProfilePictures/" + candidato.NombreImagen + ")");
                }
            }
        }

        protected void EnviarBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Upload(sender, e))
                {
                    ActualizarDatosGenerales();
                    CheckPrivacy();
                    Response.Redirect("InformacionPersonalCandidato.aspx?s=success", false);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                MasterPage.ShowMessage("Error", "El servidor encontró un error al procesar la solicitud.");
            }
        }

        protected void CambiarContrasena_Click(object sender, EventArgs e)
        {
            try
            {
                ActualizarContrasena();
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                MasterPage.ShowMessage("Error", "El servidor encontró un error al procesar la solicitud.");
            }
        }

        protected void ActualizarContrasena()
        {
            var candidato = InformacionPersonalCandidatoService.GetCandidatoByCorreo(Session[StringValues.CorreoSesion].ToString());

            string sCurrentPassword = currentPwdTextBox.Text;
            if (candidato.Password == sha256(sCurrentPassword))
            {
                if (newPwdTextBox.Text == confirmNewPwdTextBox.Text)
                {
                    Regex regexNumero = new Regex(@".*\d.*");
                    Regex regexLetra = new Regex(@".*[a-zA-z].*");
                    Match matchNumero = regexNumero.Match(newPwdTextBox.Text);
                    Match matchLetra = regexLetra.Match(newPwdTextBox.Text);

                    if (newPwdTextBox.Text.Length >= 6 && matchNumero.Success && matchLetra.Success)
                    {
                        if (candidato != null)
                        {
                            candidato.Password = sha256(newPwdTextBox.Text);
                            if (InformacionPersonalCandidatoService.UpdateCandidato(candidato))
                            {
                                MasterPage.ShowMessage("Aviso", "Contraseña cambiada con éxito.");
                            }
                            else
                            {
                                MostrarCampos();
                            }
                        }
                    }
                    else
                    {
                        MasterPage.ShowMessage("Error", "Contraseña debe ser de al menos 6 caracteres y debe contener al menos un número y una letra.");
                    }
                }
                else
                {
                    MasterPage.ShowMessage("Error", "Contraseñas no coinciden.");
                }
            }
            else
            {
                MasterPage.ShowMessage("Error", "Contraseña actual incorrecta.");
            }

            ResetFields();
        }

        protected void ActualizarDatosGenerales()
        {
            var candidato = InformacionPersonalCandidatoService.GetCandidatoByCorreo(Session[StringValues.CorreoSesion].ToString());
            if (candidato != null)
            {
                candidato.Nombre = NombresTextBox.Text;
                candidato.Apellido = ApellidosTextBox.Text;
                candidato.Direccion = DomicilioTextBox.Text.ToString();
                candidato.Nacionalidad = NacionalidadTextBox.Text.ToString();
                candidato.RFC = RFCTextBox.Text.ToString();
                candidato.Telefono = TelefonoTextBox.Text.ToString();

                if (!candidato.FechaPrivacidadDatos.HasValue)
                {
                    candidato.FechaPrivacidadDatos = DateTime.Today.Date;
                }

                if (!InformacionPersonalCandidatoService.UpdateCandidato(candidato))
                {
                    MostrarCampos();
                }
            }
        }

        protected bool Upload(object sender, EventArgs e)
        {
            if (FileUploadImage.HasFile)
            {
                // Get filename
                string fileName = Path.GetFileName(FileUploadImage.PostedFile.FileName);

                // Get string image format (png, jpg, etc)
                var startIndex = fileName.LastIndexOf(".");
                var endIndex = fileName.Length - startIndex;
                string sFormat = fileName.Substring(startIndex, endIndex).ToLower();
                string sNombreImagen = Guid.NewGuid().ToString() + sFormat;

                // Formatos Validos
                List<String> supportedFormats = new List<String>()
                {
                    ".png",
                    ".bmp",
                    ".jpg",
                    ".jpeg"
                };

                if (supportedFormats.Contains(sFormat))
                {
                    using (var image = Image.FromStream(FileUploadImage.PostedFile.InputStream, true, true))
                    {
                        using (var newImage = ScaleImage(image, 364, 364))
                        {
                            // Get logged in candidate
                            var candidato = InformacionPersonalCandidatoService.GetCandidatoByCorreo(Session[StringValues.CorreoSesion].ToString());
                            if (candidato.NombreImagen != null && candidato.NombreImagen.Length > 0)
                            {
                                // Delete previous image...
                                File.Delete(Server.MapPath("~/ProfilePictures/") + candidato.NombreImagen);
                            }

                            // Upload image to server
                            switch (sFormat)
                            {
                                case ".png":
                                    newImage.Save(Server.MapPath("~/ProfilePictures/") + sNombreImagen, ImageFormat.Png);
                                    break;
                                case ".bmp":
                                    newImage.Save(Server.MapPath("~/ProfilePictures/") + sNombreImagen, ImageFormat.Bmp);
                                    break;
                                default:
                                    newImage.Save(Server.MapPath("~/ProfilePictures/") + sNombreImagen, ImageFormat.Jpeg);
                                    break;
                            }

                            // Update data in database
                            InformacionPersonalCandidatoService.CambiaImagen(null, Session[StringValues.CorreoSesion].ToString(), sNombreImagen);
                            return true;
                        }
                    }
                }
                else
                {
                    MasterPage.ShowMessage("Error", "La imagen proporcionada debe estar en formato PNG , JPG o BMP.");
                    return false;
                }
            }
            return true;
        }

        protected void BackBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("InicioCandidato.aspx", false);
        }

        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            return newImage;
        }

        static string sha256(string rawPassword)
        {
            System.Security.Cryptography.SHA256Managed crypt = new System.Security.Cryptography.SHA256Managed();
            System.Text.StringBuilder hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(rawPassword), 0, Encoding.UTF8.GetByteCount(rawPassword));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}