﻿using PremiosInstitucionales.Values;
using System;

namespace PremiosInstitucionales.WebForms
{
    public partial class InicioCandidato : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // revisar la primera vez que se carga la pagina que se haya iniciado sesion con cuenta de admin
                if (Session[StringValues.RolSesion] != null)
                {
                    if (Session[StringValues.RolSesion].ToString() != StringValues.RolCandidato)
                        // si no es candidato, redireccionar a login
                        Response.Redirect("Login.aspx");
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }
    }
}