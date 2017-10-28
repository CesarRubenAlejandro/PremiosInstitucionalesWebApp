using PremiosInstitucionales.DBServices.InformacionPersonalCandidato;
using PremiosInstitucionales.Values;
using System;
using System.Web.UI;

namespace PremiosInstitucionales.WebForms
{
    public partial class InicioCandidato : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar si ya expiro la sesion
            if (Session.Contents.Count == 0)
            {
                Response.Redirect("~/WebForms/Error/Error401.aspx", false);
            }

            if (!IsPostBack)
            {
                // revisar la primera vez que se carga la pagina que se haya iniciado sesion con cuenta de candidato
                if (Session[StringValues.RolSesion] != null)
                {
                    if (Session[StringValues.RolSesion].ToString() != StringValues.RolCandidato)
                    {
                        // si no es candidato, redireccionar a login
                        Response.Redirect("~/WebForms/Login.aspx", false);
                    }
                    else
                    {
                        LoadMessage();
                    }
                }
                else
                {
                    Response.Redirect("~/WebForms/Login.aspx", false);

                }
            }
        }

        private void LoadMessage()
        {
            var correo = Session[StringValues.CorreoSesion].ToString();
            var candidato = InformacionPersonalCandidatoService.GetCandidatoByCorreo(correo);

            if (candidato == null)
                Response.Redirect("~/WebForms/Login.aspx", false);

            if (candidato.Nombre != null && candidato.Nombre.Length > 0)
            {
                litBienvenidoUsuario.Text = "<h1> Bienvenido, " + candidato.Nombre + " </h1>";
            }
            else
            {
                litBienvenidoUsuario.Text = "<h1> Bienvenido </h1>";
            }

            if (!candidato.FechaPrivacidadDatos.HasValue)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "openModal();", true);
            }
        }

        protected void EnviarBtn_Click(object sender, EventArgs e)
        {
            var candidato = InformacionPersonalCandidatoService.GetCandidatoByCorreo(Session[StringValues.CorreoSesion].ToString());
            if (candidato != null)
            {
                if (!candidato.FechaPrivacidadDatos.HasValue)
                {
                    candidato.FechaPrivacidadDatos = DateTime.Today.Date;
                }
                InformacionPersonalCandidatoService.UpdateCandidato(candidato);
            }
        }

        protected void CancelarBtn_Click(object sender, EventArgs e)
        {
            // Cierro la sesion actual
            Session.Abandon();

            // Redirecciono a la pagina de inicio de sesion
            Response.Redirect("~/WebForms/Login.aspx", false);
        }
    }
}