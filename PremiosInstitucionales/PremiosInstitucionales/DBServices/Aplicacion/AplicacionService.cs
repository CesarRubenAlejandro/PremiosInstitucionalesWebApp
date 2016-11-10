using PremiosInstitucionales.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
                return convocatoria.PI_BA_Categoria.ToList();

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
            PI_BA_Categoria categoria = dbContext.PI_BA_Categoria.Where(c => c.cveCategoria.Equals(idCategoria)).FirstOrDefault();
            PI_BA_Forma forma = categoria.PI_BA_Forma.First();
            var preguntas = (from fp in forma.PI_BA_PreguntasPorForma
                             join p in dbContext.PI_BA_Pregunta on fp.cvePregunta equals p.cvePregunta
                             select p).ToList();
            return preguntas;       
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
            PI_BA_Candidato candidato = dbContext.PI_BA_Candidato.Where(c => c.cveCandidato.Equals(correo)).FirstOrDefault();
            var aplicaciones = dbContext.PI_BA_Aplicacion.Where(a => a.cveCandidato.Equals(candidato.cveCandidato)).ToList();

            return aplicaciones;

        }
    }
}