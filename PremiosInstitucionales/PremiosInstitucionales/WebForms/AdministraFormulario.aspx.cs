using PremiosInstitucionales.DBServices.Aplicacion;
using PremiosInstitucionales.DBServices.Convocatoria;
using PremiosInstitucionales.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PremiosInstitucionales.WebForms
{
    public partial class AdministraFormulario : System.Web.UI.Page
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                string formaID = Request.QueryString["formaID"];
                var forma = ConvocatoriaService.GetFormaByID(formaID);

                string categoriaID = forma.cveCategoria;
                if (categoriaID != null)
                {
                    var categoria = ConvocatoriaService.GetCategoriaById(categoriaID);
                    if (categoria != null)
                    {
                        var convocatoria = ConvocatoriaService.GetConvocatoriaById(categoria.cveConvocatoria);
                        if (convocatoria != null)
                        {
                            var premio = ConvocatoriaService.GetPremioById(convocatoria.cvePremio);

                            if (premio != null)
                            {
                                LoadInfo(formaID, premio, convocatoria,categoria,forma);
                                return;
                            }
                        }
                    }
                }
                Response.Redirect("inicioAdmin.aspx");
            }
        }

        protected void LoadInfo(string formaID, PI_BA_Premio premio, PI_BA_Convocatoria convocatoria, PI_BA_Categoria categoria, PI_BA_Forma forma) {
            nombrePremio.InnerText = premio.Nombre;
            //nombreConvocatoria.InnerText = convocatoria.Nombre;   SE ESPERA A QUE SE IMPLEMENTA EN LA DB
            nombreCategoria.InnerText = categoria.Nombre;

            var listaPreguntas = AplicacionService.GetFormularioByCategoria(categoria.cveCategoria);
            if (listaPreguntas != null)
            {
                int numPregunta = 0;
                foreach (var pregunta in listaPreguntas)
                {
                    Panel panel = new Panel();
                    panel.CssClass = "list-group-item";
                    TextBox input = new TextBox();
                    input.Text = pregunta.Texto;
                    input.CssClass = "pregunta form-control";
                    input.Attributes.Add("type", "text");
                    input.Attributes.Add("name", "mytext[]");
                    input.Attributes.Add("placeholder", "Pregunta");
                    LiteralControl remove = new LiteralControl("<a href='#' class='remove'>Eliminar</a>");
                    panel.Controls.Add(input);
                    panel.Controls.Add(remove);
                    simpleList.Controls.Add(panel);
                }
            }
            else {
                Panel panel = new Panel();
                panel.CssClass = "list-group-item";
                TextBox input = new TextBox();
                input.CssClass = "pregunta form-control";
                input.Attributes.Add("type", "text");
                input.Attributes.Add("name", "mytext[]");
                input.Attributes.Add("placeholder", "Pregunta");
                LiteralControl remove = new LiteralControl("<a href='#' class='remove'>Eliminar</a>");
                panel.Controls.Add(input);
                panel.Controls.Add(remove);
                simpleList.Controls.Add(panel);
            }

            //<div class="list-group-item"><input class="pregunta form-control" type="text" name="mytext[]" placeholder= "Pregunta"/><a href="#" class="remove">Eliminar</a></div>
        }




    }
}