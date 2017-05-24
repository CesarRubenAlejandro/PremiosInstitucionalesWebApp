using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web.UI;
using PremiosInstitucionales.Values;
using PremiosInstitucionales.DBServices.InformacionPersonalCandidato;
using PremiosInstitucionales.DBServices.InformacionPersonalJuez;

namespace PremiosInstitucionales
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Si no hay sesion, regreso a Login
            if (Session[StringValues.RolSesion] == null)
            {
                Response.Redirect("Login.aspx");
            }

            // Cargo el tipo de liga hacia inicio segun el rol de la cuenta
            navInicioPorRol.Controls.Add(new LiteralControl(InicioPorRolSesion()));

            // Cargo la imagen de perfil del usuario conectado
            navProfilePicture.Controls.Add(new LiteralControl(htmlProfilePicture()));
        }
        protected void Invitar_Candidato(object sender, EventArgs e)
        {
            // Correo de la persona que invitare
            String correoInvitar = correoCandidato.Text;

            // Correo de la institucion
            String correoSender = "empresa.ejemplo.mail@gmail.com";

            // Conatrseña de la institucion
            String pswSender = "proyectointegrador";

            // Intentar mandar el correo
            try
            {
                using (MailMessage mm = new MailMessage(correoSender, correoInvitar))
                {
                    // Titulo del correo
                    mm.Subject = "Invitación a premios institucionales";
                    mm.IsBodyHtml = true;

                    // Contenido del correo
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

                    catch (Exception e1)
                    {
                        // No pude enviar el correo a ese destinatario
                    }
                }
                // Ya envie el correo a ese destinatario 
            }

            catch (Exception e2)
            {
                // No me pude conectar al servicio del mail
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
            Response.Redirect("/WebForms/Login.aspx");
        }

    }
}