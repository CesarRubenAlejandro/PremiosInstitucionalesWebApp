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
        public List<PI_BA_Categoria> ListaCategorias;
        public Premio(string nombre, List<PI_BA_Categoria> listaCategorias)
        {
            Nombre = nombre;
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
                                   "<img src = /img/" + nombreImagen + " class=\"img-square\"/>" +
                               "</div>" +
                               "<div class=\"tab-content col-sm-8 prem-info\">" +
                                   "<div id = \"" + premio.GetHashCode() + "\" class=\"tab-pane fade in active\">" +
                                       "<h4>" + premio.Nombre + "</h4>" +
                                       "<p>" +
                                            "TODO: Agregar una descripcion por premio en la BD." +
                                       "</p>" +
                                   "</div>";

            foreach (var categoria in premio.ListaCategorias)
            {
                htmlContent += "<div id = \"" + categoria.Nombre + "\" class=\"tab-pane fade\">" +
                                        "<h4>" + categoria.Nombre + "</h4>";

                var ListaAplicaciones = ConvocatoriaService.ObtenerAplicacionesPorCategoria(categoria.cveCategoria);
                foreach (var aplicacion in ListaAplicaciones)
                {
                    htmlContent += aplicacion.Status + " ";
                }

                htmlContent += "</div>";
            }

            htmlContent += "</div>" +
                                "<div class=\"vtab-row col-sm-2 prem-tabs\">" +
                                    "<ul class=\"nav nav-tabs tabs-right\" style=\"text-align: right;\">" +
                                        "<li class=\"active\"><a href = \"#" + premio.GetHashCode() + "\" data-toggle=\"tab\">Info General</a></li>";

            foreach (var categoria in premio.ListaCategorias)
            {
                htmlContent += "<li>" +
                                            "<a href = \"#" + categoria.Nombre + "\" data-toggle= \"tab\"> " + categoria.Nombre +
                                                " <span class=\"badge\" style=\"font-weight: bold;\">" +
                                                    ConvocatoriaService.ObtenerAplicacionesPorCategoria(categoria.cveCategoria).Count +
                                                "</span>" +
                                            "</a>" +
                                        "</li>";
            }

            htmlContent += "</ul>" +
                                "</div>" +
                            "</div>";

            return htmlContent;
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
                    if (premio.Nombre == EvaluacionService.GetNombrePremioByCategoria(categoria.cveCategoria))
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

    }
}