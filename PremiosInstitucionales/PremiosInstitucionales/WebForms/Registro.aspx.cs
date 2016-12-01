using PremiosInstitucionales.DBServices.Registro;
using PremiosInstitucionales.Values;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

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

            if (password1.Equals(password2))
            {
                Regex regexNumero = new Regex(@".*\d.*");
                Regex regexLetra = new Regex(@".*[a-zA-z].*");
                Match matchNumero = regexNumero.Match(password1);
                Match matchLetra = regexLetra.Match(password1);
                if (password1.Length < 6 || !matchNumero.Success || !matchLetra.Success)
                {
                    FracasoLbl.Text = "Contraseña debe ser de al menos 6 caracteres y debe contener al menos un numero y una letra";
                }
                else if (RegistroService.Registrar(EmailTextBox.Text, password1, codigoConfirmacion))
                {
                    if (EnviarCorreoConfirmacion(codigoConfirmacion))
                    {
                        FracasoLbl.Text = "Se envio un mail al correo registrado. Favor de confirmar cuenta";
                    }
                    else
                    {
                        FracasoLbl.Text = "Dirección de correo no válida";
                    }
                }
                else
                {
                    FracasoLbl.Text = "Usuario ya existe";
                }
            }
            else
            {
                FracasoLbl.Text = "Contraseñas no coinciden";
            }
            FracasoLbl.Visible = true;
        }

        private bool EnviarCorreoConfirmacion(String codigoConfirmacion)
        {
            String correoSender = "empresa.ejemplo.mail@gmail.com";
            String pswSender = "proyectointegrador";
            try
            {
                using (MailMessage mm = new MailMessage(correoSender, EmailTextBox.Text.ToString()))
                {
                    mm.Subject = "Confirma tu cuenta para Premios Institucionales del Tec de Monterrey.";
                    mm.IsBodyHtml = true;
                    var bodyContent = "";
                    try
                    {
                        bodyContent = File.ReadAllText(Server.MapPath("~/Values/CorreoConfirmaCuenta.txt"));
                        // formatear contenidos de string
                        bodyContent = bodyContent.Replace(StringValues.ContenidoCorreoFecha, DateTime.Today.ToShortDateString());
                        bodyContent = bodyContent.Replace(StringValues.ContenidoCorreoMail, EmailTextBox.Text.ToString());
                        bodyContent = bodyContent.Replace(StringValues.ContenidoCorreoConfirmacion, codigoConfirmacion);
                        // enviar
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