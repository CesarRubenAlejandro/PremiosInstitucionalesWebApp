using PremiosInstitucionales.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PremiosInstitucionales.DBServices.InformacionPersonalCandidato
{
    public class InformacionPersonalCandidatoService
    {

        public static List<PI_BA_Candidato> GetCandidatos()
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    return dbContext.GetCandidato(null, null).ToList();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return null;
                }
            }
        }

        public static PI_BA_Candidato GetCandidatoByCorreo(String sCorreo)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    return dbContext.GetCandidato(sCorreo, null).FirstOrDefault();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return null;
                }
            }
        }

        public static PI_BA_Candidato GetCandidatoById(String sUserId)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    return dbContext.GetCandidato(null, sUserId).FirstOrDefault();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return null;
                }
            }
        }

        public static bool UpdateCandidato(PI_BA_Candidato objCandidato)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    dbContext.UpdateCandidato(  objCandidato.cveCandidato,
                                                objCandidato.Password,
                                                objCandidato.Nombre,
                                                objCandidato.Apellido,
                                                objCandidato.Confirmado,
                                                objCandidato.Correo,
                                                objCandidato.CodigoConfirmacion,
                                                objCandidato.Telefono,
                                                objCandidato.Nacionalidad,
                                                objCandidato.RFC,
                                                objCandidato.Direccion,
                                                objCandidato.NombreImagen,
                                                objCandidato.FechaPrivacidadDatos  );
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

        public static bool GuardaNuevaContrasena(string sCorreo, string sNewPassword)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    var candidato = GetCandidatoByCorreo(sCorreo);
                    candidato.Password = sNewPassword;
                    UpdateCandidato(candidato);
                    return true;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return false;
                }
            }
        }

        public static bool CambiaImagen(string sUserId, string sCorreo, string sNombreImagen)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    if (sUserId != null)
                    {
                        var candidato = GetCandidatoById(sUserId);
                        candidato.NombreImagen = sNombreImagen;
                        UpdateCandidato(candidato);
                        return true;
                    }
                    else if (sCorreo != null)
                    {
                        var candidato = GetCandidatoByCorreo(sCorreo);
                        candidato.NombreImagen = sNombreImagen;
                        UpdateCandidato(candidato);
                        return true;
                    }

                    // Si no existe el usuario o no se brindo ni el id ni el correo
                    return false;
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