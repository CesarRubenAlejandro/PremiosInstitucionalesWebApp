using PremiosInstitucionales.DBServices.Convocatoria;
using PremiosInstitucionales.Entities.Models;
using PremiosInstitucionales.Values;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PremiosInstitucionales.WebForms
{
    public partial class AdministraGanadores : System.Web.UI.Page
    {

        List<string> ltColors = new List<string> { "#f44336", "#4caf50", "#2196f3", "#ffc107" };
        public struct CategoriasPendientes
        {
            public PI_BA_Premio pPremio;
            public List<PI_BA_Categoria> categorias;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar si ya expiro la sesion
            if (Session.Contents.Count == 0)
            {
                Response.Redirect("~/WebForms/Error/Error401.aspx", false);
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

                // Tab Categorias Pendientes
                var categoriesP = ConvocatoriaService.GetCategoriasPendientes();
                GetPendingCategories(categoriesP, "PanelCategoriasPendientes");

                // Tab Categorias Termindas
                var categories = ConvocatoriaService.GetCategorias();
                List<PI_BA_Categoria> categoriesT = new List<PI_BA_Categoria>();
                foreach (var item in categories)
                {
                    if (item.cveAplicacionGanadora != null)
                    {
                        categoriesT.Add(item);
                    }
                }
                GetPendingCategories(categoriesT, "PanelCategoriasTerminadas");
            }
        }

        private void GetPendingCategories(List<PI_BA_Categoria> categories, string panel)
        {
            // Por default Pendientes
            var panelControl = PanelCategoriasPendientes;
            if (panel == "PanelCategoriasTerminadas")
            {
                panelControl = PanelCategoriasTerminadas;
            }

            if (categories != null)
            {
                if (categories.Count > 0)
                {
                    List<CategoriasPendientes> ctgPendientes = new List<CategoriasPendientes>();
                    foreach (var c in categories)
                    {
                        var premioActual = ConvocatoriaService.GetPremioByCategoria(c.cveCategoria);
                        // si ya tengo el premio almenos una vez, agrego la categoria
                        if (CheckExistenceInList(premioActual, ctgPendientes))
                        {
                            foreach (var ctg in ctgPendientes)
                            {
                                if (ctg.pPremio.cvePremio.Equals(premioActual.cvePremio))
                                {
                                    ctg.categorias.Add(c);
                                }
                            }
                        }
                        else
                        {
                            // si no tengo es premio, agrego el premio con la categoria actual
                            CategoriasPendientes cp = new CategoriasPendientes();
                            cp.pPremio = premioActual;
                            cp.categorias = new List<PI_BA_Categoria>();
                            cp.categorias.Add(c);

                            ctgPendientes.Add(cp);
                        }
                    }

                    // render stuff
                    foreach (var c in ctgPendientes)
                    {
                        Panel main = new Panel();
                        main.CssClass = "col-md-10 col-md-offset-1";
                        main.Style.Add("margin-bottom", "12px");

                        LiteralControl lcPremio = new LiteralControl("<h4> Premio " + c.pPremio.Nombre + " </h4>");
                        LiteralControl lcCategoria = new LiteralControl("<h5> Categorías: </h5>");

                        Panel row = new Panel();
                        row.CssClass = "row";

                        short iCounter = 0;
                        foreach (var cat in c.categorias)
                        {
                            Panel col = new Panel();
                            col.CssClass = "col-md-4 item-list text-center";
                            col.Style.Add("margin-top", "0px");

                            Panel colItem = new Panel();
                            string sColor = ltColors[iCounter % ltColors.Count];
                            colItem.CssClass = "create-item item-description-fix";
                            colItem.Style.Add("background-color", sColor);

                            Panel pUserHeader = new Panel();
                            pUserHeader.CssClass = "award-header";
                            LiteralControl lcColItemCategory = new LiteralControl("<h5 class=\"item-description item-description-patch\">" + cat.Nombre
                                + " </h5>");
                            pUserHeader.Controls.Add(lcColItemCategory);

                            var convo = ConvocatoriaService.GetConvocatoriaById(cat.cveConvocatoria);

                            Panel pAwardTitle = new Panel();
                            pAwardTitle.CssClass = "award-description-fix text-center";
                            LiteralControl spanTitle = new LiteralControl("<span class=\"award-description\"> " + convo.TituloConvocatoria + "</span>");
                            pAwardTitle.Controls.Add(spanTitle);

                            colItem.Controls.Add(pUserHeader);
                            colItem.Controls.Add(pAwardTitle);
                            col.Controls.Add(colItem);

                            row.Controls.Add(new LiteralControl("<a href=AdministraGanadorCategoria.aspx?c=" + cat.cveCategoria + ">"));
                            row.Controls.Add(col);
                            row.Controls.Add(new LiteralControl("</a>"));

                            iCounter++;
                        }

                        main.Controls.Add(lcPremio);
                        main.Controls.Add(lcCategoria);
                        main.Controls.Add(row);

                        panelControl.Controls.Add(main);
                    }
                }

                else
                {
                    //desplegar letrero de no aplicaciones
                    HtmlControl divControl = new HtmlGenericControl("div");
                    divControl.Attributes.Add("class", "text-center");
                    divControl.Visible = true;
                    divControl.Controls.Add(new LiteralControl("<br /> <h4> No hay categorias que mostrar. </h4>"));
                    panelControl.Controls.Add(divControl);
                }
            }
        }

        private bool CheckExistenceInList(PI_BA_Premio premioId, List<CategoriasPendientes> ctgPendientes)
        {
            foreach (var ctg in ctgPendientes)
            {
                if (ctg.pPremio.cvePremio.Equals(premioId.cvePremio))
                {
                    return true;
                }
            }

            return false;
        }

        protected void BackBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("InicioAdmin.aspx", false);
        }
    }
}