using System;
using PremiosInstitucionales.DBServices.InformacionPersonalCandidato;
using PremiosInstitucionales.DBServices.InformacionPersonalJuez;
using PremiosInstitucionales.DBServices.InformacionPersonalAdministrador;

namespace PremiosInstitucionales.DBServices.Recuperar
{
    public class RecuperarService
    {
        public static String GetID(String email)
        {
            String id = null;
            var candidato = InformacionPersonalCandidatoService.GetCandidatoByCorreo(email);
            if (candidato == null)
            {
                var juez = InformacionPersonalJuezService.GetJuezByCorreo(email);
                if (juez == null)
                {
                    var admin = InformacionPersonalAdministradorService.GetAdministradorByCorreo(email);
                    if (admin != null)
                    {
                        id = "a" + admin.cveAdministrador;
                    }
                }
                else
                {
                    id = "j" + juez.cveJuez;
                }
            }
            else
            {
                id = "c" + candidato.cveCandidato;
            }
            return id;
        }

        public static bool CambiarContrasenaCandidato(String cve, String password)
        {
            var objCandidato = InformacionPersonalCandidatoService.GetCandidatoById(cve);
            if (objCandidato != null)
            {
                objCandidato.Password = password;
                return InformacionPersonalCandidatoService.UpdateCandidato(objCandidato);
            }
            return false;
        }
        public static bool CambiarContrasenaJuez(String cve, String password)
        {
            var objJuez = InformacionPersonalJuezService.GetJuezById(cve);
            if (objJuez != null)
            {
                objJuez.Password = password;
                return InformacionPersonalJuezService.UpdateJuez(objJuez);
            }
            return false;
        }
        public static bool CambiarContrasenaAdministrador(String cve, String password)
        {
            var objAdministrador = InformacionPersonalAdministradorService.GetAdministradorById(cve);
            if (objAdministrador != null)
            {
                objAdministrador.Password = password;
                return InformacionPersonalAdministradorService.UpdateAdministrador(objAdministrador);
            }
            return false;
        }
    }
}