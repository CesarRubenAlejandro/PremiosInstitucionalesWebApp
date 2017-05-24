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
                    PI_BA_Juez aux = new PI_BA_Juez();
                    aux.Password = newPwdTextBox.Text;
                    if (InformacionPersonalJuezService.GuardaNuevaContrasena(aux, Session[StringValues.CorreoSesion].ToString()))
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

            PI_BA_Juez aux = new PI_BA_Juez();
            aux.Nombre = NombresTextBox.Text;
            aux.Apellido = ApellidosTextBox.Text;

            if (InformacionPersonalJuezService.GuardarCambios(aux, Session[StringValues.CorreoSesion].ToString()))
            {

            }
            else
            {
                MostrarCampos();
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
                PI_BA_Juez aux = new PI_BA_Juez();
                aux.NombreImagen = sNombreImagen;

                InformacionPersonalJuezService.CambiaImagen(aux, Session[StringValues.CorreoSesion].ToString());

                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }
    }
}