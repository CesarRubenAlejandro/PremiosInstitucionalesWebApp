using PremiosInstitucionales.DBServices.Convocatoria;
using PremiosInstitucionales.Entities.Models;
using PremiosInstitucionales.Values;
using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PremiosInstitucionales.WebForms
{
    public partial class AdministraPremios : System.Web.UI.Page
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

                LoadAwards();
            }
        }

        private void ResetFields()
        {
            tbAwardTitle.Text = "";
            tbAwardDescription.Text = "";
        }

        private void LoadAwards()
        {
            //awardList.Controls.Add();
            tbAwardDescription.Attributes.Add("MaxLength", "500");
            var premios = ConvocatoriaService.GetAllPremios();
            foreach (var p in premios)
            {
                // Div encargado del responsivo
                Panel pCol = new Panel();
                pCol.CssClass = "col-md-4 item-list";

                // Div encargado del diseño
                Panel pCreateItem = new Panel();
                pCreateItem.CssClass = "create-item";

                // Div encargado de la parte superior (imagen)
                Panel pUserHeader = new Panel();
                pUserHeader.CssClass = "award-header";

                Panel pAwardImage = new Panel();
                pAwardImage.CssClass = "award-thumbnail";
                if(p.NombreImagen != null)
                {
                    pAwardImage.Style.Add("background-image", "url(/AwardPictures/" + p.NombreImagen + ")");
                }
                else
                {
                    pAwardImage.Style.Add("background-image", "url(/Resources/img/default-award.png)");
                }

                pUserHeader.Controls.Add(pAwardImage);

                // Div encargado de la parte inferior (titulo)
                Panel pAwardTitle = new Panel();
                pAwardTitle.CssClass = "award-description-fix text-center";

                LiteralControl spanTitle = new LiteralControl("<span class=\"award-description\"> " + p.Nombre + "</span>");

                pAwardTitle.Controls.Add(spanTitle);

                pCreateItem.Controls.Add(pUserHeader);
                pCreateItem.Controls.Add(pAwardTitle);

                pCol.Controls.Add(pCreateItem);

                // Agregar al panel principal
                awardList.Controls.Add(new LiteralControl("<a href=AdministraConvocatorias.aspx?p=" + p.cvePremio + ">"));
                awardList.Controls.Add(pCol);
                awardList.Controls.Add(new LiteralControl("</a>"));
            }
        }

        protected void EnviarBtn_Click(object sender, EventArgs e)
        {
            if (tbAwardTitle.Text.Length > 0)
            {
                CreateAward();
            }
            Response.Redirect("AdministraPremios.aspx");

        }

        private void CreateAward()
        {
            PI_BA_Premio premio = new PI_BA_Premio();
            premio.cvePremio = Guid.NewGuid().ToString();
            premio.Nombre = tbAwardTitle.Text;
            premio.Descripcion = tbAwardDescription.Text;
            premio.NombreImagen = UploadImage();
            premio.UsuarioCreacion = Session[StringValues.CorreoSesion].ToString();
            premio.UsuarioEdicion = Session[StringValues.CorreoSesion].ToString();
            premio.FechaCreacion = DateTime.Now;
            premio.FechaEdicion = DateTime.Now;
            ConvocatoriaService.CreatePremio(premio);

            ResetFields();
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
    }
}