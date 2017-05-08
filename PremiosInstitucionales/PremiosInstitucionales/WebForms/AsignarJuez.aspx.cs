using PremiosInstitucionales.DBServices.InformacionPersonalCandidato;
using PremiosInstitucionales.DBServices.InformacionPersonalJuez;
using PremiosInstitucionales.DBServices.Aplicacion;
using PremiosInstitucionales.DBServices.Convocatoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PremiosInstitucionales.Entities.Models;

namespace PremiosInstitucionales.WebForms
{
    public partial class AsignarJuez : System.Web.UI.Page
    {

        String idCategoria;

        protected void Page_Load(object sender, EventArgs e)
        {
            // obtener el premio usando el query string de su id
            idCategoria = Request.QueryString["c"];
            String nombrePremio = AplicacionService.GetPremioByClaveCategoria(idCategoria).Nombre;
            String nombreCategoria = AplicacionService.GetCategoriaByClaveCategoria(idCategoria).Nombre;

            nombrePremioCategoria.Controls.Add(new LiteralControl(
                "<h3> <strong> Premio: </strong>" +  nombrePremio + "</h3>" +
                "<h4> <strong> Categoria: </strong>" +  nombreCategoria + "</h4>"
            ));

            LoadJudgeTable();
        }

        private void LoadJudgeTable()
        {
            //litUsuarios.Text = "Jueces";
            var jueces = InformacionPersonalJuezService.GetJueces();
            string sType = Request.QueryString["t"];
            if (jueces != null)
            {
                foreach (var juez in jueces)
                {
                    TableRow tr = new TableRow();

                    // profile image column
                    TableCell tdIP = new TableCell();
                    tdIP.CssClass = "dt-profile-pic";
                    //tdIP.Attributes.Add("onclick", "window.open('AdministraInformacionPersonal.aspx?id=" + juez.cveJuez + "&t=" + sType + "');");

                    Image ipImage = new Image();
                    if (juez.NombreImagen != null)
                    {
                        ipImage.ImageUrl = "/ProfilePictures/" + juez.NombreImagen;
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

                    // name column
                    TableCell tdName = new TableCell();
                    tdName.Text = juez.Nombre;
                    //tdName.Attributes.Add("OnServerClick", "CheckJuez");

                    // last name column
                    TableCell tdLastName = new TableCell();
                    tdLastName.Text = juez.Apellido;
                    //tdLastName.Attributes.Add("onclick", "window.open('AdministraInformacionPersonal.aspx?id=" + juez.cveJuez + "&t=" + sType + "');");

                    TableCell tdEmail = new TableCell();
                    //tdEmail.Text = juez.Correo;

                    LiteralControl lHiddenValue = new LiteralControl("<span id=\"" + juez.cveJuez + "\">" + juez.Correo + "</span>");
                    tdEmail.Controls.Add(lHiddenValue);

                    //LiteralControl lcMailLink = new LiteralControl("<a href=\"mailto:" + juez.Correo + "?Subject=Premios%20Institucionales\" target=\"_top\"> " + juez.Correo + "</a>");
                    //tdEmail.Controls.Add(lcMailLink);

                    tr.Controls.Add(tdIP);
                    tr.Controls.Add(tdName);
                    tr.Controls.Add(tdLastName);
                    tr.Controls.Add(tdEmail);
                    if (!AplicacionService.GetJuecesIdsCategoria(idCategoria).Contains(juez.cveJuez))
                    {
                        listaJuecesTableBody.Controls.Add(tr);
                    }
                    else
                    {
                        listaJuezTableAsignadosBody.Controls.Add(tr);
                    }
                }
            }
        }

        protected void SaveChanges(object sender, EventArgs e)
        {
            List<String> idsJuecesAsigandos = hiddenControl.Value.Split(',').ToList();
            AplicacionService.RemoveJuezCategoria(idCategoria);

            if (idsJuecesAsigandos.Count > 0 && idsJuecesAsigandos!= null)
            {
                AplicacionService.AsignarJuecesCategoria(idCategoria, idsJuecesAsigandos);
            }
            
            Response.Redirect("AsignarJuez.aspx?c=" + idCategoria);
        }
    }
}