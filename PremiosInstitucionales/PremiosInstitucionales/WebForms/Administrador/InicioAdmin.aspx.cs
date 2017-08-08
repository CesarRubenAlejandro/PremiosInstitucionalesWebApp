using PremiosInstitucionales.DBServices.Registro;
using PremiosInstitucionales.Values;
using System;
using System.Text.RegularExpressions;
using PremiosInstitucionales.DBServices.Mail;

namespace PremiosInstitucionales.WebForms
{
    public partial class InicioAdmin : System.Web.UI.Page
    {
        MP_Global MasterPage = new MP_Global();
        protected void Page_Load(object sender, EventArgs e)
        {
            MasterPage = (MP_Global)Page.Master;
            if (!IsPostBack)
            {
                // revisar la primera vez que se carga la pagina que se haya iniciado sesion con cuenta de admin
                if (Session[StringValues.RolSesion] != null)
                {
                    if (Session[StringValues.RolSesion].ToString() != StringValues.RolAdmin)
                        // si no es admin, redireccionar a inicio general
                        Response.Redirect("~/WebForms/Login.aspx", false);
                }
                else
                {
                    Response.Redirect("~/WebForms/Login.aspx", false);
                }
            }
        }
        protected void Registra_Juez(object sender, EventArgs e)
        {
            // Correo de la persona que invitare
            String correoInvitar = correoJuez.Text;

            Regex regexCorreo = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");

            Match matchCorreo = regexCorreo.Match(correoInvitar);

            if (!matchCorreo.Success)
            {
                MasterPage.ShowMessage("Error", "Dirección de correo no válida.");
                return;
            }

            string contrasena = System.Web.Security.Membership.GeneratePassword(7, 0);
            if (RegistroService.RegistraJuez(correoInvitar, contrasena))
            {
                var MailService = new MailService();
                if (MailService.EnviarCorreoInvitacionJuez(correoInvitar, contrasena))
                {
                    // Ya envie el correo a ese destinatario 
                    MasterPage.ShowMessage("Aviso", "Invitación enviada con éxito.");
                }
                else
                {
                    // No pude enviar el correo a ese destinatario
                    MasterPage.ShowMessage("Error", "El correo no pudo enviarse a ese destinatario, verifica que el correo sea el correcto.");
                }
            }
        }
    }
}