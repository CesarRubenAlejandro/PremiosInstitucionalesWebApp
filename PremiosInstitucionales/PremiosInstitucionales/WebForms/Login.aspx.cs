using PremiosInstitucionales.DBServices.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            if (tipoUsuario == "incorrect")
            {
                Label1.Text = "Usuario/Contraseña incorrectos";
                Label1.Visible = true;
            }
            else if (tipoUsuario == "notFound")
            {
                Label1.Text = "Usuario no encontrado";
                Label1.Visible = true;
            }
            else
            {
                if (tipoUsuario == "candidato")
                {
                    var candidato = LoginService.GetCandidato(user);
                    Session["correo"] = candidato.Correo;
                    Response.Redirect("InicioCandidato.aspx");
                }
                else if (tipoUsuario == "juez")
                {
                    var juez = LoginService.GetJuez(user);
                    Session["correo"] = juez.Correo;
                    Response.Redirect("InicioJuez.aspx");
                }
                else if (tipoUsuario == "administrador")
                {
                    var administrador = LoginService.GetAdministrador(user);
                    Session["correo"] = administrador.Correo;
                    Response.Redirect("InicioAdministrador.aspx");
                }

            }

        }

    }


}