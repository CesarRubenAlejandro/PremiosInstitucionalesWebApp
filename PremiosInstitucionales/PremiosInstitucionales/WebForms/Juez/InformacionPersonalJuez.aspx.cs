using PremiosInstitucionales.DBServices.InformacionPersonalJuez;
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
    public partial class InformacionPersonalJuez : System.Web.UI.Page
    {
        MP_Global MasterPage = new MP_Global();
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar si ya expiro la sesion
            if (Session.Contents.Count == 0)
            {
                Response.Redirect("~/WebForms/Error/Error401.aspx", false);
            }

            MasterPage = (MP_Global)Page.Master;
            if (!IsPostBack)
            {
                // revisar la primera vez que se carga la pagina que se haya iniciado sesion con cuenta de juez
                if (Session[StringValues.RolSesion] != null)
                {
                    if (Session[StringValues.RolSesion].ToString() != StringValues.RolJuez)
                    {
                        // si no es juez, redireccionar a inicio general
                        Response.Redirect("~/WebForms/Login.aspx", false);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:getProfileReferences(); ", true);
                        MostrarCampos();
                        ResetFields();
                    }
                }
                else
                {
                    Response.Redirect("~/WebForms/Login.aspx", false);
                }
            }

            var status = Request.QueryString["s"];
            if (status == "success")
            {
                MasterPage.ShowMessage("Aviso", "Cambios realizados con éxito.");
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
            var juez = InformacionPersonalJuezService.GetJuezByCorreo(Session[StringValues.CorreoSesion].ToString());
            NombresTextBox.Text = juez.Nombre;
            ApellidosTextBox.Text = juez.Apellido;
            
            if (juez.NombreImagen != null && juez.NombreImagen.Length > 0)
            {
                if (File.Exists(Server.MapPath("~/ProfilePictures/" + juez.NombreImagen)))
                {
                    avatarImage.Attributes.Add("style", "background-image: url(/ProfilePictures/" + juez.NombreImagen + ")");
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
                    Response.Redirect("InformacionPersonalJuez.aspx?s=success", false);
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
            var juez = InformacionPersonalJuezService.GetJuezByCorreo(Session[StringValues.CorreoSesion].ToString());

            string sCurrentPassword = currentPwdTextBox.Text;
            if (juez.Password == sha256(sCurrentPassword))
            {
                if (newPwdTextBox.Text == confirmNewPwdTextBox.Text)
                {
                    Regex regexNumero = new Regex(@".*\d.*");
                    Regex regexLetra = new Regex(@".*[a-zA-z].*");
                    Match matchNumero = regexNumero.Match(newPwdTextBox.Text);
                    Match matchLetra = regexLetra.Match(newPwdTextBox.Text);

                    if (newPwdTextBox.Text.Length >= 6 && matchNumero.Success && matchLetra.Success)
                    {
                        if (juez != null)
                        {
                            juez.Password = sha256(newPwdTextBox.Text);
                            if (InformacionPersonalJuezService.UpdateJuez(juez))
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
            var juez = InformacionPersonalJuezService.GetJuezByCorreo(Session[StringValues.CorreoSesion].ToString());
            if (juez != null)
            {
                juez.Nombre = NombresTextBox.Text;
                juez.Apellido = ApellidosTextBox.Text;

                if (!InformacionPersonalJuezService.UpdateJuez(juez))
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
                            // Get logged in judge
                            var juez = InformacionPersonalJuezService.GetJuezByCorreo(Session[StringValues.CorreoSesion].ToString());
                            if (juez.NombreImagen != null && juez.NombreImagen.Length > 0)
                            {
                                // Delete previous image...
                                File.Delete(Server.MapPath("~/ProfilePictures/") + juez.NombreImagen);
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
                            InformacionPersonalJuezService.CambiaImagen(null, Session[StringValues.CorreoSesion].ToString(), sNombreImagen);
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

        protected void BackBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("InicioJuez.aspx", false);
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