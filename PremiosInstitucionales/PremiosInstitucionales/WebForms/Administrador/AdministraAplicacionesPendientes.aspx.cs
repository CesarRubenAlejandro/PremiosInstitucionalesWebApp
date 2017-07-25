using PremiosInstitucionales.DBServices.Aplicacion;
using PremiosInstitucionales.DBServices.InformacionPersonalCandidato;
using PremiosInstitucionales.Values;
using System;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI.HtmlControls;

namespace PremiosInstitucionales.WebForms
{
    public partial class AdministraAplicacionesPendientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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

            LoadPendingApplications();
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

                    LiteralControl lcUserProfile = new LiteralControl("<a href=\"AdministraInformacionPersonal.aspx?id=" + candidato.cveCandidato + "&t=candidato\" style=\"text-decoration: underline\"> <h5 style=\"color: #00acc1\"> Perfil de candidato </h5> </a>");

                    panelCollapseBody.Controls.Add(lcUserProfile);

                    LinkButton lbDocumento = new LinkButton();
                    lbDocumento.Text = "Descargar archivo";
                    lbDocumento.Style.Add("font-size", "16pt");
                    lbDocumento.Style.Add("color", "#00acc1");
                    lbDocumento.Style.Add("text-decoration", "underline");
                    lbDocumento.Style.Add("margin", "1.5em 0");
                    lbDocumento.Command += new CommandEventHandler(DownloadFile);
                    lbDocumento.CommandArgument = app.cveAplicacion;
                    panelCollapseBody.Controls.Add(lbDocumento);

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
                                    "<button type = \"button\" class=\"btn btn-default\">Rechazar Aplicacion</button>" +
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

                    //
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
            string razon = razonTB.Text.ToString();
            EnviarCorreoConfirmacion(razon, aplicacionID);

            // cargar nuevamente el acordeon de respuestas forzando un postback
            razonTB.Text = "";
            Response.Redirect(Request.Url.AbsoluteUri);
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

        private bool EnviarCorreoConfirmacion(String razon, String claveAplicacion)
        {
            var aplicacion = AplicacionService.ObtenerAplicacionDeClave(claveAplicacion);

            String correoSender = "empresa.ejemplo.mail@gmail.com";
            String pswSender = "proyectointegrador";
            try
            {
                using (MailMessage mm = new MailMessage(correoSender, aplicacion.PI_BA_Candidato.Correo))
                {
                    mm.Subject = "Requiere cambios la solicitud de registro en el sistema Premios Institucionales del Tec de Monterrey.";
                    var bodyContent = "";
                    bodyContent = File.ReadAllText(Server.MapPath("~/Values/CorreoSolicitudCambio.txt"));
                    // formatear contenidos de string
                    bodyContent = bodyContent.Replace(StringValues.ContenidoCorreoFecha, DateTime.Today.ToShortDateString());
                    bodyContent = bodyContent.Replace(StringValues.ContenidoCorreoNombre, aplicacion.PI_BA_Candidato.Nombre);
                    bodyContent = bodyContent.Replace(StringValues.ContenidoCorreoPremio, aplicacion.PI_BA_Categoria.PI_BA_Convocatoria.PI_BA_Premio.Nombre);
                    bodyContent = bodyContent.Replace(StringValues.ContenidoCorreoCategoria, aplicacion.PI_BA_Categoria.Nombre);
                    bodyContent = bodyContent.Replace(StringValues.ContenidoCorreoRazon, razon);
                    // enviar
                    mm.Body = bodyContent;
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential(correoSender, pswSender);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        protected void bttnAceptarAplicacion_Click(object sender, EventArgs e)
        {
            String aplicacionID = IdAppHidden.Value.ToString();
            // cambiar el status de la aplicacion a Aceptado
            AplicacionService.AceptarAplicacion(aplicacionID);
            // cargar nuevamente el acordeon de respuestas forzando un postback
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void BackBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("InicioAdmin.aspx");

        }
    }
}