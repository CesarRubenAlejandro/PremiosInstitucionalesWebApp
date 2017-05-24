using PremiosInstitucionales.DBServices.Aplicacion;
using PremiosInstitucionales.Entities.Models;
using PremiosInstitucionales.Values;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace PremiosInstitucionales.WebForms
{
    public partial class AplicacionesCandidato : System.Web.UI.Page
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
                        Response.Redirect("~/WebForms/Login.aspx");
                }
                else
                {
                    Response.Redirect("~/WebForms/Login.aspx");
                }
            }

            var aplicaciones = AplicacionService.GetAplicacionesByCorreo(Session[StringValues.CorreoSesion].ToString());
            if (aplicaciones != null)
            {
                foreach (var ap in aplicaciones)
                {
                    //desplegar mapa de estados
                    HtmlControl divControl = new HtmlGenericControl("div");
                    divControl.Attributes.Add("class", "crumbs");
                    divControl.Visible = true;
                    estadosaplicaciones.Controls.Add(divControl);

                    divControl.Controls.Add(new LiteralControl(obtenerHtmlMapaEstados(ap)));
                }
            }
            else
            {
                //desplegar letrero de no aplicaciones
                HtmlControl divControl = new HtmlGenericControl("div");
                divControl.Visible = true;
                estadosaplicaciones.Controls.Add(divControl);

                divControl.Controls.Add(new LiteralControl("<p> Por el momento no tienes aplicaciones a premios institucionales para mostrar. </p>"));
            }
        }


        public static String crearHtmlMapaEstados(PI_BA_Aplicacion ap, String estados)
        {
            return "<div class=\"panel-heading\">" +
                        "<h3 class=\"panel-title\">" +
                               "Premio: <strong>" + AplicacionService.GetPremioByClaveCategoria(ap.cveCategoria).Nombre.ToString() + "</strong>" +
                               "<strong> / </strong>" +
                               "Categoria: <strong>" + AplicacionService.GetCategoriaByClaveCategoria(ap.cveCategoria).Nombre.ToString() + "</strong>" +
                        "</h3>" +
                    "</div>" +
                    "<div class=\"panel-body\">" +
                        "<div class=\"div-img\">" +
                            "<img src = /AwardPictures/" + AplicacionService.GetPremioByClaveCategoria(ap.cveCategoria).NombreImagen.ToString() + " class=\"img-square\"/>" +
                        "</div>" +
                        "<div class=\"div-bpm\">" +
                            "Estado de la solicitud:" +
                            estados +
                        "</div>" +
                    "</div>";
        }
       

        /**
         * ObtenerHtmlMapaEstados
         * Regresa codigo html del mapa pertinente segun el estado actual de la aplicacion
         * Tipos de Mapas de estados posibles a mostrar:
         * 1) Solicitado - Aceptado - Veredicto
         * 2) Solicitado - Rechazado - Modificado - Aceptado - Veredicto
         * 3) Solicitado - Rechazado - Cerrado
         **/
        public static String obtenerHtmlMapaEstados(PI_BA_Aplicacion ap)
        {
            //obtener si las fechas de cierre de convocatoria y de veredicto final ya pasaron
            bool cierre = AplicacionService.HasEndedByCategoria(ap.cveCategoria.ToString());
            bool veredicto = AplicacionService.HasWinnersByCategoria(ap.cveCategoria.ToString());

            if (ap.Status == StringValues.Solicitado)
            {
                //Si se ha solicitado el registro en la convocatoria : Mapa 1 - Solicitado
                return "<div class=\"panel panel-primary\">" +
                        crearHtmlMapaEstados(ap,
                                        "<ul class=\"nav nav-wizard bpm-process\">" +
                                            "<li class=\"active\"><a><div class=\"step\">" + StringValues.TituloSolicitado + "</div></a></li>" +
                                            "<li><a><div class=\"step\">" + StringValues.TituloAceptado + "</div></a></li>" +
                                            "<li><a><div class=\"step\">" + StringValues.TituloVeredicto + "</div></a></li>" +
                                        "</ul>" +
                                        StringValues.ExplicacionSolicitado) +
                        "</div>";
            }
            else if (ap.Status == StringValues.Aceptado && veredicto)
            {
                //Si el registro fue aceptado y ya se tiene un veredicto : Mapa 1 - Veredicto
                return "<div class=\"panel panel-success\">" +
                        crearHtmlMapaEstados(ap,
                                        "<ul class=\"nav nav-wizard bpm-process aceptado\">" +
                                            "<li><a><div class=\"step\">" + StringValues.TituloSolicitado + "</div></a></li>" +
                                            "<li><a><div class=\"step\">" + StringValues.TituloAceptado + "</div></a></li>" +
                                            "<li class=\"active\"><a><div class=\"step\">" + StringValues.TituloVeredicto + "</div></a></li>" +
                                        "</ul>" +
                                        StringValues.ExplicacionVeredicto) +
                        "</div>";
            }
            else if (ap.Status == StringValues.Aceptado && !veredicto)
            {
                //Si el registro fue aceptado pero no se tiene un veredicto : Mapa 1 - Aceptado
                return "<div class=\"panel panel-success\">" +
                        crearHtmlMapaEstados(ap,
                                        "<ul class=\"nav nav-wizard bpm-process aceptado\">" +
                                            "<li><a><div class=\"step\">" + StringValues.TituloSolicitado + "</div></a></li>" +
                                            "<li class=\"active\"><a><div class=\"step\">" + StringValues.TituloAceptado + "</div></a></li>" +
                                            "<li><a><div class=\"step\">" + StringValues.TituloVeredicto + "</div></a></li>" +
                                        "</ul>" +
                                        StringValues.ExplicacionAceptado) +
                        "</div>";
            }
            else if (ap.Status == StringValues.Rechazado && cierre)
            {
                //Si el registro requiere cambios pero ya ha cerrado la fecha de convocatoria : Mapa 3 - Cerrado
                return "<div class=\"panel panel-danger\">" +
                        crearHtmlMapaEstados(ap,
                                        "<ul class=\"nav nav-wizard bpm-process cambios\">" +
                                            "<li><a><div class=\"step\">" + StringValues.TituloSolicitado + "</div></a></li>" +
                                            "<li><a><div class=\"step\">" + StringValues.TituloRechazado + "</div></a></li>" +
                                            "<li class=\"active\"><a><div class=\"step\">" + StringValues.TituloCerrado + "</div></a></li>" +
                                        "</ul>" +
                                        StringValues.ExplicacionCerrado) +
                        "</div>";
            }
            else if (ap.Status == StringValues.Rechazado && !cierre)
            {
                //Si el registro requiere cambios y no ha cerrado la fecha de convocatoria : Mapa 2 - Rechazado
                return "<div class=\"panel panel-danger\">" +
                        crearHtmlMapaEstados(ap,
                                        "<ul class=\"nav nav-wizard bpm-process cambios\">" +
                                            "<li><a><div class=\"step\">" + StringValues.TituloSolicitado + "</div></a></li>" +
                                            "<li class=\"active\"><a><div class=\"step\">" + StringValues.TituloRechazado + "</div></a></li>" +
                                            "<li><a><div class=\"step\">" + StringValues.TituloModificado + "</div></a></li>" +
                                            "<li><a><div class=\"step\">" + StringValues.TituloAceptado + "</div></a></li>" +
                                            "<li><a><div class=\"step\">" + StringValues.TituloVeredicto + "</div></a></li>" +
                                        "</ul>" +
                                        StringValues.ExplicacionRechazado +
                                        "<a href=\"CorrigeAplicacion.aspx?aplicacion=" +
                                            ap.cveAplicacion +
                                        "\">Haz clic aquí para modificarla.</a>") +
                        "</div>";
            }
            else if (ap.Status == StringValues.Modificado)
            {
                //Si se han enviado las modificaciones para revision : Mapa 2 - Modificado
                return "<div class=\"panel panel-warning\">" +
                        crearHtmlMapaEstados(ap,
                                        "<ul class=\"nav nav-wizard bpm-process modificada\">" +
                                            "<li><a><div class=\"step\">" + StringValues.TituloSolicitado + "</div></a></li>" +
                                            "<li><a><div class=\"step\">" + StringValues.TituloRechazado + "</div></a></li>" +
                                            "<li class=\"active\"><a><div class=\"step\">" + StringValues.TituloModificado + "</div></a></li>" +
                                            "<li><a><div class=\"step\">" + StringValues.TituloAceptado + "</div></a></li>" +
                                            "<li><a><div class=\"step\">" + StringValues.TituloVeredicto + "</div></a></li>" +
                                        "</ul>" +
                                        StringValues.ExplicacionModificado) +
                        "</div>";
            }
            else
            {
                return "";
            }
        }
    }
}