using PremiosInstitucionales.DBServices.InformacionPersonalCandidato;
using PremiosInstitucionales.Values;
using System;

namespace PremiosInstitucionales.WebForms
{
    public partial class InicioCandidato : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // revisar la primera vez que se carga la pagina que se haya iniciado sesion con cuenta de candidato
                if (Session[StringValues.RolSesion] != null)
                {
                    if (Session[StringValues.RolSesion].ToString() != StringValues.RolCandidato)
                        // si no es candidato, redireccionar a login
                        Response.Redirect("~/WebForms/Login.aspx", false);
                }
                else
                {
                    Response.Redirect("~/WebForms/Login.aspx", false);
                }

                LoadMessage();
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
        }
    }
}