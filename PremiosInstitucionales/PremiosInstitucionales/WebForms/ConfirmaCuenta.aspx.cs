using PremiosInstitucionales.DBServices.Registro;
using System;

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