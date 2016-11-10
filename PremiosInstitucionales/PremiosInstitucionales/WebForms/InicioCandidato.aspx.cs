using PremiosInstitucionales.DBServices.Aplicacion;
using PremiosInstitucionales.Values;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

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
                        Response.Redirect("Login.aspx");
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }

            var aplicaciones = AplicacionService.GetAplicacionesByCorreo(Session[StringValues.CorreoSesion].ToString());
            foreach(var ap in aplicaciones)
            {
                //elegir mapa de status a desplegar
                if(ap.Status.Equals(Values.StringValues.Rechazado) || ap.Status.Equals(Values.StringValues.Modificado))
                {
                    //desplegar mapa que incluye rechazados
                }
                else
                {
                    //desplegar mapa normal
                    HtmlControl divControl = new HtmlGenericControl("div");
                   // divControl.Attributes.Add("id", lb.Items[i].Value);
                    divControl.Visible = true; // Not really necessary
                    this.Controls.Add(divControl);

                    divControl.Controls.Add(new LiteralControl("<span>Put whatever <em>HTML</em> code here.</span>"));
                }
            }

        }
    }
}