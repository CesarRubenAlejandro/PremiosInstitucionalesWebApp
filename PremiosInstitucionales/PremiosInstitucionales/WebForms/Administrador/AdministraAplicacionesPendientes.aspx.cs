using PremiosInstitucionales.DBServices.Aplicacion;
using PremiosInstitucionales.DBServices.InformacionPersonalCandidato;
using PremiosInstitucionales.Values;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI.HtmlControls;
using PremiosInstitucionales.DBServices.Mail;

namespace PremiosInstitucionales.WebForms
{
    public partial class AdministraAplicacionesPendientes : System.Web.UI.Page
    {
        MP_Global MasterPage = new MP_Global();
        protected void Page_Load(object sender, EventArgs e)
        {
            MasterPage = (MP_Global)Page.Master;
            if (!IsPostBack)
            {
                // revisar la primera vez que se carga la pagina que se haya iniciado sesion con cuenta de admin
                if (Session[StringValues.RolSesion] != null)
                {
                    if (Session[StringValues.RolSesion].ToString() != StringValues.RolAdmin)
                    {
                        // si no es admin, redireccionar a inicio general
                        Response.Redirect("~/WebForms/Login.aspx", false);
                    }
                    else
                    {
                        LoadPendingApplications();
                    }
                }
                else
                {
                    Response.Redirect("~/WebForms/Login.aspx", false);
                }
            }
        }

