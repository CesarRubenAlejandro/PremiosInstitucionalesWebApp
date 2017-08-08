using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using PremiosInstitucionales.DBServices.Mail;
using System.Web.UI;
using PremiosInstitucionales.Values;
using PremiosInstitucionales.DBServices.InformacionPersonalCandidato;
using PremiosInstitucionales.DBServices.InformacionPersonalJuez;
using System.Text.RegularExpressions;

namespace PremiosInstitucionales
{
    public partial class MP_Global : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Si no hay sesion, regreso a Login
            if (Session[StringValues.RolSesion] == null)
            {
                Response.Redirect("~/WebForms/Login.aspx", false);
            }

            else
            {
                // Cargo el tipo de liga hacia inicio segun el rol de la cuenta
                navInicioPorRol.Controls.Add(new LiteralControl(InicioPorRolSesion()));

                // Cargo la imagen de perfil del usuario conectado
                navProfilePicture.Controls.Add(new LiteralControl(htmlProfilePicture()));
            }
        }
        protected void Invitar_Candidato(object sender, EventArgs e)
        {
            // Correo de la persona que invitare
            String correoInvitar = correoCandidato.Text;

            Regex regexCorreo = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");

            Match matchCorreo = regexCorreo.Match(correoInvitar);

            if (!matchCorreo.Success)
            {
                ShowMessage("Error", "Dirección de correo no válida.");
                return;
            }

            // Intentar mandar el correo
            var MailService = new MailService();
            if (MailService.EnviarCorreoInvitacionCandidato(correoInvitar, nombreCandidato.Text, apellidoCandidato.Text))
            {
                // Ya envie el correo a ese destinatario 
                ShowMessage("Aviso", "Invitación enviada con éxito.");
            }
            else
            {
                // No pude enviar el correo a ese destinatario
                ShowMessage("Error", "El correo no pudo enviarse a ese destinatario, verifica que el correo sea el correcto.");
            }
        }

        private string InicioPorRolSesion()
        {

            // Variable que contendra el obj con tag 'a', redirecciona al menu de inicio, segun el tipo de rol
            string htmlInicio = "<a href=";

            // Si estoy en una sesion
            if (Session[StringValues.RolSesion] != null)
            {
                // Si la sesion pertenece a un candidato
                if (Session[StringValues.RolSesion].ToString() == StringValues.RolCandidato)
                {
                    htmlInicio += "\"/Webforms/Candidato/InicioCandidato.aspx\"";
                }

                // Si la sesion pertenece a un juez
                else if (Session[StringValues.RolSesion].ToString() == StringValues.RolJuez)
                {
                    htmlInicio += "\"/webforms/Juez/InicioJuez.aspx\"";
                }

                // Si la sesion pertenece a un administrador
                else if (Session[StringValues.RolSesion].ToString() == StringValues.RolAdmin)
                {
                    htmlInicio += "\"/webforms/Administrador/InicioAdmin.aspx\"";
                }

            }
            
            // Si no estoy en una sesion
            else
            {
                htmlInicio += "\"#\"";
            }

            // Agrego el fin del tag 'a' junto con el texto "Premios TEC"
            htmlInicio += "> Premios TEC </a>";

            return htmlInicio;
        }

        private string htmlProfilePicture()
        {
            // Consigo el correo de la sesion actual
            string rolSesionActual = Session[StringValues.RolSesion].ToString();

            // Foto de perfil por default, si el usuario tiene otra, se reemplazara
            string htmlContent = "<img src=\"/Resources/img/default-pp.jpg\" class=\"avatar img-circle nav-profilepic\"/>";

            // Verifico si la sesion pertenece a la tipo de cuenta candidato
            if (Session[StringValues.RolSesion] != null)
            {
                if (Session[StringValues.RolSesion].ToString() == StringValues.RolCandidato)
                {
                    // Consigo al candidato segun su correo
                    var candidato = InformacionPersonalCandidatoService.GetCandidatoByCorreo(Session[StringValues.CorreoSesion].ToString());
                    htmlContent = "<a href=\"/Webforms/Candidato/InformacionPersonalCandidato.aspx\" title=\""+ candidato.Nombre + " " + candidato.Apellido + "\">" + htmlContent + "</a>";

                    // Si tiene imagen de perfil la muestro
                    if (candidato.NombreImagen != null && candidato.NombreImagen.Length > 0)
                    {
                        if (File.Exists(Server.MapPath("~/ProfilePictures/" + candidato.NombreImagen)))
                        {
                            htmlContent = "<a href=\"/Webforms/Candidato/InformacionPersonalCandidato.aspx\" title=\"" + candidato.Nombre + " " + candidato.Apellido + "\">" +
                                                "<img src=\"/ProfilePictures/" + candidato.NombreImagen + "\" class=\"avatar img-circle nav-profilepic\"/>" +
                                          "</a>";
                        }
                    }
                }

                // Verifico si la sesion pertenece a la tipo de cuenta juez
                else if (Session[StringValues.RolSesion].ToString() == StringValues.RolJuez)
                {
                    // Consigo al juez segun su correo
                    var juez = InformacionPersonalJuezService.GetJuezByCorreo(Session[StringValues.CorreoSesion].ToString());
                    htmlContent = "<a href=\"/webforms/Juez/InformacionPersonalJuez.aspx\" title=\"" + juez.Nombre + " " + juez.Apellido + "\">" + htmlContent + "</a>";

                    // Si tiene imagen de perfil la muestro
                    if (juez.NombreImagen != null && juez.NombreImagen.Length > 0)
                    {
                        if (File.Exists(Server.MapPath("~/ProfilePictures/" + juez.NombreImagen)))
                        {
                            htmlContent = "<a href=\"/webforms/Juez/InformacionPersonalJuez.aspx\" title=\"" + juez.Nombre + " " + juez.Apellido + "\">" +
                                                "<img src=\"/ProfilePictures/" + juez.NombreImagen + "\" class=\"avatar img-circle nav-profilepic\"/>" +
                                          "</a>";
                        }
                    }
                }

                // Verifico si la sesion pertenece a la tipo de cuenta administrador
                else if (Session[StringValues.RolSesion].ToString() == StringValues.RolAdmin)
                {
                    // administrador no utiliza imagen de perfil
                    htmlContent = "";
                }
            }

            // Retorno el string que contiene el html de la imagen de perfil
            return htmlContent;
        }

        protected void LogoutBttn_Click(object sender, EventArgs e)
        {
            // Cierro la sesion actual
            Session.Abandon();

            // Redirecciono a la pagina de inicio de sesion
            Response.Redirect("~/WebForms/Login.aspx", false);
        }

        public void ShowMessage(String MessageType, String Message)
        {
            // Creamos el titutlo del Modal
            modalMensajeTitulo.Controls.Clear();
            modalMensajeTitulo.Controls.Add(new LiteralControl(ModalTitle(MessageType)));

            // Mensaje del Modal
            Mensaje.Text = Message;

            // Mostramos el Modal
            String showMsg_JS = "$('#modalMensaje').modal('show')";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "showE", showMsg_JS, true);
        }

        public String ModalTitle(String Title)
        {
            switch (Title)
            {
                case "Error":
                    return "<h4 class=\"modal-title\"> <img src=\"../../Resources/svg/warning.svg\" class=\"error-icon\"/> Advertencia </h4>";
                case "Aviso":
                    return "<h4 class=\"modal-title\"> <img src=\"../../Resources/svg/done.svg\" class=\"error-icon\"/> Listo </h4>";
                default:
                    return "";
            }
        }
    }
}