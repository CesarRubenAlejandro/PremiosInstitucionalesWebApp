using AjaxControlToolkit;
using PremiosInstitucionales.DBServices.Convocatoria;
using PremiosInstitucionales.DBServices.Evaluacion;
using PremiosInstitucionales.Values;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PremiosInstitucionales.WebForms
{
    public partial class InicioJuez : System.Web.UI.Page
    {
        private String Categorias;
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
            Categorias = CategoriasHidden.Value;
        }

        private void CargarAplicaciones()
        {
            // borrar las categorias guardadas en hidden
            CategoriasHidden.Value = "";
            int aux = 0;
            // obtener las categorias asociadas al juez actual
            var listaCategorias = EvaluacionService.GetCategoriaByJuez(Session[StringValues.CorreoSesion].ToString());
            // desactivar boton de exportar si no hay categorias asignadas al juez
            if (listaCategorias.Count == 0)
            {
                ExportarBttn.Visible = false;
            }
            // crear un TabPanel por cada categoria
            foreach (var categoria in listaCategorias)
            {
                TabPanel tabPanel = new TabPanel();
                tabPanel.HeaderText = categoria.Nombre + "/" + EvaluacionService.GetNombrePremioByCategoria(categoria.cveCategoria);
                // obtener aplicaciones para cierta categoria
                var aplicacionesACategoria = ConvocatoriaService.ObtenerAplicacionesPorCategoria(categoria.cveCategoria);

                // obtener candidatos ligados a estas aplicaciones
                var listaCandidatos = ConvocatoriaService.JuezObtenerCandidatosPorAplicaciones(aplicacionesACategoria);
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
                    //Label usuario = new Label();
                    Label correo = new Label();

                    //usuario.Text = "<b>Usuario: </b>" + aplicacionCandidato.Value.UserName + "<br />";
                    correo.Text = "<b>Correo: </b>" + aplicacionCandidato.Value.Correo + "<br /><br />";

                    panelIndividual.ContentContainer.Controls.Add(salto);
                    //panelIndividual.ContentContainer.Controls.Add(usuario);
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
                CategoriasHidden.Value += categoria.cveCategoria + "&";

                TabContainer1.Tabs.Add(tabPanel);

                aux++;
            }
        }

        protected void ExportarBttn_Click(object sender, EventArgs e)
        {
            if (Categorias != null)
            {
                // obtener la categoria usando la tab abierta actualmente
                string[] categorias = Categorias.Split('&');
                var cveCategoria = categorias[TabContainer1.ActiveTabIndex];
                // obtener preguntas y respuestas, y crear un DataTable
                DataTable dt = new DataTable();
                var aplicaciones = ConvocatoriaService.JuezObtenerAplicacionesPorCategoria(cveCategoria);
                for (int i = 0; i < aplicaciones.Count; i++)
                {
                    var diccionarioRespuestas = ConvocatoriaService.ObtenerPreguntasConRespuestasPorAplicacion(aplicaciones[i]);
                    if (i == 0)
                    {
                        // crear columnas de dt
                        // crear columna de nombre de candidato
                        DataColumn columnNombre = new DataColumn();
                        columnNombre.ColumnName = StringValues.ColumnaReporteJuecesNombreCandidato;
                        columnNombre.DataType = typeof(string);
                        dt.Columns.Add(columnNombre);
                        // crear columna de correo de candidato
                        DataColumn columnCorreo = new DataColumn();
                        columnCorreo.ColumnName = StringValues.ColumnaReporteJuecesCorreo;
                        columnCorreo.DataType = typeof(string);
                        dt.Columns.Add(columnCorreo);

                        foreach (var pregunta in diccionarioRespuestas)
                        {
                            DataColumn column = new DataColumn();
                            column.ColumnName = pregunta.Value[0];
                            column.DataType = typeof(string);
                            dt.Columns.Add(column);
                        }
                    }
                    DataRow row = dt.NewRow();
                    // agregar renglones al DataTable
                    foreach (var preguntaRespuesta in diccionarioRespuestas)
                    {
                        row[preguntaRespuesta.Value[0]] = preguntaRespuesta.Value[1];
                    }
                    // agregar nombre de candidato a renglon
                    row[StringValues.ColumnaReporteJuecesNombreCandidato] = ConvocatoriaService.GetNombreCandidatoByAplicacion(aplicaciones[i].cveAplicacion);
                    // agregar correo de candidato a renglon
                    row[StringValues.ColumnaReporteJuecesCorreo] = ConvocatoriaService.GetCorreoCandidatoByAplicacion(aplicaciones[i].cveAplicacion);
                    dt.Rows.Add(row);

                }
                // generar reporte en excel
                GenerarExcel(dt);
            }

            // limpiar controles creados y volver a cargar
            TabContainer1.Controls.Clear();
            CargarAplicaciones();   
        }

        private void GenerarExcel(DataTable dt)
        {
            //Create a dummy GridView
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition",
             "attachment;filename=ReporteCandidatos.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                //Apply text style to each Row
                GridView1.Rows[i].Attributes.Add("class", "textmode");
            }
            GridView1.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
}