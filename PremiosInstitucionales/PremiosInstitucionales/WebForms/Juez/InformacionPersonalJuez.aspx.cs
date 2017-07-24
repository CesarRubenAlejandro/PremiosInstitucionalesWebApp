using PremiosInstitucionales.DBServices.InformacionPersonalJuez;
using PremiosInstitucionales.Entities.Models;
using PremiosInstitucionales.Values;
using System;
using System.IO;

namespace PremiosInstitucionales.WebForms
{
    public partial class InformacionPersonalJuez : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // revisar la primera vez que se carga la pagina que se haya iniciado sesion con cuenta de juez
                if (Session[StringValues.RolSesion] != null)
                {
                    if (Session[StringValues.RolSesion].ToString() != StringValues.RolJuez)
                        // si no es juez, redireccionar a inicio general
                        Response.Redirect("~/WebForms/Login.aspx");
                }
                else
                {
                    Response.Redirect("~/WebForms/Login.aspx");
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
            ActualizarDatosGenerales();
            Upload(sender, e);
        }

        protected void CambiarContrasena_Click(object sender, EventArgs e)
        {
            ActualizarContrasena();
        }

        protected void ActualizarContrasena()
        {
            var juez = InformacionPersonalJuezService.GetJuezByCorreo(Session[StringValues.CorreoSesion].ToString());

            string sCurrentPassword = currentPwdTextBox.Text;
            if (juez.Password == sCurrentPassword)
            {
                if (newPwdTextBox.Text == confirmNewPwdTextBox.Text)
                {
                    if (InformacionPersonalJuezService.GuardaNuevaContrasena(Session[StringValues.CorreoSesion].ToString(), newPwdTextBox.Text))
                    {
                        // good
                    }
                    else
                    {
                        // bad
                    }
                }
            }
        }

        protected void ActualizarDatosGenerales()
        {
            var juez = InformacionPersonalJuezService.GetJuezByCorreo(Session[StringValues.CorreoSesion].ToString());
            if (juez != null)
            {
                juez.Nombre = NombresTextBox.Text;
                juez.Apellido = ApellidosTextBox.Text;

                if (InformacionPersonalJuezService.UpdateJuez(juez))
                {

                }
                else
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

                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }
    }
}