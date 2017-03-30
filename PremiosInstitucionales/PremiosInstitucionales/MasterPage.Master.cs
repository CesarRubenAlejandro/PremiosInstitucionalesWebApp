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
            {/*
                // ya ha iniciado sesion, por lo tanto se debe desplegar el boton de Logout
                LogoutCandidato.Visible = true;
                LogoutAdmin.Visible = true;
                LogoutJuez.Visible = true;

                // Revisar tipo de usuario
                if (Session[StringValues.RolSesion].ToString() == StringValues.RolAdmin)
                {
                    //Activar opciones del menu para Admin
                    NavCandidato.Visible = false;
                    NavAdmin.Visible = true;
                    NavJuez.Visible = false;
                    NavDefault.Visible = false;

                } else if (Session[StringValues.RolSesion].ToString() == StringValues.RolCandidato)
                {
                    //Activar opciones del menu para Candidato
                    NavCandidato.Visible = true;
                    NavAdmin.Visible = false;
                    NavJuez.Visible = false;
                    NavDefault.Visible = false;

                } else
                {
                    //Activar opciones del menu para Juez
                    NavCandidato.Visible = false;
                    NavAdmin.Visible = false;
                    NavJuez.Visible = true;
                    NavDefault.Visible = false;

                }
                */
            } 
        }

        protected void LogoutBttn_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("/WebForms/Login.aspx");
        }
    }
}