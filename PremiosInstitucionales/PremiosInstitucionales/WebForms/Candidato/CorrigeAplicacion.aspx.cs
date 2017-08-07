using PremiosInstitucionales.DBServices.Aplicacion;
using PremiosInstitucionales.DBServices.Convocatoria;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using PremiosInstitucionales.Entities.Models;
using PremiosInstitucionales.Values;
using System.IO;
using System.Web;
using System.Collections.Generic;

namespace PremiosInstitucionales.WebForms
{
    public partial class CorrigeAplicacion : System.Web.UI.Page
    {
        MP_Global MasterPage = new MP_Global();
        private int iMaxCharacters = NumericValues.iMaxCharactersPerAnswer;
        private string sCharactersRemainingMessage = StringValues.sCharactersRemaining;
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
                            return;
                        }
                    }
                }
                Response.Redirect("inicioCandidato.aspx", false);
            }
            
        }

        private void CrearFormulario(String sCategoriaID, PI_BA_Premio premio, PI_BA_Categoria categoria)
        {
            // vaciar coleccion de preguntas para evitar IDs repetidos
            PanelFormulario.Controls.Clear();

            litTituloPremio.Text = "Premio " + premio.Nombre;
            litTituloCategoria.Text = "Categoría: " + categoria.Nombre;

            string idApp = Request.QueryString["aplicacion"];
            string FileName = AplicacionService.GetAplicacionById(idApp).NombreArchivo;
            if (FileName != null)
            {
                ClientScript.RegisterStartupScript(GetType(), "hwa", "writeName('"+FileName+"');", true);
            }

            // obtener lista de preguntas y respuestas para la aplicacion
            var preguntas = AplicacionService.GetFormularioByCategoria(sCategoriaID);

            if (preguntas != null)
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
                    tb.Attributes.Add("runat", "server");
                    tb.Attributes.Add("onvalid", "this.setCustomValidity('Por favor, responde la pregunta')");
                    tb.Attributes.Remove("cols");

                    var respuesta = AplicacionService.GetRespuestaByPreguntaAndAplicacion(pregunta.cvePregunta, Request.QueryString["aplicacion"]);
                    if (respuesta != null)
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
            // actualizar archivo
            string sArchivo = UploadFile();
            if (sArchivo != null)
            {
                if (sArchivo != "Error")
                {
                    AplicacionService.UpdateAplicacionArchivo(Request.QueryString["aplicacion"], sArchivo);
                }
                else
                {
                    return;
                }
            }

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
            Response.Redirect("AplicacionesCandidato.aspx?r=true", false);
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

                // Formatos Validos
                List<String> supportedFormats = new List<String>()
                {
                    ".png",
                    ".jpg",
                    ".txt",
                    ".doc",
                    ".docx",
                    ".pdf",
                    ".xlsx",
                    ".xls"
                };

                if (!supportedFormats.Contains(sFormat))
                {
                    MasterPage.ShowMessage("Error", "El archivo proporcionado debe ser un archivo de texto, una hoja de cálculo o un imagen.");
                    return "Error";
                }

                // Delete previous image...
                string idApp = Request.QueryString["aplicacion"];
                string FileName = AplicacionService.GetAplicacionById(idApp).NombreArchivo;
                if (FileName != null && FileName.Length > 0)
                {
                    File.Delete(Server.MapPath("~/UsersAppsFiles/") + FileName);
                }

                // Upload image to server
                file.SaveAs(Server.MapPath(Path.Combine("~/UsersAppsFiles/", sNombreArchivo)));
                return sNombreArchivo;
            }

            return null;
        }
    }
}