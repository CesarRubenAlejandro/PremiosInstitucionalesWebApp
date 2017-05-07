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
        //Comentario
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

        public static PI_BA_Candidato GetCandidatoByCorreo(String correo)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            try
            {
                return dbContext.PI_BA_Candidato.Where(c => c.Correo.Equals(correo)).FirstOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static PI_BA_Candidato GetCandidatoById(String id)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            try
            {
                return dbContext.PI_BA_Candidato.Where(c => c.cveCandidato.Equals(id)).FirstOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static bool GuardarCambios(PI_BA_Candidato container, string correo)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            try
            {
                var candidato = dbContext.PI_BA_Candidato.Where(c => c.Correo == correo)
                    .FirstOrDefault();
                candidato.Apellido = container.Apellido;
                candidato.Direccion = container.Direccion;
                candidato.Nacionalidad = container.Nacionalidad;
                candidato.Nombre = container.Nombre;
                candidato.RFC = container.RFC;
                candidato.Telefono = container.Telefono;
                if(container.FechaPrivacidadDatos.HasValue)
                candidato.FechaPrivacidadDatos = container.FechaPrivacidadDatos;
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }


        }

        public static bool GuardaNuevaContrasena(PI_BA_Candidato container, string correo)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            try
            {
                var candidato = dbContext.PI_BA_Candidato.Where(c => c.Correo == correo)
                    .FirstOrDefault();
                candidato.Password = container.Password;
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool CambiaImagen(PI_BA_Candidato container, string correo)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            try
            {
                var candidato = dbContext.PI_BA_Candidato.Where(c => c.Correo == correo)
                    .FirstOrDefault();
                candidato.NombreImagen = container.NombreImagen;
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }


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

        public static List<PI_BA_Candidato> GetCandidatos()
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            var candidatos = dbContext.PI_BA_Candidato.ToList();
            return candidatos;
        }
    }
}   