using PremiosInstitucionales.DBServices.InformacionPersonalCandidato;
using PremiosInstitucionales.Entities.Models;
using PremiosInstitucionales.Values;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;

namespace PremiosInstitucionales.WebForms
{
    public partial class InformacionPersonalCandidato : System.Web.UI.Page
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
            ActualizarDatosGenerales();
            Upload(sender, e);
        }

        protected void CambiarContrasena_Click(object sender, EventArgs e)
        {
            ActualizarContrasena();
        }

        public class ajax_Candidato
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string Direccion { get; set; }
            public string Nacionalidad { get; set; }
            public string RFC { get; set; }
            public string Telefono { get; set; }
        }

        [WebMethod]
        [ScriptMethod]
        public static void MyMethod(ajax_Candidato cand)
        {
            try
            {
                PI_BA_Candidato aux = new PI_BA_Candidato();
                aux.Nombre = cand.Nombre;
                aux.Apellido = cand.Apellido;
                aux.Direccion = cand.Direccion;
                aux.Nacionalidad = cand.Nacionalidad;
                aux.RFC = cand.RFC;
                aux.Telefono = cand.Telefono;

                if (InformacionPersonalCandidatoService.GuardarCambios(aux, HttpContext.Current.Session[StringValues.CorreoSesion].ToString()))
                {
                    System.Diagnostics.Debug.WriteLine("Tus cambios han sido guardados");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Hubo un error al guardar tus cambios");
                }
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("??? why tho");
                throw;
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
                    PI_BA_Candidato aux = new PI_BA_Candidato();
                    aux.Password = newPwdTextBox.Text;
                    if (InformacionPersonalCandidatoService.GuardaNuevaContrasena(aux, Session[StringValues.CorreoSesion].ToString()))
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

            PI_BA_Candidato aux = new PI_BA_Candidato();
            aux.Nombre = NombresTextBox.Text;
            aux.Apellido = ApellidosTextBox.Text;
            aux.Direccion = DomicilioTextBox.Text.ToString();
            aux.Nacionalidad = NacionalidadTextBox.Text.ToString();
            aux.RFC = RFCTextBox.Text.ToString();
            aux.Telefono = TelefonoTextBox.Text.ToString();

            if (InformacionPersonalCandidatoService.GuardarCambios(aux, Session[StringValues.CorreoSesion].ToString()))
            {
            }
            else
            {
                MostrarCampos();
            }
        }

        protected void EditarBtn_Click(object sender, EventArgs e)
        {

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
                PI_BA_Candidato aux = new PI_BA_Candidato();
                aux.NombreImagen = sNombreImagen;

                if (InformacionPersonalCandidatoService.CambiaImagen(aux, Session[StringValues.CorreoSesion].ToString()))
                {

                }
                else
                {

                }

                Response.Redirect(Request.Url.AbsoluteUri);
            }
        }
    }
}