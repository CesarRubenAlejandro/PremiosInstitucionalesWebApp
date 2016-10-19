using PremiosInstitucionales.DBServices.Convocatoria;
using PremiosInstitucionales.Values;
using System;
using System.Web.UI.WebControls;

namespace PremiosInstitucionales.WebForms
{
    public partial class InicioAdministrador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // revisar la primera vez que se carga la pagina que se haya iniciado sesion con cuenta de admin
                if (Session[StringValues.RolSesion] != null)
                {
                    if (Session[StringValues.RolSesion].ToString() != StringValues.RolAdmin)
                        // si no es admin, redireccionar a inicio general
                        Response.Redirect("InicioCandidato.aspx");
                }
                else
                {
                    Response.Redirect("InicioCandidato.aspx");
                }
            }

            // crear lista de paneles para cada premio
            // obtener lista de premios
            var listaPremios = ConvocatoriaService.GetAllPremios();
            // crear un panel para cada premio con su link respectivo
            foreach (var premio in listaPremios)
            {
                var panelNuevo = new Panel();
                panelNuevo.CssClass = "premioPanel";

                var imgButton = new ImageButton();
                imgButton.ImageUrl = "/img/" + premio.NombreImagen;
                imgButton.PostBackUrl = "EditarConvocatoria.aspx?premio=" + premio.cvePremio;
                imgButton.CssClass = "premioImgButton";

                panelNuevo.Controls.Add(imgButton);
                PanelesPremios.Controls.Add(panelNuevo);
            }
        }
    }
}