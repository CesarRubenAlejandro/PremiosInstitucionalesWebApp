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
        int numPregunta = 0;
        string cat = "";
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                string formaID = Request.QueryString["p"];
                var forma = ConvocatoriaService.GetFormaByID(formaID);

                string categoriaID = forma.cveCategoria;
                if (categoriaID != null)
                {
                    var categoria = ConvocatoriaService.GetCategoriaById(categoriaID);
                    if (categoria != null)
                    {
                        cat = categoria.cveCategoria;
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
                
                foreach (var pregunta in listaPreguntas)
                {
                    Panel panel = new Panel();
                    panel.CssClass = "list-group-item";
                    TextBox input = new TextBox();
                    input.Text = pregunta.Texto;
                    input.CssClass = "pregunta form-control";
                    input.Attributes.Add("type", "text");
                    input.Attributes.Add("name", "mytext");
                    input.Attributes.Add("placeholder", "Pregunta");
                    input.Attributes.Add("id", pregunta.cvePregunta);
                    LiteralControl lit = new LiteralControl("<input class='pregunta form-control' type='text' name='mytext' placeholder='Pregunta' value='"+pregunta.Texto+"'/>");
                    LiteralControl remove = new LiteralControl("<a href='#' class='remove'>Eliminar</a>");
                    panel.Controls.Add(lit);
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
                input.Attributes.Add("name", "mytext");
                input.Attributes.Add("placeholder", "Pregunta");
                input.Attributes.Add("id", "pregunta_" + numPregunta);
                LiteralControl remove = new LiteralControl("<a href='#' class='remove'>Eliminar</a>");
                panel.Controls.Add(input);
                panel.Controls.Add(remove);
                simpleList.Controls.Add(panel);

            }
            numPregunta++;

            //<div class="list-group-item"><input class="pregunta form-control" type="text" name="mytext[]" placeholder= "Pregunta"/><a href="#" class="remove">Eliminar</a></div>
            //Guid.NewGuid().ToString();
        }
        protected void Guarda_Formulario(object sender, EventArgs e) {
            string formaID = Request.QueryString["p"];
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
                            string[] values = Request.Form.GetValues("mytext");
                            if (values != null)
                            {
                                var listaPreguntas = AplicacionService.GetFormularioByCategoria(categoria.cveCategoria);
                                var nuevasPreguntasCount = values.Length;
                                var preguntasCount = listaPreguntas.Count;
                                if (listaPreguntas != null)
                                {
                                    if ( nuevasPreguntasCount == preguntasCount) {
                                        for (var i = 0; i < nuevasPreguntasCount; i++) {
                                            if (values[i] != "")
                                                AplicacionService.GuardaPregunta(listaPreguntas[i].cvePregunta, values[i],i);
                                        }
                                    }
                                    else if(nuevasPreguntasCount < preguntasCount)
                                    {
                                        var dif = preguntasCount - nuevasPreguntasCount;
                                        for (var i = 0; i < (preguntasCount-dif); i++)
                                        {
                                            if(values[i]!="")
                                                AplicacionService.GuardaPregunta(listaPreguntas[i].cvePregunta, values[i],i);
                                        }
                                        for( var i=nuevasPreguntasCount; i< preguntasCount; i++)
                                        {
                                            AplicacionService.RemovePregunta(formaID, listaPreguntas[i].cvePregunta);
                                        }
                                    }
                                    else{
                                        var dif = nuevasPreguntasCount - preguntasCount;
                                        for (var i = 0; i < preguntasCount; i++)
                                        {
                                            if (values[i] != "")
                                                AplicacionService.GuardaPregunta(listaPreguntas[i].cvePregunta, values[i], i);
                                        }
                                        for(var i=preguntasCount; i < nuevasPreguntasCount; i++)
                                        {
                                            if (values[i] != "")
                                                AplicacionService.InsertaPregunta(formaID, values[i], i);
                                        }
                                    }

                                }   
                            }
                        }
                    }
                }
            }
            Response.Redirect("AdministraFormulario.aspx?p=" + formaID);
        }


    }
}