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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string sPremioID = Request.QueryString["p"];
                string sCategoriaID = Request.QueryString["c"];
                if (sPremioID != null && sCategoriaID != null)
                {
                    var premio = ConvocatoriaService.GetPremioById(sPremioID);
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
                    foreach (var pregunta in preguntas)
                    {
                        Panel p = new Panel();
                        p.CssClass = "col-lg-12 text-center";
                        
                        LiteralControl h1 = new LiteralControl()
                        Label lbl = new Label();
                        lbl.Text = pregunta.Texto;
                        PanelFormulario.Controls.Add(lbl);

                        
                        TextBox tb = new TextBox();
                        PanelFormulario.Controls.Add(tb);

                        System.Diagnostics.Debug.WriteLine(pregunta.Texto);
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