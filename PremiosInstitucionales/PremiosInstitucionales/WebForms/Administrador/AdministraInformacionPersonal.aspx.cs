using PremiosInstitucionales.DBServices.InformacionPersonalCandidato;
using PremiosInstitucionales.DBServices.InformacionPersonalJuez;
using PremiosInstitucionales.Values;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace PremiosInstitucionales.WebForms
{
    public partial class AdministraInformacionPersonal : System.Web.UI.Page
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
                // revisar la primera vez que se carga la pagina que se haya iniciado sesion con cuenta de admin
                if (Session[StringValues.RolSesion] != null)
                {
                    if (Session[StringValues.RolSesion].ToString() != StringValues.RolAdmin)
                    {
                        // si no es admin, redireccionar a inicio general
                        Response.Redirect("~/WebForms/Login.aspx", false);
                    }
                }
                else
                {
                    Response.Redirect("~/WebForms/Login.aspx", false);
                }

                string sUserType = Request.QueryString["t"];
                string sUserId = Request.QueryString["id"];
                if (sUserType != null && sUserId != null)
                {
                    ResetFields();

                    if (sUserType.Equals("juez"))
                    {
                        LoadJudgeInformation(sUserId);
                    }
                    else if (sUserType.Equals("candidato"))
                    {
                        LoadCandidateInformation(sUserId);
                    }
                    else
                    {
                        Response.Redirect("InicioAdmin.aspx", false);
                    }
                }
                else
                {
                    Response.Redirect("InicioAdmin.aspx", false);
                }
            }

            var status = Request.QueryString["s"];
            if (status == "success")
            {
                MasterPage.ShowMessage("Aviso", "Cambios realizados con éxito.");
            }
        }

        private void LoadCandidateInformation(string id)
        {
            controlFormJuez.Style.Add("display", "none");
            var candidato = InformacionPersonalCandidatoService.GetCandidatoById(id);

            if (candidato != null)
            {
                NombresTextBox.Text = candidato.Nombre;
                ApellidosTextBox.Text = candidato.Apellido;
                CorreoTextBox.Text = candidato.Correo;
                DomicilioTextBox.Text = candidato.Direccion;
                RFCTextBox.Text = candidato.RFC;
                TelefonoTextBox.Text = candidato.Telefono;
                NacionalidadTextBox.Text = candidato.Nacionalidad;

                if (!candidato.FechaPrivacidadDatos.HasValue)
                {
                    NombresTextBox.ReadOnly = true;
                    NombresTextBox.Attributes.Add("disabled", "disabled");

                    ApellidosTextBox.ReadOnly = true;
                    ApellidosTextBox.Attributes.Add("disabled", "disabled");

                    DomicilioTextBox.ReadOnly = true;
                    DomicilioTextBox.Attributes.Add("disabled", "disabled");

                    RFCTextBox.ReadOnly = true;
                    RFCTextBox.Attributes.Add("disabled", "disabled");

                    TelefonoTextBox.ReadOnly = true;
                    TelefonoTextBox.Attributes.Add("disabled", "disabled");

                    NacionalidadTextBox.ReadOnly = true;
                    NacionalidadTextBox.Attributes.Add("disabled", "disabled");

                    // to-do show message
                    guardarCambiosBtn.Visible = false;
                    dataOptionsCandidato.Visible = false;
                    alertInfo.Visible = true;
                    FileUploadImage.Enabled = false;
                    litAlertMessage.Text = "El usuario <strong>actual</strong> no ha aceptado las politicas de privacidad de datos.";
                }

                if (!candidato.Confirmado.HasValue)
                {
                    litAlertMessage.Text = "El usuario <strong>actual</strong> no ha confirmado su cuenta.";
                }

                if (candidato.NombreImagen != null && candidato.NombreImagen.Length > 0)
                {
                    if (File.Exists(Server.MapPath("~/ProfilePictures/" + candidato.NombreImagen)))
                    {
                        avatarImage.Attributes.Add("style", "background-image: url(/ProfilePictures/" + candidato.NombreImagen + ")");
                    }
                }
            }
            else
            {
                Response.Redirect("InicioAdmin.aspx", false);
            }
        }

        private void LoadJudgeInformation(string id)
        {
            controlFormCandidato.Style.Add("display", "none");
            var juez = InformacionPersonalJuezService.GetJuezById(id);

            if (juez != null)
            {
                jNombresTextBox.Text = juez.Nombre;
                jApellidosTextBox.Text = juez.Apellido;
                jCorreoTextBox.Text = juez.Correo;

                if (juez.NombreImagen != null && juez.NombreImagen.Length > 0)
                {
                    if (File.Exists(Server.MapPath("~/ProfilePictures/" + juez.NombreImagen)))
                    {
                        avatarImage.Attributes.Add("style", "background-image: url(/ProfilePictures/" + juez.NombreImagen + ")");
                    }
                }
            }
            else
            {
                Response.Redirect("InicioAdmin.aspx", false);
            }
        }

        protected void EnviarBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Upload(sender, e))
                {
                    ActualizarDatosGenerales();
                    Response.Redirect("AdministraInformacionPersonal.aspx?s=success" + "&t=" + Request.QueryString["t"] + "&id=" + Request.QueryString["id"], false);
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
            string sUserType = Request.QueryString["t"];
            string sUserId = Request.QueryString["id"];

            if (sUserType.Equals("juez"))
            {
                var juez = InformacionPersonalJuezService.GetJuezById(sUserId);

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
                                LoadJudgeInformation(sUserId);
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
            else if (sUserType.Equals("candidato"))
            {
                var candidato = InformacionPersonalCandidatoService.GetCandidatoById(sUserId);

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
                                LoadCandidateInformation(sUserId);
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
            ResetFields();
        }

        private void ResetFields()
        {
            newPwdTextBox.Text = "";
            confirmNewPwdTextBox.Text = "";
            CorreoTextBox.Text = Session[StringValues.CorreoSesion].ToString();
        }

        protected void ActualizarDatosGenerales()
        {
            string sUserType = Request.QueryString["t"];
            string sUserId = Request.QueryString["id"];

            if (sUserType.Equals("juez"))
            {
                var juez = InformacionPersonalJuezService.GetJuezById(sUserId);
                if (juez != null)
                {
                    juez.Nombre = jNombresTextBox.Text;
                    juez.Apellido = jApellidosTextBox.Text;
                    InformacionPersonalJuezService.UpdateJuez(juez);
                }
            }
            else if (sUserType.Equals("candidato"))
            {
                var candidato = InformacionPersonalCandidatoService.GetCandidatoById(sUserId);
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

                    InformacionPersonalCandidatoService.UpdateCandidato(candidato);
                }
            }
        }

        protected bool Upload(object sender, EventArgs e)
        {
            if (FileUploadImage.HasFile)
            {
                // Get filename
                string fileName = Path.GetFileName(FileUploadImage.PostedFile.FileName);

                // Get logged in candidate
                string sUserType = Request.QueryString["t"];
                string sUserId = Request.QueryString["id"];

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
                            // Get logged in user
                            if (sUserType.Equals("juez"))
                            {
                                var juez = InformacionPersonalJuezService.GetJuezById(sUserId);
                                if (juez.NombreImagen != null && juez.NombreImagen.Length > 0)
                                {
                                    // Delete previous image...
                                    File.Delete(Server.MapPath("~/ProfilePictures/") + juez.NombreImagen);
                                }
                            }
                            else if (sUserType.Equals("candidato"))
                            {
                                var candidato = InformacionPersonalCandidatoService.GetCandidatoById(sUserId);
                                if (candidato.NombreImagen != null && candidato.NombreImagen.Length > 0)
                                {
                                    // Delete previous image...
                                    File.Delete(Server.MapPath("~/ProfilePictures/") + candidato.NombreImagen);
                                }
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
                            if (sUserType.Equals("juez"))
                            {
                                InformacionPersonalJuezService.CambiaImagen(sUserId, null, sNombreImagen);
                            }
                            else if (sUserType.Equals("candidato"))
                            {
                                InformacionPersonalCandidatoService.CambiaImagen(sUserId, null, sNombreImagen);
                            }
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

        protected void CloseBtn_Click(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(Page, GetType(), "ReloadParent", "", true);
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "ClosePage", "window.close();", true);
            ClientScript.RegisterStartupScript(typeof(Page), "closePage", "<script type='text/JavaScript'>window.close();</script>");
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