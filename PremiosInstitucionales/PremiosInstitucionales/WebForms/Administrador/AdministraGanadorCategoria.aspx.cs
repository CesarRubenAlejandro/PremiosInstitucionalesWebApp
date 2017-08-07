using PremiosInstitucionales.DBServices.Aplicacion;
using PremiosInstitucionales.DBServices.Evaluacion;
using PremiosInstitucionales.DBServices.InformacionPersonalCandidato;
using PremiosInstitucionales.Entities.Models;
using PremiosInstitucionales.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PremiosInstitucionales.WebForms
{
    public partial class AdministraGanadorCategoria : System.Web.UI.Page
    {
        MP_Global MasterPage = new MP_Global();
        protected void Page_Load(object sender, EventArgs e)
        {
            MasterPage = (MP_Global)Page.Master;

            if (!IsPostBack)
            {
                // revisar la primera vez que se carga la pagina que se haya iniciado sesion con cuenta de admin
                if (Session[StringValues.RolSesion] != null)
                {
                    if (Session[StringValues.RolSesion].ToString() != StringValues.RolAdmin)
                    {
                        // si no es admin, redireccionar a inicio general
                        Response.Redirect("~/WebForms/Login.aspx", false);
                    }
                }
                else
                {
                    Response.Redirect("~/WebForms/Login.aspx", false);
                }
            }

            var categoria = Request.QueryString["c"];
            if (categoria != null)
            {
                LoadCandidatesWithEvaluations(categoria);
            }

            String statusGanador = Request.QueryString["sg"];
            String statusVeredicto = Request.QueryString["sv"];

            // Mensaje si pude asignar a un ganador
            if (statusGanador != null)
            {
                if (statusGanador == "success") MasterPage.ShowMessage("Aviso", "Se ha asignado al ganador con éxito.");
                else if (statusGanador == "failed") MasterPage.ShowMessage("Error", "El servidor encontró un error al procesar la solicitud.");
            }

            // Mensaje si se pudieron mandar los correos del veredicto final
            if (statusVeredicto != null)
            {
                if (statusVeredicto == "success") MasterPage.ShowMessage("Aviso", "Los correos se enviaron exitosamente.");
                else if (statusVeredicto == "failed") MasterPage.ShowMessage("Error", "El servidor encontró un error al procesar la solicitud.");
            }
        }

        private void LoadCandidatesWithEvaluations(string categoriaId)
        {
            var aplicaciones = AplicacionService.GetAplicacionesByCategoria(categoriaId);
            var categoria = AplicacionService.GetCategoriaByClaveCategoria(categoriaId);
            var premio = AplicacionService.GetPremioByClaveCategoria(categoriaId);

            if (categoria == null && premio == null)
            {
                return;
            }

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

                    // profile image column
                    TableCell tdIP = new TableCell();
                    tdIP.CssClass = "dt-profile-pic";

                    Image ipImage = new Image();
                    if (cand.NombreImagen != null)
                    {
                        ipImage.ImageUrl = "/ProfilePictures/" + cand.NombreImagen;
                    }
                    else
                    {
                        ipImage.ImageUrl = "/Resources/img/default-pp.jpg";
                    }
                    ipImage.CssClass = "avatar img-circle";
                    ipImage.AlternateText = "avatar";
                    ipImage.Style.Add("width", "28px");
                    ipImage.Style.Add("height", "28px");

                    tdIP.Controls.Add(ipImage);
                    tdIP.Attributes.Add("onclick", "window.open('ObservaAplicacion.aspx?a=" + app.cveAplicacion + "');");

                    // name column
                    TableCell tdName = new TableCell();
                    tdName.Text = cand.Nombre;
                    tdName.Attributes.Add("onclick", "window.open('ObservaAplicacion.aspx?a=" + app.cveAplicacion + "');");

                    // last name column
                    TableCell tdLastName = new TableCell();
                    tdLastName.Text = cand.Apellido;
                    tdLastName.Attributes.Add("onclick", "window.open('ObservaAplicacion.aspx?a=" + app.cveAplicacion + "');");

                    TableCell tdCantEvals = new TableCell();
                    tdCantEvals.Text = evaluaciones.Count.ToString();
                    tdCantEvals.Attributes.Add("onclick", "window.open('ObservaAplicacion.aspx?a=" + app.cveAplicacion + "');");

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
                        LiteralControl lcCalificacion = new LiteralControl("<strong> <div style=\"display: none; \"> " + (prom - 100) + " </div> " + prom + " </strong>");
                        tdCalificacion.Controls.Add(lcCalificacion);
                    }
                    else
                    {
                        tdCalificacion.Style.Add("color", "#f44336");
                        LiteralControl lcCalificacion = new LiteralControl("<strong> <div style=\"display: none; \"> 1000 </div> Sin evaluaciones </strong>");
                        tdCalificacion.Controls.Add(lcCalificacion);
                    }
                    tdCalificacion.Attributes.Add("onclick", "window.open('ObservaAplicacion.aspx?a=" + app.cveAplicacion + "');");

                    TableCell tdGanador = new TableCell();
                    tdGanador.CssClass = "dt-profile-pic";
                    LiteralControl lcGanador = new LiteralControl("<center><input type=\"radio\" name=\"ganador\" value=\"" + app.cveAplicacion + "\"/></center>");
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

            // Mostrar Botones / Ganador
            if (categoria.cveAplicacionGanadora != null)
            {
                ClientScript.RegisterStartupScript(GetType(), "svf", "showVeredictoFinal();", true);
                ClientScript.RegisterStartupScript(GetType(), "sag", "showAsignarGanador();", true);
                ClientScript.RegisterStartupScript(GetType(), "sg", "selectGanador('" + categoria.cveAplicacionGanadora + "');", true);
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
            Response.Redirect("AdministraGanadores.aspx", false);
        }

        protected void GanadorBtn_Click(object sender, EventArgs e)
        {
            var cveAppSeleccionada = hiddenControl.Value;
            var cveCategoria = Request.QueryString["c"];

            try
            {
                AplicacionService.AsignarGanadorCategoria(cveCategoria, cveAppSeleccionada);
                Response.Redirect("AdministraGanadorCategoria.aspx?c=" + cveCategoria + "&sg=success", false);
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                Response.Redirect("AdministraGanadorCategoria.aspx?c=" + cveCategoria + "&sg=failed", false);
            }
        }

        private String getMails()
        {
            String Mails = "";

            var cveCategoria = Request.QueryString["c"];
            List<PI_BA_Aplicacion> TotalApps = AplicacionService.GetAplicacionesByCategoria(cveCategoria);
            List<PI_BA_Aplicacion> AcepApps = new List<PI_BA_Aplicacion>();

            // Obtengo todas las aplicaciones aceptadas de dicha categoria
            foreach (PI_BA_Aplicacion App in TotalApps)
            {
                if (App.Status == StringValues.Aceptado)
                {
                    AcepApps.Add(App);
                }
            }

            // Concateno mails usando comas
            foreach (PI_BA_Aplicacion App in AcepApps)
            {
                Mails += "," + App.PI_BA_Candidato.Correo;
            }

            // Elimino primer coma
            return Mails.Substring(1, Mails.Length - 1);
        }

        protected void VeredictoBtn_Click(object sender, EventArgs e)
        {
            String correoSender = "empresa.ejemplo.mail@gmail.com";
            String pswSender = "proyectointegrador";
            String toMails = getMails();

            try
            {
                using (MailMessage mm = new MailMessage(correoSender, toMails))
                {
                    mm.Subject = "Veredicto Final.";
                    mm.IsBodyHtml = true;
                    var bodyContent = "Ya existe un ganador";
                    try
                    {
                        mm.Body = bodyContent;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential(correoSender, pswSender);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        smtp.Send(mm);
                    }
                    catch (Exception Ex2)
                    {
                        // No pude enviar el correo a ese destinatario
                        Console.WriteLine("Catched Exception: " + Ex2.Message + Environment.NewLine);
                        Response.Redirect("AdministraGanadorCategoria.aspx?c=" + Request.QueryString["c"] + "&sv=failed", false);
                    }
                }
                // Ya envie el correo a ese destinatario 
                Response.Redirect("AdministraGanadorCategoria.aspx?c=" + Request.QueryString["c"] + "&sv=success", false);
            }
            catch (Exception Ex)
            {
                // No me pude conectar al servicio del mail
                Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                Response.Redirect("AdministraGanadorCategoria.aspx?c=" + Request.QueryString["c"] + "&sv=failed", false);
            }
        }
    }
}