using PremiosInstitucionales.DBServices.Registro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PremiosInstitucionales.WebForms
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String password1 = PasswordTextBox.Text;
            String password2 = ConfirmPasswordTextBox.Text;

            if (password1.Equals(password2) && RegistroService.Registrar(EmailTextBox.Text, password1))
            {
                //exito redireccionar pagina de inicio
                Response.Redirect("login.aspx");
            }
            else
            {
                //fracaso
                FracasoLbl.Visible = true;
            }
        }
    }
}