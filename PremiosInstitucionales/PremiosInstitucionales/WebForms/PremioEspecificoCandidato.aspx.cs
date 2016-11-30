using PremiosInstitucionales.DBServices.Aplicacion;
using PremiosInstitucionales.Entities.Models;
using PremiosInstitucionales.Values;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PremiosInstitucionales.WebForms
{
    public partial class PremioEspecificoCandidato : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // revisar la primera vez que se carga la pagina que se haya iniciado sesion con cuenta de admin
                if (Session[StringValues.RolSesion] != null)
                {
                    if (Session[StringValues.RolSesion].ToString() != StringValues.RolCandidato)
                        // si no es candidato, redireccionar a login
                        Response.Redirect("Login.aspx");
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
                // obtener lista de categorias con el id del premio
                String idPremio = Request.QueryString["premio"];
                var categorias = AplicacionService.GetCategoriasByPremio(idPremio);
                if (categorias != null)
                {
                    // asignar el datasource al DropDown de categorias
                    CategoriasDDL.DataSource = categorias;
                    CategoriasDDL.DataTextField = "Nombre";
                    CategoriasDDL.DataValueField = "cveCategoria";
                    CategoriasDDL.DataBind();

                    // desplegar formulario para categoria seleccionada por default
                    CrearFormulario();
                }
                else
                {
                    // desplegar mensaje de error
                    ErrorLbl.Text = "No hay convocatorias abiertas por el momento o no se ha definido un formulario.";
                    ErrorLbl.Visible = true;
                    EnviarBttn.Visible = false;
                }
            }
            
        }

        protected void CategoriasDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            CrearFormulario();
        }

        private void CrearFormulario()
        {
            String emailCandidato = Session[StringValues.CorreoSesion].ToString();
            if (AplicacionService.CheckCandidatoInCategoria(emailCandidato, CategoriasDDL.SelectedValue.ToString())) {
                // el candidato actual ya tiene una aplicacion en esta categoria
                // desplegar msj de error
                ErrorLbl.Text = "Ya se realizó una aplicación a esta categoría.";
                ErrorLbl.Visible = true;
                EnviarBttn.Visible = false;
            } else
            {
                // esconder mensaje de error
                ErrorLbl.Visible = false;
                // mostrar boton de enviar
                EnviarBttn.Visible = true;
                // vaciar coleccion de preguntas para evitar IDs repetidos
                if (PanelFormulario.Controls.Count > 0)
                {
                    PanelFormulario.Controls.Clear();
                }


                // obtener lista de preguntas para la categoria y desplegar el formulario
                var preguntas = AplicacionService.GetFormularioByCategoria(CategoriasDDL.SelectedValue.ToString());
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

            
        }

        protected void EnviarBttn_Click(object sender, EventArgs e)
        {
            // crear el objeto aplicacion
            PI_BA_Aplicacion aplicacionNueva = new PI_BA_Aplicacion();
            aplicacionNueva.cveAplicacion = Guid.NewGuid().ToString();
            aplicacionNueva.Status = StringValues.Solicitado;
            aplicacionNueva.cveCandidato = AplicacionService.GetCveCandidatoByCorreo(Session[StringValues.CorreoSesion].ToString());
            aplicacionNueva.cveCategoria = CategoriasDDL.SelectedValue.ToString();

            List<PI_BA_Respuesta> respuestas = new List<PI_BA_Respuesta>();

            // obtener string con controles en Request
            string[] ctrls = Request.Form.ToString().Split('&');
            // obtener la lista de preguntas para la categoria y obtener los controles en base a su ID
            var preguntas = AplicacionService.GetFormularioByCategoria(CategoriasDDL.SelectedValue.ToString());
            foreach(var pregunta in preguntas)
            {
                for (int i = 0; i < ctrls.Length; i++)
                {
                    if (ctrls[i].Contains(pregunta.IdentificadorObjeto))
                    {
                        // obtener el valor del control
                        string ctrlValue = ctrls[i].Split('=')[1];
                        //Decode the Value
                        ctrlValue = Server.UrlDecode(ctrlValue);
                        // crear un objeto respuesta y guardarlo en la lista
                        PI_BA_Respuesta respActual = new PI_BA_Respuesta();
                        respActual.cveRespuesta = Guid.NewGuid().ToString();
                        respActual.cvePregunta = pregunta.cvePregunta;
                        respActual.cveAplicacion = aplicacionNueva.cveAplicacion;
                        respActual.Valor = ctrlValue;
                        respuestas.Add(respActual);
                    }
                }
            }
            // crear la aplicacion nueva
            AplicacionService.CrearAplicacion(aplicacionNueva, respuestas);
            Response.Redirect("InicioCandidato.aspx");
        }

// endclass
    }
}