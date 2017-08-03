using PremiosInstitucionales.DBServices.Aplicacion;
using PremiosInstitucionales.DBServices.Convocatoria;
using PremiosInstitucionales.Entities.Models;
using PremiosInstitucionales.Values;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web;

namespace PremiosInstitucionales.WebForms
{
    public partial class Formulario : System.Web.UI.Page
    {

        private int iMaxCharacters = NumericValues.iMaxCharactersPerAnswer;
        private string sCharactersRemainingMessage = StringValues.sCharactersRemaining;
        MP_Global MasterPage = new MP_Global();

        protected void Page_Load(object sender, EventArgs e)
        {
            MasterPage = (MP_Global)Page.Master;
            if (!IsPostBack)
            {
                // revisar la primera vez que se carga la pagina que se haya iniciado sesion con cuenta de candidato
                if (Session[StringValues.RolSesion] != null)
                {
                    if (Session[StringValues.RolSesion].ToString() != StringValues.RolCandidato)
                        // si no es candidato, redireccionar a login
                        Response.Redirect("~/WebForms/Login.aspx", false);
                }
                else
                {
                    Response.Redirect("~/WebForms/Login.aspx", false);
                }

                string sCategoriaID = Request.QueryString["c"];
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
                        Response.Redirect("inicioCandidato.aspx", false);
                    }

                }
                else
                {
                    Response.Redirect("inicioCandidato.aspx", false);
                }
            }
        }

        private void SetForm(PI_BA_Premio premio, PI_BA_Categoria categoria)
        {
            litTituloPremio.Text = "Premio " + premio.Nombre;
            litTituloCategoria.Text = "Categoría: " + categoria.Nombre;

            String emailCandidato = Session[StringValues.CorreoSesion].ToString();
            if (AplicacionService.CheckCandidatoInCategoria(emailCandidato, categoria.cveCategoria))
            {
                // mostrar error y ocultar boton de enviar
                alreadySubmittedLabel.Visible = true;
                EnviarBtn.Visible = false;

                btnManager.Visible = false;
            }
            else
            {
                // vaciar coleccion de preguntas para evitar IDs repetidos
                PanelFormulario.Controls.Clear();

                // obtener lista de preguntas para la categoria y desplegar el formulario
                var preguntas = AplicacionService.GetFormularioByCategoria(categoria.cveCategoria);

                if (preguntas != null && preguntas.Count > 0)
                {
                    uploadFile.Visible = true;
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
                        tb.Attributes.Add("onKeyUp", "updateCharactersLeft(this); validateAnswerCharacters(event);");
                        tb.Attributes.Add("maxlength", iMaxCharacters.ToString());
                        tb.Attributes.Remove("cols");
                        tb.Attributes.Add("runat", "server");
                        tb.Attributes.Add("onvalid", "this.setCustomValidity('Por favor, responde la pregunta')");

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
                aplicacionNueva.NombreArchivo = UploadFile();
                AplicacionService.CrearAplicacion(aplicacionNueva, respuestas);
                Response.Redirect("AplicacionesCandidato.aspx?r=true", false);
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

        protected string UploadFile()
        {
            HttpPostedFile file = Request.Files["file"];

            //check file was submitted
            if (file != null && file.ContentLength > 0)
            {
                string fname = Path.GetFileName(file.FileName);

                // Get string image format (png, jpg, etc)
                var startIndex = fname.LastIndexOf(".");
                var endIndex = fname.Length - startIndex;
                string sFormat = fname.Substring(startIndex, endIndex);
                string sName = fname.Substring(0, fname.Length - sFormat.Length);
                string sNombreArchivo = sName + new Random().Next(10000, 99999) + sFormat;

                // Upload image to server
                file.SaveAs(Server.MapPath(Path.Combine("~/UsersAppsFiles/", sNombreArchivo)));
                return sNombreArchivo;
            }

            return null;
        }
    }
}