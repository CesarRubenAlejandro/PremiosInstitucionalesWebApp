using PremiosInstitucionales.DBServices.Aplicacion;
using PremiosInstitucionales.DBServices.Evaluacion;
using PremiosInstitucionales.DBServices.InformacionPersonalCandidato;
using PremiosInstitucionales.Entities.Models;
using PremiosInstitucionales.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PremiosInstitucionales.WebForms
{
    public partial class AdministraGanadorCategoria : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // revisar la primera vez que se carga la pagina que se haya iniciado sesion con cuenta de admin
                if (Session[StringValues.RolSesion] != null)
                {
                    if (Session[StringValues.RolSesion].ToString() != StringValues.RolAdmin)
                        // si no es admin, redireccionar a inicio general
                        Response.Redirect("~/WebForms/Login.aspx");
                }
                else
                {
                    Response.Redirect("~/WebForms/Login.aspx");
                }

                var categoria = Request.QueryString["c"];
                if (categoria != null)
                {
                    LoadCandidatesWithEvaluations(categoria);
                    return;
                }
                Response.Redirect("InicioAdmin.aspx");
            }

        }

        private void LoadCandidatesWithEvaluations(string categoriaId)
        {
            var aplicaciones = AplicacionService.GetAplicacionesByCategoria(categoriaId);
            var categoria = AplicacionService.GetCategoriaByClaveCategoria(categoriaId);
            var premio = AplicacionService.GetPremioByClaveCategoria(categoriaId);

            if (categoria == null && premio == null)
                return;

            litTituloPremio.Text = "Premio " + premio.Nombre;
            litTituloCategoria.Text = "Categoría: " + categoria.Nombre;

            if (aplicaciones != null)
            {
                foreach (var app in aplicaciones)
                {
                    var evaluaciones = EvaluacionService.GetEvaluacionesByAplicacion(app.cveAplicacion);
                    if (evaluaciones == null)
                        break;

                    var cand = InformacionPersonalCandidatoService.GetCandidatoById(app.cveCandidato);

                    TableRow tr = new TableRow();
                    // tr.Attributes.Add("onclick", "window.open('Pending.aspx?a=" + cand.cveCandidato + "');");
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
                    tdName.Text = cand.Nombre;

                    // last name column
                    TableCell tdLastName = new TableCell();
                    tdLastName.Text = cand.Apellido;

                    TableCell tdCantEvals = new TableCell();
                    tdCantEvals.Text = evaluaciones.Count.ToString();

                    // status column
                    TableCell tdCalificacion = new TableCell();
                    double prom = GetPromedioEvaluaciones(evaluaciones);
                    if (prom >= 70)
                    {
                        tdCalificacion.Style.Add("color", "#4caf50");
                        LiteralControl lcCalificacion = new LiteralControl("<strong> <div style=\"display: none; \"> " + (prom - 100) + " </div> " + prom + " </strong>");
                        tdCalificacion.Controls.Add(lcCalificacion);
                    }
                    else if (prom >= 0)
                    {
                        tdCalificacion.Style.Add("color", "#f9a825");
                        LiteralControl lcStatus = new LiteralControl("<strong> <div style=\"display: none; \"> " + (prom - 100) + " </div> " + prom + " </strong>");
                        tdCalificacion.Controls.Add(lcStatus);
                    }
                    else
                    {
                        tdCalificacion.Style.Add("color", "#f44336");
                        LiteralControl lcStatus = new LiteralControl("<strong> <div style=\"display: none; \"> 1000 </div> Sin evaluaciones </strong>");
                        tdCalificacion.Controls.Add(lcStatus);
                    }

                    TableCell tdGanador = new TableCell();
                    tdGanador.CssClass = "dt-profile-pic";
                    LiteralControl lcGanador = new LiteralControl("<center><input type=\"radio\" name=\"ganador\" value=\"" + cand.Nombre + " " + cand.Apellido + "\"/></center>");
                    tdGanador.Controls.Add(lcGanador);

                    tr.Controls.Add(tdIP);
                    tr.Controls.Add(tdName);
                    tr.Controls.Add(tdLastName);
                    tr.Controls.Add(tdCalificacion);
                    tr.Controls.Add(tdCantEvals);
                    tr.Controls.Add(tdGanador);

                    listaParticipantesTableBody.Controls.Add(tr);
                }
            }
        }

        private double GetPromedioEvaluaciones(List<PI_BA_Evaluacion> evaluaciones)
        {
            double? result = evaluaciones.Average(eval => eval.Calificacion);
            if (result.HasValue)
                return result.Value;
            return -1;
        }

        protected void BackBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdministraGanadores.aspx");
        }
    }
}