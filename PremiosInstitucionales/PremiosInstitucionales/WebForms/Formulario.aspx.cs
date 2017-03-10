using PremiosInstitucionales.DBServices.Aplicacion;
using PremiosInstitucionales.DBServices.Convocatoria;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
                        Response.Redirect("inicioCandidato.aspx");
                    }
                    
                } else
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
                // el candidato actual ya tiene una aplicacion en esta categoria
                // desplegar msj de error
                /*
                ErrorLbl.Text = "Ya se realizó una aplicación a esta categoría.";
                ErrorLbl.Visible = true;
                EnviarBttn.Visible = false;*/
            }
            else
            {
                /*
                // esconder mensaje de error
                ErrorLbl.Visible = false;
                // mostrar boton de enviar
                EnviarBttn.Visible = true;*/
                // vaciar coleccion de preguntas para evitar IDs repetidos

                PanelFormulario.Controls.Clear();

                // obtener lista de preguntas para la categoria y desplegar el formulario
                var preguntas = AplicacionService.GetFormularioByCategoria(categoria.cveCategoria);
                
                if (preguntas != null)
                {
                    short iNumber = 1;
                    foreach (var pregunta in preguntas)
                    {
                        Panel panel = new Panel();
                        panel.CssClass = "question-box";

                        LiteralControl h5 = new LiteralControl("<h5>" + iNumber + ". " + pregunta.Texto + "</h5>");
                        panel.Controls.Add(h5);
                        LiteralControl p = new LiteralControl("<p>" + iMaxCharacters + " " + sCharactersRemainingMessage + "</p>");
                        panel.Controls.Add(p);

                        TextBox tb = new TextBox();
                        tb.TextMode = TextBoxMode.MultiLine;
                        tb.Rows = 4;
                        tb.MaxLength = iMaxCharacters;
                        tb.CssClass = "form-control form-text-area scrollbar-custom";
                        tb.Attributes.Add("onKeyUp", "updateCharactersLeft(this)");
                        tb.Attributes.Add("maxlength", iMaxCharacters.ToString());
                        tb.Attributes.Remove("cols");
                        panel.Controls.Add(tb);

                        PanelFormulario.Controls.Add(panel);

                        iNumber++;
                    }
                }
                /*
                else
                {
                    // esconder control EnviarBttn para evitar incongruencias
                    // EnviarBttn.Visible = false;
                }

            }*/


            }
        }
    }
}