        private void LoadPendingApplications()
        {
            var aplicaciones = AplicacionService.GetAplicacionesByStatus(StringValues.Solicitado);
            var aplicacionesModificadas = AplicacionService.GetAplicacionesByStatus(StringValues.Modificado);
            aplicaciones.AddRange(aplicacionesModificadas);

            aplicaciones = aplicaciones.OrderBy(a => a.cveCategoria).ToList();

            if (aplicaciones.Count > 0)
            {
                int iCont = 0;
                foreach (var app in aplicaciones)
                {
                    var categoria = AplicacionService.GetCategoriaByClaveCategoria(app.cveCategoria);
                    var premio = AplicacionService.GetPremioByClaveCategoria(app.cveCategoria);
                    var candidato = InformacionPersonalCandidatoService.GetCandidatoById(app.cveCandidato);

                    Panel panelMain = new Panel();
                    panelMain.CssClass = "panel panel-default";

                    Panel panelHeader = new Panel();
                    panelHeader.CssClass = "panel-heading";
                    panelHeader.Attributes.Add("role", "tab");
                    panelHeader.Attributes.Add("id", "heading" + iCont);

                    LiteralControl lcPanelTitle = new LiteralControl("" +
                        "<h4 class=\"panel-title\">" +
                            "<a role = \"button\" data-toggle=\"collapse\" data-parent=\"#" + "ContentPlaceHolder_accordion" + "\" href=\"#collapse" + iCont + "\" aria-expanded=\"true\" aria-controls=\"collapse" + iCont + "\" onclick=\"SetActualAppId('" + app.cveAplicacion + "');\">" +
                                "<strong> Premio " + premio.Nombre + " - Categoria " + categoria.Nombre+"</strong> | " +
                                candidato.Nombre + " " + candidato.Apellido +
                            "</a>" +
                        "</h4>" +
                        "");

                    panelHeader.Controls.Add(lcPanelTitle);

                    Panel panelCollapse = new Panel();
                    panelCollapse.CssClass = "panel-collapse collapse";
                    panelCollapse.Attributes.Add("role", "tabpanel");
                    panelCollapse.Attributes.Add("aria-labelledby", "heading" + iCont);
                    panelCollapse.Attributes.Add("id", "collapse" + iCont);

                    Panel panelCollapseBody = new Panel();
                    panelCollapseBody.CssClass = "panel-body";

                    panelCollapseBody.Controls.Add(new LiteralControl("<div class='row text-center'> <div class='col-sm-6'> <h5> <strong> Candidato: </strong> </h5>"));

                    LinkButton lbUserProfile = new LinkButton();
                    lbUserProfile.Text = candidato.Nombre + " " + candidato.Apellido;
                    lbUserProfile.Style.Add("font-size", "14pt");
                    lbUserProfile.Style.Add("color", "#00acc1");
                    lbUserProfile.Style.Add("text-decoration", "underline");
                    lbUserProfile.Attributes.Add("onclick", "window.open('AdministraInformacionPersonal.aspx?id=" + candidato.cveCandidato + "&t=candidato');");
                    panelCollapseBody.Controls.Add(lbUserProfile);

                    panelCollapseBody.Controls.Add(new LiteralControl("</div> <div class='col-sm-6'> <h5> <strong> Archivo proporcionado: </strong> </h5>"));

                    LinkButton lbDocumento = new LinkButton();
                    lbDocumento.Text = app.NombreArchivo;
                    lbDocumento.Style.Add("font-size", "14pt");
                    lbDocumento.Style.Add("color", "#00acc1");
                    lbDocumento.Style.Add("text-decoration", "underline");
                    lbDocumento.Command += new CommandEventHandler(DownloadFile);
                    lbDocumento.CommandArgument = app.cveAplicacion;
                    panelCollapseBody.Controls.Add(lbDocumento);

                    panelCollapseBody.Controls.Add(new LiteralControl("</div> </div>"));

                    Panel panelCollapseBodyQuestions = new Panel();
                    panelCollapseBodyQuestions.CssClass = "row question-form";
                    panelCollapseBodyQuestions.Style.Add("margin-left", "10px");
                    panelCollapseBodyQuestions.Style.Add("margin-right", "10px");

                    // Desplegar preguntas y respuestas
                    var preguntas = AplicacionService.GetFormularioByCategoria(app.cveCategoria);

                    if (preguntas.Count > 0)
                    {
                        short iNumber = 0;
                        foreach (var pregunta in preguntas)
                        {
                            Panel panelQuestion = new Panel();
                            panelQuestion.CssClass = "question-box";

                            LiteralControl lcPregunta = new LiteralControl("<h5> <strong>" + (iNumber + 1) + ". " + pregunta.Texto + "</strong> </h5>");
                            panelQuestion.Controls.Add(lcPregunta);

                            var respuesta = AplicacionService.GetRespuestaByPreguntaAndAplicacion(pregunta.cvePregunta, app.cveAplicacion);
                            if (respuesta == null)
                                break;

                            string[] lines = respuesta.Valor.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                            for (int i = 0; i < lines.Length; i++)
                            {
                                LiteralControl lcRespuesta = new LiteralControl("<h5>" + lines[i] + "</h5>");
                                panelQuestion.Controls.Add(lcRespuesta);
                            }

                            panelCollapseBodyQuestions.Controls.Add(panelQuestion);
                            iNumber++;
                        }
                    }

                    panelCollapseBody.Controls.Add(panelCollapseBodyQuestions);

                    // Desplegar opciones de aceptar o rechazar aplicacion
                    LiteralControl btnGroupAcceptReject = new LiteralControl("" +
                        "<div class=\"btn-group-right\" style=\"padding: 0px\">" +
                                "<a data-toggle=\"modal\" data-target=\"#modalRechazApp\" style=\"text-decoration: none\">" +
                                    "<button type = \"button\" class=\"btn btn-default\" style='margin-right: 5px;'>Rechazar Aplicacion</button>" +
                                "</a>" +
                                "<a data-toggle= \"modal\" data-target= \"#modalAcceptApp\" style= \"text-decoration: none\" >" +
                                    "<button type=\"button\" class=\"btn btn-primary\">Aceptar Aplicacion</button>" +
                                "</a>" +
                            "</div>"
                        );
                    panelCollapseBody.Controls.Add(btnGroupAcceptReject);
                    panelCollapse.Controls.Add(panelCollapseBody);
                    panelMain.Controls.Add(panelHeader);
                    panelMain.Controls.Add(panelCollapse);
                    accordion.Controls.Add(panelMain);

                    iCont++;
                }

            }

            else
            {
                //desplegar letrero de no aplicaciones
                HtmlControl divControl = new HtmlGenericControl("div");
                divControl.Attributes.Add("class", "text-center");
                divControl.Visible = true;
                divControl.Controls.Add(new LiteralControl("<h4> No hay registros pendientes que mostrar. </h4>"));
                appsdiv.Controls.Add(divControl);
            }
        }
        protected void bttnEnviarRechazo_Click(object sender, EventArgs e)
        {
            String aplicacionID = IdAppHidden.Value.ToString();
            String razonRechazo = razonTB.Text.ToString();

            // cambiar el status de la aplicacion a Rechazado
            AplicacionService.RechazarAplicacion(aplicacionID);

            // enviar correo notificando al candidato de la aplicacion
            var aplicacion = AplicacionService.ObtenerAplicacionDeClave(aplicacionID);
            string razon = razonTB.Text.ToString();
            var MailService = new MailService();
            MailService.EnviarCorreoRechazarAplicacion(aplicacion, razon);

            // cargar nuevamente el acordeon de respuestas forzando un postback
            razonTB.Text = "";
            Response.Redirect(Request.Url.AbsoluteUri, false);
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
                MasterPage.ShowMessage("Error", "El servidor no encontró el archivo.");
            }

        }

        protected void bttnAceptarAplicacion_Click(object sender, EventArgs e)
        {
            String aplicacionID = IdAppHidden.Value.ToString();
            // cambiar el status de la aplicacion a Aceptado
            AplicacionService.AceptarAplicacion(aplicacionID);
            // cargar nuevamente el acordeon de respuestas forzando un postback
            Response.Redirect(Request.Url.AbsoluteUri, false);
        }

        protected void BackBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("InicioAdmin.aspx", false);
        }
    }
}