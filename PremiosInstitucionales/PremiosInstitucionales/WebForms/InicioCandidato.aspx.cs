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
                LoadMessage();
            }
        }

        private void LoadMessage()
        {
            var correo = Session[StringValues.CorreoSesion].ToString();
            var candidato = InformacionPersonalCandidatoService.GetCandidatoByCorreo(correo);

            if (candidato == null)
                Response.Redirect("Login.aspx");

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