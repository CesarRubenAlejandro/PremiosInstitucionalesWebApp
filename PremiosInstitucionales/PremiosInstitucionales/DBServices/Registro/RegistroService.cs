using PremiosInstitucionales.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PremiosInstitucionales.DBServices.Registro
{
    public class RegistroService
    {
        private static wPremiosInstitucionalesdbEntities dbContext;

        public static bool Registrar(String email, String password)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            if (!exists(email))
            {
                dbContext = new wPremiosInstitucionalesdbEntities();
                PI_BA_Candidato candidato = new PI_BA_Candidato();
                candidato.cveCandidato = Guid.NewGuid().ToString();
                candidato.Correo = email;
                candidato.Password = password;
                dbContext.PI_BA_Candidato.Add(candidato);
                dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool exists(String email)
        {
            return dbContext.PI_BA_Candidato.Where(c => c.Correo.Equals(email)).ToList().Count > 0;
        }

    }
}