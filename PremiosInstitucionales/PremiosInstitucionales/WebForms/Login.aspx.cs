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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["c"] != null)
                {
                    String codigoConfirmacion = Request.QueryString["c"].ToString();
                    RegistroService.ConfirmarCandidato(codigoConfirmacion);

                    showErrorMsg("Aviso");
                    Label1.Text = "Su cuenta ha quedado confirmada.";
                }
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {

            String user1 = user.Text;
            String password = passlogin.Text;

            //Checar si existe en todas las tablas y compara contrasena
            var tipoUsuario = LoginService.GetUsuario(user1, password);

            //Crear sesion o decir que no existe
            if (tipoUsuario == StringValues.RolIncorrecto)
            {
                showErrorMsg("Error");
                Label1.Text = "Usuario/Contraseña incorrectos.";
            }
            else if (tipoUsuario == StringValues.RolNotFound)
            {
                showErrorMsg("Error");
                Label1.Text = "Usuario no encontrado.";
            }
            else
            {
                if (tipoUsuario == StringValues.RolCandidato)
                {
                    var candidato = LoginService.GetCandidato(user1);
                    Session[StringValues.CorreoSesion] = candidato.Correo;
                    Session[StringValues.RolSesion] = StringValues.RolCandidato;

                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "animacionLogin", "transformToNavBar('InicioCandidato.aspx')", true);
                }
                else if (tipoUsuario == StringValues.RolJuez)
                {
                    var juez = LoginService.GetJuez(user1);
                    Session[StringValues.CorreoSesion] = juez.Correo;
                    Session[StringValues.RolSesion] = StringValues.RolJuez;
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "animacionLogin", "transformToNavBar('InicioJuez.aspx')", true);
                }
                else if (tipoUsuario == StringValues.RolAdmin)
                {
                    var administrador = LoginService.GetAdministrador(user1);
                    Session[StringValues.CorreoSesion] = administrador.Correo;
                    Session[StringValues.RolSesion] = StringValues.RolAdmin;
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "animacionLogin", "transformToNavBar('InicioAdmin.aspx')", true);
                }
            }
        }

        protected void Registro_Click(object sender, EventArgs e)
        {
            String password1 = passreg.Text;
            String password2 = passreg2.Text;
            String codigoConfirmacion = Guid.NewGuid().ToString();

            if (email.Text == "" || passreg.Text == "" || passreg2.Text == "" || name.Text == "" || lname.Text == "")
            {
                showErrorMsg("Error");
                Label1.Text = "Debes llenar todos los campos.";
            }
            else if (password1.Equals(password2))
            {
                Regex regexNumero = new Regex(@".*\d.*");
                Regex regexLetra = new Regex(@".*[a-zA-z].*");
                Match matchNumero = regexNumero.Match(password1);
                Match matchLetra = regexLetra.Match(password1);

                if (password1.Length < 6 || !matchNumero.Success || !matchLetra.Success)
                {
                    showErrorMsg("Error");
                    Label1.Text = "Contraseña debe ser de al menos 6 caracteres y debe contener al menos un numero y una letra.";
                }
                else if (RegistroService.Registrar(email.Text, password1, name.Text, lname.Text, codigoConfirmacion))
                {
                    if (EnviarCorreoConfirmacion(codigoConfirmacion))
                    {
                        showErrorMsg("Aviso");
                        Label1.Text = "Se envio un mail al correo registrado. Favor de confirmar cuenta.";
                    }
                    else
                    {
                        showErrorMsg("Error");
                        Label1.Text = "Dirección de correo no válida.";
                    }
                }
                else
                {
                    showErrorMsg("Error");
                    Label1.Text = "Usuario ya existe.";
                }
            }
            else
            {
                showErrorMsg("Error");
                Label1.Text = "Contraseñas no coinciden.";
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

        private void showErrorMsg(string tipoErrorTitulo)
        {
            // Creamos el titutlo del Modal
            modalMensajeTitulo.Controls.Add(new LiteralControl(TituloModal(tipoErrorTitulo)));

            // Mostramos el Modal
            string showMsg_JS = "$('#modalMensaje').modal('show')";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "showE", showMsg_JS, true);
        }

        private string TituloModal(string tipoTitulo)
        {
            if (tipoTitulo == "Error")
            {
                return "<h4 class=\"modal-title\"> <img src=\"../Resources/svg/warning.svg\" class=\"error-icon\"/> Advertencia </h4>";
            }

            else if (tipoTitulo == "Aviso")
            {
                return "<h4 class=\"modal-title\"> <img src=\"../Resources/svg/done.svg\" class=\"error-icon\"/> Listo </h4>";
            }

            return "";
        }

        protected void Recover_Click(object sender, EventArgs e)
        {
            String email = userforgot.Text.ToString();
            String id = RecuperarService.GetID(email);
            if (id != null)
            {
                if (EnviarCorreoRecuperacion(email, id))
                {
                    showErrorMsg("Aviso");
                    Label1.Text = "Se envió un correo para la recuperación de la contraseña.";
                }
                else
                {
                    showErrorMsg("Error");
                    Label1.Text = "Dirección de correo no válida.";
                }
            }
            else
            {
                showErrorMsg("Error");
                Label1.Text = "Usuario no existe.";
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
            catch (Exception sfe)
            {
                return false;
            }
        }
    }


}