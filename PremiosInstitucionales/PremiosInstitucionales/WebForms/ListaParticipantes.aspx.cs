using PremiosInstitucionales.DBServices.Convocatoria;
using PremiosInstitucionales.DBServices.Evaluacion;
using PremiosInstitucionales.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PremiosInstitucionales.Entities.Models;

namespace PremiosInstitucionales.WebForms
{
    public partial class ListaParticipantes : System.Web.UI.Page
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
                        Response.Redirect("Login.aspx");
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }

                string sCategoriaID = Request.QueryString["c"];
                string sMail = Session[StringValues.CorreoSesion].ToString();
                if (sCategoriaID != null && sMail != null)
                {
                    CargarAplicaciones(sMail, sCategoriaID);
                }
            }
        }

        private void CargarAplicaciones(string sMail, string sCategoriaID)
        {
            var listaCategorias = EvaluacionService.GetCategoriaByJuez(sMail);
            bool bValidJudge = CheckValidCategory(listaCategorias, sCategoriaID);

            if (bValidJudge)
            {
                // obtener aplicaciones para cierta categoria
                var aplicacionesACategoria = ConvocatoriaService.ObtenerAplicacionesPorCategoria(sCategoriaID);

                // obtener candidatos ligados a estas aplicaciones
                var listaCandidatos = ConvocatoriaService.JuezObtenerCandidatosPorAplicaciones(aplicacionesACategoria);
                foreach(var cand in listaCandidatos)
                {
                    TableRow tr = new TableRow();
                    tr.Attributes.Add("onclick", "window.open('EvaluaAplicacion.aspx?a=" + cand.Key.cveAplicacion + "');");
                    // profile image column
                    TableCell tdIP = new TableCell();
                    tdIP.CssClass = "dt-profile-pic";

                    Image ipImage = new Image();
                    ipImage.ImageUrl = "https://x1.xingassets.com/assets/frontend_minified/img/users/nobody_m.original.jpg";
                    ipImage.CssClass = "avatar img-circle";
                    ipImage.AlternateText = "avatar";
                    ipImage.Style.Add("max-width", "28px");

                    tdIP.Controls.Add(ipImage);

                    // name column
                    TableCell tdName = new TableCell();
                    tdName.Text = cand.Value.Nombre;

                    // last name column
                    TableCell tdLastName = new TableCell();
                    tdLastName.Text = cand.Value.Apellido;

                    // status column
                    TableCell tdStatus = new TableCell();
                    tdStatus.Style.Add("color", "#f44336");
                    LiteralControl lcStatus = new LiteralControl("<strong> <div style=\"display: none; \"> 2 </div> Nuevo </strong>");
                    tdStatus.Controls.Add(lcStatus);

                    tr.Controls.Add(tdIP);
                    tr.Controls.Add(tdName);
                    tr.Controls.Add(tdLastName);
                    tr.Controls.Add(tdStatus);

                    listaParticipantesTableBody.Controls.Add(tr);
                }
                
            }
            else
            {
                Response.Redirect("InicioJuez.aspx");
            }
        }

        private bool CheckValidCategory(List<PI_BA_Categoria> ltCategories, string sCategoryID)
        {
            foreach (var category in ltCategories)
            {
                if (category.cveCategoria.Equals(sCategoryID))
                {
                    return true;
                }
            }
            return false;
        }
    }
}