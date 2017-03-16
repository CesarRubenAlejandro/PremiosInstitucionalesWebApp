using PremiosInstitucionales.DBServices.Aplicacion;
using PremiosInstitucionales.DBServices.Convocatoria;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using PremiosInstitucionales.Entities.Models;
using PremiosInstitucionales.Values;

namespace PremiosInstitucionales.WebForms
{
    public partial class CorrigeAplicacion : System.Web.UI.Page
    {
        private int iMaxCharacters = NumericValues.iMaxCharactersPerAnswer;
        private string sCharactersRemainingMessage = StringValues.sCharactersRemaining;
        protected void Page_Load(object sender, EventArgs e)
        {
            // confirmar que la aplicacion haya sido rechazada
            String idApp = Request.QueryString["aplicacion"];

            String sCategoriaID = AplicacionService.GetCveCategoriaByAplicacion(idApp);

            if (sCategoriaID != null)
            {
                var premio = ConvocatoriaService.GetPremioByCategoria(sCategoriaID);
                var categoria = ConvocatoriaService.GetCategoriaById(sCategoriaID);

                if (premio != null && categoria != null)
                {
                    if (AplicacionService.GetEsRechazadoByAplicacion(idApp))
                    {
                        CrearFormulario(sCategoriaID, premio, categoria);
                    }
                    else
                    {
                        Response.Redirect("inicioCandidato.aspx");
                    }
                }
                else
                {
                    Response.Redirect("inicioCandidato.aspx");
                }

            }
            else
            {
                Response.Redirect("inicioCandidato.aspx");
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
                    panel.Attributes.Add("runat", "server");

                    LiteralControl h5 = new LiteralControl("<h5>" + (iNumber + 1) + ". " + pregunta.Texto + "</h5>");
                    panel.Controls.Add(h5);
                    LiteralControl p = new LiteralControl("<p>" + iMaxCharacters + " " + sCharactersRemainingMessage + "</p>");
                    panel.Controls.Add(p);

                    TextBox tb = new TextBox();
                    tb.ID = "textbox_" + pregunta.cvePregunta;
                    tb.TextMode = TextBoxMode.MultiLine;
                    tb.Rows = 4;
                    tb.MaxLength = iMaxCharacters;
                    tb.CssClass = "form-control form-text-area scrollbar-custom";
                    tb.Attributes.Add("onKeyUp", "updateCharactersLeft(this)");
                    tb.Attributes.Add("maxlength", iMaxCharacters.ToString());
                    tb.Attributes.Add("runat", "server");
                    tb.Attributes.Add("onvalid", "this.setCustomValidity('Por favor, responde la pregunta')");
                    tb.Attributes.Remove("cols");

                    var respuesta = AplicacionService.GetRespuestaByPreguntaAndAplicacion(pregunta.cvePregunta, Request.QueryString["aplicacion"]);
                    tb.Text = respuesta.Valor;

                    RequiredFieldValidator validator = new RequiredFieldValidator();
                    validator.ControlToValidate = tb.ID;

                    Panel pAlert = new Panel();
                    pAlert.CssClass = "alert alert-danger alert-no-answer";

                    LiteralControl lcText = new LiteralControl("<strong>Error:</strong> Por favor rellene este campo.");
                    pAlert.Controls.Add(lcText);

                    validator.Controls.Add(pAlert);

                    panel.Controls.Add(tb);
                    panel.Controls.Add(validator);

                    PanelFormulario.Controls.Add(panel);

                    iNumber++;
                }
            }

        }

        protected void EnviarBttn_Click(object sender, EventArgs e)
        {
            // modificar el texto de las respuestas
            // obtener string con controles en Request
            string[] ctrls = Request.Form.ToString().Split('&');
            // obtener la lista de preguntas para la categoria y obtener los controles en base a su ID
            var preguntas = AplicacionService.GetFormularioByCategoria(AplicacionService.GetCveCategoriaByAplicacion(Request.QueryString["aplicacion"]));
            foreach (var pregunta in preguntas)
            {
                int iIndex = GetIndexFromArray(ctrls, "textbox_" + pregunta.cvePregunta);

                if (iIndex > -1)
                {
                    // obtener el valor del control
                    string ctrlValue = ctrls[iIndex].Split('=')[1];
                    //Decode the Value
                    ctrlValue = Server.UrlDecode(ctrlValue);
                    // guardar cambios en la respuesta
                    AplicacionService.SaveRespuestaModificada(Request.QueryString["aplicacion"], pregunta.cvePregunta, ctrlValue);
                }
            }

            // cambiar status de la aplicacion a Modificado
            AplicacionService.SetAplicacionModificada(Request.QueryString["aplicacion"]);
            // redireccionar a inicio
            Response.Redirect("InicioCandidato.aspx");
        }

        private int GetIndexFromArray(String[] arr, String value)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                string sRow = arr[i];
                if (sRow.Contains(value))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}