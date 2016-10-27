using PremiosInstitucionales.DBServices.Registro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PremiosInstitucionales.WebForms
{
    public partial class ConfirmaCuenta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["codigo"] != null)
            {
                String codigoConfirmacion = Request.QueryString["codigo"].ToString();
                RegistroService.ConfirmarCandidato(codigoConfirmacion);
                MensajeLbl.Visible = true;
                LoginHL.Visible = true;
            }
        }
    }
}