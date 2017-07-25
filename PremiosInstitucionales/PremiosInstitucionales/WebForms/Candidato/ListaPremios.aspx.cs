using PremiosInstitucionales.DBServices.Convocatoria;
using PremiosInstitucionales.Values;
using System;
using System.Web.UI.WebControls;

namespace PremiosInstitucionales.WebForms
{
    public partial class ListaPremios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // revisar la primera vez que se carga la pagina que se haya iniciado sesion con cuenta de candidato
                if (Session[StringValues.RolSesion] != null)
                {
                    if (Session[StringValues.RolSesion].ToString() != StringValues.RolCandidato)
                        // si no es candidato, redireccionar a login
                        Response.Redirect("~/WebForms/Login.aspx");
                }
                else
                {
                    Response.Redirect("~/WebForms/Login.aspx");
                }
            }

            // obtener lista de premios
            var listaPremios = ConvocatoriaService.GetAllPremios();
            var i = 0;

            // crear un panel para cada premio con su link respectivo
            foreach (var premio in listaPremios)
            {
                // Si no hay convocatoria, no muestro el premio
                var conv = ConvocatoriaService.GetMostRecentConvocatoria(premio.cvePremio);
                if (conv == null) continue;

                // Si no hay categorias en la convocatoria, no muestro el premio
                var listCat = ConvocatoriaService.GetCategoriasByConvocatoria(conv.cveConvocatoria);
                if (listCat == null) continue;
                if (listCat.Count == 0) continue;

                Literal lit = new Literal();

                // Un panel para cada premio (imagen, descripcion y boton de detalle)
                lit.Text = "<div class='col-md-6'>" +
                                "<div class='blockquote-box clearfix' style=''>" +
                                    "<div class='square pull-left'>" +
                                        "<img src = '/AwardPictures/" + premio.NombreImagen + "' class=\"img-square\"/>" +
                                    "</div>" +
                                    "<h4>Premio: <strong>" + premio.Nombre + "</strong></h4>" +
                                    "<p>" + premio.Descripcion + "</p>" +
                                    "<div style='text-align: right; '>" +
                                        "<button id = '" + premio.cvePremio + "' type = 'button' class='btn btn-sm btn-primary'  data-toggle='modal' data-target='#myModal" + i + "'>" +
                                            "Detalles" +
                                        "</button>" +
                                    "</div>" +
                                "</div>" +
                            "</div>";
                colPremio.Controls.Add(lit);

                // Encabezado y cuerpo del modal
                Literal lit2 = new Literal();
                lit2.Text = "<div class='modal fade' id='myModal"+i+"' tabindex='-1' role='dialog' aria-labelledby='myModalLabel'>" +
                                "<div class='modal-dialog' role='document'>" +
                                    "<div class='modal-content'>" +
                                        "<div class='modal-header text-center'>" +
                                            "<button type='button' class='close' data-dismiss='modal' aria-label='Close'>" + 
                                                "<span aria-hidden='true'>&times;</span>" +
                                            "</button>" +
                                            "<h3 class='modal-title' id='myModalLabel'>" + premio.Nombre + "</h3>" +
                                            "<hr class='shorthr'>" +
                                        "</div>" +
                                        "<div class='modal-body'>" +
                                            "<p>" +
                                                premio.Descripcion +
                                            "</p>" +
                                            "<div class='form-group'>" +
                                                "<label for='sel"+i+"'> Selecciona la categoria correspondiente:</label>"+
                                                "<select class='form-control' onchange='changeAnchor(this,\""+premio.cvePremio+"\")' id ='sel"+i+"'>";
                modalList.Controls.Add(lit2);

                // Agregar por cada categoria, la opcion en el dropdown
                foreach (var cat in listCat) {
                    Literal litCa = new Literal();
                    litCa.Text = "<option id='"+cat.cveCategoria+"'>" + cat.Nombre + "</option>";
                    modalList.Controls.Add(litCa);
                }

                // Pie del modal
                Literal lit3 = new Literal();
                lit3.Text= "</select></div></div>"+
                            "<div class='modal-footer'>"+
                                "<button type='button' class='btn btn-default' style=\"margin-right: 10px;\" data-dismiss='modal'>" +
                                    "Cancelar" +
                                "</button>"+
                                "<a id='"+premio.cvePremio+"' href='Formulario.aspx?c="+listCat[0].cveCategoria+"'>" +
                                    "<button type='button' class='btn btn-primary'>" +
                                        "Aplicar" +
                                    "</button>" +
                                "</a>" +
                            "</div>"+
                            "</div></div></div>";
                modalList.Controls.Add(lit3);

                i = i + 1;
            }
        }

        protected void BackBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("InicioCandidato.aspx");
        }

    }
}