using PremiosInstitucionales.DBServices.Convocatoria;
using PremiosInstitucionales.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PremiosInstitucionales.WebForms
{
    public partial class AdministraPremios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadAwards();
        }

        private void ResetFields()
        {
            tbAwardTitle.Text = "";
            tbAwardDescription.Text = "";
        }

        private void LoadAwards()
        {
            //awardList.Controls.Add();
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
                pAwardImage.Style.Add("background-image", "url(/AwardPictures/" + p.NombreImagen + ")");

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
                CreateAward();
        }

        private void CreateAward()
        {
            PI_BA_Premio premio = new PI_BA_Premio();
            premio.cvePremio = Guid.NewGuid().ToString();
            premio.Nombre = tbAwardTitle.Text;
            premio.Descripcion = tbAwardDescription.Text;
            ConvocatoriaService.CreatePremio(premio);

            ResetFields();
        }
    }
}