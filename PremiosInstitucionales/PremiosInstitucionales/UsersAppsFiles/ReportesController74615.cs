using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using waTrayectorias.Models;
using waTrayectorias.Models.Entidades;
using ClosedXML.Excel;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.IO;
using waTrayectorias.Models.Entidades.Solicitud;
using waTrayectorias.Controllers;

namespace waTrayectorias.Controllers
{
    public class ReportesController : ApiController
    {
        ReportesModel r = new ReportesModel();
        ConcentracionesModel cm = new ConcentracionesModel();

        [EnableCors(origins: "*", methods: "*", headers: "*")]
        [AcceptVerbs("Get")]
        [Route("TMTY/Reportes/{Carrera}")]
        public IHttpActionResult Reporte(string Carrera) {
            List<EntidadReportes> repo = new List<EntidadReportes>();
            Censo c = new Censo();

            List<respuestaConcentraciones> nec = new List<respuestaConcentraciones>();
            PlanEstudiosModel pe = new PlanEstudiosModel();

            nec = cm.Concentraciones(Carrera);
            string com = string.Empty;
            int posicion = 1;

            c = r.consultaNumerica();
            repo = r.ConsultaGlobal();

            var excel = new MemoryStream();
            XLWorkbook workbook = new XLWorkbook();
            IXLWorksheet workshet = workbook.Worksheets.Add("Censo");
            #region Encabezado Censo
            workshet.Range("A1:B1").Merge();
            workshet.Range("C1:D1").Merge();
            workshet.Range("E1:F1").Merge();
            workshet.Range("G1:H1").Merge();
            workshet.Range("I1:K1").Merge();

            workshet.Cell(1, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            workshet.Cell(1, 1).Style.Border.BottomBorderColor = XLColor.LightBlue;

            workshet.Cell(1, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            workshet.Cell(1, 2).Style.Border.BottomBorderColor = XLColor.LightBlue;
            
            workshet.Cell(1, 3).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            workshet.Cell(1, 3).Style.Border.BottomBorderColor = XLColor.LightBlue;

            workshet.Cell(1, 4).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            workshet.Cell(1, 4).Style.Border.BottomBorderColor = XLColor.LightBlue;
            
            workshet.Cell(1, 5).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            workshet.Cell(1, 5).Style.Border.BottomBorderColor = XLColor.LightBlue;

            workshet.Cell(1, 6).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            workshet.Cell(1, 6).Style.Border.BottomBorderColor = XLColor.LightBlue;

            workshet.Cell(1, 7).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            workshet.Cell(1, 7).Style.Border.BottomBorderColor = XLColor.LightBlue;

            workshet.Cell(1, 8).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            workshet.Cell(1, 8).Style.Border.BottomBorderColor = XLColor.LightBlue;

            workshet.Cell(1, 9).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            workshet.Cell(1, 9).Style.Border.BottomBorderColor = XLColor.LightBlue;

            workshet.Cell(1, 10).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            workshet.Cell(1, 10).Style.Border.BottomBorderColor = XLColor.LightBlue;

            workshet.Cell(1, 11).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            workshet.Cell(1, 11).Style.Border.BottomBorderColor = XLColor.LightBlue;

            workshet.Cell(1, 1).SetValue("Semestre I").Style.Font.Bold = true;
            workshet.Cell(1, 3).SetValue("Topicos").Style.Font.Bold = true;
            workshet.Cell(1, 5).SetValue("Complementos").Style.Font.Bold = true;
            workshet.Cell(1, 7).SetValue("Concentraciones").Style.Font.Bold = true;
            workshet.Cell(1, 9).SetValue("Asignaturas").Style.Font.Bold = true;
            #endregion

            #region Datos Censo
            for (int i = 0; i < c.SemestreI.Count; i++) {
                posicion = posicion + 1;
                workshet.Cell(posicion, 1).SetValue(c.SemestreI[i].Semestre);
                workshet.Cell(posicion, 2).SetValue(c.SemestreI[i].cantidad);
            }
            posicion = 1;
            for (int i = 0; i < c.Topicos.Count; i++) {
                posicion = posicion + 1;
                workshet.Cell(posicion, 3).SetValue(c.Topicos[i].Topicos);
                workshet.Cell(posicion, 4).SetValue(c.Topicos[i].Cantidad);
            }
            posicion = 1;
            for (int i = 0; i < c.Complementos.Count; i++)
            {
                posicion = posicion + 1;
                com = nec.Where(w => w.Attributes.cveConcentracion.ToString() == c.Complementos[i].Complemento).Select(s => s.Attributes.descConcentracion).First().ToString();
                workshet.Cell(posicion, 5).SetValue(com);
                workshet.Cell(posicion, 6).SetValue(c.Complementos[i].Cantidad);
            }
            posicion = 1;
            
            for (int i = 0; i < c.Concentraciones.Count; i++) {
                posicion = posicion + 1;
                com = nec.Where(w => w.Attributes.cveConcentracion.ToString() == c.Concentraciones[i].Concentracion).Select(s => s.Attributes.descConcentracion).First().ToString(); 
                workshet.Cell(posicion, 7).SetValue(com);
                workshet.Cell(posicion, 8).SetValue(c.Concentraciones[i].Cantidad);
            }
            posicion = 1;
            for (int i = 0; i < c.Asignaturas.Count; i++) {
                posicion = posicion + 1;
                workshet.Cell(posicion, 9).SetValue(c.Asignaturas[i].Asignatura);
                workshet.Cell(posicion, 10).SetValue(c.Asignaturas[i].ClaveAsignatura);
                workshet.Cell(posicion, 11).SetValue(c.Asignaturas[i].Cantidad);
            }
            posicion = 1;
            #endregion

            workshet = workbook.Worksheets.Add("Desglose");
            #region Encabezado Desglose
            workshet.Cell(1, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            workshet.Cell(1, 1).Style.Border.BottomBorderColor = XLColor.LightBlue;
            workshet.Cell(1, 1).SetValue("Matricula").Style.Font.Bold = true;

            workshet.Cell(1, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            workshet.Cell(1, 2).Style.Border.BottomBorderColor = XLColor.LightBlue;
            workshet.Cell(1, 2).SetValue("Campus").Style.Font.Bold = true;

            workshet.Cell(1, 3).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            workshet.Cell(1, 3).Style.Border.BottomBorderColor = XLColor.LightBlue;
            workshet.Cell(1, 3).SetValue("Clave Campus").Style.Font.Bold = true;

            workshet.Cell(1, 4).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            workshet.Cell(1, 4).Style.Border.BottomBorderColor = XLColor.LightBlue;
            workshet.Cell(1, 4).SetValue("Periodo").Style.Font.Bold = true;

            workshet.Cell(1, 5).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            workshet.Cell(1, 5).Style.Border.BottomBorderColor = XLColor.LightBlue;
            workshet.Cell(1, 5).SetValue("Optativa").Style.Font.Bold = true;

            workshet.Cell(1, 6).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            workshet.Cell(1, 6).Style.Border.BottomBorderColor = XLColor.LightBlue;
            workshet.Cell(1, 6).SetValue("Asignatura").Style.Font.Bold = true;

            workshet.Cell(1, 7).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            workshet.Cell(1, 7).Style.Border.BottomBorderColor = XLColor.LightBlue;
            workshet.Cell(1, 7).SetValue("Clave").Style.Font.Bold = true;

            workshet.Cell(1, 8).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            workshet.Cell(1, 8).Style.Border.BottomBorderColor = XLColor.LightBlue;
            workshet.Cell(1, 8).SetValue("Semestre I").Style.Font.Bold = true;

            workshet.Cell(1, 9).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            workshet.Cell(1, 9).Style.Border.BottomBorderColor = XLColor.LightBlue;
            workshet.Cell(1, 9).SetValue("Concentración").Style.Font.Bold = true;

            workshet.Cell(1, 10).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            workshet.Cell(1, 10).Style.Border.BottomBorderColor = XLColor.LightBlue;
            workshet.Cell(1, 10).SetValue("Complementaria").Style.Font.Bold = true;

            workshet.Cell(1, 11).Style.Border.BottomBorder = XLBorderStyleValues.Thick;
            workshet.Cell(1, 11).Style.Border.BottomBorderColor = XLColor.LightBlue;
            workshet.Cell(1, 11).SetValue("Tópico").Style.Font.Bold = true;
            #endregion

            #region Datos Desglose
            for (int i = 0; i < repo.Count; i++) {
                posicion = posicion + 1;
                workshet.Cell(posicion, 1).SetValue(repo[i].Matricula);
                workshet.Cell(posicion, 2).SetValue(repo[i].Campus);
                workshet.Cell(posicion, 3).SetValue(repo[i].ClaveCampus);
                workshet.Cell(posicion, 4).SetValue(repo[i].ClavePeriodo);
                workshet.Cell(posicion, 5).SetValue(repo[i].MateriaPuente);
                workshet.Cell(posicion, 6).SetValue(repo[i].Asignatura);
                workshet.Cell(posicion, 7).SetValue(repo[i].ClaveAsignatura);
                workshet.Cell(posicion, 8).SetValue(repo[i].SemestreI);
                com = (repo[i].Concentracion != "" ? nec.Where(w => w.Attributes.cveConcentracion.ToString() == repo[i].Concentracion).Select(s => s.Attributes.descConcentracion).First().ToString() : "");
                workshet.Cell(posicion, 9).SetValue(com);
                com = (repo[i].Complemento != "" ? nec.Where(w => w.Attributes.cveConcentracion.ToString() == repo[i].Complemento).Select(s => s.Attributes.descConcentracion).First().ToString() : "");
                workshet.Cell(posicion, 10).SetValue(com);
                workshet.Cell(posicion, 11).SetValue(repo[i].Topico);
            }
            posicion = 1;
            #endregion

            workbook.SaveAs(excel);
            excel.Position = 0;

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(excel.GetBuffer())
            };
            result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = "Reporte.xlsx"
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

            var response = ResponseMessage(result);

            return response;
        }
    }
}