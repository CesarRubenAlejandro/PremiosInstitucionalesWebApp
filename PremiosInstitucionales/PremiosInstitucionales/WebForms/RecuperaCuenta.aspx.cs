using PremiosInstitucionales.DBServices.Recuperar;
using System;
using System.Text.RegularExpressions;
using System.Web.UI;

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
                Regex regexNumero = new Regex(@".*\d.*");
                Regex regexLetra = new Regex(@".*[a-zA-z].*");
                Match matchNumero = regexNumero.Match(password1);
                Match matchLetra = regexLetra.Match(password1);
                if (password1.Length < 6 || !matchNumero.Success || !matchLetra.Success)
                {
                    showErrorMsg("Error");
                    Mensaje.Text = "La contraseña debe ser de al menos 6 caracteres <br/> y contener al menos un número y una letra.";
                }
                else
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
            }
            else
            {
                showErrorMsg("Error");
                Mensaje.Text = "Contraseñas no coinciden.";
            }
            if (sePudo)
            {
                showErrorMsg("Aviso");
                Mensaje.Text = "Contraseña cambiada exitosamente.";
            }
            else if (contrasenas)
            {
                showErrorMsg("Error");
                Mensaje.Text = "Error interno.";
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
    }
}