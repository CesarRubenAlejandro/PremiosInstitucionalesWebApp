using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PremiosInstitucionales.DBServices.Recuperar;
using System.Net;
using System.Net.Mail;
using System.IO;
using PremiosInstitucionales.Values;
using PremiosInstitucionales.DBServices.InformacionPersonalCandidato;

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
                if (EnviarCorreoRecuperacion(email, id))
                {
                    Info.Text = "Se envió un correo para la recuperación de la contraseña";
                    EnviarBoton.Visible = false;
                    EmailTextBox.Visible = false;
                }
                else
                {
                    Info.Text = "Dirección de correo no válida";
                }
            }
            else
            {
                Info.Text = "Usuario no existe";
            }
            Info.Visible = true;
        }

        private bool EnviarCorreoRecuperacion(String destinatario, String id)
        {
            String correoSender = "empresa.ejemplo.mail@gmail.com";
            String pswSender = "proyectointegrador";
            // enviando correo
            try
            {
                using (MailMessage mm = new MailMessage(correoSender, destinatario))
                {
                    mm.Subject = "Recuperación de contraseña para el sistema Premios Institucionales del Tec de Monterrey";
                    mm.IsBodyHtml = true;
                    var bodyContent = "";
                    bodyContent = File.ReadAllText(Server.MapPath("~/Values/CorreoRecuperaPassword.txt"));
                    // formatear contenidos de string
                    bodyContent = bodyContent.Replace(StringValues.ContenidoCorreoFecha, DateTime.Today.ToShortDateString());
                    bodyContent = bodyContent.Replace(StringValues.ContenidoCorreoNombre, 
                        InformacionPersonalCandidatoService.GetNombre(destinatario).Item1);
                    bodyContent = bodyContent.Replace(StringValues.ContenidoCorreoId, id);
                    mm.Body = bodyContent;
                    // enviar
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential(correoSender, pswSender);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);
                }
                return true;
            }
            catch (System.FormatException sfe)
            {
                return false;
            }
        }
    }
}