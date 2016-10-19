using PremiosInstitucionales.DBServices.Login;
using PremiosInstitucionales.Values;
using System;

namespace PremiosInstitucionales.WebForms
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {

            String user = TextBox1.Text;
            String password = TextBox2.Text;

            //Checar si existe en todas las tablas y compara contrasena
            var tipoUsuario = LoginService.GetUsuario(user, password);

            //Crear sesion o decir que no existe
            if (tipoUsuario == StringValues.RolIncorrecto)
            {
                Label1.Text = "Usuario/Contraseña incorrectos";
                Label1.Visible = true;
            }
            else if (tipoUsuario == StringValues.RolNotFound)
            {
                Label1.Text = "Usuario no encontrado";
                Label1.Visible = true;
            }
            else
            {
                if (tipoUsuario == StringValues.RolCandidato)
                {
                    var candidato = LoginService.GetCandidato(user);
                    Session[StringValues.CorreoSesion] = candidato.Correo;
                    Session[StringValues.RolSesion] = StringValues.RolCandidato;
                    Response.Redirect("InicioCandidato.aspx");
                }
                else if (tipoUsuario == StringValues.RolJuez)
                {
                    var juez = LoginService.GetJuez(user);
                    Session[StringValues.CorreoSesion] = juez.Correo;
                    Session[StringValues.RolSesion] = StringValues.RolJuez;
                    Response.Redirect("InicioJuez.aspx");
                }
                else if (tipoUsuario == StringValues.RolAdmin)
                {
                    var administrador = LoginService.GetAdministrador(user);
                    Session[StringValues.CorreoSesion] = administrador.Correo;
                    Session[StringValues.RolSesion] = StringValues.RolAdmin;
                    Response.Redirect("InicioAdministrador.aspx");
                }

            }

        }

    }


}