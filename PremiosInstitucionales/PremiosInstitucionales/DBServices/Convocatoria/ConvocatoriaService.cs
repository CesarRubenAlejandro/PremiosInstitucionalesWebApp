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
    }

    
}