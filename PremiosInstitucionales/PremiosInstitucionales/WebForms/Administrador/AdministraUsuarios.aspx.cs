using PremiosInstitucionales.DBServices.InformacionPersonalCandidato;
using PremiosInstitucionales.DBServices.InformacionPersonalJuez;
using PremiosInstitucionales.Values;
using System;
using System.IO;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.SS.UserModel;

namespace PremiosInstitucionales.WebForms
{
    public partial class AdministraUsuarios : System.Web.UI.Page
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

        protected DataTable loadDataTable()
        {
            DataTable DT = new DataTable();

            string sType = Request.QueryString["t"];
            if (sType != null)
            {
                if (sType == "candidato")
                {
                    var candidatos = InformacionPersonalCandidatoService.GetCandidatos();
                    if (candidatos != null)
                    {
                        DT.Clear();
                        DT.Columns.AddRange(new DataColumn[]
                        {
                            new DataColumn("Nombre"),
                            new DataColumn("Apellido"),
                            new DataColumn("Correo"),
                            new DataColumn("Telefono"),
                            new DataColumn("Nacionalidad"),
                            new DataColumn("RFC"),
                            new DataColumn("Direccion")
                        });

                        foreach (var cand in candidatos)
                        {
                            DT.Rows.Add(new object[]
                            {
                                cand.Nombre,
                                cand.Apellido,
                                cand.Correo,
                                cand.Telefono,
                                cand.Nacionalidad,
                                cand.RFC,
                                cand.Direccion
                            });

                            DT.AcceptChanges();
                        }
                    }
                }

                else if (sType == "juez")
                {
                    var jueces = InformacionPersonalJuezService.GetJueces();
                    if (jueces != null)
                    {
                        DT.Clear();
                        DT.Columns.AddRange(new DataColumn[]
                        {
                            new DataColumn("Nombre"),
                            new DataColumn("Apellido"),
                            new DataColumn("Correo")
                        });

                        foreach (var juez in jueces)
                        {
                            DT.Rows.Add(new object[]
                            {
                                juez.Nombre,
                                juez.Apellido,
                                juez.Correo
                            });

                            DT.AcceptChanges();
                        }
                    }
                }
            }

            return DT;
        }

