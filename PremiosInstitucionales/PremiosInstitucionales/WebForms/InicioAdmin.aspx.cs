using PremiosInstitucionales.DBServices.Registro;
using PremiosInstitucionales.Values;
using System;
using System.Net;
using System.Net.Mail;

namespace PremiosInstitucionales.WebForms
{
    public partial class InicioAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // revisar la primera vez que se carga la pagina que se haya iniciado sesion con cuenta de admin
                if (Session[StringValues.RolSesion] != null)
                {
                    if (Session[StringValues.RolSesion].ToString() != StringValues.RolAdmin)
                        Response.Redirect("Login.aspx");
                }
            }
        }
        protected void Registra_Juez(object sender, EventArgs e)
        {
            String correo = correoJuez.Text;
            string contrasena = System.Web.Security.Membership.GeneratePassword(7, 0);
            if (RegistroService.RegistraJuez(correo, contrasena))
            {
                EnviarCorreoConfirmacion(contrasena);
            }

        }

        private bool EnviarCorreoConfirmacion(String contrasena)
        {
            String correoSender = "empresa.ejemplo.mail@gmail.com";
            String pswSender = "proyectointegrador";
            try
            {
                using (MailMessage mm = new MailMessage(correoSender, correoJuez.Text.ToString()))
                {
                    mm.Subject = "Fuiste Asignado como Juez a los premios institucionales.";
                    mm.IsBodyHtml = true;
                    var bodyContent = "Tu contrasena es  " + contrasena;
                    try
                    {
                        mm.Body = bodyContent;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential(correoSender, pswSender);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        smtp.Send(mm);
                    }
                    catch (Exception e)
                    {
                        return false;
                    }

                }
                return true;
            }
            catch (Exception sfe)
            {
                return false;
            }
        }
    }
}