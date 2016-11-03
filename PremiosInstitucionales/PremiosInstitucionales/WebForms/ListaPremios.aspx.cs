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
            // crear un panel para cada premio con su link respectivo
            foreach (var premio in listaPremios)
            {
                var panelNuevo = new Panel();
                panelNuevo.CssClass = "premioPanel";

                var imgButton = new ImageButton();
                imgButton.ImageUrl = "/img/" + premio.NombreImagen;
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

                panelNuevo.Controls.Add(imgButton);
                PanelesPremios.Controls.Add(panelNuevo);
            }
        }
    }
}