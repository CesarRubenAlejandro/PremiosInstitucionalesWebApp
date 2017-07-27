using PremiosInstitucionales.DBServices.Aplicacion;
using PremiosInstitucionales.DBServices.Convocatoria;
using PremiosInstitucionales.Entities.Models;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using PremiosInstitucionales.Values;

namespace PremiosInstitucionales.WebForms
{
    public partial class AdministraCategorias : System.Web.UI.Page
    {
        List<string> ltColors = new List<string> { "#f44336", "#4caf50", "#2196f3", "#ffc107" };
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
            litTituloPremio.Text = premio.Nombre;
            TituloNuevaConvocatoriaTB.Text = convocatoria.TituloConvocatoria;

            String fInicio = "'" + convocatoria.FechaInicio.ToString().Substring(0, 10) + "' ,";
            String fFin =       "'" + convocatoria.FechaFin.ToString().Substring(0, 10) + "' ,";
            String fVeredicto = "'" + convocatoria.FechaVeredicto.ToString().Substring(0, 10) + "'";

            ClientScript.RegisterStartupScript(GetType(), "sD", "setDates("+ fInicio + fFin + fVeredicto +");", true);

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
                categoryList.Controls.Add(new LiteralControl("<a href=AdministraFormulario.aspx?p=" + sFormaID + ">"));
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

        protected void BackBtn_Click(object sender, EventArgs e)
        {
            string sConvocatoriaID = Request.QueryString["c"];
            var convocatoria = ConvocatoriaService.GetConvocatoriaById(sConvocatoriaID);
            Response.Redirect("AdministraConvocatorias.aspx?p="+convocatoria.cvePremio);
        }

        private void CreateCategory()
        {
            PI_BA_Categoria categoria = new PI_BA_Categoria();
            categoria.cveCategoria = Guid.NewGuid().ToString();
            categoria.Nombre = tbCategoryTitle.Text;
            categoria.cveConvocatoria = Request.QueryString["c"];
            categoria.FechaCreacion = DateTime.Now;
            categoria.UsuarioCreacion = Session[StringValues.CorreoSesion].ToString();
            categoria.FechaEdicion = DateTime.Now;
            categoria.UsuarioEdicion = Session[StringValues.CorreoSesion].ToString();
            ConvocatoriaService.CreateCategoria(categoria);

            PI_BA_Forma forma = new PI_BA_Forma();
            forma.cveForma = Guid.NewGuid().ToString();
            forma.cveCategoria = categoria.cveCategoria;
            forma.FechaCreacion = DateTime.Now;
            forma.UsuarioCreacion = Session[StringValues.CorreoSesion].ToString();
            forma.FechaEdicion = DateTime.Now;
            forma.UsuarioEdicion = Session[StringValues.CorreoSesion].ToString();
            ConvocatoriaService.CreateForma(forma);

            ResetFields();
        }

        protected void GuardarBttn_Click(object sender, EventArgs e)
        {
            // Obtener el obj convocatoria actual
            var cvEditada = ConvocatoriaService.GetConvocatoriaById(Request.QueryString["c"]);

            // Actualizar los campos que el admin haya cambiado
            cvEditada.TituloConvocatoria = TituloNuevaConvocatoriaTB.Text.ToString();
            cvEditada.FechaInicio = DateTime.ParseExact(String.Format("{0}", Request.Form["FechaInicioNuevaConvo"]), "MM/dd/yyyy",
                                       System.Globalization.CultureInfo.InvariantCulture);
            cvEditada.FechaFin = DateTime.ParseExact(String.Format("{0}", Request.Form["FechaFinNuevaConvo"]), "MM/dd/yyyy",
                           System.Globalization.CultureInfo.InvariantCulture);
            cvEditada.FechaVeredicto = DateTime.ParseExact(String.Format("{0}", Request.Form["FechaVeredicto"]), "MM/dd/yyyy",
                           System.Globalization.CultureInfo.InvariantCulture);
            cvEditada.FechaEdicion = DateTime.Now;
            cvEditada.UsuarioEdicion = Session[StringValues.CorreoSesion].ToString();

            // guardar convocatoria editada
            ConvocatoriaService.ActualizarConvocatoria(cvEditada);

            // forzar el refresh de la pagina para traer los cambios
            Response.Redirect("AdministraCategorias.aspx?c=" + Request.QueryString["c"]);
        }
        private void ResetFields ()
        {
            tbCategoryTitle.Text = "";
        }
    }
}