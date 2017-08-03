using PremiosInstitucionales.DBServices.Aplicacion;
using PremiosInstitucionales.DBServices.Convocatoria;
using PremiosInstitucionales.DBServices.Evaluacion;
using PremiosInstitucionales.DBServices.InformacionPersonalJuez;
using PremiosInstitucionales.Entities.Models;
using PremiosInstitucionales.Values;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PremiosInstitucionales.WebForms
{
    public partial class EvaluaAplicacion : System.Web.UI.Page
    {
        MP_Global MasterPage = new MP_Global();
        String cveAplicacion = null;

        int cveMensaje;
        List<Tuple<String, String>> MessagesList = new List<Tuple<String, String>>();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Load Globals
            MasterPage = (MP_Global)Page.Master;
            cveAplicacion = Request.QueryString["a"];
            
            // Load Page
            LoadFile();
            LoadMessages();

            if (!IsPostBack)
            {
                // Show Message
                if (int.TryParse(Request.QueryString["m"], out cveMensaje))
                {
                    if (cveMensaje >= 0 && cveMensaje < MessagesList.Count)
                    {
                        MasterPage.ShowMessage(MessagesList[cveMensaje].Item1, MessagesList[cveMensaje].Item2);
                    }    
                }
                    
                // revisar la primera vez que se carga la pagina que se haya iniciado sesion con cuenta de juez
                if (Session[StringValues.RolSesion] != null)
                {
                    if (Session[StringValues.RolSesion].ToString() != StringValues.RolJuez)
                    {
                        // si no es juez, redireccionar a inicio general
                        Response.Redirect("~/WebForms/Login.aspx", false);
                    }
                }
                else
                {
                    Response.Redirect("~/WebForms/Login.aspx", false);
                }

                // Mostrar Caificación guardada / Mostrar botones correctos 
                if (cveAplicacion != null)
                {
                    String sCategoriaID = AplicacionService.GetCveCategoriaByAplicacion(cveAplicacion);
                    if (sCategoriaID != null)
                    {
                        var premio = ConvocatoriaService.GetPremioByCategoria(sCategoriaID);
                        var categoria = ConvocatoriaService.GetCategoriaById(sCategoriaID);

                        if (premio != null && categoria != null)
                        {
                            string sMail = Session[StringValues.CorreoSesion].ToString();
                            var listaCategorias = EvaluacionService.GetCategoriaByJuez(sMail);
                            bool bValidJudge = CheckValidCategory(listaCategorias, sCategoriaID);
                            var Eval = EvaluacionService.GetEvaluacionByAplicacionAndJuez(sMail, cveAplicacion);
                            if (bValidJudge)
                            {
                                if (Eval != null)
                                {
                                    evaluateApplicationBtn.Visible = false;
                                    modifiyEvaluationBtn.Visible = true;
                                    aplicationEvaluationNumber.Text = Eval.Calificacion.ToString();
                                }
                                else
                                {
                                    evaluateApplicationBtn.Visible = true;
                                    modifiyEvaluationBtn.Visible = false;
                                }

                                CrearFormulario(sCategoriaID, premio, categoria);
                                return;
                            }
                        }
                    }
                }
                Response.Redirect("inicioJuez.aspx", false);
            }
        }

        private void LoadFile()
        {
            var Aplicacion = AplicacionService.GetAplicacionById(cveAplicacion);

            PanelArchivo.Controls.Add(new LiteralControl("<div class='row text-center'>"+
                "<div class='col-sm-6'>"+
                    "<h5>"+
                        "<strong> Candidato: </strong> <br/><br/>" + Aplicacion.PI_BA_Candidato.Nombre + " " + Aplicacion.PI_BA_Candidato.Apellido +
                    "</h5>"+
                "</div>"+
                    "<div class='col-sm-6'>"+
                    "<h5> <strong> Archivo proporcionado: </strong> </h5>"));

            // Archivo a descargar
            LinkButton lbDocumento = new LinkButton();
            lbDocumento.Text = Aplicacion.NombreArchivo;
            lbDocumento.Style.Add("font-size", "16pt");
            lbDocumento.Style.Add("color", "#00acc1");
            lbDocumento.Style.Add("text-decoration", "underline");
            lbDocumento.Style.Add("margin", "1.5em 0");
            lbDocumento.Command += new CommandEventHandler(DownloadFile);
            lbDocumento.CommandArgument = cveAplicacion;
            PanelArchivo.Controls.Add(lbDocumento);

            PanelArchivo.Controls.Add(new LiteralControl("</div> </div>"));
        }

        private void LoadMessages()
        {
            // Mensaje 0
            MessagesList.Add(Tuple.Create("Error", "El servidor encontró un error al procesar la solicitud."));
            // Mensaje 1
            MessagesList.Add(Tuple.Create("Aviso", "Evaluación guardada con éxito."));
            // Mensaje 2
            MessagesList.Add(Tuple.Create("Error", "Evaluación no encontrada."));
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

                    var respuesta = AplicacionService.GetRespuestaByPreguntaAndAplicacion(pregunta.cvePregunta, cveAplicacion);
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

        protected void EvaluarAplicacion(object sender, EventArgs e)
        {
            try
            {
                String cveJuez = InformacionPersonalJuezService.GetJuezByCorreo(Session[StringValues.CorreoSesion].ToString()).cveJuez;
                // Verificar que no exista ya una evaluación
                if (EvaluacionService.GetEvaluacionByAplicacionAndJuez(cveAplicacion, cveJuez) == null)
                {
                    short evaluacion = 0;
                    short.TryParse(aplicationEvaluationNumber.Text, out evaluacion);
                    PI_BA_Evaluacion ev = new PI_BA_Evaluacion();
                    ev.cveEvaluacion = Guid.NewGuid().ToString();
                    ev.cveAplicacion = cveAplicacion;
                    ev.cveJuez = cveJuez;
                    ev.Calificacion = evaluacion;
                    EvaluacionService.CrearEvaluacion(ev);
                    cveMensaje = 1;
                }
                // Si ya existe
                else
                {
                    ModificarAplicacion(sender, e);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                cveMensaje = 0;
            }
            Response.Redirect("EvaluaAplicacion.aspx" + "?m=" + cveMensaje + "&a=" + cveAplicacion, false);
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

        protected void ModificarAplicacion(object sender, EventArgs e)
        {
            try
            {
                var juez = InformacionPersonalJuezService.GetJuezByCorreo(Session[StringValues.CorreoSesion].ToString());
                var eval = EvaluacionService.GetEvaluacionByAplicacionAndJuez(juez.Correo, cveAplicacion);
                if(eval != null)
                {
                    try
                    {
                        short evaluacion = 0;
                        short.TryParse(aplicationEvaluationNumber.Text, out evaluacion);

                        EvaluacionService.ActualizaEvaluacion(eval.cveEvaluacion, evaluacion);
                        cveMensaje = 1;
                    }
                    catch (Exception Ex2)
                    {
                        Console.WriteLine("Catched Exception: " + Ex2.Message + Environment.NewLine);
                        cveMensaje = 2;
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                cveMensaje = 0;
            }
            Response.Redirect("EvaluaAplicacion.aspx" + "?m=" + cveMensaje + "&a=" + cveAplicacion, false);
        }

        protected void CloseBtn_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "ClosePage", "window.close();", true);
        }
    }
}