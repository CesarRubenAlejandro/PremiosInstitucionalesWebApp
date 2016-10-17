using PremiosInstitucionales.DBServices.Convocatoria;
using PremiosInstitucionales.Entities.Models;
using System;

namespace PremiosInstitucionales.WebForms
{
    public partial class EditarConvocatoria : System.Web.UI.Page
    {
        private PI_BA_Premio premioActual;
        private PI_BA_Convocatoria convoActual;

        protected void Page_Load(object sender, EventArgs e)
        {
            // obtener el premio usando el query string de su id
            String idPremio = Request.QueryString["premio"];
            premioActual = ConvocatoriaService.GetPremioById(idPremio);
            // obtener la convocatoria mas reciente en base al premio y desplegar sus datos
            convoActual = ConvocatoriaService.GetMostRecentConvocatoria(idPremio);
            if (premioActual != null)
            {
                // declarar fuente de la imagen del premio seleccionado
                ImageHeader.ImageUrl = "/img/" + premioActual.NombreImagen;
            }
            if (convoActual != null)
            {
                TituloConvocatoriaActualLbl.Text = convoActual.TituloConvocatoria;
                TextoConvocatoriaActualLbl.Text = convoActual.Descripcion;
                EditarConvocatoriaActualBttn.Visible = true;
            }   
        }

        protected void GuardarNuevaBttn_Click(object sender, EventArgs e)
        {
            // crear un nuevo objeto convocatoria y guardar sus datos
            var nuevaConvo = new PI_BA_Convocatoria();
            nuevaConvo.cveConvocatoria = Guid.NewGuid().ToString();
            nuevaConvo.Descripcion = TextoNuevaConvocatoriaTB.Text.ToString();
            nuevaConvo.TituloConvocatoria = TituloNuevaConvocatoriaTB.Text.ToString();
            nuevaConvo.FechaInicio = FechaInicioNuevaConvo.SelectedDate.Date;
            nuevaConvo.FechaFin = FechaFinNuevaConvo.SelectedDate.Date;
            // guardar nueva convocatoria
            ConvocatoriaService.SaveNewConvocatoria(premioActual.cvePremio, nuevaConvo);
            // limpiar campos de nueva convocatoria
            TextoNuevaConvocatoriaTB.Text = "";
            TituloNuevaConvocatoriaTB.Text = "";
            // cambiar el tab seleccionado
            TabContainer1.ActiveTabIndex = 0;
            // forzar el refresh de la pagina para traer los cambios
            Response.Redirect("EditarConvocatoria.aspx?premio=" + premioActual.cvePremio);
        }

        protected void EditarConvocatoriaActualBttn_Click(object sender, EventArgs e)
        {
            // habilitar y deshabilitar controles
            TituloConvocatoriaActualTB.Visible = true;
            TextoConvocatoriaActualTB.Visible = true;
            GuardarCambiosBttn.Visible = true;
            CancelarCambiosBttn.Visible = true;

            TituloConvocatoriaActualLbl.Visible = false;
            TextoConvocatoriaActualLbl.Visible = false;
            EditarConvocatoriaActualBttn.Visible = false;

            // desplegar la informacion correspondiente
            TituloConvocatoriaActualTB.Text = TituloConvocatoriaActualLbl.Text.ToString();
            TextoConvocatoriaActualTB.Text = TextoConvocatoriaActualLbl.Text.ToString();
        }

        protected void CancelarCambiosBttn_Click(object sender, EventArgs e)
        {
            CancelarCambios();
        }

        private void CancelarCambios()
        {
            // habilitar y deshabilitar controles
            TituloConvocatoriaActualTB.Visible = false;
            TextoConvocatoriaActualTB.Visible = false;
            GuardarCambiosBttn.Visible = false;
            CancelarCambiosBttn.Visible = false;

            TituloConvocatoriaActualLbl.Visible = true;
            TextoConvocatoriaActualLbl.Visible = true;
            EditarConvocatoriaActualBttn.Visible = true;
        }

        protected void GuardarCambiosBttn_Click(object sender, EventArgs e)
        {
            // obtener datos y guardar
            String titulo = TituloConvocatoriaActualTB.Text.ToString();
            String descripcion = TextoConvocatoriaActualTB.Text.ToString();
            ConvocatoriaService.ActualizarConvocatoria(convoActual.cveConvocatoria, descripcion, titulo);
            // desplegar vista read only
            CancelarCambios();
            // forzar refresh para actualizar informacion
            Response.Redirect("EditarConvocatoria.aspx?premio=" + premioActual.cvePremio);
        }
    }
}