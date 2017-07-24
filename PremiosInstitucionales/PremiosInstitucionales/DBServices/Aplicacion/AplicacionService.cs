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
            }
            else
            {
                return false;
            }
        }

        public static PI_BA_Forma GetFormByCategoria(String idCategoria)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            try
            {
                PI_BA_Categoria categoria = dbContext.PI_BA_Categoria.Where(c => c.cveCategoria.Equals(idCategoria)).FirstOrDefault();
                PI_BA_Forma forma = categoria.PI_BA_Forma.First();
                return forma;
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public static List<PI_BA_Pregunta> GetFormularioByCategoria(String idCategoria)
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
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public static List<String> GetJuecesIdsCategoria(String idCategoria)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            try
            {
                dbContext = new wPremiosInstitucionalesdbEntities();
                var query = (from juezCategoria in dbContext.PI_BA_JuezPorCategoria
                             where juezCategoria.cveCategoria.Equals(idCategoria)
                             select juezCategoria.cveJuez).ToList();
                return query;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static void RemovePregunta(String idForma, String idPregunta)
        {

            try
            {
                dbContext = new wPremiosInstitucionalesdbEntities();
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
            catch (Exception e)
            {
                return;
            }

        }
        public static void InsertaPregunta(String idForma, String valor, int orden)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
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
        public static void RemoveJuezCategoria(String idCategoria)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            var query = (from juezCategoria in dbContext.PI_BA_JuezPorCategoria
                            where juezCategoria.cveCategoria.Equals(idCategoria)
                            select juezCategoria).ToList();

            foreach (var juez in query)
            {
                dbContext.PI_BA_JuezPorCategoria.Remove(juez);
            }

            dbContext.SaveChanges();
        }

        public static void AsignarJuecesCategoria(String idCategoria, List<String> idJueces)
        {
            try
            {
                dbContext = new wPremiosInstitucionalesdbEntities();
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
            catch (Exception e)
            {
                return;
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
            try
            {
                var aplicaciones = dbContext.PI_BA_Aplicacion.Where(a => a.cveCandidato.Equals(candidato.cveCandidato)).ToList();
                return aplicaciones;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static List<PI_BA_Aplicacion> GetAplicacionesByStatus(String status)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            var aplicaciones = dbContext.PI_BA_Aplicacion.Where(a => a.Status.Equals(status)).ToList();
            return aplicaciones;
        }

        public static List<PI_BA_Aplicacion> GetAplicacionesByCategoria(String categoria)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            var aplicaciones = dbContext.PI_BA_Aplicacion.Where(a => a.cveCategoria.Equals(categoria)).ToList();
            return aplicaciones;
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
            dbContext = new wPremiosInstitucionalesdbEntities();
            PI_BA_Categoria categoria = dbContext.PI_BA_Categoria.Where(c => c.cveCategoria.Equals(idCategoria)).FirstOrDefault();

            return categoria;
        }

        public static void CambiarNombreCategoria(String idCategoria, String nombre)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            PI_BA_Categoria categoria = GetCategoriaByClaveCategoria(idCategoria);
            categoria.Nombre = nombre;
            dbContext.SaveChanges();
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
            try
            {
                DateTime fechaactual = DateTime.Now;
                DateTime fechafin = Convert.ToDateTime(convocatoria.FechaFin);
                result = DateTime.Compare(fechaactual, fechafin);
            }
            catch (Exception e)
            {
                result = -1;
            }

            if (result <= 0)
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
            try
            {
                DateTime fechaactual = DateTime.Now;
                DateTime fechaveredicto = Convert.ToDateTime(convocatoria.FechaVeredicto);
                result = DateTime.Compare(fechaactual, fechaveredicto);
            }
            catch (Exception e)
            {
                result = -1;
            }

            if (result <= 0)
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

            }
            catch (Exception e)
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

        public static void GuardaPregunta(String idPregunta, String valor, int orden)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            try
            {
                var resp = dbContext.PI_BA_Pregunta.Where(r => r.cvePregunta.Equals(idPregunta)).FirstOrDefault();
                resp.Texto = valor;
                resp.Orden = orden;
                dbContext.SaveChanges();
            }
            catch(Exception e)
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