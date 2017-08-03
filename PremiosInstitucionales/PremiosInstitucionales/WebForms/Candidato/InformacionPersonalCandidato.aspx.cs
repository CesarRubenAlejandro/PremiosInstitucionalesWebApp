using PremiosInstitucionales.DBServices.InformacionPersonalCandidato;
using PremiosInstitucionales.Values;
using System;
using System.IO;
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
                ActualizarDatosGenerales();
                Upload(sender, e);
                CheckPrivacy();
                MasterPage.ShowMessage("Aviso", "Cambios realizados con éxito.");
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
            if (candidato.Password == sCurrentPassword)
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
                            candidato.Password = newPwdTextBox.Text;
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
            if(candidato != null)
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

        protected void Upload(object sender, EventArgs e)
        {
            if (FileUploadImage.HasFile)
            {
                // Get filename
                string fileName = Path.GetFileName(FileUploadImage.PostedFile.FileName);

                // Get logged in candidate
                var candidato = InformacionPersonalCandidatoService.GetCandidatoByCorreo(Session[StringValues.CorreoSesion].ToString());
                if (candidato.NombreImagen != null && candidato.NombreImagen.Length > 0)
                {
                    // Delete previous image...
                    File.Delete(Server.MapPath("~/ProfilePictures/") + candidato.NombreImagen);
                }


                // Get string image format (png, jpg, etc)
                var startIndex = fileName.LastIndexOf(".");
                var endIndex = fileName.Length - startIndex;
                string sFormat = fileName.Substring(startIndex, endIndex);
                string sNombreImagen = Guid.NewGuid().ToString() + sFormat;

                // Upload image to server
                FileUploadImage.PostedFile.SaveAs(Server.MapPath("~/ProfilePictures/") + sNombreImagen);

                // Update data in database
                InformacionPersonalCandidatoService.CambiaImagen(null, Session[StringValues.CorreoSesion].ToString(), sNombreImagen);

                Response.Redirect(Request.Url.AbsoluteUri, false);
            }
        }

        protected void BackBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("InicioCandidato.aspx", false);
        }

    }
}