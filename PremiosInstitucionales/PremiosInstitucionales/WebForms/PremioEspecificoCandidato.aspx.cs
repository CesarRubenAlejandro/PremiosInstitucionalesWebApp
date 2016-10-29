using PremiosInstitucionales.DBServices.Aplicacion;
using PremiosInstitucionales.Values;
using System;
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
            } else
            {
                // desplegar mensaje de error
                ErrorLbl.Text = "No hay convocatorias abiertas por el momento";
                ErrorLbl.Visible = true;
            }
        }

        protected void CategoriasDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            CrearFormulario();
        }

        private void CrearFormulario()
        {
            // obtener lista de preguntas para la categoria y desplegar el formulario
            var preguntas = AplicacionService.GetFormularioByCategoria(CategoriasDDL.SelectedValue.ToString());
            foreach(var pregunta in preguntas)
            {
                // crear lbl con el texto de la pregunta
                Label lbl = new Label();
                lbl.Text = pregunta.Texto;
                PanelFormulario.Controls.Add(lbl);

                PanelFormulario.Controls.Add(new LiteralControl("<br />"));

                TextBox tb = new TextBox();
                tb.ID = pregunta.IdentificadorObjeto;
                PanelFormulario.Controls.Add(tb);

                PanelFormulario.Controls.Add(new LiteralControl("<br />"));
                PanelFormulario.Controls.Add(new LiteralControl("<br />"));
            }
        }
    }
}