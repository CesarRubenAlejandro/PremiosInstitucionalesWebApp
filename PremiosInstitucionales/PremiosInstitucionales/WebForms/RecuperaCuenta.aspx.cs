﻿using PremiosInstitucionales.DBServices.Recuperar;
using System;
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
                    MasterPage.showErrorMsg("Error", "La contraseña debe ser de al menos 6 caracteres <br/> y contener al menos un número y una letra.");
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
                MasterPage.showErrorMsg("Error", "Contraseñas no coinciden.");
            }
            if (sePudo)
            {
                MasterPage.showErrorMsg("Aviso", "Contraseña cambiada exitosamente.");
            }
            else if (contrasenas)
            {
                MasterPage.showErrorMsg("Error", "Error interno.");
            }
        }
    }
}