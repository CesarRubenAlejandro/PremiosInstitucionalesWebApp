using PremiosInstitucionales.DBServices.Aplicacion;
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
    public partial class AdministraCategorias : System.Web.UI.Page
    {
        List<string> ltColors = new List<string> { "#f44336", "#4caf50", "#2196f3", "#ffc107" };
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string sConvocatoriaID = Request.QueryString["c"];
                if (sConvocatoriaID != null)
                {
                    var convocatoria = ConvocatoriaService.GetConvocatoriaById(sConvocatoriaID);
                    if(convocatoria != null)
                    {
                        var premio = ConvocatoriaService.GetPremioById(convocatoria.cvePremio);

                        if (premio != null)
                        {
                            LoadCategories(premio, convocatoria);
                            return;
                        }
                    }
                }
                Response.Redirect("inicioAdmin.aspx");
            }
        }

        private void LoadCategories(PI_BA_Premio premio, PI_BA_Convocatoria convocatoria)
        {
            litTituloPremio.Text = "<h4> <strong> Premio: </strong> " + premio.Nombre + " </h4>";
            litTituloConvocatoria.Text = "<h5> <strong> Convocatoria: </strong> " + convocatoria.TituloConvocatoria + " </h5>";

            var categorias = ConvocatoriaService.GetCategoriasByConvocatoria(convocatoria.cveConvocatoria);
            int iCounter = 0;
            foreach(var c in categorias)
            {
                // Div encargado del responsivo
                Panel pCol = new Panel();
                pCol.CssClass = "col-md-4 item-list text-center";

                // Div encargado de la caja de categoria
                Panel pItem = new Panel();
                string sColor = ltColors[iCounter % ltColors.Count];
                pItem.CssClass = "create-item item-description-fix";
                pItem.Style.Add("background-color", sColor);

                // h5 encargado de mostrar el nombre de la categoria
                LiteralControl lcNombreCategoria = new LiteralControl("<h5 class=\"item-description\"> " + c.Nombre + "</h5>");

                pItem.Controls.Add(lcNombreCategoria);
                pCol.Controls.Add(pItem);

                String sFormaID = AplicacionService.GetFormByCategoria(c.cveCategoria).cveForma;
                categoryList.Controls.Add(new LiteralControl("<a href=AdministraForma.aspx?p=" + sFormaID + ">"));
                categoryList.Controls.Add(pCol);
                categoryList.Controls.Add(new LiteralControl("</a>"));

                iCounter++;
            }
        }

        protected void EnviarBtn_Click(object sender, EventArgs e)
        {
            if (tbCategoryTitle.Text.Length > 0)
            {
                CreateCategory();
            }
            Response.Redirect("AdministraCategorias.aspx?c=" + Request.QueryString["c"]);

        }

        private void CreateCategory()
        {
            PI_BA_Categoria categoria = new PI_BA_Categoria();
            categoria.cveCategoria = Guid.NewGuid().ToString();
            categoria.Nombre = tbCategoryTitle.Text;
            categoria.cveConvocatoria = Request.QueryString["c"];
            ConvocatoriaService.CreateCategoria(categoria);

            PI_BA_Forma forma = new PI_BA_Forma();
            forma.cveForma = Guid.NewGuid().ToString();
            forma.cveCategoria = categoria.cveCategoria;
            ConvocatoriaService.CreateForma(forma);

            ResetFields();
        }

        private void ResetFields ()
        {
            tbCategoryTitle.Text = "";
        }
    }
}