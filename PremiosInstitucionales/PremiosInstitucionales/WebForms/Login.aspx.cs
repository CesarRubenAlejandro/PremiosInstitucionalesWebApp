using PremiosInstitucionales.DBServices.Login;
using PremiosInstitucionales.DBServices.Registro;
using PremiosInstitucionales.Values;
using System;
using System.IO;
using System.Web.UI;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using PremiosInstitucionales.DBServices.Recuperar;
using PremiosInstitucionales.DBServices.InformacionPersonalCandidato;

namespace PremiosInstitucionales.WebForms
{
    public partial class Login : System.Web.UI.Page
    {

        MP_Login MasterPage = new MP_Login();

        protected void Page_Load(object sender, EventArgs e)
        {
            MasterPage = (MP_Login)Page.Master;
            if (!IsPostBack)
            {
                if (Request.QueryString["c"] != null)
                {
                    String codigoConfirmacion = Request.QueryString["c"].ToString();
                    if (RegistroService.ConfirmarCandidato(codigoConfirmacion))
                    {
                        MasterPage.ShowMessage("Aviso", "Su cuenta ha quedado confirmada.");
                    }
                    else
                    {
                        MasterPage.ShowMessage("Error", "Código de confirmación inválido.");
                    }
                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            // mail / contraseña
            String user1 = user.Text;
            String password = passlogin.Text;

            //Checar si existe en todas las tablas y compara contrasena
            var tipoUsuario = LoginService.GetUsuario(user1, password);

            //Crear sesion o decir que no existe
            if (tipoUsuario == StringValues.RolIncorrecto)
            {
                MasterPage.ShowMessage("Error", "Usuario/Contraseña incorrectos.");
            }
            else if (tipoUsuario == StringValues.RolNotFound)
            {
                MasterPage.ShowMessage("Error", "Usuario no encontrado.");
            }
            else
            {
                if (tipoUsuario == StringValues.RolCandidato)
                {
                    var candidato = LoginService.GetCandidato(user1);
                    Session[StringValues.CorreoSesion] = candidato.Correo;
                    Session[StringValues.RolSesion] = StringValues.RolCandidato;

                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "animacionLogin", "transformToNavBar('Candidato/InicioCandidato.aspx')", true);
                }
                else if (tipoUsuario == StringValues.RolJuez)
                {
                    var juez = LoginService.GetJuez(user1);
                    Session[StringValues.CorreoSesion] = juez.Correo;
                    Session[StringValues.RolSesion] = StringValues.RolJuez;
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "animacionLogin", "transformToNavBar('Juez/InicioJuez.aspx')", true);
                }
                else if (tipoUsuario == StringValues.RolAdmin)
                {
                    var administrador = LoginService.GetAdministrador(user1);
                    Session[StringValues.CorreoSesion] = administrador.Correo;
                    Session[StringValues.RolSesion] = StringValues.RolAdmin;
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "animacionLogin", "transformToNavBar('Administrador/InicioAdmin.aspx')", true);
                }
            }
        }

        protected void Registro_Click(object sender, EventArgs e)
        {
            String password1 = passreg.Text;
            String password2 = passreg2.Text;
            String correo = email.Text;
            String codigoConfirmacion = Guid.NewGuid().ToString();

            if (email.Text == "" || passreg.Text == "" || passreg2.Text == "" || name.Text == "" || lname.Text == "")
            {
                MasterPage.ShowMessage("Error", "Debes llenar todos los campos.");
            }
            else if (password1.Equals(password2))
            {
                Regex regexNumero = new Regex(@".*\d.*");
                Regex regexLetra = new Regex(@".*[a-zA-z].*");
                Regex regexCorreo = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");

                Match matchNumero = regexNumero.Match(password1);
                Match matchLetra = regexLetra.Match(password1);
                Match matchCorreo = regexCorreo.Match(correo);

                if (!matchCorreo.Success)
                {
                    MasterPage.ShowMessage("Error", "Dirección de correo no válida.");
                }
                else
                { 
                    if (password1.Length < 6 || !matchNumero.Success || !matchLetra.Success)
                    {
                        MasterPage.ShowMessage("Error", "Contraseña debe ser de al menos 6 caracteres y debe contener al menos un número y una letra.");
                    }
                    else if (RegistroService.RegistraCandidato(email.Text, password1, name.Text, lname.Text, codigoConfirmacion))
                    {
                        if (EnviarCorreoConfirmacion(codigoConfirmacion))
                        {
                            MasterPage.ShowMessage("Aviso", "Se envió un mail al correo registrado. Favor de confirmar cuenta.");
                        }
                        else
                        {
                            MasterPage.ShowMessage("Error", "Dirección de correo no válida.");
                        }
                    }
                    else
                    {
                        MasterPage.ShowMessage("Error", "Ya existe un usuario registrado a ese correo.");
                    }
                }
            }
            else
            {
                MasterPage.ShowMessage("Error", "Contraseñas no coinciden.");
            }
        }

        private bool EnviarCorreoConfirmacion(String codigoConfirmacion)
        {
            String correoSender = "empresa.ejemplo.mail@gmail.com";
            String pswSender = "proyectointegrador";
            try
            {
                using (MailMessage mm = new MailMessage(correoSender, email.Text.ToString()))
                {
                    mm.Subject = "Confirma tu cuenta para Premios Institucionales del Tec de Monterrey";
                    mm.IsBodyHtml = true;
                    var bodyContent = "";
                    try
                    {
                        bodyContent = File.ReadAllText(Server.MapPath("~/Values/CorreoConfirmaCuenta.txt"));
                        // formatear contenidos de string
                        bodyContent = bodyContent.Replace(StringValues.ContenidoCorreoFecha, DateTime.Today.ToShortDateString());
                        bodyContent = bodyContent.Replace(StringValues.ContenidoCorreoMail, email.Text.ToString());
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
                    catch (Exception Ex2)
                    {
                        Console.WriteLine("Catched Exception: " + Ex2.Message + Environment.NewLine);
                        return false;
                    }

                }
                return true;
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                return false;
            }
        }

        protected void Recover_Click(object sender, EventArgs e)
        {
            String email = userforgot.Text.ToString();
            String id = RecuperarService.GetID(email);
            if (id != null)
            {
                if (EnviarCorreoRecuperacion(email, id))
                {
                    MasterPage.ShowMessage("Aviso", "Se envió un correo para la recuperación de la contraseña.");
                }
                else
                {
                    MasterPage.ShowMessage("Error", "Dirección de correo no válida.");
                }
            }
            else
            {
                MasterPage.ShowMessage("Error", "No existe ningún usuario registrado con ese correo.");
            }
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
                        InformacionPersonalCandidatoService.GetCandidatoByCorreo(destinatario).Nombre);
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
            catch (Exception Ex)
            {
                Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                return false;
            }
        }
    }
}