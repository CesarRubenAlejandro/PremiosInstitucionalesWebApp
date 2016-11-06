using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PremiosInstitucionales.DBServices.Recuperar;
using System.Net;
using System.Net.Mail;

namespace PremiosInstitucionales.WebForms
{
    public partial class Recuperar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String email = EmailTextBox.Text.ToString();
            String id = RecuperarService.GetID(email);
            if (id != null)
            {
                EnviarCorreoRecuperacion(email, id);
            }
            else
            {
                Mensaje.Text = "Usuario no existe";
            }
            Mensaje.Visible = true;
        }

        private void EnviarCorreoRecuperacion(String destinatario, String id)
        {
            String correoSender = "empresa.ejemplo.mail@gmail.com";
            String pswSender = "proyectointegrador";
            using (MailMessage mm = new MailMessage(correoSender, destinatario))
            {
                mm.Subject = "Recuperación de contraseña para el sistema Premios Institucionales del Tec de Monterrey";
                mm.Body = "Para recuperar tu contraseña, haz click en el siguiente link: http://localhost:2943/WebForms/RecuperaCuenta.aspx?codigo=" + id;
                mm.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(correoSender, pswSender);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }
            Mensaje.Text = "Se envió un correo a " + destinatario + " para la recuperación de la contraseña";
            EmailTextBox.Enabled = false;
            EnviarBoton.Enabled = false;
        }
    }
}