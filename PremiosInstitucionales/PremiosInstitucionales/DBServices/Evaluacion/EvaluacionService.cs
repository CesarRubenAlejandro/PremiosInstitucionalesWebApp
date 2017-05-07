using PremiosInstitucionales.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PremiosInstitucionales.DBServices.Evaluacion
{
    public class EvaluacionService
    {
        private static wPremiosInstitucionalesdbEntities dbContext;

        public static List<PI_BA_Categoria> GetCategoriaByJuez(String email)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            var juez = dbContext.PI_BA_Juez.Where(j => j.Correo.Equals(email)).First();
            var result = (from jc in dbContext.PI_BA_JuezPorCategoria
                          from cat in dbContext.PI_BA_Categoria
                          where jc.cveJuez.Equals(juez.cveJuez) && cat.cveCategoria.Equals(jc.cveCategoria)
                          select cat).ToList();
            return result;
        }

        public static string GetNombrePremioByCategoria(String categoriaId)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            var result = (from p in dbContext.PI_BA_Premio
                          join convo in dbContext.PI_BA_Convocatoria on p.cvePremio equals convo.cvePremio
                          join cat in dbContext.PI_BA_Categoria on convo.cveConvocatoria equals cat.cveConvocatoria
                          where cat.cveCategoria.Equals(categoriaId)
                          select p).First().Nombre;
            return result;
        }

        public static void CrearEvaluacion(PI_BA_Evaluacion evaluacion)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            dbContext.PI_BA_Evaluacion.Add(evaluacion);
            dbContext.SaveChanges();
        }

        public static void ActualizaEvaluacion(String sEvalId, short calif)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            var eval = GetEvaluacionById(sEvalId);
            if(eval != null)
            {
                eval.Calificacion = calif;
            }
            dbContext.SaveChanges();
        }

        public static PI_BA_Evaluacion GetEvaluacionById(String evalId)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            var eval = dbContext.PI_BA_Evaluacion.Where(e => e.cveEvaluacion.Equals(evalId)).FirstOrDefault();
            return eval;
        }

        public static List<PI_BA_Evaluacion> GetEvaluacionesByAplicacion(String appId)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            var eval = dbContext.PI_BA_Evaluacion.Where(e => e.cveAplicacion.Equals(appId)).ToList();
            return eval;
        }

        public static PI_BA_Evaluacion GetEvaluacionByAplicacionAndJuez(String juezMail, String appId)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            var juez = dbContext.PI_BA_Juez.Where(j => j.Correo.Equals(juezMail)).First();

            if (juez == null)
                return null;

            var evaluacion = dbContext.PI_BA_Evaluacion.Where(e => e.cveJuez.Equals(juez.cveJuez) && e.cveAplicacion.Equals(appId)).FirstOrDefault();

            return evaluacion;
        }
    }
}