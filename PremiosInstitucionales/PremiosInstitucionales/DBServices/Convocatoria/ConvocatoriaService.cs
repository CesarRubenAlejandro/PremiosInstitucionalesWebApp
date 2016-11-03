using PremiosInstitucionales.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PremiosInstitucionales.DBServices.Convocatoria
{
    
    public class ConvocatoriaService
    {
        private static wPremiosInstitucionalesdbEntities dbContext;
        public static List<PI_BA_Premio> GetAllPremios()
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            return dbContext.PI_BA_Premio.ToList();
        }

        public static PI_BA_Premio GetPremioById(String idPremio)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            return dbContext.PI_BA_Premio.Where(p => p.cvePremio == idPremio)
                    .FirstOrDefault(); 
        }

        public static void SaveNewConvocatoria(string idPremio, PI_BA_Convocatoria nuevaConvocatoria)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            nuevaConvocatoria.cvePremio = idPremio;
            dbContext.PI_BA_Convocatoria.Add(nuevaConvocatoria);
            dbContext.SaveChanges();
        }

        public static PI_BA_Convocatoria GetMostRecentConvocatoria(string idPremio)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            var query = (from convo in dbContext.PI_BA_Convocatoria
                         where convo.cvePremio.Equals(idPremio)
                         orderby convo.FechaFin descending
                         select convo).FirstOrDefault();
            return query;
            
        }

        public static void ActualizarConvocatoria(string idConvocatoria, string descripcion, string titulo)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            var convo = dbContext.PI_BA_Convocatoria.Where(c => c.cveConvocatoria == idConvocatoria)
                .FirstOrDefault<PI_BA_Convocatoria>();
            convo.Descripcion = descripcion;
            convo.TituloConvocatoria = titulo;
            dbContext.SaveChanges();
        }

        public static List<PI_BA_Candidato> ObtenerCandidatosPorAplicaciones(List<PI_BA_Aplicacion> listaAplicaciones)
        {// Obtengo lista de aplicaciones, regreso lista de candidatos

            dbContext = new wPremiosInstitucionalesdbEntities();
            var lista = new List<PI_BA_Candidato>();

            foreach (var aplicacion in listaAplicaciones)
            {
                var candidato = dbContext.PI_BA_Candidato.Where(c => c.cveCandidato == aplicacion.cveCandidato)
                    .FirstOrDefault<PI_BA_Candidato>();
                lista.Add(candidato);
            }

            return lista;  
        }

        public static List<PI_BA_Aplicacion> ObtenerAplicacionesPorCategoria(string cve_categoria)
        {// Obtengo lista de aplicaciones dada una categoria
            dbContext = new wPremiosInstitucionalesdbEntities();
            var categoria = dbContext.PI_BA_Categoria.Where(c => c.cveCategoria == cve_categoria)
                .FirstOrDefault<PI_BA_Categoria>();

            var aplicaciones = dbContext.PI_BA_Aplicacion.Where(c => c.cveCategoria == categoria.cveCategoria).ToList();
         
            return aplicaciones;

        }

        public static Dictionary<string, string[]> ObtenerPreguntasConRespuestasPorAplicacion(PI_BA_Aplicacion aplicacion)
        { //Se obtiene un diccionario de preguntas y respuestas por aplicacion, ordenado por clave aplicacion

            dbContext = new wPremiosInstitucionalesdbEntities();

            var respuestas = dbContext.PI_BA_Respuesta.Where(c => c.PI_BA_Aplicacion == aplicacion).ToList();

            var diccionarioPregResp = new Dictionary<string,string[]>();

            foreach (var respuesta in respuestas)
            {
                var obtengoPregunta = dbContext.PI_BA_Pregunta.Where(c => c.cvePregunta == respuesta.cvePregunta)
                    .FirstOrDefault<PI_BA_Pregunta>();

                diccionarioPregResp.Add(respuesta.cveAplicacion, new string[] {obtengoPregunta.Texto, respuesta.Valor});
            }

            return diccionarioPregResp;

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

            }
            else
            {
                return null;
            }
        }

    }

    
}