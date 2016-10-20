using PremiosInstitucionales.DBServices.Registro;
using System;
using System.Net;
using System.Net.Mail;

namespace PremiosInstitucionales.WebForms
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String password1 = PasswordTextBox.Text;
            String password2 = ConfirmPasswordTextBox.Text;
            String codigoConfirmacion = Guid.NewGuid().ToString();

            if (password1.Equals(password2) && RegistroService.Registrar(EmailTextBox.Text, password1, codigoConfirmacion))
            {
                //exito: desplegar mensaje de confirmacion 
                //Response.Redirect("login.aspx");
                ConfirmacionLbl.Visible = true;
                // enviar correo
                EnviarCorreoConfirmacion(codigoConfirmacion);
            }
            else
            {
                //fracaso
                FracasoLbl.Visible = true;
            }
        }

        private void EnviarCorreoConfirmacion(String codigoConfirmacion)
        {
            String correoSender = "empresa.ejemplo.mail@gmail.com";
            String pswSender = "proyectointegrador";
            using (MailMessage mm = new MailMessage(correoSender, EmailTextBox.Text.ToString()))
            {
                mm.Subject = "Confirmación de cuenta para el sistema Premios Institucionales del Tec de Monterrey";
                mm.Body = "Para confirmar esta cuenta, haz click en el siguiente link: http://localhost:2943/WebForms/ConfirmaCuenta.aspx?codigo=" + codigoConfirmacion;
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
        }
    }
}