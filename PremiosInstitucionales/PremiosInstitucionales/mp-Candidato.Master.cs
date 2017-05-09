using PremiosInstitucionales.DBServices.Registro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PremiosInstitucionales
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Invitar_Candidato(object sender, EventArgs e)
        {
            String correo = correoCandidato.Text;
            InvitarCandidato(correo);

        }

        private bool InvitarCandidato(String correo)
        {
            String correoSender = "empresa.ejemplo.mail@gmail.com";
            String pswSender = "proyectointegrador";
            try
            {
                using (MailMessage mm = new MailMessage(correoSender, correo))
                {
                    mm.Subject = "Invitación a premios institucionales";
                    mm.IsBodyHtml = true;
                    var bodyContent = "Te están invitando a que participes en uno de los premios institucionales. Entra a INSERTA LINK AQUI";
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