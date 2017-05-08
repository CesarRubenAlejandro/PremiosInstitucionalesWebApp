﻿using PremiosInstitucionales.Entities.Models;
using PremiosInstitucionales.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PremiosInstitucionales.DBServices.Convocatoria
{
    
    public class ConvocatoriaService
    {
        private static wPremiosInstitucionalesdbEntities dbContext;
        public static List<PI_BA_Premio> GetAllPremios()
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            return dbContext.PI_BA_Premio.ToList();
        }

        public static void CreatePremio(PI_BA_Premio premio)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            dbContext.PI_BA_Premio.Add(premio);
            dbContext.SaveChanges();
        }

        public static void CreateCategoria(PI_BA_Categoria categoria)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            dbContext.PI_BA_Categoria.Add(categoria);
            dbContext.SaveChanges();
        }

        public static void CreateForma(PI_BA_Forma forma)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            dbContext.PI_BA_Forma.Add(forma);
            dbContext.SaveChanges();
        }

        public static List<PI_BA_Categoria> GetCategoriasByConvocatoria(String idConvocatoria)
        {
            var query = (from cat in dbContext.PI_BA_Categoria
                         where cat.cveConvocatoria.Equals(idConvocatoria)
                         orderby cat.Nombre descending
                         select cat).ToList();
            return query;
        }
        public static PI_BA_Premio GetPremioById(String idPremio)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            return dbContext.PI_BA_Premio.Where(p => p.cvePremio == idPremio)
                    .FirstOrDefault();
        }

        public static PI_BA_Forma GetFormaByCategoria(string idCategoria) {
            dbContext = new wPremiosInstitucionalesdbEntities();
            return dbContext.PI_BA_Forma.Where(p => p.cveCategoria == idCategoria).FirstOrDefault();
        }

        public static PI_BA_Forma GetFormaByID(string idForma) {
            dbContext = new wPremiosInstitucionalesdbEntities();
            return dbContext.PI_BA_Forma.Where(p => p.cveForma == idForma).FirstOrDefault();
        }

        public static PI_BA_Categoria GetCategoriaById(String idCategoria)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            return dbContext.PI_BA_Categoria.Where(p => p.cveCategoria == idCategoria)
                    .FirstOrDefault();
        }

        public static PI_BA_Convocatoria GetConvocatoriaById(String idConvocatoria)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            return dbContext.PI_BA_Convocatoria.Where(cvc => cvc.cveConvocatoria == idConvocatoria)
                    .FirstOrDefault();
        }

        public static PI_BA_Premio GetPremioByCategoria(string idCategoria)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();

            var categoria = GetCategoriaById(idCategoria);
            if (categoria != null)
            {
                var convocatoria = GetConvocatoriaById(categoria.cveConvocatoria);
                if(categoria != null)
                {
                    var premio = GetPremioById(convocatoria.cvePremio);
                    return premio;
                }
            }

            return null;
        }

        public static void SaveNewConvocatoria(string idPremio, PI_BA_Convocatoria nuevaConvocatoria)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            nuevaConvocatoria.cvePremio = idPremio;
            dbContext.PI_BA_Convocatoria.Add(nuevaConvocatoria);
            dbContext.SaveChanges();
        }

        public static PI_BA_Convocatoria GetMostRecentConvocatoria(string idPremio)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            var query = (from convo in dbContext.PI_BA_Convocatoria
                         where convo.cvePremio.Equals(idPremio)
                         orderby convo.FechaFin descending
                         select convo).FirstOrDefault();
            return query;
        }

        public static List<PI_BA_Convocatoria> GetConvocatoriasPremio(string idPremio)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            var query = (from convo in dbContext.PI_BA_Convocatoria
                         where convo.cvePremio.Equals(idPremio)
                         orderby convo.FechaFin descending
                         select convo).ToList();
            return query;
        }

        public static void ActualizarConvocatoria(string idConvocatoria, string descripcion, string titulo)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            var convo = dbContext.PI_BA_Convocatoria.Where(c => c.cveConvocatoria == idConvocatoria)
                .FirstOrDefault<PI_BA_Convocatoria>();
            convo.Descripcion = descripcion;
            convo.TituloConvocatoria = titulo;
            dbContext.SaveChanges();
        }

        public static void ActualizarPremio(string idPremio, string titulo, string descripcion, string imagenurl)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            var premio = dbContext.PI_BA_Premio.Where(p => p.cvePremio == idPremio)
                .FirstOrDefault<PI_BA_Premio>();
            premio.Nombre = titulo;
            premio.Descripcion = descripcion;
            premio.NombreImagen = imagenurl;
            dbContext.SaveChanges();
        }

        public static Dictionary<PI_BA_Aplicacion, PI_BA_Candidato> JuezObtenerCandidatosPorAplicaciones(List<PI_BA_Aplicacion> listaAplicaciones)
        {// Obtengo lista de aplicaciones, regreso diccionario de aplicaciones con candidatos

            dbContext = new wPremiosInstitucionalesdbEntities();
            var lista = new Dictionary<PI_BA_Aplicacion, PI_BA_Candidato>();

            foreach (var aplicacion in listaAplicaciones)
            {
                var candidato = dbContext.PI_BA_Candidato.Where(c => c.cveCandidato == aplicacion.cveCandidato)
                    .FirstOrDefault<PI_BA_Candidato>();

                // Se despliegan las aplicaciones aceptadas unicamente
                if (aplicacion.Status == Values.StringValues.Aceptado)
                {
                    lista.Add(aplicacion, candidato);
                }

            }

            return lista;
        }

        public static Dictionary<PI_BA_Aplicacion, PI_BA_Candidato> AdminObtenerCandidatosPorAplicaciones(List<PI_BA_Aplicacion> listaAplicaciones)
        {// Obtengo lista de aplicaciones, regreso diccionario de aplicaciones con candidatos

            dbContext = new wPremiosInstitucionalesdbEntities();
            var lista = new Dictionary<PI_BA_Aplicacion, PI_BA_Candidato>();

            foreach (var aplicacion in listaAplicaciones)
            {
                var candidato = dbContext.PI_BA_Candidato.Where(c => c.cveCandidato == aplicacion.cveCandidato)
                    .FirstOrDefault<PI_BA_Candidato>();

                // No se despliegan las aplicaciones rechazadas
                if(aplicacion.Status!= Values.StringValues.Rechazado)
                {
                    lista.Add(aplicacion, candidato);
                }
                
            }

            return lista;  
        }

        public static List<PI_BA_Aplicacion> ObtenerAplicacionesPorCategoria(string cve_categoria)
        {// Obtengo lista de aplicaciones dada una categoria
            dbContext = new wPremiosInstitucionalesdbEntities();
            try
            {
                var categoria = dbContext.PI_BA_Categoria.Where(c => c.cveCategoria.Equals(cve_categoria))
                .First();

                if (categoria.PI_BA_Aplicacion != null)
                    return categoria.PI_BA_Aplicacion.ToList();
                else
                    return null;
            } catch (Exception e)
            {
                return null;
            }          
        }

        public static List<PI_BA_Aplicacion> JuezObtenerAplicacionesPorCategoria(string cve_categoria)
        {// Obtengo lista de aplicaciones dada una categoria
            dbContext = new wPremiosInstitucionalesdbEntities();
            try
            {
                var categoria = dbContext.PI_BA_Categoria.Where(c => c.cveCategoria.Equals(cve_categoria))
                .First();

                if (categoria.PI_BA_Aplicacion != null)
                    return categoria.PI_BA_Aplicacion.Where(a=> a.Status.Equals(StringValues.Aceptado)).ToList();
                else
                    return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static Dictionary<string, string[]> ObtenerPreguntasConRespuestasPorAplicacion(PI_BA_Aplicacion aplicacion)
        { //Se obtiene un diccionario de preguntas y respuestas por aplicacion, ordenado por clave aplicacion

            dbContext = new wPremiosInstitucionalesdbEntities();

            var respuestas = dbContext.PI_BA_Respuesta.Where(c => c.cveAplicacion == aplicacion.cveAplicacion).ToList();
            var query = (from resp in dbContext.PI_BA_Respuesta
                         where resp.cveAplicacion.Equals(aplicacion.cveAplicacion)
                         join pr in dbContext.PI_BA_Pregunta on resp.cvePregunta equals pr.cvePregunta
                         orderby pr.Orden
                         select new
                         {
                             idResp = resp.cveRespuesta,
                             txtPreg = pr.Texto,
                             valorResp = resp.Valor
                         }).ToList();
            var diccionarioPregResp = new Dictionary<string,string[]>();
            foreach (var auxPregResp in query)
            {
                diccionarioPregResp.Add(auxPregResp.idResp, new string[] { auxPregResp.txtPreg, auxPregResp.valorResp });
            }

            return diccionarioPregResp;

        }

        public static String GetNombreCandidatoByAplicacion(String cveAplicacion)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            var app = dbContext.PI_BA_Aplicacion.Where(a => a.cveAplicacion.Equals(cveAplicacion)).First();
            var result = dbContext.PI_BA_Candidato.Where(c => c.cveCandidato.Equals(app.cveCandidato)).First().Nombre;
            return result;
        }

        public static String GetCorreoCandidatoByAplicacion(String cveAplicacion)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            var app = dbContext.PI_BA_Aplicacion.Where(a => a.cveAplicacion.Equals(cveAplicacion)).First();
            var result = dbContext.PI_BA_Candidato.Where(c => c.cveCandidato.Equals(app.cveCandidato)).First().Correo;
            return result;
        }

        public static List<PI_BA_Categoria> GetCategoriasByPremio(String idPremio)
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            // revisar que el premio cuente con una convocatoria vigente
            var premio = dbContext.PI_BA_Premio.Where(p => p.cvePremio.Equals(idPremio)).First();

            if (premio.PI_BA_Convocatoria.Count > 0)
            {
                // obtener la convocatoria vigente
                var convocatoria = (from convo in premio.PI_BA_Convocatoria
                                    where DateTime.Today >= convo.FechaInicio && DateTime.Today <= convo.FechaFin
                                    select convo).FirstOrDefault();
                // regresar las categorias de la convocatoria
                try
                {
                    return convocatoria.PI_BA_Categoria.ToList();
                } catch (Exception e)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static List<PI_BA_Categoria> GetCategoriasPendientes()
        {
            dbContext = new wPremiosInstitucionalesdbEntities();
            var categorias = dbContext.PI_BA_Categoria.Where(a => a.cveAplicacionGanadora == null).ToList();

            if (categorias == null)
                return categorias;

            List<PI_BA_Categoria> validCategories = new List<PI_BA_Categoria>();

            foreach(var c in categorias)
            {
                var convo = GetConvocatoriaById(c.cveConvocatoria);
                if(DateTime.Today >= convo.FechaInicio)
                {
                    validCategories.Add(c);
                }
            }

            if (validCategories.Count == 0)
                return null;

            return validCategories;
        }

    }

    
}