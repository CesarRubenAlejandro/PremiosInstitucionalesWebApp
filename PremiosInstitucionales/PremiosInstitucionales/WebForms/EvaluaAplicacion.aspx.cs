using PremiosInstitucionales.DBServices.Aplicacion;
using PremiosInstitucionales.DBServices.Convocatoria;
using PremiosInstitucionales.DBServices.Evaluacion;
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

                if(idApp != null)
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

                            if (bValidJudge)
                            {
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
            // vaciar coleccion de preguntas para evitar IDs repetidos
            PanelFormulario.Controls.Clear();

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
                    for(int i = 0; i < lines.Length; i++)
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
    }
}