using PremiosInstitucionales.Entities.Models;
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
            return dbContext.PI_BA_Candidato.Where(c => c.Correo == correoCandidato)
                    .FirstOrDefault();
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
                tipo = "candidato";
            }
            else
            {
                var juez = GetJuez(correo);

                if (juez != null)
                {
                    user = juez.Correo;
                    password = juez.Password;
                    tipo = "juez";
                }
                else
                {
                    var administrador = GetAdministrador(correo);

                    if (administrador != null)
                    {
                        user = administrador.Correo;
                        password = administrador.Password;
                        tipo = "administrador";
                    }
                    else
                    {
                        user = "notFound";
                        password = "notFound";
                        tipo = "notFound";
                    }
                }
            }

            if (user != "notFound")
            {
                if (password != givenPassword)
                {
                    return "incorrect";
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