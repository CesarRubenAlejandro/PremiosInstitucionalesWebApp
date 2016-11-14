using PremiosInstitucionales.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PremiosInstitucionales.DBServices.InformacionPersonalCandidato
{
    public class InformacionPersonalCandidatoService
    {
        private static wPremiosInstitucionalesdbEntities dbContext;

        public static Tuple<string, string> GetNombre(string correo)
        {
            string nombres = "";
            string apellidos = "";
            dbContext = new wPremiosInstitucionalesdbEntities();
            PI_BA_Candidato candidato = dbContext.PI_BA_Candidato.Where(c => (c.Correo == correo) && (c.Confirmado == true))
                    .FirstOrDefault();
            if (candidato != null)
            {
                nombres = candidato.Nombre;
                apellidos = candidato.Apellido;
            }
            return new Tuple<string, string>(nombres, apellidos);
        }

        public static bool Set(string correoAntiguo, string nombres, string apellidos, string correoNuevo)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            PI_BA_Candidato candidato = dbContext.PI_BA_Candidato.Where(c => (c.Correo == correoAntiguo) && (c.Confirmado == true))
                    .FirstOrDefault();
            if (candidato == null)
            {
                return false;    
            }
            candidato.Nombre = nombres;
            candidato.Apellido = apellidos;
            candidato.Correo = correoNuevo;
            dbContext.SaveChanges();
            return true;
        }
    }
}   