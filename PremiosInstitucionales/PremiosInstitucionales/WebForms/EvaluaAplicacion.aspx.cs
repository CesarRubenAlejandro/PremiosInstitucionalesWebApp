using PremiosInstitucionales.DBServices.Aplicacion;
using PremiosInstitucionales.DBServices.Convocatoria;
using PremiosInstitucionales.DBServices.Evaluacion;
using PremiosInstitucionales.DBServices.InformacionPersonalJuez;
using PremiosInstitucionales.Entities.Models;
using PremiosInstitucionales.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PremiosInstitucionales.WebForms
{
    public partial class EvaluaAplicacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // confirmar que la aplicacion haya sido rechazada
                String idApp = Request.QueryString["a"];

                if (idApp != null)
                {
                    String sCategoriaID = AplicacionService.GetCveCategoriaByAplicacion(idApp);
                    if (sCategoriaID != null)
                    {
                        var premio = ConvocatoriaService.GetPremioByCategoria(sCategoriaID);
                        var categoria = ConvocatoriaService.GetCategoriaById(sCategoriaID);

                        if (premio != null && categoria != null)
                        {
                            string sMail = Session[StringValues.CorreoSesion].ToString();
                            var listaCategorias = EvaluacionService.GetCategoriaByJuez(sMail);
                            bool bValidJudge = CheckValidCategory(listaCategorias, sCategoriaID);
                            var eval = CheckExistenceOfEvaluation(sMail, idApp);
                            if (bValidJudge)
                            {
                                if (eval != null)
                                {
                                    evaluateApplicationBtn.Visible = false;
                                    modifiyEvaluationBtn.Visible = true;
                                    aplicationEvaluationNumber.Text = eval.Calificacion.ToString();
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
                Response.Redirect("inicioJuez.aspx");
            }
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

        private PI_BA_Evaluacion CheckExistenceOfEvaluation(string sMail, string sAppId)
        {
            var eval = EvaluacionService.GetEvaluacionByAplicacionAndJuez(sMail, sAppId);
            return eval;
        }

        protected void EvaluarAplicacion(object sender, EventArgs e)
        {
            try
            {
                PI_BA_Evaluacion ev = new PI_BA_Evaluacion();
                ev.cveEvaluacion = Guid.NewGuid().ToString();
                ev.cveAplicacion = Request.QueryString["a"];
                ev.cveJuez = InformacionPersonalJuezService.GetJuezByCorreo(Session[StringValues.CorreoSesion].ToString()).cveJuez;
                ev.Calificacion = short.Parse(aplicationEvaluationNumber.Text);

                EvaluacionService.CrearEvaluacion(ev);
                Response.Redirect("inicioJuez.aspx");
            }
            catch (Exception ex)
            {
                Response.Redirect("inicioJuez.aspx");
            }

        }

        protected void ModificarAplicacion(object sender, EventArgs e)
        {
            try
            {
                var aplicacion = Request.QueryString["a"];
                var juez = InformacionPersonalJuezService.GetJuezByCorreo(Session[StringValues.CorreoSesion].ToString());

                var eval = EvaluacionService.GetEvaluacionByAplicacionAndJuez(juez.Correo, aplicacion);
                if(eval != null)
                {
                    try
                    {
                        EvaluacionService.ActualizaEvaluacion(eval.cveEvaluacion, short.Parse(aplicationEvaluationNumber.Text));
                    }
                    catch (Exception exception)
                    {

                    }
                }

                Response.Redirect("inicioJuez.aspx");
            }
            catch (Exception ex)
            {
                Response.Redirect("inicioJuez.aspx");
            }

        }
    }
}