using PremiosInstitucionales.Entities.Models;
using PremiosInstitucionales.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PremiosInstitucionales.DBServices.Login
{

    public class LoginService
    {
        private static wPremiosInstitucionalesdbEntities dbContext;

        public static PI_BA_Candidato GetCandidato(String correoCandidato)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            return dbContext.PI_BA_Candidato.Where(c => (c.Correo == correoCandidato) && (c.Confirmado == true))
                    .FirstOrDefault();
        }

        public static PI_BA_Candidato GetCandidatoByConfirmacion(String codigo)
        {
            try
            {
                dbContext = new wPremiosInstitucionalesdbEntities();
                return dbContext.PI_BA_Candidato.Where(c => c.CodigoConfirmacion == codigo).FirstOrDefault();
            } catch (Exception e)
            {
                return null;
            } 
        }

        public static PI_BA_Juez GetJuez(String correoJuez)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            return dbContext.PI_BA_Juez.Where(c => c.Correo == correoJuez)
                    .FirstOrDefault();
        }

        public static PI_SE_Administrador GetAdministrador(String correoAdmin)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            return dbContext.PI_SE_Administrador.Where(c => c.Correo == correoAdmin)
                    .FirstOrDefault();
        }

        public static string GetUsuario(String correo, String givenPassword)
        {
            String user;
            String password;
            String tipo;

            var candidato = GetCandidato(correo);

            if (candidato != null)
            {
                user = candidato.Correo;
                password = candidato.Password;
                tipo = StringValues.RolCandidato;
            }
            else
            {
                var juez = GetJuez(correo);

                if (juez != null)
                {
                    user = juez.Correo;
                    password = juez.Password;
                    tipo = StringValues.RolJuez;
                }
                else
                {
                    var administrador = GetAdministrador(correo);

                    if (administrador != null)
                    {
                        user = administrador.Correo;
                        password = administrador.Password;
                        tipo = StringValues.RolAdmin;
                    }
                    else
                    {
                        user = StringValues.RolNotFound;
                        password = StringValues.RolNotFound;
                        tipo = StringValues.RolNotFound;
                    }
                }
            }

            if (user != StringValues.RolNotFound)
            {
                if (password != givenPassword)
                {
                    return StringValues.RolIncorrecto;
                }
                else
                {
                    return tipo;
                }
            }

            return user;
        }


    }


}