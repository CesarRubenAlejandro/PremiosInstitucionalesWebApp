using PremiosInstitucionales.DBServices.InformacionPersonalJuez;
using PremiosInstitucionales.DBServices.Aplicacion;
using PremiosInstitucionales.DBServices.Convocatoria;
using PremiosInstitucionales.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using PremiosInstitucionales.Values;

namespace PremiosInstitucionales.WebForms
{
    public partial class AdministraFormulario : System.Web.UI.Page
    {
        int numPregunta = 0;
        String idCategoria;
        String formaID;
        TextBox tbCategoria;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                // revisar la primera vez que se carga la pagina que se haya iniciado sesion con cuenta de admin
                if (Session[StringValues.RolSesion] != null)
                {
                    if (Session[StringValues.RolSesion].ToString() != StringValues.RolAdmin)
                        // si no es admin, redireccionar a inicio general
                        Response.Redirect("~/WebForms/Login.aspx");
                }
                else
                {
                    Response.Redirect("~/WebForms/Login.aspx");
                }
            }

            // Obtener Ids
            formaID = Request.QueryString["p"];
            var forma = ConvocatoriaService.GetFormaByID(formaID);
            idCategoria = forma.cveCategoria;
            String nombrePremio = AplicacionService.GetPremioByClaveCategoria(idCategoria).Nombre;
            String nombreCategoria = AplicacionService.GetCategoriaByClaveCategoria(idCategoria).Nombre;

            // Nombre de Titulos
            nombrePremioCategoria.Controls.Add(new LiteralControl(
                "<h3> <strong> Premio: </strong>" + nombrePremio + "</h3>" +
                "<h4 style=\"display: inline;\"> <strong> Categoria: </strong> </h4>"
            ));

            tbCategoria = new TextBox();
            tbCategoria.CssClass = "form-control";
            tbCategoria.Style.Add("display", "inline-block");
            tbCategoria.Style.Add("font-size", "1.75em");
            tbCategoria.Style.Add("width", "225px");
            tbCategoria.Attributes.Add("type", "text");
            tbCategoria.Text = nombreCategoria;
            nombrePremioCategoria.Controls.Add(tbCategoria);

            LoadJudgeTable();

            if (!IsPostBack)
            {
                if (idCategoria != null)
                {
                    var categoria = ConvocatoriaService.GetCategoriaById(idCategoria);
                    if (categoria != null)
                    {
                        idCategoria = categoria.cveCategoria;
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

        private void LoadJudgeTable()
        {
            var jueces = InformacionPersonalJuezService.GetJueces();
            string sType = Request.QueryString["t"];
            if (jueces != null)
            {
                foreach (var juez in jueces)
                {
                    TableRow tr = new TableRow();

                    // profile image column
                    TableCell tdIP = new TableCell();
                    tdIP.CssClass = "dt-profile-pic";

                    Image ipImage = new Image();
                    if (juez.NombreImagen != null)
                    {
                        ipImage.ImageUrl = "/ProfilePictures/" + juez.NombreImagen;
                    }
                    else
                    {
                        ipImage.ImageUrl = "/Resources/img/default-pp.jpg";
                    }
                    ipImage.CssClass = "avatar img-circle";
                    ipImage.AlternateText = "avatar";
                    ipImage.Style.Add("width", "28px");
                    ipImage.Style.Add("height", "28px");

                    tdIP.Controls.Add(ipImage);

                    // name column
                    TableCell tdName = new TableCell();
                    tdName.Text = juez.Nombre;

                    // last name column
                    TableCell tdLastName = new TableCell();
                    tdLastName.Text = juez.Apellido;

                    TableCell tdEmail = new TableCell();

                    LiteralControl lHiddenValue = new LiteralControl("<span id=\"" + juez.cveJuez + "\">" + juez.Correo + "</span>");
                    tdEmail.Controls.Add(lHiddenValue);

                    tr.Controls.Add(tdIP);
                    tr.Controls.Add(tdName);
                    tr.Controls.Add(tdLastName);
                    tr.Controls.Add(tdEmail);
                    if (!AplicacionService.GetJuecesIdsCategoria(idCategoria).Contains(juez.cveJuez))
                    {
                        listaJuecesTableBody.Controls.Add(tr);
                    }
                    else
                    {
                        listaJuezTableAsignadosBody.Controls.Add(tr);
                    }
                }
            }
        }

        protected void Guarda_Formulario()
        {
            formaID = Request.QueryString["p"];
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
                                    if (nuevasPreguntasCount == preguntasCount)
                                    {
                                        for (var i = 0; i < nuevasPreguntasCount; i++)
                                        {
                                            if (values[i] != "")
                                                AplicacionService.GuardaPregunta(listaPreguntas[i].cvePregunta, values[i], i);
                                        }
                                    }
                                    else if (nuevasPreguntasCount < preguntasCount)
                                    {
                                        var dif = preguntasCount - nuevasPreguntasCount;
                                        for (var i = 0; i < (preguntasCount - dif); i++)
                                        {
                                            if (values[i] != "")
                                                AplicacionService.GuardaPregunta(listaPreguntas[i].cvePregunta, values[i], i);
                                        }
                                        for (var i = nuevasPreguntasCount; i < preguntasCount; i++)
                                        {
                                            AplicacionService.RemovePregunta(formaID, listaPreguntas[i].cvePregunta);
                                        }
                                    }
                                    else
                                    {
                                        var dif = nuevasPreguntasCount - preguntasCount;
                                        for (var i = 0; i < preguntasCount; i++)
                                        {
                                            if (values[i] != "")
                                                AplicacionService.GuardaPregunta(listaPreguntas[i].cvePregunta, values[i], i);
                                        }
                                        for (var i = preguntasCount; i < nuevasPreguntasCount; i++)
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
        }

        protected void Guarda_Jueces()
        {
            List<String> idsJuecesAsigandos = hiddenControl.Value.Split(',').ToList();
            AplicacionService.RemoveJuezCategoria(idCategoria);

            if (idsJuecesAsigandos.Count > 0 && idsJuecesAsigandos != null)
            {
                AplicacionService.AsignarJuecesCategoria(idCategoria, idsJuecesAsigandos);
            }
        }

        protected void SaveChanges(object sender, EventArgs e)
        {
            formaID = Request.QueryString["p"];
            Guarda_Jueces();
            Guarda_Formulario();
            AplicacionService.CambiarNombreCategoria(idCategoria, tbCategoria.Text);
            Response.Redirect("AdministraFormulario.aspx?p=" + formaID);
        }

    }
}