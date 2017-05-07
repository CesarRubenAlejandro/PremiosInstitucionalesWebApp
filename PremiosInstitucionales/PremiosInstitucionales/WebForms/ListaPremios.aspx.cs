using PremiosInstitucionales.DBServices.Convocatoria;
using PremiosInstitucionales.Values;
using System;
using System.IO;
using System.Web.UI.WebControls;

namespace PremiosInstitucionales.WebForms
{
    public partial class ListaPremios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // revisar la primera vez que se carga la pagina que se haya iniciado sesion con cuenta de admin o candidato
                if (Session[StringValues.RolSesion] != null)
                {
                    if (Session[StringValues.RolSesion].ToString() != StringValues.RolAdmin && Session[StringValues.RolSesion].ToString() != StringValues.RolCandidato)
                    {    
                        // si no es admin ni candidato, redireccionar a inicio general
                        Response.Redirect("Login.aspx");
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }

            // crear lista de paneles para cada premio
            // obtener lista de premios
            var listaPremios = ConvocatoriaService.GetAllPremios();
            var i = 0;
            // crear un panel para cada premio con su link respectivo
            foreach (var premio in listaPremios)
            {
                Literal lit = new Literal();
                var panelNuevo = new Panel();
                panelNuevo.CssClass = "premioPanel";

                var imgButton = new ImageButton();
                if (File.Exists(Server.MapPath("~/AwardPictures/" + premio.NombreImagen)))
                {
                    imgButton.ImageUrl = "/AwardPictures/" + premio.NombreImagen;
                }
                else
                {
                    imgButton.AlternateText = premio.Nombre;
                }

                if (Session[StringValues.RolSesion].ToString() == StringValues.RolAdmin)
                {
                    //Si es admin
                    imgButton.PostBackUrl = "PremioEspecificoAdmin.aspx?premio=" + premio.cvePremio;
                } else
                {
                    //Si es candidato
                    imgButton.PostBackUrl = "PremioEspecificoCandidato.aspx?premio=" + premio.cvePremio;
                }
                imgButton.CssClass = "premioImgButton";


                lit.Text = "<div class='col-md-6'>" + "<div class='blockquote-box clearfix' style=''><div class='square pull-left'>" +
                    "<img src = '/AwardPictures/" + premio.NombreImagen + "' class='img-square'/>" + "</div><h4>" + premio.Nombre + "</h4><p>Lorem Ipsum</p>" +
                    "<div style='text-align: right; '><button id = '" + premio.cvePremio + "' type = 'button' class='btn btn-sm btn-primary'  data-toggle='modal' data-target='#myModal" + i + "'>Detalles</button></div></div></div>";
                colPremio.Controls.Add(lit);
                Literal lit2 = new Literal();
                lit2.Text = "<div class='modal fade' id='myModal"+i+"' tabindex='-1' role='dialog' aria-labelledby='myModalLabel'>" +
                            "<div class='modal-dialog' role='document'>" +
                                "<div class='modal-content'>" +
                                    "<div class='modal-header text-center'>" +
                                        "<button type='button' class='close' data-dismiss='modal' aria-label='Close'><span aria-hidden='true'>&times;</span></button>" +
                                            "<h3 class='modal-title' id='myModalLabel'>" + premio.Nombre + "</h3>" +
                                                "<hr class='shorthr'>" +
                                                    "</div>" +
                                        "<div class='modal-body'>" +
                                        "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus tempor felis et mi luctus facilisis. Aenean consectetur malesuada facilisis. Cras vitae dolor sollicitudin, gravida lacus sed, pretium tortor. Sed rhoncus eros vitae mauris auctor condimentum. Proin ullamcorper augue mi. Nulla id felis ac ipsum aliquam sollicitudin.</p>" +
                                        "<div class='form-group'>" +
                                        "<label for='sel"+i+"'> Selecciona la categoria correspondiente:</label>"+
                                        "<select class='form-control' id='sel"+i+"'>";
                var conv = ConvocatoriaService.GetMostRecentConvocatoria(premio.cvePremio);
                if (conv == null) return;
                var listCat = ConvocatoriaService.GetCategoriasByConvocatoria(conv.cveConvocatoria);
                modalList.Controls.Add(lit2);

                foreach (var cat in listCat) {
                    Literal litCa = new Literal();
                    litCa.Text = "<option>" + cat.Nombre + "</option>";
                    modalList.Controls.Add(litCa);
                }
                Literal lit3 = new Literal();
                lit3.Text= "</select></div></div>"+
                            "<div class='modal-footer'>"+
                                "<button type='button' class='btn btn-default' data-dismiss='modal'>Cancelar</button>"+
                                    // HARD CODED
                                    "<a href='Formulario.aspx?c=categoria3'><button type='button' class='btn btn-primary'> Aplicar</button></a>" +
                                    "</div></div></div></div>";
                modalList.Controls.Add(lit3);
                panelNuevo.Controls.Add(imgButton);
                i = i + 1;
            }
        }
    }
}