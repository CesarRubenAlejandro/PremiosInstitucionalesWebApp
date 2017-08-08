using PremiosInstitucionales.DBServices.Login;
using PremiosInstitucionales.DBServices.Registro;
using PremiosInstitucionales.Values;
using System;
using System.Web.UI;
using System.Text.RegularExpressions;
using PremiosInstitucionales.DBServices.Recuperar;
using PremiosInstitucionales.DBServices.Mail;
using System.Text;

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
                // Reset Cookie Inicio
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "setCookieInicio", "setCookie('tab', 'inicio');", true);

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
            else
            {
                // Set Selected tab
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "setSelectedTab", "autoChangeTab();", true);
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            // mail / contraseña
            String user1 = user.Text;
            String password = passlogin.Text;

            //Checar si existe en todas las tablas y compara contrasena
            var tipoUsuario = LoginService.GetUsuario(user1, sha256(password));

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
                    else if (RegistroService.RegistraCandidato(email.Text, sha256(password1), name.Text, lname.Text, codigoConfirmacion))
                    {
                        var MailService = new MailService();
                        if (MailService.EnviarCorreoConfirmacion(email.Text.ToString(), codigoConfirmacion))
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "setCookieInicio2", "setCookie('tab', 'inicio');", true);
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "setSelectedTab2", "autoChangeTab();", true);
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

        protected void Recover_Click(object sender, EventArgs e)
        {
            String email = userforgot.Text.ToString();
            String id = RecuperarService.GetID(email);
            if (id != null)
            {
                var MailService = new MailService();
                if (MailService.EnviarCorreoRecuperacion(email, id))
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "forgotPassword", "forgotPassword(false);", true);
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

        static string sha256(string rawPassword)
        {
            System.Security.Cryptography.SHA256Managed crypt = new System.Security.Cryptography.SHA256Managed();
            System.Text.StringBuilder hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(rawPassword), 0, Encoding.UTF8.GetByteCount(rawPassword));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}