using PremiosInstitucionales.DBServices.Aplicacion;
using PremiosInstitucionales.DBServices.Convocatoria;
using PremiosInstitucionales.DBServices.Evaluacion;
using PremiosInstitucionales.Entities.Models;
using PremiosInstitucionales.Values;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace PremiosInstitucionales.WebForms
{

    public struct Premio
    {
        public string Nombre;
        public string Descripcion;
        public List<PI_BA_Categoria> ListaCategorias;
        public Premio(PI_BA_Premio premio, List<PI_BA_Categoria> listaCategorias)
        {
            Nombre = premio.Nombre;
            Descripcion = premio.Descripcion;
            ListaCategorias = listaCategorias;
        }
    }
    public partial class PremiosInstitucionalesJuez : System.Web.UI.Page
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

        private String CrearPremioHtml(Premio premio)
        {
            String htmlContent;
            String nombreImagen = AplicacionService.GetPremioByClaveCategoria(premio.ListaCategorias[0].cveCategoria).NombreImagen.ToString();

            htmlContent = "<div class=\"blockquote-box\">" +
                               "<div class=\"col-sm-2 prem-img\">" +
                                   "<img src = /AwardPictures/" + nombreImagen + " class=\"img-square\" style=\"margin-top: 15px; margin-bottom: 15px; \"/>" +
                               "</div>" +
                               "<div class=\"tab-content col-sm-8 prem-info\">" +
                                   "<div id = \"" + premio.GetHashCode() + "\" class=\"tab-pane fade in active\">" +
                                       "<h4>Premio: <strong>" + premio.Nombre + "</strong></h4>" +
                                       "<p>" +
                                            premio.Descripcion +
                                       "</p>" +
                                   "</div>";

            foreach (var categoria in premio.ListaCategorias)
            {
                htmlContent += "<div id = \"" + categoria.cveCategoria + "\" class=\"tab-pane fade\">" +
                                   "<div class=\"row\" style=\"margin-top:15px;\">" +

                                        //<!-- Dashboard -->
                                        "<div class=\"col-sm-6\" style=\"text-align: center; margin-bottom:-5px; margin-left:-25px;\">" +

                                            //<!-- Todas -->
                                            "<div style=\"margin-right:7.5px; margin-left:7.5px; width:95px; display: inline-block;\">" +
                                                "<div class=\"panel panel-primary\">" +
                                                    "<div class=\"panel-heading\">" +
                                                        "<div class=\"row\" style=\"text-align: center;\">" +
                                                            "<div class=\"huge\">" + GetAplicacionesAceptadas(categoria.cveCategoria).ToString() + "</div>" +
                                                        "</div>" +
                                                    "</div>" +
                                                    "<a href=\"ListaParticipantes.aspx?c=" + categoria.cveCategoria + "\">" +
                                                        "<div class=\"panel-footer\" style=\"background-color: white;\">" +
                                                            "<span style = \"text-align: center;\"><strong> Totales </strong></span>" +
                                                            "<div class=\"clearfix\"></div>" +
                                                        "</div>" +
                                                    "</a>" +
                                                "</div>" +
                                            "</div>" +

                                            //<!-- Completas -->
                                            "<div style=\"margin-right:7.5px; margin-left:7.5px; width:95px; display: inline-block;\">" +
                                                "<div class=\"panel panel-green\">" +
                                                    "<div class=\"panel-heading\">" +
                                                        "<div class=\"row\" style=\"text-align: center;\">" +
                                                            "<div class=\"huge\">" + EstadoAplicaciones(categoria.cveCategoria)[0].ToString() + "</div>" +
                                                        "</div>" +
                                                    "</div>" +
                                                    "<a href=\"ListaParticipantes.aspx?c=" + categoria.cveCategoria + "\">" +
                                                        "<div class=\"panel-footer\" style=\"background-color: white;\">" +
                                                            "<span style = \"text-align: center;\"><strong> Completas </strong></span>" +
                                                            "<div class=\"clearfix\"></div>" +
                                                        "</div>" +
                                                    "</a>" +
                                                "</div>" +
                                            "</div>" +

                                            //<!-- Nuevas/Pendientes -->
                                            "<div style=\"margin-right:7.5px; margin-left:7.5px; width:95px; display: inline-block;\">" +
                                                "<div class=\"panel panel-yellow\">" +
                                                    "<div class=\"panel-heading\">" +
                                                        "<div class=\"row\" style=\"text-align: center;\">" +
                                                            "<div class=\"huge\">" + EstadoAplicaciones(categoria.cveCategoria)[1].ToString() + "</div>" +
                                                        "</div>" +
                                                    "</div>" +
                                                    "<a href=\"ListaParticipantes.aspx?c=" + categoria.cveCategoria + "\">" +
                                                        "<div class=\"panel-footer\" style=\"background-color: white;\">" +
                                                            "<span style = \"text-align: center;\"><strong> Nuevas </strong></span>" +
                                                            "<div class=\"clearfix\"></div>" +
                                                        "</div>" +
                                                    "</a>" +
                                                "</div>" +
                                            "</div>" +
                                         "</div>" +

                                        //<!-- Texto -->
                                        "<div class=\"col-sm-6\">" +
                                            "<p>" +
                                                "<h4>Categoria: <strong>" + categoria.Nombre + "</strong></h4>" +
                                                "Recuerda que tienes hasta el día <strong>" + ConvocatoriaService.GetConvocatoriaById(categoria.cveConvocatoria).FechaFin.ToString().Substring(0, 10) + "</strong> para evaluar todas tus aplicaciones." +
                                            "</p>" +

                                            // <!-- Botton -->
                                            "<span class=\"pull-right\">" +
                                                "<a class=\"btn btn-primary\"  href=\"ListaParticipantes.aspx?c=" + categoria.cveCategoria + "\" style=\"margin-top:5px; margin-bottom:15px;\">Ver Aplicaciones</a>" +
                                            "</span>" +
                                        "</div>" +
                                    "</div>" +
                                "</div>";
            }

            htmlContent += "</div>" +
                                "<div class=\"vtab-row col-sm-2 prem-tabs\">" +
                                    "<ul class=\"nav nav-tabs tabs-right\" style=\"text-align: right;\">" +
                                        "<li class=\"active\"><a href = \"#" + premio.GetHashCode() + "\" data-toggle=\"tab\">Info General</a></li>";

            foreach (var categoria in premio.ListaCategorias)
            {
                htmlContent += "<li>" +
                                            "<a href = \"#" + categoria.cveCategoria + "\" data-toggle= \"tab\"> " + categoria.Nombre +
                                                " <span class=\"badge\" style=\"font-weight: bold;\">" +
                                                    GetAplicacionesAceptadas(categoria.cveCategoria) +
                                                "</span>" +
                                            "</a>" +
                                        "</li>";
            }

            htmlContent += "</ul>" +
                                "</div>" +
                            "</div>";

            return htmlContent;
        }


        private int GetAplicacionesAceptadas(string idCategoria)
        {
            int cantidad = 0;
            List<PI_BA_Aplicacion> apliciones = ConvocatoriaService.ObtenerAplicacionesPorCategoria(idCategoria);

            foreach (var aplicacion in apliciones)
            {
                if (aplicacion.Status == "aceptada") cantidad++;
            }

            return cantidad;
        }

        private void CargarAplicaciones()
        {
            // obtener las categorias asociadas al juez actual
            var listaCategorias = EvaluacionService.GetCategoriaByJuez(Session[StringValues.CorreoSesion].ToString());

            // crear un TabPanel por cada categoria

            List<Premio> listaPremios = new List<Premio>();
            bool categoriaAgregada;

            foreach (var categoria in listaCategorias)
            {
                ///

                categoriaAgregada = false;

                foreach (var premio in listaPremios)
                {
                    if (premio.Nombre == EvaluacionService.GetNombrePremioByCategoria(categoria.cveCategoria).Nombre)
                    {
                        premio.ListaCategorias.Add(categoria);
                        categoriaAgregada = true;
                    }
                }
                if (!categoriaAgregada)
                {
                    List<PI_BA_Categoria> nuevaLista = new List<PI_BA_Categoria>();
                    nuevaLista.Add(categoria);
                    listaPremios.Add(new Premio(EvaluacionService.GetNombrePremioByCategoria(categoria.cveCategoria), nuevaLista));
                }
            }


            ///
            foreach (var premio in listaPremios)
            {
                HtmlControl divControl = new HtmlGenericControl("div");
                divControl.Visible = true;
                premiosJuez.Controls.Add(divControl);
                premiosJuez.Controls.Add(new LiteralControl("<div class=\"row\">" + CrearPremioHtml(premio) + "</div>"));
            }

        }

        private List<int> EstadoAplicaciones(string sCategoriaID)
        {
            List<int> CompletoNuevo = new List<int>();

            // Init
            CompletoNuevo.Add(0);
            CompletoNuevo.Add(0);

            string sMail = Session[StringValues.CorreoSesion].ToString();
            var categoria = AplicacionService.GetCategoriaByClaveCategoria(sCategoriaID);
            var premio = AplicacionService.GetPremioByClaveCategoria(sCategoriaID);

            if (premio == null || categoria == null || sMail == null)
                return CompletoNuevo;

            var listaCategorias = EvaluacionService.GetCategoriaByJuez(sMail);
            bool bValidJudge = CheckValidCategory(listaCategorias, sCategoriaID);

            if (bValidJudge)
            {
                // obtener aplicaciones para cierta categoria
                var aplicacionesACategoria = ConvocatoriaService.ObtenerAplicacionesPorCategoria(sCategoriaID);

                // obtener candidatos ligados a estas aplicaciones
                var listaCandidatos = ConvocatoriaService.JuezObtenerCandidatosPorAplicaciones(aplicacionesACategoria);
                foreach (var cand in listaCandidatos)
                {
                    // status column
                    var Eval = EvaluacionService.GetEvaluacionByAplicacionAndJuez(sMail, cand.Key.cveAplicacion);
                    if (Eval != null)
                    {
                        //Completo
                        CompletoNuevo[0]++;
                    }
                    else
                    {
                        //Nuevo
                        CompletoNuevo[1]++;
                    }
                }

            }

            return CompletoNuevo;
        }

        private bool CheckValidCategory(List<PI_BA_Categoria> ltCategories, string sCategoryID)
        {
            foreach (var category in ltCategories)
            {
                if (category.cveCategoria.Equals(sCategoryID))
                {
                    return true;
                }
            }
            return false;
        }


    }
}