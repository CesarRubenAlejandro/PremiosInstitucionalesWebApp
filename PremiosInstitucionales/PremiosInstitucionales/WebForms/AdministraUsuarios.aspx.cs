using PremiosInstitucionales.DBServices.InformacionPersonalCandidato;
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

        }

        private void LoadCandidateTable()
        {
            
            var candidatos = InformacionPersonalCandidatoService.GetCandidatos();
            string sType = Request.QueryString["t"];
            if (candidatos != null)
            {
                foreach (var cand in candidatos)
                {
                    TableRow tr = new TableRow();
                    tr.Attributes.Add("onclick", "window.open('AdministraInformacionPersonal.aspx?id=" + cand.cveCandidato + "&t=" + sType + "');");
                    // profile image column
                    TableCell tdIP = new TableCell();
                    tdIP.CssClass = "dt-profile-pic";

                    Image ipImage = new Image();
                    if (cand.NombreImagen != null)
                    {
                        ipImage.ImageUrl = "/ProfilePictures/" + cand.NombreImagen;
                    }
                    else{
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

                    // last name column
                    TableCell tdLastName = new TableCell();
                    tdLastName.Text = cand.Apellido;

                    TableCell tdEmail = new TableCell();
                    tdEmail.Text = cand.Correo;

                    TableCell tdPhone = new TableCell();
                    tdPhone.Text = cand.Telefono;

                    TableCell tdNationality = new TableCell();
                    tdNationality.Text = cand.Nacionalidad;

                    TableCell tdRFC = new TableCell();
                    tdRFC.Text = cand.RFC;

                    TableCell tdAddress = new TableCell();
                    tdAddress.Text = cand.Direccion;

                    // status column
                    TableCell tdStatus = new TableCell();
                    tdStatus.Style.Add("color", "#f44336");
                    LiteralControl lcStatus = new LiteralControl("<strong> <div style=\"display: none; \"> 2 </div> Nuevo </strong>");
                    tdStatus.Controls.Add(lcStatus);

                    tr.Controls.Add(tdIP);
                    tr.Controls.Add(tdName);
                    tr.Controls.Add(tdLastName);
                    tr.Controls.Add(tdEmail);
                    tr.Controls.Add(tdPhone);
                    tr.Controls.Add(tdNationality);
                    tr.Controls.Add(tdRFC);
                    tr.Controls.Add(tdAddress);
                    tr.Controls.Add(tdStatus);

                    listaCandidatosTableBody.Controls.Add(tr);
                }
            }
        }
    }
}