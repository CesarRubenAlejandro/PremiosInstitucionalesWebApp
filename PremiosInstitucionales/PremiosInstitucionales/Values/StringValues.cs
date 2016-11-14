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
        public static readonly String Solicitado = "solicitada";
        public static readonly String Rechazado = "rechazada";
        public static readonly String Modificado = "modificada";
        public static readonly String Aceptado = "aceptada";

        //explicacion de valores para status de aplicacion
        public static readonly String ExplicacionSolicitado = "Tu solicitud ha sido enviada y se encuentra en espera de revisión. Vuelve más tarde.";
        public static readonly String ExplicacionRechazado = "Tu solicitud ha sido revisada y rechazada. Haz clic aquí para corregirla.";
        public static readonly String ExplicacionModificado = "Tu modificación ha sido enviada y se encuentra en espera de revisión. Vuelve más tarde.";
        public static readonly String ExplicacionAceptado = "Tu solicitud ha sido revisada y aceptada. ¡Mucha suerte!";
        public static readonly String ExplicacionFin = "El veredicto final ya ha sido dado. ¡Gracias por participar!";

    }
}