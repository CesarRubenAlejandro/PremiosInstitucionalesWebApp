using PremiosInstitucionales.DBServices.Aplicacion;
using PremiosInstitucionales.DBServices.Convocatoria;
using PremiosInstitucionales.Entities.Models;
using PremiosInstitucionales.Values;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PremiosInstitucionales.WebForms
{
    public partial class Formulario : System.Web.UI.Page
    {

        private int iMaxCharacters = 500;
        private string sCharactersRemainingMessage = "caracteres restantes";
        private string sCategoriaID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                sCategoriaID = Request.QueryString["c"];
                if (sCategoriaID != null)
                {
                    var premio = ConvocatoriaService.GetPremioByCategoria(sCategoriaID);
                    var categoria = ConvocatoriaService.GetCategoriaById(sCategoriaID);

                    if (premio != null && categoria != null)
                    {
                        SetForm(premio, categoria);
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
        }

        private void SetForm(Entities.Models.PI_BA_Premio premio, Entities.Models.PI_BA_Categoria categoria)
        {
            litTituloPremio.Text = "Premio " + premio.Nombre;
            litTituloCategoria.Text = "Categoría: " + categoria.Nombre;

            String emailCandidato = Session[StringValues.CorreoSesion].ToString();
            if (AplicacionService.CheckCandidatoInCategoria(emailCandidato, categoria.cveCategoria))
            {
                // mostrar error y ocultar boton de enviar
                alreadySubmittedLabel.Visible = true;
                EnviarBttn.Visible = false;
            }
            else
            {
                // vaciar coleccion de preguntas para evitar IDs repetidos
                PanelFormulario.Controls.Clear();

                // obtener lista de preguntas para la categoria y desplegar el formulario
                var preguntas = AplicacionService.GetFormularioByCategoria(categoria.cveCategoria);

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
                        tb.Attributes.Remove("cols");
                        tb.Attributes.Add("runat", "server");
                        panel.Controls.Add(tb);

                        PanelFormulario.Controls.Add(panel);

                        iNumber++;
                    }
                }
            }
        }

        protected void EnviarAplicacion(object sender, EventArgs e)
        {
            PI_BA_Aplicacion aplicacionNueva = new PI_BA_Aplicacion();
            aplicacionNueva.cveAplicacion = Guid.NewGuid().ToString();
            aplicacionNueva.Status = StringValues.Solicitado;
            aplicacionNueva.cveCandidato = AplicacionService.GetCveCandidatoByCorreo(Session[StringValues.CorreoSesion].ToString());
            aplicacionNueva.cveCategoria = Request.QueryString["c"];

            List<PI_BA_Respuesta> respuestas = new List<PI_BA_Respuesta>();
            List<string> ltRespuestas = new List<string>();
            string[] ctrls = Request.Form.ToString().Split('&');
            var preguntas = AplicacionService.GetFormularioByCategoria(aplicacionNueva.cveCategoria);

            System.Diagnostics.Debug.Print("debug");
            System.Diagnostics.Debug.Print(ctrls.ToString());
            for (int i = 0; i < preguntas.Count; i++)
            {
                PI_BA_Pregunta pregunta = preguntas[i];
                int iIndex = GetIndexFromArray(ctrls, "textbox_" + pregunta.cvePregunta);
                System.Diagnostics.Debug.Print(pregunta.cvePregunta + " " + iIndex);
                if (iIndex > -1)
                {
                    String sRespuesta = ctrls[iIndex].Split('=')[1];
                    PI_BA_Respuesta respActual = new PI_BA_Respuesta();
                    respActual.cveRespuesta = Guid.NewGuid().ToString();
                    respActual.cvePregunta = pregunta.cvePregunta;
                    respActual.cveAplicacion = aplicacionNueva.cveAplicacion;
                    respActual.Valor = Server.UrlDecode(sRespuesta);
                    respuestas.Add(respActual);

                    ltRespuestas.Add(sRespuesta);
                }
            }

            if (ltRespuestas.Count == preguntas.Count)
            {
                AplicacionService.CrearAplicacion(aplicacionNueva, respuestas);
                Response.Redirect("InicioCandidato.aspx");
            }
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