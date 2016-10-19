using PremiosInstitucionales.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PremiosInstitucionales
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session[StringValues.RolSesion] != null)
            {
                // ya ha iniciado sesion, por lo tanto se debe desplegar el boton de Logout
                loginItem.Visible = false;
                logoutItem.Visible = true;
            } else
            {
                loginItem.Visible = true;
                logoutItem.Visible = false;
            }
        }

        protected void LogoutBttn_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("/WebForms/Login.aspx");
        }
    }
}