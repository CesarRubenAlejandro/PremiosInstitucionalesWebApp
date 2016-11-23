using PremiosInstitucionales.DBServices.Aplicacion;
using PremiosInstitucionales.Entities.Models;
using PremiosInstitucionales.Values;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PremiosInstitucionales.WebForms
{
    public partial class InicioCandidato : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // revisar la primera vez que se carga la pagina que se haya iniciado sesion con cuenta de candidato
                if (Session[StringValues.RolSesion] != null)
                {
                    if (Session[StringValues.RolSesion].ToString() != StringValues.RolCandidato)
                        // si no es candidato, redireccionar a login
                        Response.Redirect("Login.aspx");
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }

            var aplicaciones = AplicacionService.GetAplicacionesByCorreo(Session[StringValues.CorreoSesion].ToString());
            if (aplicaciones != null) { 
                foreach (var ap in aplicaciones)
                {
                    //desplegar categoria
                    Literal lit = new Literal();
                    lit.Text = @"<h3>" + AplicacionService.GetPremioCategoriaByClaveCategoria(ap.cveCategoria).ToString() + "</h3> <br/>";
                    contenidoiniciocandidato.Controls.Add(lit);

                    //desplegar mapa de estados
                    HtmlControl divControl = new HtmlGenericControl("div");
                    divControl.Attributes.Add("class", "crumbs");
                    divControl.Visible = true; // Not really necessary
                    contenidoiniciocandidato.Controls.Add(divControl);

                    divControl.Controls.Add(new LiteralControl(obtenerHtmlMapaEstados(ap)));
               }
            } else
            {
                //desplegar letrero de no aplicaciones
                HtmlControl divControl = new HtmlGenericControl("div");
                divControl.Visible = true; // Not really necessary
                contenidoiniciocandidato.Controls.Add(divControl);

                divControl.Controls.Add(new LiteralControl("<p> Por el momento no tienes aplicaciones a premios institucionales para mostrar. </p>"));
            }
        }

        public static String obtenerHtmlMapaEstados(PI_BA_Aplicacion ap)
        {
            //regresar codigo html del mapa pertinente segun el estado actual de la aplicacion
            if (AplicacionService.GetHasEndedByCategoria(ap.cveCategoria.ToString()))
            {
                return "<ul>" +
                            "<li><a href = \"#1\"> Solicitada </a></li>" +
                            "<li><a href = \"#4\"> Aceptada </a></li>" +
                            "<li class=\"edoactual\"><a href = \"#5\"> Convocatoria cerrada </a></li>" +
                            "</ul> <br/> <br/> <br/>" + 
                            StringValues.ExplicacionFin + "<br/> <br/> <br/>";
            } else if (ap.Status == StringValues.Solicitado)
            {
                return "<ul>" +
                            "<li class=\"edoactual\"><a href = \"#1\"> Solicitada </a></li>" +
                            "<li><a href = \"#4\"> Aceptada </a></li>" +
                            "<li><a href = \"#5\"> Convocatoria cerrada </a></li>" +
                            "</ul> <br/> <br/> <br/> " +
                            StringValues.ExplicacionSolicitado + "<br/> <br/> <br/>";
            } else if (ap.Status == StringValues.Rechazado)
            {
                return "<ul>" +
                            "<li><a href = \"#1\"> Solicitada </a></li>" +
                            "<li class=\"edoactual\"><a href = \"#2\"> Requiere cambios </a></li>" +
                            "<li><a href = \"#3\"> Modificada </a></li>" +
                            "<li><a href = \"#4\"> Aceptada </a></li>" +
                            "<li><a href = \"#5\"> Convocatoria cerrada </a></li>" +
                            "</ul> <br/> <br/> <br/> " +
                            StringValues.ExplicacionRechazado + "<a href=\"CorrigeAplicacion.aspx?aplicacion=" + ap.cveAplicacion + "\">Corrige</a>" 
                            +  "<br/> <br/> <br/>";
            } else if (ap.Status == StringValues.Modificado)
            {
                return "<ul>" +
                            "<li><a href = \"#1\"> Solicitada </a></li>" +
                            "<li><a href = \"#2\"> Requiere cambios </a></li>" +
                            "<li class=\"edoactual\"><a href = \"#3\"> Modificada </a></li>" +
                            "<li><a href = \"#4\"> Aceptada </a></li>" +
                            "<li><a href = \"#5\"> Convocatoria cerrada </a></li>" +
                            "</ul> <br/> <br/> <br/>" +
                            StringValues.ExplicacionModificado + "<br/> <br/> <br/>"; ;
            } else if (ap.Status == StringValues.Aceptado)
            {
                return "<ul>" +
                            "<li><a href = \"#1\"> Solicitada </a></li>" +
                            "<li class=\"edoactual\"><a href = \"#4\"> Aceptada </a></li>" +
                            "<li><a href = \"#5\"> Convocatoria cerrada </a></li>" +
                            "</ul> <br/> <br/> <br/>" +
                            StringValues.ExplicacionAceptado + "<br/> <br/> <br/>";
            }
            else
            {
                return "";
            }
        }
    }
}