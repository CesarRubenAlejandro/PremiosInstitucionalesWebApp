using PremiosInstitucionales.DBServices.Login;
using PremiosInstitucionales.DBServices.Registro;
using PremiosInstitucionales.Values;
using System;
using System.Web.UI.HtmlControls;

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
                // hacer login y enviar a pagina de info candidato
                var candidato = LoginService.GetCandidatoByConfirmacion(codigoConfirmacion);
                if(candidato != null)
                {
                    Session[StringValues.CorreoSesion] = candidato.Correo;
                    Session[StringValues.RolSesion] = StringValues.RolCandidato;
                    MensajeLbl.Visible = true;
                    //LoginHL.Visible = true;
                    HtmlMeta meta = new HtmlMeta();
                    meta.HttpEquiv = "Refresh";
                    meta.Content = "5;url=InformacionPersonalCandidato.aspx";
                    this.Page.Controls.Add(meta);
                }
                
            }
        }
    }
}