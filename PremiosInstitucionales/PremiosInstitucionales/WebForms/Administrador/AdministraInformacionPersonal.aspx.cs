using PremiosInstitucionales.DBServices.InformacionPersonalCandidato;
using PremiosInstitucionales.DBServices.InformacionPersonalJuez;
using PremiosInstitucionales.Values;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace PremiosInstitucionales.WebForms
{
    public partial class AdministraInformacionPersonal : System.Web.UI.Page
    {
        MP_Global MasterPage = new MP_Global();
        protected void Page_Load(object sender, EventArgs e)
        {
            MasterPage = (MP_Global)Page.Master;
            if (!IsPostBack)
            {
                // revisar la primera vez que se carga la pagina que se haya iniciado sesion con cuenta de admin
                if (Session[StringValues.RolSesion] != null)
                {
                    if (Session[StringValues.RolSesion].ToString() != StringValues.RolAdmin)
                        // si no es admin, redireccionar a inicio general
                        Response.Redirect("~/WebForms/Login.aspx", false);
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
                        return;
                    }
                    else if (sUserType.Equals("candidato"))
                    {
                        LoadCandidateInformation(sUserId);
                        return;
                    }
                }
                Response.Redirect("InicioAdmin.aspx", false);
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
                ActualizarDatosGenerales();
                Upload(sender, e);
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
                            juez.Password = newPwdTextBox.Text;
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
                            candidato.Password = newPwdTextBox.Text;
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

        protected void Upload(object sender, EventArgs e)
        {
            if (FileUploadImage.HasFile)
            {
                // Get filename
                string fileName = Path.GetFileName(FileUploadImage.PostedFile.FileName);

                // Get logged in candidate
                string sUserType = Request.QueryString["t"];
                string sUserId = Request.QueryString["id"];

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

                // Get string image format (png, jpg, etc)
                var startIndex = fileName.LastIndexOf(".");
                var endIndex = fileName.Length - startIndex;
                string sFormat = fileName.Substring(startIndex, endIndex);
                string sNombreImagen = Guid.NewGuid().ToString() + sFormat;

                // Upload image to server
                FileUploadImage.PostedFile.SaveAs(Server.MapPath("~/ProfilePictures/") + sNombreImagen);

                // Update data in database
                if (sUserType.Equals("juez"))
                {
                    InformacionPersonalJuezService.CambiaImagen(sUserId, null, sNombreImagen);
                }
                else if (sUserType.Equals("candidato"))
                {
                    InformacionPersonalCandidatoService.CambiaImagen(sUserId, null, sNombreImagen);
                }

                Response.Redirect(Request.Url.AbsoluteUri, false);
            }
        }

        protected void CloseBtn_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "ReloadParent", "if (window.opener && !window.opener.closed) { window.opener.location.reload(true); }", true);
            ClientScript.RegisterStartupScript(GetType(), "ClosePage", "window.close();", true);
        }
    }
}