using PremiosInstitucionales.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using PremiosInstitucionales.Values;

namespace PremiosInstitucionales.DBServices.Aplicacion
{
    public class AplicacionService
    {
        public static bool CheckCandidatoInCategoria(String email, String idCategoria)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    // Revisar que el candidato no tenga una aplicacion para la categoria determinada 
                    PI_BA_Candidato candidato = dbContext.PI_BA_Candidato.Where(c => c.Correo.Equals(email)).First();

                    // Revisar si alguna aplicacion de este candidato coincide con la categoria determinada
                    if (candidato.PI_BA_Aplicacion.Count > 0)
                    {
                        var query = candidato.PI_BA_Aplicacion.Where(a => a.cveCategoria.Equals(idCategoria)).ToList();
                        return query.Count > 0;
                    }
                    else return false;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return false;
                }
            }
        }

        public static PI_BA_Forma GetFormByCategoria(String idCategoria)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    PI_BA_Categoria categoria = dbContext.PI_BA_Categoria.Where(c => c.cveCategoria.Equals(idCategoria)).FirstOrDefault();
                    PI_BA_Forma forma = categoria.PI_BA_Forma.First();
                    return forma;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return null;
                }
            }
        }

        public static List<PI_BA_Pregunta> GetFormularioByCategoria(String idCategoria)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    PI_BA_Categoria categoria = dbContext.PI_BA_Categoria.Where(c => c.cveCategoria.Equals(idCategoria)).FirstOrDefault();
                    PI_BA_Forma forma = categoria.PI_BA_Forma.First();
                    var preguntas = (from fp in forma.PI_BA_PreguntasPorForma
                                     join p in dbContext.PI_BA_Pregunta on fp.cvePregunta equals p.cvePregunta
                                     orderby p.Orden
                                     select p).ToList();
                    return preguntas;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return null;
                }
            }
        }

        public static List<String> GetJuecesIdsCategoria(String idCategoria)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    var query = (from juezCategoria in dbContext.PI_BA_JuezPorCategoria
                                 where juezCategoria.cveCategoria.Equals(idCategoria)
                                 select juezCategoria.cveJuez).ToList();
                    return query;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return null;
                }
            }
        }

        public static void RemovePregunta(String idForma, String idPregunta)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    var query = (from pregunta in dbContext.PI_BA_PreguntasPorForma
                                 where pregunta.cveForma.Equals(idForma) && pregunta.cvePregunta.Equals(idPregunta)
                                 select pregunta).FirstOrDefault();
                    dbContext.PI_BA_PreguntasPorForma.Remove(query);

                    dbContext.SaveChanges();

                    var query2 = (from pregunta in dbContext.PI_BA_Pregunta
                                  where pregunta.cvePregunta.Equals(idPregunta)
                                  select pregunta).FirstOrDefault();
                    dbContext.PI_BA_Pregunta.Remove(query2);
                    dbContext.SaveChanges();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                }
            }
        }

        public static void InsertaPregunta(String idForma, String valor, int orden)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    PI_BA_Pregunta pregunta = new PI_BA_Pregunta();
                    pregunta.cvePregunta = Guid.NewGuid().ToString();
                    pregunta.Orden = orden;
                    pregunta.Texto = valor;
                    dbContext.PI_BA_Pregunta.Add(pregunta);
                    dbContext.SaveChanges();
                    PI_BA_PreguntasPorForma pregForma = new PI_BA_PreguntasPorForma();
                    pregForma.cvePreguntaPorForma = Guid.NewGuid().ToString();
                    pregForma.cveForma = idForma;
                    pregForma.cvePregunta = pregunta.cvePregunta;
                    dbContext.PI_BA_PreguntasPorForma.Add(pregForma);
                    dbContext.SaveChanges();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                }
            }
        }

        public static void RemoveJuezCategoria(String idCategoria)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    var query = (from juezCategoria in dbContext.PI_BA_JuezPorCategoria
                                 where juezCategoria.cveCategoria.Equals(idCategoria)
                                 select juezCategoria).ToList();

                    foreach (var juez in query)
                    {
                        dbContext.PI_BA_JuezPorCategoria.Remove(juez);
                    }

                    dbContext.SaveChanges();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                }
            }
        }

        public static void AsignarJuecesCategoria(String idCategoria, List<String> idJueces)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    foreach (var idJuez in idJueces)
                    {

                        PI_BA_JuezPorCategoria row = new PI_BA_JuezPorCategoria();
                        row.cveCategoria = idCategoria;
                        row.cveJuez = idJuez;
                        row.cveJuezPorCategoria = Guid.NewGuid().ToString();
                        dbContext.PI_BA_JuezPorCategoria.Add(row);
                    }
                    dbContext.SaveChanges();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                }
            }
        }

        public static void CrearAplicacion(PI_BA_Aplicacion aplicacion, List<PI_BA_Respuesta> respuestas)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    dbContext.PI_BA_Aplicacion.Add(aplicacion);
                    foreach (var resp in respuestas)
                    {
                        dbContext.PI_BA_Respuesta.Add(resp);
                    }
                    dbContext.SaveChanges();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                }
            }
        }

        public static String GetCveCandidatoByCorreo(String correo)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    var cve = (from c in dbContext.PI_BA_Candidato
                               where c.Correo.Equals(correo)
                               select c.cveCandidato).First().ToString();
                    return cve;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return null;
                }
            }
        }

        public static List<PI_BA_Aplicacion> GetAplicacionesByCorreo(String correo)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    PI_BA_Candidato candidato = dbContext.PI_BA_Candidato.Where(c => c.Correo.Equals(correo)).FirstOrDefault();
                    var aplicaciones = dbContext.PI_BA_Aplicacion.Where(a => a.cveCandidato.Equals(candidato.cveCandidato)).ToList();
                    return aplicaciones;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return null;
                }
            }
        }

        public static PI_BA_Aplicacion GetAplicacionById(String cveApp)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    var aplicacion = dbContext.PI_BA_Aplicacion.Where(a => a.cveAplicacion.Equals(cveApp)).ToList().FirstOrDefault();
                    return aplicacion;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return null;
                }
            }
        }

        public static void UpdateAplicacionArchivo(String cveApp, String sArchivo)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    var aplicacion = dbContext.PI_BA_Aplicacion.Where(a => a.cveAplicacion.Equals(cveApp)).ToList().FirstOrDefault();
                    aplicacion.NombreArchivo = sArchivo;
                    dbContext.SaveChanges();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                }
            }
        }

        public static List<PI_BA_Aplicacion> GetAplicacionesByStatus(String status)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    var aplicaciones = dbContext.PI_BA_Aplicacion.Where(a => a.Status.Equals(status)).ToList();
                    return aplicaciones;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return null;
                }
            }
        }

        public static List<PI_BA_Aplicacion> GetAplicacionesByCategoria(String categoria)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    var aplicaciones = dbContext.PI_BA_Aplicacion.Where(a => a.cveCategoria.Equals(categoria)).ToList();
                    return aplicaciones;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return null;
                }
            }
        }

        public static PI_BA_Premio GetPremioByClaveCategoria(String idCategoria)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    return dbContext.GetPremioByIdCategoria(idCategoria).FirstOrDefault();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return null;
                }
            }
        }

        public static PI_BA_Categoria GetCategoriaByClaveCategoria(String idCategoria)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    PI_BA_Categoria categoria = dbContext.PI_BA_Categoria.Where(c => c.cveCategoria.Equals(idCategoria)).FirstOrDefault();
                    return categoria;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return null;
                }
            }
        }

        public static void CambiarNombreCategoria(String idCategoria, String nombre)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    PI_BA_Categoria categoria = dbContext.PI_BA_Categoria.Where(c => c.cveCategoria.Equals(idCategoria)).FirstOrDefault();
                    categoria.Nombre = nombre;
                    dbContext.SaveChanges();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                }
            }
        }
        
        public static void AsignarGanadorCategoria(String idCategoria, String cveApp)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    if (idCategoria == null || cveApp == null) return;

                    PI_BA_Categoria categoria = dbContext.PI_BA_Categoria.Where(c => c.cveCategoria.Equals(idCategoria)).FirstOrDefault();
                    categoria.cveAplicacionGanadora = cveApp;
                    dbContext.SaveChanges();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                }
            }
        }

        public static void RechazarAplicacion(String claveAplicacion)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    PI_BA_Aplicacion aplicacion = dbContext.PI_BA_Aplicacion.Where(c => c.cveAplicacion.Equals(claveAplicacion)).FirstOrDefault();
                    aplicacion.Status = Values.StringValues.Rechazado;
                    dbContext.SaveChanges();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                }
            }
        }

        public static void AceptarAplicacion(String claveAplicacion)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    PI_BA_Aplicacion aplicacion = dbContext.PI_BA_Aplicacion.Where(c => c.cveAplicacion.Equals(claveAplicacion)).FirstOrDefault();
                    aplicacion.Status = Values.StringValues.Aceptado;
                    dbContext.SaveChanges();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                }
            }
        }

        public static PI_BA_Aplicacion ObtenerAplicacionDeClave(String claveAplicacion)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    PI_BA_Aplicacion aplicacion = dbContext.PI_BA_Aplicacion.Where(c => c.cveAplicacion.Equals(claveAplicacion)).FirstOrDefault();
                    return aplicacion;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return null;
                }
            }
        }

        public static Boolean HasEndedByCategoria(String idCategoria)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    PI_BA_Categoria categoria = dbContext.PI_BA_Categoria.Where(c => c.cveCategoria.Equals(idCategoria)).FirstOrDefault();
                    PI_BA_Convocatoria convocatoria = dbContext.PI_BA_Convocatoria.Where(c => c.cveConvocatoria.Equals(categoria.cveConvocatoria)).FirstOrDefault();
                    DateTime fechaactual = DateTime.Now;
                    DateTime fechafin = Convert.ToDateTime(convocatoria.FechaFin);
                    if (DateTime.Compare(fechaactual, fechafin) <= 0) return false;
                    else return true;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return false;
                }
            }
        }

        public static Boolean HasWinnersByCategoria(String idCategoria)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    PI_BA_Categoria categoria = dbContext.PI_BA_Categoria.Where(c => c.cveCategoria.Equals(idCategoria)).FirstOrDefault();
                    PI_BA_Convocatoria convocatoria = dbContext.PI_BA_Convocatoria.Where(c => c.cveConvocatoria.Equals(categoria.cveConvocatoria)).FirstOrDefault();
                    DateTime fechaactual = DateTime.Now;
                    DateTime fechaveredicto = Convert.ToDateTime(convocatoria.FechaVeredicto);
                    if (DateTime.Compare(fechaactual, fechaveredicto) <= 0) return false;
                    else return true;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return false;
                }
            }
        }

        public static Boolean GetEsRechazadoByAplicacion(String idAplicacion)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    PI_BA_Aplicacion aplicacion = dbContext.PI_BA_Aplicacion.Where(c => c.cveAplicacion.Equals(idAplicacion)).FirstOrDefault();
                    return aplicacion.Status.Equals(StringValues.Rechazado);
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return false;
                }
            }
        }

        public static String GetCveCategoriaByAplicacion(String idAplicacion)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    PI_BA_Aplicacion aplicacion = dbContext.PI_BA_Aplicacion.Where(c => c.cveAplicacion.Equals(idAplicacion)).FirstOrDefault();
                    var result = dbContext.PI_BA_Categoria.Where(c => c.cveCategoria.Equals(aplicacion.cveCategoria)).FirstOrDefault().cveCategoria;
                    return result;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return null;
                }
            }
        }

        public static PI_BA_Respuesta GetRespuestaByPreguntaAndAplicacion(String idPregunta, String idAplicacion)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    var result = dbContext.PI_BA_Respuesta.Where(r => r.cveAplicacion.Equals(idAplicacion)
                                 && r.cvePregunta.Equals(idPregunta)).FirstOrDefault();
                    return result;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return null;
                }
            }
        }

        public static void SetAplicacionModificada(String idAplicacion)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    PI_BA_Aplicacion aplicacion = dbContext.PI_BA_Aplicacion.Where(c => c.cveAplicacion.Equals(idAplicacion)).FirstOrDefault();
                    aplicacion.Status = StringValues.Modificado;
                    dbContext.SaveChanges();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                }
            }
        }

        public static void GuardaPregunta(String idPregunta, String valor, int orden)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    var resp = dbContext.PI_BA_Pregunta.Where(r => r.cvePregunta.Equals(idPregunta)).FirstOrDefault();
                    resp.Texto = valor;
                    resp.Orden = orden;
                    dbContext.SaveChanges();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                }
            }
        }

        public static void SaveRespuestaModificada(String idAplicacion, String idPregunta, String valorModificado)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    var resp = dbContext.PI_BA_Respuesta.Where(r => r.cveAplicacion.Equals(idAplicacion) 
                               && r.cvePregunta.Equals(idPregunta)).FirstOrDefault();
                    resp.Valor = valorModificado;
                    dbContext.SaveChanges();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                }
            }
        }
    }
}