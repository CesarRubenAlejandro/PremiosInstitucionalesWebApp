using PremiosInstitucionales.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PremiosInstitucionales.DBServices.InformacionPersonalJuez
{
    public class InformacionPersonalJuezService
    {
        public static List<PI_BA_Juez> GetJueces()
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    return dbContext.GetJuez(null, null).ToList();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return null;
                }
            }
        }

        public static PI_BA_Juez GetJuezByCorreo(string correo)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    return dbContext.GetJuez(correo, null).FirstOrDefault();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return null;
                }
            }
        }

        public static PI_BA_Juez GetJuezById(string id)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    return dbContext.GetJuez(null, id).FirstOrDefault();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return null;
                }
            }
        }

        public static bool UpdateJuez(PI_BA_Juez objJuez)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    dbContext.UpdateJuez(   objJuez.cveJuez,
                                            objJuez.Password,
                                            objJuez.Nombre,
                                            objJuez.Apellido,
                                            objJuez.Correo,
                                            objJuez.NombreImagen   );
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
                    var juez = GetJuezByCorreo(sCorreo);
                    juez.Password = sNewPassword;
                    UpdateJuez(juez);
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
                        var juez = GetJuezById(sUserId);
                        juez.NombreImagen = sNombreImagen;
                        UpdateJuez(juez);
                        return true;
                    }
                    else if (sCorreo != null)
                    {
                        var juez = GetJuezByCorreo(sCorreo);
                        juez.NombreImagen = sNombreImagen;
                        UpdateJuez(juez);
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