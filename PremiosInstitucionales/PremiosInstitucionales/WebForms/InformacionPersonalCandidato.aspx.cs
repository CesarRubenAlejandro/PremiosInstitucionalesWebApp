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

namespace PremiosInstitucionales.WebForms
{
    public partial class InformacionPersonalCandidato : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MostrarCampos();
            }
        }

        private void MostrarCampos()
        {
            var candidato = InformacionPersonalCandidatoService.GetCandidatoByCorreo(Session[StringValues.CorreoSesion].ToString());
            NombresTextBox.Text = candidato.Nombre;
            ApellidosTextBox.Text = candidato.Apellido;
            CorreoTextBox.Text = Session[StringValues.CorreoSesion].ToString();
            DomicilioTextBox.Text = candidato.Direccion;
            RFCTextBox.Text = candidato.RFC;
            TelefonoTextBox.Text = candidato.Telefono;
            NacionalidadTextBox.Text = candidato.Nacionalidad;
        }

        protected void EnviarBtn_Click(object sender, EventArgs e)
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
                Mensaje.Text = "Tus cambios han sido guardados";
            }
            else
            {
                Mensaje.Text = "Hubo un error al guardar tus cambios";
                MostrarCampos();
            }
            Enable();
        }

        private void Enable()
        {/*
            if (EditarBtn.Text.ToString() == StringValues.InfoPersonalEditar)
            {
                EditarBtn.Text = StringValues.InfoPersonalCancelar;
            }
            else
            {
                EditarBtn.Text = StringValues.InfoPersonalEditar;
                MostrarCampos();
            }
            EnviarBtn.Enabled = !EnviarBtn.Enabled;
            NombresTextBox.Enabled = !NombresTextBox.Enabled;
            ApellidosTextBox.Enabled = !ApellidosTextBox.Enabled;
            DomicilioTextBox.Enabled = !DomicilioTextBox.Enabled;
            TelefonoTextBox.Enabled = !TelefonoTextBox.Enabled;
            RFCTextBox.Enabled = !RFCTextBox.Enabled;
            NacionalidadTextBox.Enabled = !NacionalidadTextBox.Enabled;*/
        }

        protected void EditarBtn_Click(object sender, EventArgs e)
        {
            Enable();
        }

        protected void Upload(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                // Get filename
                string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);

                // Get logged in candidate
                var candidato = InformacionPersonalCandidatoService.GetCandidatoByCorreo(Session[StringValues.CorreoSesion].ToString());
                if(candidato.NombreImagen.Length > 0)
                {
                    // Delete previous image...
                    File.Delete(Server.MapPath("~/ProfilePictures/") + candidato.NombreImagen);
                }

                // Get string image format (png, jpg, etc)
                var startIndex = fileName.LastIndexOf(".");
                var endIndex = fileName.Length - startIndex;
                string sFormat = fileName.Substring(startIndex, endIndex);
                string sNombreImagen = candidato.cveCandidato + sFormat;

                // Upload image to server
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/ProfilePictures/") + sNombreImagen);

                // Update data in database
                PI_BA_Candidato aux = new PI_BA_Candidato();
                aux.NombreImagen = sNombreImagen;
                
                if (InformacionPersonalCandidatoService.CambiaImagen(aux, Session[StringValues.CorreoSesion].ToString()))
                {
                    Mensaje.Text = "Tus cambios han sido guardados";
                }
                else
                {
                    Mensaje.Text = "Hubo un error al guardar tus cambios";
                }

                // Response.Redirect(Request.Url.AbsoluteUri);
            }
        }
    }
}