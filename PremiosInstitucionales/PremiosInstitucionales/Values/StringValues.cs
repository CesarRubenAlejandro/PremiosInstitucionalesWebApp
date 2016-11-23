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

        // valores para status de aplicacion - asi estan en BD
        public static readonly String Solicitado = "solicitada";
        public static readonly String Rechazado = "rechazada";
        public static readonly String Modificado = "modificada";
        public static readonly String Aceptado = "aceptada";

        //titulo de valores para status a mostrar al usuario
        public static readonly String TituloSolicitado = "Solicitud registrada";
        public static readonly String TituloRechazado = "Requiere cambios";
        public static readonly String TituloModificado = "Solicitud modificada";
        public static readonly String TituloAceptado = "Solicitud aceptada";
        public static readonly String TituloFin = "Convocatoria cerrada";

        //explicacion de valores para status a mostrar al usuario
        public static readonly String ExplicacionSolicitado = "Tu solicitud ha sido registrada y se encuentra en espera de revisión, te llegará una notificación más adelante.";
        public static readonly String ExplicacionRechazado = "Tu solicitud ha sido revisada y encontramos que requiere más información. ";
        public static readonly String ExplicacionModificado = "Tu registro modificado ha sido enviado y se encuentra en espera de revisión, te llegará una notificación más adelante.";
        public static readonly String ExplicacionAceptado = "Tu registro de solicitud ha sido revisado y aceptado. ¡Mucha suerte!";
        public static readonly String ExplicacionFin = "El veredicto final ya ha sido dado. ¡Gracias por participar!";

        // valores para columnas de reporte para jueces
        public static readonly String ColumnaReporteJuecesNombreCandidato = "Nombre candidato";
        public static readonly String ColumnaReporteJuecesCorreo = "Correo candidato";
    }
}