using PremiosInstitucionales.Entities.Models;
using PremiosInstitucionales.Values;
using System;
using System.Linq;

namespace PremiosInstitucionales.DBServices.Login
{
    public class LoginService
    {
        public static PI_BA_Candidato GetCandidato(String sCorreo)
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

        public static PI_BA_Juez GetJuez(String sCorreo)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    return dbContext.GetJuez(sCorreo, null).FirstOrDefault();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return null;
                }
            }
        }

        public static PI_SE_Administrador GetAdministrador(String sCorreo)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    return dbContext.GetAdministrador(sCorreo, null).FirstOrDefault();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return null;
                }
            }
        }

        public static string GetUsuario(String sCorreo, String givenPassword)
        {
            try
            {
                String sUser;
                String sPassword;
                String sTipo;

                var candidato = GetCandidato(sCorreo);

                if (candidato != null)
                {
                    sUser = candidato.Correo;
                    sPassword = candidato.Password;
                    sTipo = StringValues.RolCandidato;
                }
                else
                {
                    var juez = GetJuez(sCorreo);

                    if (juez != null)
                    {
                        sUser = juez.Correo;
                        sPassword = juez.Password;
                        sTipo = StringValues.RolJuez;
                    }
                    else
                    {
                        var administrador = GetAdministrador(sCorreo);

                        if (administrador != null)
                        {
                            sUser = administrador.Correo;
                            sPassword = administrador.Password;
                            sTipo = StringValues.RolAdmin;
                        }
                        else
                        {
                            sUser = StringValues.RolNotFound;
                            sPassword = StringValues.RolNotFound;
                            sTipo = StringValues.RolNotFound;
                        }
                    }
                }

                if (sUser != StringValues.RolNotFound)
                {
                    if (sPassword != givenPassword)
                    {
                        return StringValues.RolIncorrecto;
                    }
                    else
                    {
                        return sTipo;
                    }
                }
                return sUser;
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                return null;
            }
        }
    }
}