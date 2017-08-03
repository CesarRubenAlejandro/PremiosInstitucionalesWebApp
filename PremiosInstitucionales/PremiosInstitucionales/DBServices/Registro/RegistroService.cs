using PremiosInstitucionales.Entities.Models;
using System;
using System.Linq;

namespace PremiosInstitucionales.DBServices.Registro
{
    public class RegistroService
    {
        public static bool RegistraJuez(string correo, string contrasena)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    // Si no hay ningun usuario registrado con ese correo
                    if (!ExisteUsuario(correo))
                    {
                        dbContext.AddJuez(Guid.NewGuid().ToString(), contrasena, null, null, correo, null);
                        dbContext.SaveChanges();
                        return true;
                    }

                    // Si alguien ya esta registrado con este correo
                    return false;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return false;
                }
            }
        }

        public static bool RegistraCandidato(string email, string password, string nombre, string apellido, string codigoConfirmacion)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    // Si no hay ningun usuario registrado con ese correo
                    if (!ExisteUsuario(email))
                    {
                        dbContext.AddCandidato(Guid.NewGuid().ToString(), password, nombre, apellido, null, email, codigoConfirmacion, null, null, null, null, null, null);
                        dbContext.SaveChanges();
                        return true;
                    }

                    // Si alguien ya esta registrado con este correo
                    return false;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return false;
                }
            }
        }

        private static bool ExisteUsuario(string email)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    int cantCandidatos = dbContext.GetCandidato(email, null).ToList().Count;
                    int cantJueces     = dbContext.GetJuez(email, null).ToList().Count;
                    int cantAdmins     = dbContext.GetAdministrador(email, null).ToList().Count;

                    return (cantCandidatos + cantJueces + cantAdmins) > 0;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return false;
                }
            }
        }

        public static bool ConfirmarCandidato(string codigoConfirmacion)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    dbContext.ConfirmarCandidato(codigoConfirmacion);
                    dbContext.SaveChanges();
                    return true;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return false;
                }
            }
        }
    }
}