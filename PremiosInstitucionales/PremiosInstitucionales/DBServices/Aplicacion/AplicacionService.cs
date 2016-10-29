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
    }
}