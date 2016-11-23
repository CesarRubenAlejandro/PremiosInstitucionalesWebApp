using PremiosInstitucionales.DBServices.Aplicacion;
using PremiosInstitucionales.DBServices.Convocatoria;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PremiosInstitucionales.WebForms
{
    public partial class CorrigeAplicacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // confirmar que la aplicacion haya sido rechazada
            String idApp = Request.QueryString["aplicacion"];
            if (AplicacionService.GetEsRechazadoByAplicacion(idApp))
            {
                CrearFormulario();
            } else
            {
                ErrorLbl.Visible = true;
            }        
        }

        private void CrearFormulario()
        {
           
            // esconder mensaje de error
            ErrorLbl.Visible = false;
            // mostrar boton
            EnviarBttn.Visible = true;
            // vaciar coleccion de preguntas para evitar IDs repetidos
            if (PanelFormulario.Controls.Count > 0)
            {
                PanelFormulario.Controls.Clear();
            }

            // obtener lista de preguntas y respuestas para la aplicacion
            var preguntas = AplicacionService.GetFormularioByCategoria(AplicacionService.GetCveCategoriaByAplicacion(Request.QueryString["aplicacion"]));
            if (preguntas != null)
            {
                foreach (var pregunta in preguntas)
                {
                    // crear lbl con el texto de la pregunta
                    Label lbl = new Label();
                    lbl.Text = pregunta.Texto;
                    PanelFormulario.Controls.Add(lbl);

                    PanelFormulario.Controls.Add(new LiteralControl("<br />"));

                    TextBox tb = new TextBox();
                    tb.ID = pregunta.IdentificadorObjeto;
                    // obtener respuesta y asignar el valor al textbox
                    var respuesta = AplicacionService.GetRespuestaByPreguntaAndAplicacion(pregunta.cvePregunta, Request.QueryString["aplicacion"]);
                    tb.Text = respuesta.Valor;

                    RequiredFieldValidator validator = new RequiredFieldValidator();
                    validator.ControlToValidate = pregunta.IdentificadorObjeto;
                    validator.ErrorMessage = "Campo requerido";
                    validator.ForeColor = System.Drawing.Color.Red;

                    PanelFormulario.Controls.Add(tb);
                    PanelFormulario.Controls.Add(validator);
                    PanelFormulario.Controls.Add(new LiteralControl("<br />"));
                    PanelFormulario.Controls.Add(new LiteralControl("<br />"));
                }
            }
            else
            {
                // esconder control EnviarBttn para evitar incongruencias
                EnviarBttn.Visible = false;
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
                for (int i = 0; i < ctrls.Length; i++)
                {
                    if (ctrls[i].Contains(pregunta.IdentificadorObjeto))
                    {
                        // obtener el valor del control
                        string ctrlValue = ctrls[i].Split('=')[1];
                        //Decode the Value
                        ctrlValue = Server.UrlDecode(ctrlValue);
                        // guardar cambios en la respuesta
                        AplicacionService.SaveRespuestaModificada(Request.QueryString["aplicacion"], pregunta.cvePregunta, ctrlValue);
                    }
                }
            }

            // cambiar status de la aplicacion a Modificado
            AplicacionService.SetAplicacionModificada(Request.QueryString["aplicacion"]);
            // redireccionar a inicio
            Response.Redirect("InicioCandidato.aspx");
        }
    }
}