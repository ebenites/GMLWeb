using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GMLWeb.Models;
using System.Globalization;

namespace GMLWeb.Controllers
{
    public class ProgramacionesController : Controller
    {

        private GMLConnectionString db = new GMLConnectionString();

        // GET: Solicitud
        public ActionResult Index()
        {

            // Fecha1
            ViewBag.fecha1 = Request.Params["fecha1"];


            // Fecha2
            ViewBag.fecha2 = Request.Params["fecha2"];


            // Locales
            ViewBag.locales = new SelectList(db.Local, "codigo", "nombre", Request.Params["local"]);


            // Estados
            ViewBag.estados = new SelectList(
                                new List<SelectListItem>
                                {
                                    new SelectListItem { Text = "PENDIENTE", Value = "P"},
                                    new SelectListItem { Text = "PROGRAMADO", Value = "G"},
                                }, "Value", "Text", Request.Params["estado"]);
            

            // Solicitudes
            List <Solicitud> solicitudes = null;

            if (Request.HttpMethod == "POST")
            {
                string fecha1 = Request.Params["fecha1"];
                string fecha2 = Request.Params["fecha2"];
                string local = Request.Params["local"];
                string estado = Request.Params["estado"];
                System.Diagnostics.Debug.WriteLine(Request.Params);

                var query = db.Solicitud.Include(s => s.Equipo).Include(s => s.Equipo.Local);

                if (fecha1 != null && fecha1 != String.Empty)
                {
                    DateTime fecha1AsDateTime = DateTime.ParseExact(fecha1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    query = query.Where(x => x.fecha >= fecha1AsDateTime);
                }

                if (fecha2 != null && fecha2 != String.Empty)
                {
                    DateTime fecha2AsDateTime = DateTime.ParseExact(fecha2, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    query = query.Where(x => x.fecha <= fecha2AsDateTime);
                }

                if (local != null && local != String.Empty)
                {
                    int localAsInt = int.Parse(local);
                    query = query.Where(x => x.Equipo.Local.codigo == localAsInt);
                }

                if (estado != null && estado != String.Empty)
                {
                    query = query.Where(x => x.estado == estado);
                }

                solicitudes = query.ToList();
                
            }
            else
            {
                /*solicitudes = (
                from s in db.Solicitud
                join e  in db.Equipo on s.codigo_equipo equals e.codigo
                join l in db.Local on e.codigo_local equals l.codigo
                select new { ProjectData = s, Project = e }
                /*select s/*new {
                    fecha = s.fecha,
                    numero = s.numero,
                    solicitante = s.solicitante,
                    local = l.nombre,
                    programado = s.estado
                }*/
                /*).ToList();*/

                //solicitudes = db.Solicitud.Include(s => s.Equipo).Include(s => s.Equipo.Local).ToList();
            }
            
            ViewBag.solicitudes = solicitudes;
            System.Diagnostics.Debug.WriteLine(solicitudes);

            return View();
        }
        
        public ActionResult Programar(int? id)
        {

            // Tecnicos
            var tecnicos = (from t in db.Tecnico
                            select new
                            {
                                codigo = t.codigo,
                                nombrescompleto = t.nombres + " " + t.apellidos
                            });
            ViewBag.tecnicos = new SelectList(tecnicos, "codigo", "nombrescompleto"); ;

            // Solicitud
            ViewBag.solicitud = db.Solicitud.Include(s => s.Equipo).Include(s => s.Equipo.Local).Single(x => x.codigo == id);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrar()
        {

            // Recuperando parámetros

            string fecprogramada = Request.Params["fecprogramada"];
            string tecnico = Request.Params["tecnico"];
            string equipo = Request.Params["codigo_equipo"];
            string codigo = Request.Params["codigo"];
            System.Diagnostics.Debug.WriteLine(Request.Params);

            DateTime fecprogramadaAsDateTime = DateTime.ParseExact(fecprogramada, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            int tecnicoAsInt = int.Parse(tecnico);
            int equipoAsInt = int.Parse(equipo);
            int codigoAsInt = int.Parse(codigo);

            // Registrando la OS

            OrdenServicio orden = new OrdenServicio {
                numero = "OS",
                fecha = fecprogramadaAsDateTime,
                estado = "P",
                codigo_equipo = equipoAsInt,
                codigo_tecnico = tecnicoAsInt,
                codigo_solicitud = codigoAsInt
            };
            db.OrdenServicio.Add(orden);

            // Cambiando el estado de la solicitud
            
            var solicitud = db.Solicitud.Include(s => s.Equipo).Include(s => s.Equipo.Local).Single(x => x.codigo == codigoAsInt);
            solicitud.estado = "G";

            // Grabando cambios

            db.SaveChanges();

            // Actualizando el numero de OS

            orden.numero = "OS-" + DateTime.Now.ToString("yyyy-MM-") + orden.codigo.ToString("D4") ;

            // Grabando cambios

            db.SaveChanges();


            TempData["Message"] = "La Solicitud de Mantenimiento Correctivo se ha programado satisfactoriamente.";

            return RedirectToAction("Index");
        }
    }
}