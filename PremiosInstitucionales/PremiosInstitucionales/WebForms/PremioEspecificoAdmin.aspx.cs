using AjaxControlToolkit;
using PremiosInstitucionales.DBServices.Convocatoria;
using PremiosInstitucionales.Entities.Models;
using PremiosInstitucionales.Values;
using System;
using System.Web.UI.WebControls;

namespace PremiosInstitucionales.WebForms
{
    public partial class EditarConvocatoria : System.Web.UI.Page
    {
        private PI_BA_Premio premioActual;
        private PI_BA_Convocatoria convoActual;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // revisar la primera vez que se carga la pagina que se haya iniciado sesion con cuenta de admin
                if (Session[StringValues.RolSesion] != null)
                {
                    if (Session[StringValues.RolSesion].ToString() != StringValues.RolAdmin)
                        // si no es admin, redireccionar a inicio general
                        Response.Redirect("InicioCandidato.aspx");
                }
                else
                {
                    Response.Redirect("InicioCandidato.aspx");
                }

            }

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

            //obtener categorias para el premio
            var categorias = ConvocatoriaService.GetCategoriasByPremio(idPremio);
            if (categorias != null)
            {
                // asignar el datasource al DropDown de categorias
                CategoriasDDL.DataSource = categorias;
                CategoriasDDL.DataTextField = "Nombre";
                CategoriasDDL.DataValueField = "cveCategoria";
                CategoriasDDL.DataBind();

                // desplegar los candidatos para la categoria correspondiente
                CrearListaDeAplicantes();
            }
            else
            {
                // desplegar mensaje de error
                ErrorLbl.Text = "No hay convocatorias abiertas por el momento";
                ErrorLbl.Visible = true;
            }
        }

        protected void CategoriasDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            CrearListaDeAplicantes();
        }

        private void CrearListaDeAplicantes()
        {

            // obtener aplicaciones para cierta categoria
            var aplicacionesACategoria = ConvocatoriaService.ObtenerAplicacionesPorCategoria(CategoriasDDL.SelectedValue.ToString());

            // obtener candidatos ligados a estas aplicaciones
            var listaCandidatos = ConvocatoriaService.ObtenerCandidatosPorAplicaciones(aplicacionesACategoria);

            Accordion ContenedorDeCandidatos = new Accordion();

            foreach (var candidato in listaCandidatos)
            {
                AccordionPane panelIndividual = new AccordionPane();
                Label nombreCandidato = new Label();
                nombreCandidato.Text = candidato.Nombre + " " + candidato.Apellido;

                // Encabezado incluye nombre y apellido del candidato, seguido de opciones
                panelIndividual.HeaderContainer.Controls.Add(nombreCandidato);

                MyAccordion.Panes.Add(panelIndividual);
             
            }



            /*
            // obtener lista de preguntas para la categoria y desplegar el formulario
            var preguntas = ConvocatoriaService.GetFormularioByCategoria(CategoriasDDL.SelectedValue.ToString());
            foreach (var pregunta in preguntas)
            {
                // crear lbl con el texto de la pregunta
                Label lbl = new Label();
                lbl.Text = pregunta.Texto;
                PanelFormulario.Controls.Add(lbl);

                PanelFormulario.Controls.Add(new LiteralControl("<br />"));

                TextBox tb = new TextBox();
                tb.ID = pregunta.IdentificadorObjeto;
                PanelFormulario.Controls.Add(tb);

                PanelFormulario.Controls.Add(new LiteralControl("<br />"));
                PanelFormulario.Controls.Add(new LiteralControl("<br />"));
            }*/
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
            Response.Redirect("PremioEspecificoAdmin.aspx?premio=" + premioActual.cvePremio);
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
            Response.Redirect("PremioEspecificoAdmin.aspx?premio=" + premioActual.cvePremio);
        }

        protected void ObtenerCandidatos()
        {
            //var listaUsuarios = ConvocatoriaService.ObtenerCandidatosPorAplicaciones("aplicacion");
        }
    }
}