        // Excel
        protected void GetExcel_Click(object sender, EventArgs e)
        {
            DataTable excelData = loadDataTable();

            string FileName;
            string DownloadFileName;
            string FolderPath;

            string sType = Request.QueryString["t"];
            if (sType == "candidato")
            {
                FileName = "ListaCandidatos.xlsx";
            }
            else {
                FileName = "ListaJueces.xlsx";
            }

            //DownloadFileName = Path.GetFileNameWithoutExtension(FileName) + Path.GetExtension(FileName);
            DownloadFileName = Path.GetFileNameWithoutExtension(FileName) + new Random().Next(10000, 99999) + Path.GetExtension(FileName);
            FolderPath = ".\\" + DownloadFileName;

            GetParents(Server.MapPath("~/Document/" + FileName), Server.MapPath("~/Document/" + DownloadFileName), excelData);

            string path = Server.MapPath("~/Document/" + FolderPath);
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                try
                {
                    HttpResponse response = HttpContext.Current.Response;
                    response.Clear();
                    response.ClearContent();
                    response.ClearHeaders();
                    response.Buffer = true;
                    response.ContentType = MimeType(Path.GetExtension(FolderPath));
                    response.AddHeader("Content-Disposition", "attachment;filename=" + DownloadFileName);
                    byte[] data = File.ReadAllBytes(path);
                    response.BinaryWrite(data);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    response.End();
                }

                catch (Exception ex)
                {
                    ex.ToString();
                }
                finally
                {
                    DeleteOrganisationtoSupplierTemplate(path);
                }
            }
        }
        public string GetParents(string FilePath, string TempFilePath, DataTable DTTBL)
        {
            File.Copy(Path.Combine(FilePath), Path.Combine(TempFilePath), true);
            FileInfo file = new FileInfo(TempFilePath);
            try
            {
                DatatableToExcel(DTTBL, TempFilePath, 0);

                return TempFilePath;

            }

            catch (Exception ex)
            {
                return "";
            }

        }


        public static string MimeType(string Extension)
        {
            string mime = "application/octetstream";
            if (string.IsNullOrEmpty(Extension))
                return mime;
            string ext = Extension.ToLower();
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (rk != null && rk.GetValue("Content Type") != null)
                mime = rk.GetValue("Content Type").ToString();
            return mime;
        }


        static bool DeleteOrganisationtoSupplierTemplate(string filePath)
        {
            try
            {
                File.Delete(filePath);
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }


        public void DatatableToExcel(DataTable dtable, string pFilePath, int excelSheetIndex = 1)
        {
            try
            {
                if (dtable != null && dtable.Rows.Count > 0)
                {
                    IWorkbook workbook = null;
                    ISheet worksheet = null;

                    using (FileStream stream = new FileStream(pFilePath, FileMode.Open, FileAccess.ReadWrite))
                    {

                        workbook = WorkbookFactory.Create(stream);
                        worksheet = workbook.GetSheetAt(excelSheetIndex);

                        int iRow = 1;

                        foreach (DataRow row in dtable.Rows)
                        {
                            IRow file = worksheet.CreateRow(iRow);
                            int iCol = 0;
                            foreach (DataColumn column in dtable.Columns)
                            {
                                ICell cell = null;
                                object cellValue = row[iCol];

                                switch (column.DataType.ToString())
                                {
                                    case "System.Boolean":
                                        if (cellValue != DBNull.Value)
                                        {
                                            cell = file.CreateCell(iCol, CellType.Boolean);

                                            if (Convert.ToBoolean(cellValue))
                                            {
                                                cell.SetCellFormula("TRUE()");
                                            }
                                            else
                                            {
                                                cell.SetCellFormula("FALSE()");
                                            }
                                        }
                                        break;

                                    case "System.String":
                                        if (cellValue != DBNull.Value)
                                        {
                                            cell = file.CreateCell(iCol, CellType.String);
                                            cell.SetCellValue(Convert.ToString(cellValue));
                                        }
                                        break;

                                    case "System.Int32":
                                        if (cellValue != DBNull.Value)
                                        {
                                            cell = file.CreateCell(iCol, CellType.Numeric);
                                            cell.SetCellValue(Convert.ToInt32(cellValue));
                                        }
                                        break;
                                    case "System.Int64":
                                        if (cellValue != DBNull.Value)
                                        {
                                            cell = file.CreateCell(iCol, CellType.Numeric);
                                            cell.SetCellValue(Convert.ToInt64(cellValue));
                                        }
                                        break;
                                    case "System.Decimal":
                                        if (cellValue != DBNull.Value)
                                        {
                                            cell = file.CreateCell(iCol, CellType.Numeric);
                                            cell.SetCellValue(Convert.ToDouble(cellValue));
                                        }
                                        break;
                                    case "System.Double":
                                        if (cellValue != DBNull.Value)
                                        {
                                            cell = file.CreateCell(iCol, CellType.Numeric);
                                            cell.SetCellValue(Convert.ToDouble(cellValue));
                                        }
                                        break;

                                    case "System.DateTime":
                                        if (cellValue != DBNull.Value)
                                        {
                                            cell = file.CreateCell(iCol, CellType.String);
                                            DateTime dateTime = Convert.ToDateTime(cellValue);
                                            cell.SetCellValue(dateTime.ToString("dd/MM/yyyy"));
                                            DateTime cDate = Convert.ToDateTime(cellValue);
                                        }
                                        break;

                                    default:
                                        break;
                                }
                                iCol++;
                            }
                            iRow++;
                        }
                        using (var WritetoExcelfile = new FileStream(pFilePath, FileMode.Create, FileAccess.ReadWrite))
                        {
                            workbook.Write(WritetoExcelfile);
                            WritetoExcelfile.Close();
                            stream.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
}