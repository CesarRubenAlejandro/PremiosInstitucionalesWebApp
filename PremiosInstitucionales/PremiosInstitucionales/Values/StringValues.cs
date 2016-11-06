using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PremiosInstitucionales.Values
{
    public static class StringValues
    {
        // valores para roles de usuarios
        public static readonly String RolCandidato = "candidato";
        public static readonly String RolJuez = "juez";
        public static readonly String RolAdmin = "admin";
        public static readonly String RolIncorrecto = "incorrect";
        public static readonly String RolNotFound = "not found";

        // valores para variables a guardar en sesion
        public static readonly String CorreoSesion = "correo";
        public static readonly String RolSesion = "rol";

        // valores para status de aplicacion
        public static readonly String FormularioEnviado = "0";
    }
}