using PremiosInstitucionales.DBServices.Convocatoria;
using PremiosInstitucionales.Entities.Models;
using PremiosInstitucionales.Values;
using System;
using System.IO;
using System.Web.UI;

namespace PremiosInstitucionales.WebForms
{
    public partial class AdministrarConvocatoria : System.Web.UI.Page
    {
        private PI_BA_Premio premioActual;

        protected void Page_Load(object sender, EventArgs e)
        {
            // obtener el premio usando el query string de su id
            String idPremio = Request.QueryString["p"];
            premioActual = ConvocatoriaService.GetPremioById(idPremio);

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

                if (premioActual != null)
                {
                    // Nombre & Descripcion Premio
                    TituloPremioSeleccionado.Text = premioActual.Nombre;
                    DescripcionPremioSeleccionado.Text = premioActual.Descripcion;

                    // Imagen Premio
                    if (premioActual.NombreImagen != null)
                    {
                        avatarImage.Style.Add("background-image", "url(/AwardPictures/" + premioActual.NombreImagen + ")");
                    }
                    else
                    {
                        avatarImage.Style.Add("background-image", "url(/Resources/img/default-award.png)");
                    }

                    // Cargar lista de convocatorias
                    var convocatoriasPremio = ConvocatoriaService.GetConvocatoriasPremio(idPremio);
                    foreach (var convocatoria in convocatoriasPremio)
                    {
                        Prueba.Controls.Add(new LiteralControl(
                            "<tr onclick=\"window.location.assign('AdministraCategorias.aspx?c=" + convocatoria.cveConvocatoria + "'); \">" +
                                "<td class=\"dt-profile-pic\">" +
                                     "<img src = \"/Resources/img/trophy.png\" class=\"avatar img-circle\" alt=\"avatar\" style=\"max-width: 28px;\">" +
                                "</td>" +
                                "<td>" + convocatoria.TituloConvocatoria.ToString() + "</td>" +
                                "<td>" + convocatoria.FechaInicio.ToString().Substring(0, 10) + "</td>" +
                                "<td>" + convocatoria.FechaFin.ToString().Substring(0, 10) + "</td>" +
                                "<td>" + convocatoria.FechaVeredicto.ToString().Substring(0, 10) + "</td>" +
                                ConvocatoriaStatus(convocatoria.FechaVeredicto) +
                            "</tr>"
                        ));
                    }
                }
            }
        }

        protected String ConvocatoriaStatus(DateTime? fechaVeredicto)
        {
            String color;
            String status;

            if(fechaVeredicto > DateTime.Today)
            {
                color = "#4caf50";
                status = "Abierta";
            }
            else
            {
                color = "#f44336";
                status = "Cerrada";
            }

            return "<td style = \"color:"+ color + "\">" +
                        "<strong>" +
                            "<div style=\"display: none;\"> 2 </div>" +
                            status +
                        "</strong>" +
                    "</td>";
        }

        protected void GuardarNuevaBttn_Click(object sender, EventArgs e)
        {
            // crear un nuevo objeto convocatoria y guardar sus datos
            var nuevaConvo = new PI_BA_Convocatoria();
            nuevaConvo.cveConvocatoria = Guid.NewGuid().ToString();
            nuevaConvo.TituloConvocatoria = TituloNuevaConvocatoriaTB.Text.ToString();

            nuevaConvo.FechaInicio = DateTime.ParseExact(String.Format("{0}", Request.Form["FechaInicioNuevaConvo"]), "MM/dd/yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);
            nuevaConvo.FechaFin = DateTime.ParseExact(String.Format("{0}", Request.Form["FechaFinNuevaConvo"]), "MM/dd/yyyy",
                           System.Globalization.CultureInfo.InvariantCulture);
            nuevaConvo.FechaVeredicto = DateTime.ParseExact(String.Format("{0}", Request.Form["FechaVeredicto"]), "MM/dd/yyyy",
                           System.Globalization.CultureInfo.InvariantCulture);
            nuevaConvo.FechaCreacion = DateTime.Now;
            nuevaConvo.UsuarioCreacion = Session[StringValues.CorreoSesion].ToString();
            nuevaConvo.FechaEdicion = DateTime.Now;
            nuevaConvo.UsuarioEdicion = Session[StringValues.CorreoSesion].ToString();
            // guardar nueva convocatoria
            ConvocatoriaService.CreateConvocatoria(premioActual.cvePremio, nuevaConvo);

            // limpiar campos de nueva convocatoria
            TituloNuevaConvocatoriaTB.Text = "";

            // forzar el refresh de la pagina para traer los cambios
            Response.Redirect("AdministraConvocatorias.aspx?p=" + premioActual.cvePremio);
        }

        private string UploadImage()
        {
            if (FileUploadImage.HasFile)
            {
                // Get filename
                string fileName = Path.GetFileName(FileUploadImage.PostedFile.FileName);

                // Get string image format (png, jpg, etc)
                var startIndex = fileName.LastIndexOf(".");
                var endIndex = fileName.Length - startIndex;
                string sFormat = fileName.Substring(startIndex, endIndex);
                string sNombreImagen = Guid.NewGuid().ToString() + sFormat;

                // Upload image to server
                FileUploadImage.PostedFile.SaveAs(Server.MapPath("~/AwardPictures/") + sNombreImagen);
                return sNombreImagen;
            }

            return null;
        }

        protected void UpdateInfo(object sender, EventArgs e)
        {
            string imgUrl = UploadImage();

            if (UploadImage() == null)
            {
                imgUrl = premioActual.NombreImagen;
            }

            string user = Session[StringValues.CorreoSesion].ToString();
            ConvocatoriaService.ActualizarPremio(premioActual.cvePremio, TituloPremioSeleccionado.Text, DescripcionPremioSeleccionado.Text, imgUrl, user);
            Response.Redirect("AdministraConvocatorias.aspx?p=" + premioActual.cvePremio);
        }


    }
}