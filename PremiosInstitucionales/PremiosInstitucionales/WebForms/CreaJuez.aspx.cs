using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using PremiosInstitucionales.DBServices.Registro;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.IO;
using PremiosInstitucionales.Values;
using System.Net;

namespace PremiosInstitucionales.WebForms
{
    public partial class CreaJuez : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Registra_Juez(object sender, EventArgs e) {
            String correo = correoJuez.Text;
            string contrasena = System.Web.Security.Membership.GeneratePassword(7, 0);
            if (RegistroService.RegistraJuez(correo, contrasena)) {
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
                    mm.Subject = "Fuiste Asignado como Juez a lso premios institucionales.";
                    mm.IsBodyHtml = true;
                    var bodyContent = "Tu contrasena es  "+contrasena;
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