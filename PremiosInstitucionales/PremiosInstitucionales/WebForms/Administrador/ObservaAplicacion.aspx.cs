using PremiosInstitucionales.DBServices.Aplicacion;
using PremiosInstitucionales.DBServices.Convocatoria;
using PremiosInstitucionales.DBServices.Evaluacion;
using PremiosInstitucionales.DBServices.InformacionPersonalCandidato;
using PremiosInstitucionales.Entities.Models;
using PremiosInstitucionales.Values;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PremiosInstitucionales.WebForms
{
    public partial class ObservaAplicacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String idApp = Request.QueryString["a"];
            if (idApp != null)
            {
                PerfilCandidato();
                CrearArchivo();

                String sCategoriaID = AplicacionService.GetCveCategoriaByAplicacion(idApp);
                if (sCategoriaID != null)
                {
                    var premio = ConvocatoriaService.GetPremioByCategoria(sCategoriaID);
                    var categoria = ConvocatoriaService.GetCategoriaById(sCategoriaID);

                    if (premio != null && categoria != null)
                    {
                        string sMail = Session[StringValues.CorreoSesion].ToString();
                        var listaCategorias = EvaluacionService.GetCategoriaByJuez(sMail);
                        var eval = CheckExistenceOfEvaluation(sMail, idApp);

                        CrearFormulario(sCategoriaID, premio, categoria);
                    }
                }
            }

            if (!IsPostBack)
            {
                // revisar la primera vez que se carga la pagina que se haya iniciado sesion con cuenta de admin
                if (Session[StringValues.RolSesion] != null)
                {
                    if (Session[StringValues.RolSesion].ToString() != StringValues.RolAdmin)
                        // si no es admin, redireccionar a inicio general
                        Response.Redirect("~/WebForms/Login.aspx");
                }
                else
                {
                    Response.Redirect("~/WebForms/Login.aspx");
                }
            }
        }

        private void CrearArchivo()
        {
            LiteralControl lcPregunta = new LiteralControl("<h5> <strong>" + "Archivo proporcionado:" + "</strong> </h5>");
            PanelArchivo.Controls.Add(lcPregunta);

            string appId = Request.QueryString["a"];
            var app = AplicacionService.GetAplicacionById(appId);

            LinkButton lbDocumento = new LinkButton();
            lbDocumento.Text = app.NombreArchivo;
            lbDocumento.Style.Add("font-size", "14pt");
            lbDocumento.Style.Add("color", "#00acc1");
            lbDocumento.Style.Add("text-decoration", "underline");
            lbDocumento.Command += new CommandEventHandler(DownloadFile);
            lbDocumento.CommandArgument = appId;
            PanelArchivo.Controls.Add(lbDocumento);
        }

        private void PerfilCandidato()
        {
            // Perfil
            String idApp = Request.QueryString["a"];
            var app = AplicacionService.GetAplicacionById(idApp);
            var candidato = InformacionPersonalCandidatoService.GetCandidatoById(app.cveCandidato);

            LiteralControl lcPerfil = new LiteralControl("<h5> <strong>" + "Candidato:" + "</strong> </h5>");
            PanelArchivo.Controls.Add(lcPerfil);

            LinkButton lbUserProfile = new LinkButton();
            lbUserProfile.Text = candidato.Nombre + " " + candidato.Apellido;
            lbUserProfile.Style.Add("font-size", "14pt");
            lbUserProfile.Style.Add("color", "#00acc1");
            lbUserProfile.Style.Add("text-decoration", "underline");
            lbUserProfile.Attributes.Add("onclick", "window.open('AdministraInformacionPersonal.aspx?id=" + candidato.cveCandidato + "&t=candidato');");
            PanelArchivo.Controls.Add(lbUserProfile);

            // Calif. Promedio
            LiteralControl lcPregunta = new LiteralControl("<h5> <strong>" + "Calificación promedio:" + "</strong> </h5>");
            PanelArchivo.Controls.Add(lcPregunta);

            LiteralControl lcCalificacion = new LiteralControl();
            var evaluaciones = EvaluacionService.GetEvaluacionesByAplicacion(app.cveAplicacion);
            double prom = GetPromedioEvaluaciones(evaluaciones);
            if (prom >= 70)
            {
                lcCalificacion = new LiteralControl("<h5 style=\"color: #4caf50;\"> <div style=\"display: none; \"> " + (prom - 100) + " </div> " + prom + " </h5>");
            }
            else if (prom >= 0)
            {
                lcCalificacion = new LiteralControl("<h5 style=\"color: #f9a825;\"> <div style=\"display: none; \"> " + (prom - 100) + " </div> " + prom + " </h5>");
            }
            else
            {
                lcCalificacion = new LiteralControl("<h5 style=\"color: #f44336;\"> <div style=\"display: none; \"> 1000 </div> Sin evaluaciones </h5>");
            }
            PanelArchivo.Controls.Add(lcCalificacion);
        }

        private void CrearFormulario(String sCategoriaID, PI_BA_Premio premio, PI_BA_Categoria categoria)
        {
            litTituloPremio.Text = "Premio " + premio.Nombre;
            litTituloCategoria.Text = "Categoría: " + categoria.Nombre;

            // obtener lista de preguntas y respuestas para la aplicacion
            var preguntas = AplicacionService.GetFormularioByCategoria(sCategoriaID);

            if (preguntas != null)
            {
                short iNumber = 0;
                foreach (var pregunta in preguntas)
                {
                    Panel panel = new Panel();
                    panel.CssClass = "question-box";

                    LiteralControl lcPregunta = new LiteralControl("<h5> <strong>" + (iNumber + 1) + ". " + pregunta.Texto + "</strong> </h5>");
                    panel.Controls.Add(lcPregunta);

                    var respuesta = AplicacionService.GetRespuestaByPreguntaAndAplicacion(pregunta.cvePregunta, Request.QueryString["a"]);
                    string[] lines = respuesta.Valor.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        LiteralControl lcRespuesta = new LiteralControl("<h5>" + lines[i] + "</h5>");
                        panel.Controls.Add(lcRespuesta);
                    }

                    PanelFormulario.Controls.Add(panel);
                    iNumber++;
                }
            }

        }

        private PI_BA_Evaluacion CheckExistenceOfEvaluation(string sMail, string sAppId)
        {
            var eval = EvaluacionService.GetEvaluacionByAplicacionAndJuez(sMail, sAppId);
            return eval;
        }

        private double GetPromedioEvaluaciones(List<PI_BA_Evaluacion> evaluaciones)
        {
            double? result = evaluaciones.Average(eval => eval.Calificacion);
            if (result.HasValue)
                return result.Value;
            return -1;
        }

        public void DownloadFile(object sender, CommandEventArgs e)
        {
            var app = AplicacionService.GetAplicacionById(e.CommandArgument.ToString());
            string FileName = app.NombreArchivo;
            string FilePath = Server.MapPath("~/UsersAppsFiles/") + FileName;
            FileInfo fs = new FileInfo(FilePath);
            int FileLength = Convert.ToInt32(fs.Length);

            if (File.Exists(FilePath))
            {
                Response.Clear();
                Response.BufferOutput = false;
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Length", FileLength.ToString());
                Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
                Response.TransmitFile(FilePath);
                Response.Flush();
            }
            else
            {
                //lblMsg.Text = "Error: File not found!";
            }

        }
    }
}