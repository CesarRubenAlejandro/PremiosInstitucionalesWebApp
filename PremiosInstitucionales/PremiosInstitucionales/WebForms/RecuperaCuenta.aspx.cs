using PremiosInstitucionales.DBServices.Recuperar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PremiosInstitucionales.WebForms
{
    public partial class RecuperaCuenta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String password1 = PasswordTextBox.Text;
            String password2 = ConfirmPasswordTextBox.Text;
            bool sePudo = false;
            bool contrasenas = false;
            if (password1.Equals(password2))
            {
                contrasenas = true;
                String cve = Request.QueryString["codigo"];
                char tipo = cve[0];
                if (tipo == 'c')
                {
                    sePudo = RecuperarService.CambiarContrasenaCandidato(cve.Substring(1), password1);
                }
                else if (tipo == 'j')
                {
                    sePudo = RecuperarService.CambiarContrasenaJuez(cve.Substring(1), password1);
                }
                else if (tipo == 'a')
                {
                    sePudo = RecuperarService.CambiarContrasenaAdministrador(cve.Substring(1), password1);
                }
            }
            else
            {
                Mensaje.Text = "Contraseñas no coinciden";
            }
            if (sePudo)
            {
                Mensaje.Text = "Contraseña cambiada exitosamente";
                PasswordTextBox.Enabled = false;
                ConfirmPasswordTextBox.Enabled = false;
                Boton.Enabled = false;
                HyperLinkRegresar.Visible = true;
            }
            else if (contrasenas)
            {
                Mensaje.Text = "Error";
            }
            Mensaje.Visible = true;
        }
    }
}