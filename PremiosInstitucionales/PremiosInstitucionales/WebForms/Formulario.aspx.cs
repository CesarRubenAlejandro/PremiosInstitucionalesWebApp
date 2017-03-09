using PremiosInstitucionales.DBServices.Convocatoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                    litTituloPremio.Text = "Premio " + premio.Nombre;
                    litTituloCategoria.Text = "Categoría: " + categoria.Nombre;
                } else
                {
                    Response.Redirect("inicioCandidato.aspx");
                }
            }
        }
    }
}