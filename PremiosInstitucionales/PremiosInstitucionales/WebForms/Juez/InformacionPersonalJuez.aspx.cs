using PremiosInstitucionales.DBServices.InformacionPersonalJuez;
using PremiosInstitucionales.Values;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace PremiosInstitucionales.WebForms
{
    public partial class InformacionPersonalJuez : System.Web.UI.Page
    {
        MP_Global MasterPage = new MP_Global();
        protected void Page_Load(object sender, EventArgs e)
        {
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
                }
                else
                {
                    Response.Redirect("~/WebForms/Login.aspx", false);
                }

                ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:getProfileReferences(); ", true);
                MostrarCampos();
                ResetFields();
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
            if (juez.Password == sCurrentPassword)
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
                            juez.Password = newPwdTextBox.Text;
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

        protected void Upload(object sender, EventArgs e)
        {
            if (FileUploadImage.HasFile)
            {
                // Get filename
                string fileName = Path.GetFileName(FileUploadImage.PostedFile.FileName);

                // Get logged in judge
                var juez = InformacionPersonalJuezService.GetJuezByCorreo(Session[StringValues.CorreoSesion].ToString());
                if (juez.NombreImagen != null && juez.NombreImagen.Length > 0)
                {
                    // Delete previous image...
                    File.Delete(Server.MapPath("~/ProfilePictures/") + juez.NombreImagen);
                }

                // Get string image format (png, jpg, etc)
                var startIndex = fileName.LastIndexOf(".");
                var endIndex = fileName.Length - startIndex;
                string sFormat = fileName.Substring(startIndex, endIndex);
                string sNombreImagen = Guid.NewGuid().ToString() + sFormat;

                // Upload image to server
                FileUploadImage.PostedFile.SaveAs(Server.MapPath("~/ProfilePictures/") + sNombreImagen);

                // Update data in database
                InformacionPersonalJuezService.CambiaImagen(null, Session[StringValues.CorreoSesion].ToString(), sNombreImagen);

                Response.Redirect(Request.Url.AbsoluteUri, false);
            }
        }

        protected void BackBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("InicioJuez.aspx", false);
        }
    }
}