using AjaxControlToolkit;
using PremiosInstitucionales.DBServices.Convocatoria;
using PremiosInstitucionales.DBServices.Evaluacion;
using PremiosInstitucionales.Values;
using System;
using System.Web.UI.WebControls;

namespace PremiosInstitucionales.WebForms
{
    public partial class InicioJuez : System.Web.UI.Page
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
                        Response.Redirect("Login.aspx");
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
                CargarAplicaciones();
            }
        }

        private void CargarAplicaciones()
        {
            int aux = 1;
            // obtener las categorias asociadas al juez actual
            var listaCategorias = EvaluacionService.GetCategoriaByJuez(Session[StringValues.CorreoSesion].ToString());
            // crear un TabPanel por cada categoria
            foreach(var categoria in listaCategorias)
            {
                TabPanel tabPanel = new TabPanel();
                tabPanel.HeaderText = categoria.Nombre + "/" + EvaluacionService.GetNombrePremioByCategoria(categoria.cveCategoria);
                // obtener aplicaciones para cierta categoria
                var aplicacionesACategoria = ConvocatoriaService.ObtenerAplicacionesPorCategoria(categoria.cveCategoria);

                // obtener candidatos ligados a estas aplicaciones
                var listaCandidatos = ConvocatoriaService.ObtenerCandidatosPorAplicaciones(aplicacionesACategoria);
                // crear acordeon nuevo
                Accordion acordeonNuevo = new Accordion();
                acordeonNuevo.ID = "acordeon" + aux;
                acordeonNuevo.SelectedIndex = -1;
                acordeonNuevo.HeaderCssClass = "accordionHeader";
                acordeonNuevo.HeaderSelectedCssClass = "accordionHeaderSelected";
                acordeonNuevo.ContentCssClass = "accordionContent";
                acordeonNuevo.AutoSize = AutoSize.None;
                acordeonNuevo.FadeTransitions = true;
                acordeonNuevo.TransitionDuration = 250;
                acordeonNuevo.FramesPerSecond = 40;
                acordeonNuevo.RequireOpenedPane = false;
                acordeonNuevo.SuppressHeaderPostbacks = true;
                // poblar un acordeon nuevo con panes de aplicaciones
                foreach (var aplicacionCandidato in listaCandidatos)
                {
                    AccordionPane panelIndividual = new AccordionPane();
                    #region controls acordeon pane
                    // Salto de linea para agregar donde se ocupe
                    Label salto = new Label();
                    salto.Text = "<br />";

                    Label nombreCandidato = new Label();
                    nombreCandidato.Text = aplicacionCandidato.Value.Nombre + " " + aplicacionCandidato.Value.Apellido;
                    nombreCandidato.Font.Bold = true;

                    // Encabezado incluye nombre y apellido del candidato, seguido de opciones
                    panelIndividual.HeaderContainer.Controls.Add(nombreCandidato);

                    // Agregar datos del candidato
                    Label usuario = new Label();
                    Label correo = new Label();

                    usuario.Text = "<b>Usuario: </b>" + aplicacionCandidato.Value.UserName + "<br />";
                    correo.Text = "<b>Correo: </b>" + aplicacionCandidato.Value.Correo + "<br /><br />";

                    panelIndividual.ContentContainer.Controls.Add(salto);
                    panelIndividual.ContentContainer.Controls.Add(usuario);
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
                    #endregion
                    acordeonNuevo.Panes.Add(panelIndividual);
                }
                tabPanel.Controls.Add(acordeonNuevo);
                TabContainer1.Tabs.Add(tabPanel);
                aux++;
            }
        }
    }
}