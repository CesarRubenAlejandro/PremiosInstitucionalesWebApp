using PremiosInstitucionales.DBServices.Recuperar;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace PremiosInstitucionales.WebForms
{
    public partial class RecuperaCuenta : System.Web.UI.Page
    {
        MP_Login MasterPage = new MP_Login();
        protected void Page_Load(object sender, EventArgs e)
        {
            MasterPage = (MP_Login)Page.Master;
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
                    MasterPage.ShowMessage("Error", "La contraseña debe ser de al menos 6 caracteres <br/> y contener al menos un número y una letra.");
                }
                else
                {
                    contrasenas = true;
                    String cve = Request.QueryString["codigo"];
                    char tipo = cve[0];
                    if (tipo == 'c')
                    {
                        sePudo = RecuperarService.CambiarContrasenaCandidato(cve.Substring(1), sha256(password1));
                    }
                    else if (tipo == 'j')
                    {
                        sePudo = RecuperarService.CambiarContrasenaJuez(cve.Substring(1), sha256(password1));
                    }
                    else if (tipo == 'a')
                    {
                        sePudo = RecuperarService.CambiarContrasenaAdministrador(cve.Substring(1), sha256(password1));
                    }
                }      
            }
            else
            {
                MasterPage.ShowMessage("Error", "Contraseñas no coinciden.");
            }
            if (sePudo)
            {
                MasterPage.ShowMessage("Aviso", "Contraseña cambiada exitosamente.");
            }
            else if (contrasenas)
            {
                MasterPage.ShowMessage("Error", "Error interno.");
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