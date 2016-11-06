using PremiosInstitucionales.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PremiosInstitucionales.DBServices.Recuperar
{
    public class RecuperarService
    {
        private static wPremiosInstitucionalesdbEntities dbContext;

        public static String GetID(String email)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            String id = null;
            var candidato = dbContext.PI_BA_Candidato.Where(c => (c.Correo == email) && (c.Confirmado == true)).FirstOrDefault();
            if (candidato == null)
            {
                var juez = dbContext.PI_BA_Juez.Where(c => c.Correo == email).FirstOrDefault();
                if (juez == null)
                {
                    var admin = dbContext.PI_SE_Administrador.Where(c => c.Correo == email).FirstOrDefault();
                    if (admin != null)
                    {
                        id = "a" + admin.CveAdministrador;
                    }
                }
                else
                {
                    id = "j" + juez.cveJuez;
                }
            } else
            {
                id = "c" + candidato.cveCandidato;
            }
            return id;
        }

        public static bool CambiarContrasenaCandidato(String cve, String contrasena)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            PI_BA_Candidato candidato = dbContext.PI_BA_Candidato.Where(c => (c.cveCandidato == cve) && (c.Confirmado == true))
                    .FirstOrDefault();
            if (candidato == null)
            {
                return false;
            }
            else
            {
                candidato.Password = contrasena;
                dbContext.SaveChanges();
                return true;
            }
        }
        public static bool CambiarContrasenaJuez(String cve, String contrasena)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            PI_BA_Juez juez = dbContext.PI_BA_Juez.Where(c => (c.cveJuez == cve)).FirstOrDefault();
            if (juez == null)
            {
                return false;
            }
            else
            {
                juez.Password = contrasena;
                dbContext.SaveChanges();
                return true;
            }
        }
        public static bool CambiarContrasenaAdministrador(String cve, String contrasena)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            PI_SE_Administrador administrador = dbContext.PI_SE_Administrador.Where(c => (c.CveAdministrador == cve)).FirstOrDefault();
            if (administrador == null)
            {
                return false;
            }
            else
            {
                administrador.Password = contrasena;
                dbContext.SaveChanges();
                return true;
            }
        }
    }
}