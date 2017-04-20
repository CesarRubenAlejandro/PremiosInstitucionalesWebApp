using PremiosInstitucionales.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PremiosInstitucionales.DBServices.InformacionPersonalJuez
{
    public class InformacionPersonalJuezService
    {
        private static wPremiosInstitucionalesdbEntities dbContext;
        //Comentario

        public static PI_BA_Juez GetJuezByCorreo(String correo)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            try
            {
                return dbContext.PI_BA_Juez.Where(c => c.Correo.Equals(correo)).FirstOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static PI_BA_Juez GetJuezById(String id)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            try
            {
                return dbContext.PI_BA_Juez.Where(c => c.cveJuez.Equals(id)).FirstOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static bool GuardarCambios(PI_BA_Juez container, string correo)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            try
            {
                var juez = dbContext.PI_BA_Juez.Where(c => c.Correo == correo)
                    .FirstOrDefault();
                juez.Apellido = container.Apellido;
                juez.Nombre = container.Nombre;
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }


        }

        public static bool GuardaNuevaContrasena(PI_BA_Juez container, string correo)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            try
            {
                var juez = dbContext.PI_BA_Juez.Where(c => c.Correo == correo)
                    .FirstOrDefault();
                juez.Password = container.Password;
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool CambiaImagen(PI_BA_Juez container, string correo)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            try
            {
                var juez = dbContext.PI_BA_Juez.Where(c => c.Correo == correo)
                    .FirstOrDefault();
                juez.NombreImagen = container.NombreImagen;
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }


        }

        public static List<PI_BA_Juez> GetJueces()
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            var jueces = dbContext.PI_BA_Juez.ToList();
            return jueces;
        }

    }
}   