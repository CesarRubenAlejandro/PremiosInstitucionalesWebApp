using PremiosInstitucionales.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using PremiosInstitucionales.DBServices.InformacionPersonalCandidato;

namespace PremiosInstitucionales.DBServices.Convocatoria
{
    public class ConvocatoriaService
    {
        public static List<PI_BA_Premio> GetAllPremios()
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    return dbContext.GetPremio(null).ToList();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return null;
                }
            }
        }

        public static PI_BA_Premio GetPremioById(String idPremio)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    return dbContext.GetPremio(idPremio).FirstOrDefault();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return null;
                }
            }
        }

        public static PI_BA_Premio GetPremioByCategoria(string idCategoria)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    return dbContext.GetPremioByIdCategoria(idCategoria).FirstOrDefault();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return null;
                }
            }
        }

        public static void CreatePremio(PI_BA_Premio pr)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    dbContext.AddPremio(pr.cvePremio, pr.Nombre, pr.NombreImagen, pr.Descripcion, pr.FechaCreacion, pr.UsuarioCreacion, pr.FechaEdicion, pr.UsuarioEdicion);
                    dbContext.SaveChanges();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                }
            }
        }

        public static void ActualizarPremio(string idPremio, string titulo, string descripcion, string imagenurl, string user)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    var pr = GetPremioById(idPremio);
                    dbContext.UpdatePremio(pr.cvePremio, titulo, imagenurl, descripcion, pr.FechaCreacion, pr.UsuarioCreacion, DateTime.Now, user);
                    dbContext.SaveChanges();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                }
            }
        }

        public static void CreateCategoria(PI_BA_Categoria cat)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    dbContext.AddCategoria(cat.cveCategoria, cat.Nombre, cat.cveConvocatoria, cat.cveAplicacionGanadora, cat.FechaCreacion, cat.UsuarioCreacion, cat.FechaEdicion, cat.UsuarioEdicion);
                    dbContext.SaveChanges();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                }
            }
        }

        public static PI_BA_Categoria GetCategoriaById(String idCategoria)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    return dbContext.GetCategoria(idCategoria, null).FirstOrDefault();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return null;
                }
            }
        }

        public static List<PI_BA_Categoria> GetCategoriasByConvocatoria(String idConvocatoria)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    return dbContext.GetCategoria(null, idConvocatoria).ToList();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return null;
                }
            }
        }

        public static void CreateForma(PI_BA_Forma fr)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    dbContext.AddForma(fr.cveForma, fr.cveCategoria, fr.FechaCreacion, fr.UsuarioCreacion, fr.FechaEdicion, fr.UsuarioEdicion);
                    dbContext.SaveChanges();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                }
            }
        }

        public static PI_BA_Forma GetFormaByID(string idForma)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    return dbContext.GetForma(idForma).FirstOrDefault();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return null;
                }
            }
        }

        public static void CreateConvocatoria(string idPremio, PI_BA_Convocatoria cv)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    dbContext.AddConvocatoria(cv.cveConvocatoria, cv.Descripcion, cv.FechaInicio, cv.FechaFin, idPremio, cv.TituloConvocatoria, cv.FechaVeredicto, cv.FechaCreacion, cv.UsuarioCreacion, cv.FechaEdicion, cv.UsuarioEdicion);
                    dbContext.SaveChanges();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                }
            }
        }

        public static PI_BA_Convocatoria GetConvocatoriaById(String idConvocatoria)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    return dbContext.GetConvocatoria(idConvocatoria).FirstOrDefault();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return null;
                }
            }
        }

        public static PI_BA_Convocatoria GetMostRecentConvocatoria(string idPremio)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    return dbContext.GetMostRecentConvocatoria(idPremio).FirstOrDefault();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return null;
                }
            }
        }

        public static List<PI_BA_Convocatoria> GetConvocatoriasPremio(string idPremio)
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    return dbContext.GetMostRecentConvocatoria(idPremio).ToList();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return null;
                }
            }
        }

        public static Dictionary<PI_BA_Aplicacion, PI_BA_Candidato> JuezObtenerCandidatosPorAplicaciones(List<PI_BA_Aplicacion> listaAplicaciones)
        {// Obtengo lista de aplicaciones, regreso diccionario de aplicaciones con candidatos
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    var lista = new Dictionary<PI_BA_Aplicacion, PI_BA_Candidato>();

                    foreach (var aplicacion in listaAplicaciones)
                    {
                        var candidato = InformacionPersonalCandidatoService.GetCandidatoById(aplicacion.cveCandidato);

                        // Se despliegan las aplicaciones aceptadas unicamente
                        if (aplicacion.Status == Values.StringValues.Aceptado)
                        {
                            lista.Add(aplicacion, candidato);
                        }

                    }
                    return lista;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return null;
                }
            }
        }

        public static List<PI_BA_Aplicacion> ObtenerAplicacionesPorCategoria(string idCategoria)
        {// Obtengo lista de aplicaciones dada una categoria
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    // TODO
                    return dbContext.PI_BA_Aplicacion.Where(a => a.cveCategoria.Equals(idCategoria)).ToList();
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return null;
                }
            }
        }

        public static List<PI_BA_Categoria> GetCategoriasPendientes()
        {
            using (var dbContext = new wPremiosInstitucionalesdbEntities())
            {
                try
                {
                    var categorias = dbContext.GetCategoriasPendientes().ToList();
                    List<PI_BA_Categoria> validCategories = new List<PI_BA_Categoria>();

                    foreach (var c in categorias)
                    {
                        var convo = GetConvocatoriaById(c.cveConvocatoria);
                        if (DateTime.Today >= convo.FechaInicio)
                        {
                            validCategories.Add(c);
                        }
                    }

                    return validCategories;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Catched Exception: " + Ex.Message + Environment.NewLine);
                    return null;
                }
            }
        }
    }
}