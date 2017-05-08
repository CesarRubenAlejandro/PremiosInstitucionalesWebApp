using PremiosInstitucionales.DBServices.InformacionPersonalCandidato;
using PremiosInstitucionales.DBServices.InformacionPersonalJuez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PremiosInstitucionales.WebForms
{
    public partial class AdministraUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string sUserType = Request.QueryString["t"];
                if (sUserType != null)
                {
                    if (sUserType.Equals("juez"))
                    {
                        LoadJudgeTable();
                        return;
                    }
                    else if (sUserType.Equals("candidato"))
                    {
                        LoadCandidateTable();
                        return;
                    }
                }
                Response.Redirect("InicioAdmin.aspx");
            }
        }

        private void LoadJudgeTable()
        {
            litUsuarios.Text = "Jueces";
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
                    tdIP.Attributes.Add("onclick", "window.open('AdministraInformacionPersonal.aspx?id=" + juez.cveJuez + "&t=" + sType + "');");

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
                    tdName.Attributes.Add("onclick", "window.open('AdministraInformacionPersonal.aspx?id=" + juez.cveJuez + "&t=" + sType + "');");

                    // last name column
                    TableCell tdLastName = new TableCell();
                    tdLastName.Text = juez.Apellido;
                    tdLastName.Attributes.Add("onclick", "window.open('AdministraInformacionPersonal.aspx?id=" + juez.cveJuez + "&t=" + sType + "');");

                    TableCell tdEmail = new TableCell();
                    LiteralControl lcMailLink = new LiteralControl("<a href=\"mailto:" + juez.Correo + "?Subject=Premios%20Institucionales\" target=\"_top\"> " + juez.Correo + "</a>");
                    tdEmail.Controls.Add(lcMailLink);

                    tr.Controls.Add(tdIP);
                    tr.Controls.Add(tdName);
                    tr.Controls.Add(tdLastName);
                    tr.Controls.Add(tdEmail);

                    listaJuecesTableBody.Controls.Add(tr);

                }
            }
        }

        private void LoadCandidateTable()
        {
            litUsuarios.Text = "Candidatos";
            var candidatos = InformacionPersonalCandidatoService.GetCandidatos();
            string sType = Request.QueryString["t"];
            if (candidatos != null)
            {
                foreach (var cand in candidatos)
                {
                    TableRow tr = new TableRow();
                    //tr.Attributes.Add("onclick", "window.open('AdministraInformacionPersonal.aspx?id=" + cand.cveCandidato + "&t=" + sType + "');");
                    // profile image column
                    TableCell tdIP = new TableCell();
                    tdIP.CssClass = "dt-profile-pic";
                    tdIP.Attributes.Add("onclick", "window.open('AdministraInformacionPersonal.aspx?id=" + cand.cveCandidato + "&t=" + sType + "');");
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

                    // name column
                    TableCell tdName = new TableCell();
                    tdName.Text = cand.Nombre;
                    tdName.Attributes.Add("onclick", "window.open('AdministraInformacionPersonal.aspx?id=" + cand.cveCandidato + "&t=" + sType + "');");

                    // last name column
                    TableCell tdLastName = new TableCell();
                    tdLastName.Text = cand.Apellido;
                    tdLastName.Attributes.Add("onclick", "window.open('AdministraInformacionPersonal.aspx?id=" + cand.cveCandidato + "&t=" + sType + "');");

                    TableCell tdEmail = new TableCell();
                    LiteralControl lcMailLink = new LiteralControl("<a href=\"mailto:" + cand.Correo + "?Subject=Premios%20Institucionales\" target=\"_top\"> " + cand.Correo + "</a>");
                    tdEmail.Controls.Add(lcMailLink);

                    TableCell tdPhone = new TableCell();
                    tdPhone.Text = cand.Telefono;
                    tdPhone.Attributes.Add("onclick", "window.open('AdministraInformacionPersonal.aspx?id=" + cand.cveCandidato + "&t=" + sType + "');");

                    TableCell tdNationality = new TableCell();
                    tdNationality.Text = cand.Nacionalidad;
                    tdNationality.Attributes.Add("onclick", "window.open('AdministraInformacionPersonal.aspx?id=" + cand.cveCandidato + "&t=" + sType + "');");

                    TableCell tdRFC = new TableCell();
                    tdRFC.Text = cand.RFC;
                    tdRFC.Attributes.Add("onclick", "window.open('AdministraInformacionPersonal.aspx?id=" + cand.cveCandidato + "&t=" + sType + "');");

                    TableCell tdAddress = new TableCell();
                    tdAddress.Text = cand.Direccion;
                    tdAddress.Attributes.Add("onclick", "window.open('AdministraInformacionPersonal.aspx?id=" + cand.cveCandidato + "&t=" + sType + "');");

                    TableCell tdConfirmacion = new TableCell();
                    tdConfirmacion.Attributes.Add("onclick", "window.open('AdministraInformacionPersonal.aspx?id=" + cand.cveCandidato + "&t=" + sType + "');");
                    LiteralControl lcConfirmacion;
                    if (cand.Confirmado.HasValue && cand.Confirmado.Value)
                    {
                        tdConfirmacion.Style.Add("color", "#4caf50");
                        lcConfirmacion = new LiteralControl("<strong> <div style=\"display: none; \"> 2 </div> Confirmado </strong>");
                    }
                    else
                    {
                        tdConfirmacion.Style.Add("color", "#f9a825");
                        lcConfirmacion = new LiteralControl("<strong> <div style=\"display: none; \"> 2 </div> Sin confirmar </strong>");
                    }
                    tdConfirmacion.Controls.Add(lcConfirmacion);

                    // status column
                    TableCell tdPrivacidad = new TableCell();
                    tdPrivacidad.Attributes.Add("onclick", "window.open('AdministraInformacionPersonal.aspx?id=" + cand.cveCandidato + "&t=" + sType + "');");
                    LiteralControl lcPrivacidad;
                    if (cand.FechaPrivacidadDatos != null)
                    {
                        tdPrivacidad.Style.Add("color", "#4caf50");
                        lcPrivacidad = new LiteralControl("<strong> <div style=\"display: none; \"> 2 </div> Aceptado </strong>");
                    }
                    else
                    {
                        tdPrivacidad.Style.Add("color", "#f9a825");
                        lcPrivacidad = new LiteralControl("<strong> <div style=\"display: none; \"> 2 </div> Sin aceptar </strong>");
                    }

                    tdPrivacidad.Controls.Add(lcPrivacidad);

                    tr.Controls.Add(tdIP);
                    tr.Controls.Add(tdName);
                    tr.Controls.Add(tdLastName);
                    tr.Controls.Add(tdEmail);
                    tr.Controls.Add(tdPhone);
                    tr.Controls.Add(tdNationality);
                    tr.Controls.Add(tdRFC);
                    tr.Controls.Add(tdAddress);
                    tr.Controls.Add(tdConfirmacion);
                    tr.Controls.Add(tdPrivacidad);

                    listaCandidatosTableBody.Controls.Add(tr);
                }
            }
        }
    }
}