using PremiosInstitucionales.DBServices.InformacionPersonalJuez;
using PremiosInstitucionales.Values;
using System;

namespace PremiosInstitucionales.WebForms
{

    public partial class InicioJuez : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // revisar la primera vez que se carga la pagina que se haya iniciado sesion con cuenta de juez
                if (Session[StringValues.RolSesion] != null)
                {
                    if (Session[StringValues.RolSesion].ToString() != StringValues.RolJuez)
                        // si no es juez, redireccionar a inicio general
                        Response.Redirect("~/WebForms/Login.aspx");
                }
                else
                {
                    Response.Redirect("~/WebForms/Login.aspx");
                }

                LoadMessage();
            }
        }

        private void LoadMessage()
        {
            var correo = Session[StringValues.CorreoSesion].ToString();
            var juez = InformacionPersonalJuezService.GetJuezByCorreo(correo);

            if (juez == null)
                Response.Redirect("~/WebForms/Login.aspx");

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