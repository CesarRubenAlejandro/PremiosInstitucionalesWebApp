using PremiosInstitucionales.DBServices.Aplicacion;
using PremiosInstitucionales.DBServices.InformacionPersonalCandidato;
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
                LoadMessage();
            }
        }

        private void LoadMessage()
        {
            var correo = Session[StringValues.CorreoSesion].ToString();
            var candidato = InformacionPersonalCandidatoService.GetCandidatoByCorreo(correo);

            if (candidato == null)
                Response.Redirect("Login.aspx");

            if (candidato.Nombre != null && candidato.Nombre.Length > 0)
            {
                litBienvenidoUsuario.Text = "<h1> Bienvenido, " + candidato.Nombre + " </h1>";
            }
            else
            {
                litBienvenidoUsuario.Text = "<h1> Bienvenido </h1>";
            }
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

            if (ap.Status == StringValues.Solicitado){

                //Si se ha solicitado el registro en la convocatoria : Mapa 1 - Solicitado
                return "<ul>" +
                            "<li class=\"edoactual\"><a href = \"#\">" + StringValues.TituloSolicitado + "</a></li>" +
                            "<li><a href = \"#\">" + StringValues.TituloAceptado + "</a></li>" +
                            "<li><a href = \"#\">" + StringValues.TituloVeredicto + "</a></li>" +
                            "</ul> <br/> <br/> <br/>" +
                            StringValues.ExplicacionSolicitado + "<br/> <br/> <br/>";

            } else if (ap.Status == StringValues.Aceptado && veredicto) {
            
                //Si el registro fue aceptado y ya se tiene un veredicto : Mapa 1 - Veredicto
                return "<ul>" +
                            "<li><a href = \"#\">" + StringValues.TituloSolicitado + "</a></li>" +
                            "<li><a href = \"#\">" + StringValues.TituloAceptado + "</a></li>" +
                            "<li class=\"edoactual\"><a href = \"#\">" + StringValues.TituloVeredicto + "</a></li>" +
                            "</ul> <br/> <br/> <br/>" +
                            StringValues.ExplicacionVeredicto + "<br/> <br/> <br/>";

            } else if (ap.Status == StringValues.Aceptado && !veredicto) {
                //Si el registro fue aceptado pero no se tiene un veredicto : Mapa 1 - Aceptado
                return "<ul>" +
                            "<li><a href = \"#\">" + StringValues.TituloSolicitado + "</a></li>" +
                            "<li class=\"edoactual\"><a href = \"#\">" + StringValues.TituloAceptado + "</a></li>" +
                            "<li><a href = \"#\">" + StringValues.TituloVeredicto + "</a></li>" +
                            "</ul> <br/> <br/> <br/>" +
                            StringValues.ExplicacionAceptado + "<br/> <br/> <br/>";
            } else if (ap.Status == StringValues.Rechazado && cierre) {
                //Si el registro requiere cambios pero ya ha cerrado la fecha de convocatoria : Mapa 3 - Cerrado
                return "<ul>" +
                            "<li><a href = \"#\">" + StringValues.TituloSolicitado + "</a></li>" +
                            "<li><a href = \"#\">" + StringValues.TituloRechazado + "</a></li>" +
                            "<li class=\"edoactual\"><a href = \"#\">" + StringValues.TituloCerrado + "</a></li>" +
                            "</ul> <br/> <br/> <br/>" +
                            StringValues.ExplicacionCerrado + "<br/> <br/> <br/>";
            } else if (ap.Status == StringValues.Rechazado && !cierre) {
                //Si el registro requiere cambios y no ha cerrado la fecha de convocatoria : Mapa 2 - Rechazado
                return "<ul>" +
                            "<li><a href = \"#\">" + StringValues.TituloSolicitado + "</a></li>" +
                            "<li class=\"edoactual\"><a href = \"#\">" + StringValues.TituloRechazado + "</a></li>" +
                            "<li><a href = \"#\">" + StringValues.TituloModificado + "</a></li>" +
                            "<li><a href = \"#\">" + StringValues.TituloAceptado + "</a></li>" +
                            "<li><a href = \"#\">" + StringValues.TituloVeredicto + "</a></li>" +
                            "</ul> <br/> <br/> <br/>" +
                            StringValues.ExplicacionRechazado + "<a href=\"CorrigeAplicacion.aspx?aplicacion=" + 
                            ap.cveAplicacion + "\">Haz clic aquí para modificarla.</a>"  + "<br/> <br/> <br/>";
            } else if (ap.Status == StringValues.Modificado) {
                //Si se han enviado las modificaciones para revision : Mapa 2 - Modificado
                return "<ul>" +
                            "<li><a href = \"#\">" + StringValues.TituloSolicitado + "</a></li>" +
                            "<li><a href = \"#\">" + StringValues.TituloRechazado + "</a></li>" +
                            "<li class=\"edoactual\"><a href = \"#\">" + StringValues.TituloModificado + "</a></li>" +
                            "<li><a href = \"#\">" + StringValues.TituloAceptado + "</a></li>" +
                            "<li><a href = \"#\">" + StringValues.TituloVeredicto + "</a></li>" +
                            "</ul> <br/> <br/> <br/>" +
                            StringValues.ExplicacionModificado + "<br/> <br/> <br/>";
            } else
            {
                return "";
            }
        }
    }
}