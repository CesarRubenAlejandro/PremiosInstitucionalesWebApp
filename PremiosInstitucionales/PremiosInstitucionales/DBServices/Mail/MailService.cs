using PremiosInstitucionales.DBServices.InformacionPersonalCandidato;
using PremiosInstitucionales.DBServices.InformacionPersonalJuez;
using PremiosInstitucionales.DBServices.Convocatoria;
using PremiosInstitucionales.Entities.Models;
using PremiosInstitucionales.Values;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace PremiosInstitucionales.DBServices.Mail
{
    public class MailService : System.Web.UI.Page
    {
        public PI_SE_Configuracion GetConfiguracion(String id)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    return dbContext.GetConfiguracion(id).FirstOrDefault();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return null;
                }
            }
        }

        public bool EnviarCorreo (String toMail, String titulo, String cuerpo)
        {
            // Por default es el 001, en caso de que tengan mas de uno tendrian que agregar el parametro de la cveConfiguracion
            var mailConfig = GetConfiguracion("001");

            try
            {
                using (MailMessage mm = new MailMessage(mailConfig.Correo, toMail))
                {
                    mm.Subject = titulo;
                    mm.IsBodyHtml = true;
                    var bodyContent = cuerpo;
                    try
                    {
                        mm.Body = bodyContent;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = mailConfig.Host;
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential(mailConfig.Correo, mailConfig.Password);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = mailConfig.Puerto;
                        smtp.Send(mm);
                    }
                    catch (Exception Ex2)
                    {
                        // No pude enviar el correo a ese destinatario
                        Console.WriteLine("Catched Exception: " + Ex2.Message + Environment.NewLine);
                        return false;
                    }
                }
                // Ya envie el correo a ese destinatario 
                return true;
            }
            catch (Exception Ex)
            {
                // No me pude conectar al servicio del mail
                Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                return false;
            }
        }

        public bool EnviarCorreoRecuperacion(String toMail, String id)
        {
            String titulo = "Recuperación de contraseña para el sistema Premios Institucionales del Tec de Monterrey";
            String cuerpo = "";
            cuerpo = File.ReadAllText(Server.MapPath("~/Values/CorreoRecuperaPassword.txt"));
            cuerpo = cuerpo.Replace(StringValues.ContenidoCorreoFecha, DateTime.Today.ToShortDateString());
            cuerpo = cuerpo.Replace(StringValues.ContenidoCorreoId, id);

            switch (id[0])
            {
                case 'c':
                    cuerpo = cuerpo.Replace(StringValues.ContenidoCorreoNombre,
                    InformacionPersonalCandidatoService.GetCandidatoByCorreo(toMail).Nombre);
                    break;
                case 'j':
                    cuerpo = cuerpo.Replace(StringValues.ContenidoCorreoNombre,
                    InformacionPersonalJuezService.GetJuezByCorreo(toMail).Nombre);
                    break;
                case 'a':
                    cuerpo = cuerpo.Replace(StringValues.ContenidoCorreoNombre,
                    "Administrador");
                    break;
            }

            return EnviarCorreo(toMail, titulo, cuerpo);
        }

        public bool EnviarCorreoConfirmacion(String toMail, String codigoConfirmacion)
        {
            String titulo = "Confirma tu cuenta para Premios Institucionales del Tec de Monterrey";
            String cuerpo = "";
            cuerpo = File.ReadAllText(Server.MapPath("~/Values/CorreoConfirmaCuenta.txt"));
            cuerpo = cuerpo.Replace(StringValues.ContenidoCorreoFecha, DateTime.Today.ToShortDateString());
            cuerpo = cuerpo.Replace(StringValues.ContenidoCorreoMail, toMail);
            cuerpo = cuerpo.Replace(StringValues.ContenidoCorreoConfirmacion, codigoConfirmacion);

            return EnviarCorreo(toMail, titulo, cuerpo);
        }

        public bool EnviarCorreoInvitacionCandidato(String toMail, String nombre, String apellido)
        {
            String titulo = "Invitación para unirse a Premios Institucionales del Tec de Monterrey";
            String cuerpo = "Te están invitando a que participes en uno de los premios institucionales. Entra a INSERTA LINK AQUI";
            return EnviarCorreo(toMail, titulo, cuerpo);
        }

        public bool EnviarCorreoInvitacionJuez(String toMail, String password)
        {
            String titulo = "Fuiste Asignado como Juez para Premios Institucionales del Tec de Monterrey.";
            String cuerpo = "Utilizando esta misma direccion de correo ("+toMail+") y la contrasena " + password + ", podras acceder a la plataforma en la liga INSERTA LINK AQUI, donde podras participar como jurado en las categorias que se te asignen. Por último, le pedimos atentamente si es posible que modifique su contraseña lo antes posible.";
            return EnviarCorreo(toMail, titulo, cuerpo);
        }

        public bool EnviarCorreoRechazarAplicacion(PI_BA_Aplicacion aplicacion, String razon)
        {
            var candidato = InformacionPersonalCandidatoService.GetCandidatoById(aplicacion.cveCandidato);
            var categoria = ConvocatoriaService.GetCategoriaById(aplicacion.cveCategoria);
            var convocatoria = ConvocatoriaService.GetConvocatoriaById(categoria.cveConvocatoria);
            var premio = ConvocatoriaService.GetPremioByCategoria(categoria.cveCategoria);

            String toMail = candidato.Correo;
            String titulo = "Requiere cambios la solicitud de registro en el sistema Premios Institucionales del Tec de Monterrey.";
            String cuerpo = "";
            cuerpo = File.ReadAllText(Server.MapPath("~/Values/CorreoSolicitudCambio.txt"));
            cuerpo = cuerpo.Replace(StringValues.ContenidoCorreoFecha, DateTime.Today.ToShortDateString());
            cuerpo = cuerpo.Replace(StringValues.ContenidoCorreoNombre, candidato.Nombre);
            cuerpo = cuerpo.Replace(StringValues.ContenidoCorreoPremio, premio.Nombre);
            cuerpo = cuerpo.Replace(StringValues.ContenidoCorreoCategoria, categoria.Nombre);
            cuerpo = cuerpo.Replace(StringValues.ContenidoCorreoRazon, razon);

            return EnviarCorreo(toMail, titulo, cuerpo);
        }

        public bool EnviarCorreoVeredictoFinal(String toMail)
        {
            String titulo = "Veredicto Final.";
            String cuerpo = "Ya existe un ganador";

            return EnviarCorreo(toMail, titulo, cuerpo);
        }
    }
}