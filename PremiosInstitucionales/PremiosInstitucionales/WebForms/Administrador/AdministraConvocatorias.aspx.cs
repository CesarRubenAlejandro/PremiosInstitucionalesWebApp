using PremiosInstitucionales.DBServices.Convocatoria;
using PremiosInstitucionales.Entities.Models;
using PremiosInstitucionales.Values;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Web.UI;

namespace PremiosInstitucionales.WebForms
{
    public partial class AdministrarConvocatoria : System.Web.UI.Page
    {
        private PI_BA_Premio premioActual;
        MP_Global MasterPage = new MP_Global();
        protected void Page_Load(object sender, EventArgs e)
        {
            MasterPage = (MP_Global)Page.Master;

            // obtener el premio usando el query string de su id
            String idPremio = Request.QueryString["p"];
            premioActual = ConvocatoriaService.GetPremioById(idPremio);

            // Mensaje si pude editar los datos del premio
            switch (Request.QueryString["s"])
            {
                case "success":
                    MasterPage.ShowMessage("Aviso", "Los cambios fueron realizados con éxito.");
                    break;
                case "failed":
                    MasterPage.ShowMessage("Error", "El servidor encontró un error al procesar la solicitud.");
                    break;
            }

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
                                "<td>" + FormatearStringFecha(convocatoria.FechaInicio.ToString()) + "</td>" +
                                "<td>" + FormatearStringFecha(convocatoria.FechaFin.ToString()) + "</td>" +
                                "<td>" + FormatearStringFecha(convocatoria.FechaVeredicto.ToString()) + "</td>" +
                                ConvocatoriaStatus(convocatoria.FechaVeredicto) +
                            "</tr>"
                        ));
                    }
                }
                else
                {
                    Response.Redirect("inicioAdmin.aspx", false);
                }
            }
        }

        protected String FormatearStringFecha(String sFecha)
        {
            String returnValue = sFecha;

            if (sFecha[2] == '/')
            {
                if (sFecha[5] == '/')
                {
                    returnValue = (sFecha).Replace('/', '-').Substring(0, 10);
                }
                else
                {
                    returnValue = (sFecha.Substring(0, 3) + '0' + sFecha.Substring(3)).Replace('/', '-').Substring(0, 10);
                }
            }
            else if (sFecha[1] == '/')
            {
                if (sFecha[4] == '/')
                {
                    returnValue = (sFecha.Substring(0, 0) + '0' + sFecha.Substring(0)).Replace('/', '-').Substring(0, 10);
                }
                else
                {
                    var aux = sFecha.Substring(0, 0) + '0' + sFecha.Substring(0);
                    returnValue = (aux.Substring(0, 3) + '0' + aux.Substring(3)).Replace('/', '-').Substring(0, 10);
                }
            }

            return LocaleStringFecha(returnValue.Replace('/', '-').Substring(0, 10));
        }

        protected String LocaleStringFecha(String sFecha)
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

            if (currentCulture.Name.Substring(0, 2) == "en")
            {
                sFecha = sFecha.Substring(3, 2) + "-" + sFecha.Substring(0, 2) + "-" + sFecha.Substring(6, 4);
            }
            return sFecha;
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

            IFormatProvider FormatProvider = CultureInfo.InvariantCulture;
            String idParam = "{0}";

            nuevaConvo.FechaInicio = DateTime.ParseExact(String.Format(idParam, Request.Form["FechaInicioNuevaConvo"]), "dd-MM-yyyy", FormatProvider);
            nuevaConvo.FechaFin = DateTime.ParseExact(String.Format(idParam, Request.Form["FechaFinNuevaConvo"]), "dd-MM-yyyy", FormatProvider);
            nuevaConvo.FechaVeredicto = DateTime.ParseExact(String.Format(idParam, Request.Form["FechaVeredicto"]), "dd-MM-yyyy", FormatProvider);

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
                string sFormat = fileName.Substring(startIndex, endIndex).ToLower();
                string sNombreImagen = Guid.NewGuid().ToString() + sFormat;

                // Formatos Validos
                List<String> supportedFormats = new List<String>()
                {
                    ".png",
                    ".bmp",
                    ".jpeg",
                    ".jpg"
                };

                if (!supportedFormats.Contains(sFormat))
                {
                    MasterPage.ShowMessage("Error", "La imagen proporcionada debe estar en formato PNG o JPG.");
                    return "Error";
                }

                // Borrar imagen anterior
                if (premioActual.NombreImagen != null && premioActual.NombreImagen.Length > 0)
                {
                    // Delete previous image...
                    File.Delete(Server.MapPath("~/AwardPictures/") + premioActual.NombreImagen);
                }

                // Upload image to server
                FileUploadImage.PostedFile.SaveAs(Server.MapPath("~/AwardPictures/") + sNombreImagen);
                return sNombreImagen;
            }

            return null;
        }

        protected void BackBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdministraPremios.aspx", false);
        }

        protected void UpdateInfo(object sender, EventArgs e)
        {
            try
            {
                string imgUrl = UploadImage();

                if (imgUrl == null)
                {
                    imgUrl = premioActual.NombreImagen;
                }

                if (imgUrl != "Error")
                {
                    string user = Session[StringValues.CorreoSesion].ToString();
                    ConvocatoriaService.ActualizarPremio(premioActual.cvePremio, TituloPremioSeleccionado.Text, DescripcionPremioSeleccionado.Text, imgUrl, user);
                    Response.Redirect("AdministraConvocatorias.aspx?p=" + premioActual.cvePremio + "&s=" + "success", false);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                Response.Redirect("AdministraConvocatorias.aspx?p=" + premioActual.cvePremio + "&s=" + "failed", false);
            }
        }
    }
}