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
            // obtener el premio usando el query string de su id
            String idPremio = Request.QueryString["premio"];
            premioActual = ConvocatoriaService.GetPremioById(idPremio);
            // obtener la convocatoria mas reciente en base al premio y desplegar sus datos
            convoActual = ConvocatoriaService.GetMostRecentConvocatoria(idPremio);
            if (!IsPostBack)
            {
                // revisar la primera vez que se carga la pagina que se haya iniciado sesion con cuenta de admin
                if (Session[StringValues.RolSesion] != null)
                {
                    if (Session[StringValues.RolSesion].ToString() != StringValues.RolAdmin)
                        // si no es admin, redireccionar a inicio general
                        Response.Redirect("Login.aspx");
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }

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
                if (categorias != null && categorias.Count > 0)
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
                    ErrorLbl.Text = "No hay convocatorias abiertas por el momento o no hay categorías registradas";
                    ErrorLbl.Visible = true;
                }

            }
        }

        protected void CategoriasDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            CrearListaDeAplicantes();
        }

        private void CrearListaDeAplicantes()
        {

            MyAccordion.Panes.Clear();

            // obtener aplicaciones para cierta categoria
            var aplicacionesACategoria = ConvocatoriaService.ObtenerAplicacionesPorCategoria(CategoriasDDL.SelectedValue.ToString());

            // obtener candidatos ligados a estas aplicaciones
            var listaCandidatos = ConvocatoriaService.ObtenerCandidatosPorAplicaciones(aplicacionesACategoria);

            Accordion ContenedorDeCandidatos = new Accordion();

            foreach (var aplicacionCandidato in listaCandidatos)
            {
                AccordionPane panelIndividual = new AccordionPane();
                panelIndividual.CssClass = "accordionPane";
                // Salto de linea para agregar donde se ocupe
                Label salto = new Label();
                salto.Text = "<br />";

                Label nombreCandidato = new Label();
                nombreCandidato.Text = aplicacionCandidato.Value.Nombre + " " + aplicacionCandidato.Value.Apellido;
                nombreCandidato.Font.Bold = true;

                // Encabezado incluye nombre y apellido del candidato, seguido de opciones
                panelIndividual.HeaderContainer.Controls.Add(nombreCandidato);

                // Agregar datos del candidato
                Label status = new Label();
                Label correo = new Label();

                status.Text = "<b>Estado: </b>" + aplicacionCandidato.Key.Status + "<br />";
                correo.Text = "<b>Correo: </b>" + aplicacionCandidato.Value.Correo + "<br /><br />";

                panelIndividual.ContentContainer.Controls.Add(salto);
                panelIndividual.ContentContainer.Controls.Add(status);
                panelIndividual.ContentContainer.Controls.Add(correo);

                // Obtengo preguntas de la aplicacion con sus respectivas respuestas
                var preguntasYrespuestas = ConvocatoriaService.ObtenerPreguntasConRespuestasPorAplicacion(aplicacionCandidato.Key);

                foreach (var pregunta in preguntasYrespuestas)
                {
                    Label textoPregunta = new Label();
                    textoPregunta.Text = "<b>" + pregunta.Value[0] + "</b><br />" + pregunta.Value[1] + "<br />";

                    panelIndividual.ContentContainer.Controls.Add(textoPregunta);
                }
                panelIndividual.ContentContainer.Controls.Add(salto);
                // AGREGAR BOTON RECHAZAR
                Button btn1 = new Button();
                btn1.ID = Guid.NewGuid().ToString();
                btn1.Text = "rechazar";
                btn1.OnClientClick = "return ShowModalPopup(\"" + aplicacionCandidato.Key.cveAplicacion + "\")";
                panelIndividual.ContentContainer.Controls.Add(btn1);

                MyAccordion.Panes.Add(panelIndividual);
             
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

        protected void bttnEnviarRechazo_Click(object sender, EventArgs e)
        {
            String aplicacionID = IdAppHidden.Value.ToString();
            String razonRechazo = razonTB.Text.ToString();
            // cambiar el status de la aplicacion a Rechazado

            // enviar correo notificando al candidato de la aplicacion

            // cargar nuevamente el acordeon de respuestas forzando un postback
            razonTB.Text = "";
            Response.Redirect("PremioEspecificoAdmin.aspx?premio=" + premioActual.cvePremio);
        }

    }
}