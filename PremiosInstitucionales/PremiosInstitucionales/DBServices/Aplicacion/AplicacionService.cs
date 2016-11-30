using PremiosInstitucionales.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using PremiosInstitucionales.Values;
using System.Web;

namespace PremiosInstitucionales.DBServices.Aplicacion
{
    public class AplicacionService
    {
        private static wPremiosInstitucionalesdbEntities dbContext; 

        public static List<PI_BA_Categoria>GetCategoriasByConvocatoria(String idConvocatoria)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            return dbContext.PI_BA_Categoria.Where(c => c.cveConvocatoria.Equals(idConvocatoria)).ToList();
        }

        public static List<PI_BA_Categoria> GetCategoriasByPremio(String idPremio)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            // revisar que el premio cuente con una convocatoria vigente
            var premio = dbContext.PI_BA_Premio.Where(p => p.cvePremio.Equals(idPremio)).First();
            if (premio.PI_BA_Convocatoria.Count > 0)
            {
                // obtener la convocatoria vigente
                var convocatoria = (from convo in premio.PI_BA_Convocatoria
                             where DateTime.Today >= convo.FechaInicio && DateTime.Today <= convo.FechaFin
                             select convo).FirstOrDefault();
                // regresar las categorias de la convocatoria
                try
                {
                    return convocatoria.PI_BA_Categoria.ToList();
                } catch (Exception e)
                {
                    return null;
                }
            } else
            {
                return null;
            }
        }

        public static bool CheckCandidatoInCategoria(String email, String idCategoria)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            // revisar que el candidato no tenga una aplicacion para la categoria determinada
            // obtener el objeto candidato
            PI_BA_Candidato candidato = dbContext.PI_BA_Candidato.Where(c => c.Correo.Equals(email)).First();
            // revisar si alguna aplicacion de este candidato coincide con la categoria determinada
            if (candidato.PI_BA_Aplicacion.Count > 0)
            {
                var query = candidato.PI_BA_Aplicacion.Where(a => a.cveCategoria.Equals(idCategoria)).ToList();
                return query.Count > 0;
            } else
            {
                return false;
            }
        }

        public static List<PI_BA_Pregunta>GetFormularioByCategoria(String idCategoria)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            try
            {
                PI_BA_Categoria categoria = dbContext.PI_BA_Categoria.Where(c => c.cveCategoria.Equals(idCategoria)).FirstOrDefault();
                PI_BA_Forma forma = categoria.PI_BA_Forma.First();
                var preguntas = (from fp in forma.PI_BA_PreguntasPorForma
                                 join p in dbContext.PI_BA_Pregunta on fp.cvePregunta equals p.cvePregunta
                                 orderby p.Orden
                                 select p).ToList();
                return preguntas;
            } catch (Exception e)
            {
                return null;
            }
                 
        }

        public static void CrearAplicacion(PI_BA_Aplicacion aplicacion, List<PI_BA_Respuesta> respuestas)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            dbContext.PI_BA_Aplicacion.Add(aplicacion);
            foreach (var resp in respuestas)
            {
                dbContext.PI_BA_Respuesta.Add(resp);
            }
            dbContext.SaveChanges();
        }

        public static String GetCveCandidatoByCorreo(String correo)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            var cve = (from c in dbContext.PI_BA_Candidato
                       where c.Correo.Equals(correo)
                       select c.cveCandidato).First().ToString();
            return cve;
        }

        public static List<PI_BA_Aplicacion> GetAplicacionesByCorreo(String correo)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            PI_BA_Candidato candidato = dbContext.PI_BA_Candidato.Where(c => c.Correo.Equals(correo)).FirstOrDefault();
            try { 
                var aplicaciones = dbContext.PI_BA_Aplicacion.Where(a => a.cveCandidato.Equals(candidato.cveCandidato)).ToList();
                return aplicaciones;
            } catch (Exception e)
            {
                return null;
            }
        }

        public static String GetPremioCategoriaByClaveCategoria(String idCategoria)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            PI_BA_Categoria categoria = dbContext.PI_BA_Categoria.Where(c => c.cveCategoria.Equals(idCategoria)).FirstOrDefault();
            PI_BA_Convocatoria convocatoria = dbContext.PI_BA_Convocatoria.Where(c => c.cveConvocatoria.Equals(categoria.cveConvocatoria)).FirstOrDefault();
            PI_BA_Premio premio = dbContext.PI_BA_Premio.Where(p => p.cvePremio.Equals(convocatoria.cvePremio)).FirstOrDefault();

            return "Premio " + premio.Nombre.ToString() + " - Categoría " + categoria.Nombre.ToString();
        }

        public static void RechazarAplicacion(String claveAplicacion)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            PI_BA_Aplicacion aplicacion = dbContext.PI_BA_Aplicacion.Where(c => c.cveAplicacion.Equals(claveAplicacion)).FirstOrDefault();

            aplicacion.Status = Values.StringValues.Rechazado;

            dbContext.SaveChanges();         
        }

        public static void AceptarAplicacion(String claveAplicacion)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            PI_BA_Aplicacion aplicacion = dbContext.PI_BA_Aplicacion.Where(c => c.cveAplicacion.Equals(claveAplicacion)).FirstOrDefault();

            aplicacion.Status = Values.StringValues.Aceptado;

            dbContext.SaveChanges();
        }

        public static PI_BA_Aplicacion ObtenerAplicacionDeClave(String claveAplicacion)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            PI_BA_Aplicacion aplicacion = dbContext.PI_BA_Aplicacion.Where(c => c.cveAplicacion.Equals(claveAplicacion)).FirstOrDefault();

            return aplicacion;
        }

        public static Boolean HasEndedByCategoria(String idCategoria)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            PI_BA_Categoria categoria = dbContext.PI_BA_Categoria.Where(c => c.cveCategoria.Equals(idCategoria)).FirstOrDefault();
            PI_BA_Convocatoria convocatoria = dbContext.PI_BA_Convocatoria.Where(c => c.cveConvocatoria.Equals(categoria.cveConvocatoria)).FirstOrDefault();

            int result;
            try { 
                DateTime fechaactual = DateTime.Now;
                DateTime fechafin = Convert.ToDateTime(convocatoria.FechaFin);
                result = DateTime.Compare(fechaactual, fechafin);
            } catch (Exception e)
            {
                result = -1;
            }

            if (result < 0)
                return false;
            else if (result == 0)
                return false;
            else
                return true;
        }

        public static Boolean HasWinnersByCategoria(String idCategoria)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            PI_BA_Categoria categoria = dbContext.PI_BA_Categoria.Where(c => c.cveCategoria.Equals(idCategoria)).FirstOrDefault();
            PI_BA_Convocatoria convocatoria = dbContext.PI_BA_Convocatoria.Where(c => c.cveConvocatoria.Equals(categoria.cveConvocatoria)).FirstOrDefault();

            int result;
            try { 
                DateTime fechaactual = DateTime.Now;
                DateTime fechaveredicto = Convert.ToDateTime(convocatoria.FechaVeredicto);
                result = DateTime.Compare(fechaactual, fechaveredicto);
            } catch (Exception e)
            {
                result = -1;
            }

            if (result < 0)
                return false;
            else if (result == 0)
                return false;
            else
                return true;
        }

        public static Boolean GetEsRechazadoByAplicacion(String idAplicacion)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            try
            {
                PI_BA_Aplicacion aplicacion = dbContext.PI_BA_Aplicacion.Where(c => c.cveAplicacion.Equals(idAplicacion)).FirstOrDefault();
                return aplicacion.Status.Equals(StringValues.Rechazado);

            } catch (Exception e)
            {
                return false;
            }
        }

        public static String GetCveCategoriaByAplicacion(String idAplicacion)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            try
            {
                PI_BA_Aplicacion aplicacion = dbContext.PI_BA_Aplicacion.Where(c => c.cveAplicacion.Equals(idAplicacion)).FirstOrDefault();
                var result = dbContext.PI_BA_Categoria.Where(c => c.cveCategoria.Equals(aplicacion.cveCategoria)).FirstOrDefault().cveCategoria;
                return result;
            }
            catch (Exception e)
            {
                return "";
            }
        }

        public static PI_BA_Respuesta GetRespuestaByPreguntaAndAplicacion(String idPregunta, String idAplicacion)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            try
            {
                var result = dbContext.PI_BA_Respuesta.Where(r => r.cveAplicacion.Equals(idAplicacion) && r.cvePregunta
                .Equals(idPregunta)).FirstOrDefault();
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static void SetAplicacionModificada(String idAplicacion)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            try
            {
                PI_BA_Aplicacion aplicacion = dbContext.PI_BA_Aplicacion.Where(c => c.cveAplicacion.Equals(idAplicacion)).FirstOrDefault();
                aplicacion.Status = StringValues.Modificado;
                dbContext.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }

        public static void SaveRespuestaModificada(String idAplicacion, String idPregunta, String valorModificado)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            try
            {
                var resp = dbContext.PI_BA_Respuesta.Where(r => r.cveAplicacion.Equals(idAplicacion) && r.cvePregunta
                .Equals(idPregunta)).FirstOrDefault();
                resp.Valor = valorModificado;
                dbContext.SaveChanges();
            }
            catch (Exception e)
            {

            }
        }
    }
}