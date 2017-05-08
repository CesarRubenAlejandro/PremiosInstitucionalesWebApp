using PremiosInstitucionales.DBServices.InformacionPersonalJuez;
using PremiosInstitucionales.Values;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace PremiosInstitucionales.WebForms
{

    public partial class InicioJuez : System.Web.UI.Page
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
            var juez = InformacionPersonalJuezService.GetJuezByCorreo(correo);

            if (juez == null)
                Response.Redirect("Login.aspx");

            if(juez.Nombre != null && juez.Nombre.Length > 0)
            {
                litBienvenidoUsuario.Text = "<h1> Bienvenido, " + juez.Nombre + " </h1>";
            }
            else
            {
                litBienvenidoUsuario.Text = "<h1> Bienvenido </h1>";
            }
        }
    }
}