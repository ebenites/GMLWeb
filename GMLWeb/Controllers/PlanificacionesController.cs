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
using GMLWeb.Services;

namespace GMLWeb.Controllers
{
    public class PlanificacionesController : Controller
    {

        private IPlanificacionBL planificacionBL = new PlanificacionBL();

        // GET: Planificaciones
        public ActionResult Index()
        {

            // Anio
            if (TempData["anio"] != null)
            {
                ViewBag.anio = (int)TempData["anio"];
            }

            // Anios
            List<int> anios = planificacionBL.listarAnios();
            ViewBag.anios = anios;

            return View();
        }

        public ActionResult VerificarPrecondicion()
        {
            string anioParam = Request.Params["anio"];
            int anio = int.Parse(anioParam);

            // Verificar cronograma de actividades
            int total = planificacionBL.totalCronogramaActividades(anio);
            
            if (total == 0)
            {
                var data = new { Type = "error", Message = "No existe un cronograma de actividades para el año seleccionado." };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                // Verificar disponibilidad de técnicos
                List<Tecnico> tecnicos = planificacionBL.listarTecnicos(anio, String.Empty);
                if(tecnicos.Count == 0)
                {
                    var data = new { Type = "error", Message = "No existe disponibilidad de técnicos para el año seleccionado." };
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data = new { Type = "success" };
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult ListarLocales()
        {
            // Anio actual
            int anio = DateTime.Now.Year + 1;

            string anioParam = Request.Params["anio"];
            if (anioParam != null && anioParam != String.Empty)
            {
                anio = int.Parse(anioParam);
            }

            int estado = -1;

            string estadoParam = Request.Params["estado"];
            if (estadoParam != null && estadoParam != String.Empty)
            {
                estado = int.Parse(estadoParam);
            }

            // Locales
            List<PlanLocal> planlocales = planificacionBL.listarLocales(anio, estado);
            ViewBag.planlocales = planlocales;

            return View();
        }

        public ActionResult ListarTecnicos()
        {
            // Anio actual
            int anio = DateTime.Now.Year + 1;

            string anioParam = Request.Params["anio"];
            if (anioParam != null && anioParam != String.Empty)
            {
                anio = int.Parse(anioParam);
            }

            string tipo = Request.Params["tipo"];
            if (tipo == null)
            {
                tipo = String.Empty;
            }

            // Tecnicos
            List<Tecnico> tecnicos = planificacionBL.listarTecnicos(anio, tipo);
            ViewBag.tecnicos = tecnicos;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Generar()
        {

            string anioParam = Request.Params["anio"];
            string localParam = Request.Params["local"];
            string tecnicoParam = Request.Params["tecnico"];

            System.Diagnostics.Debug.WriteLine(anioParam);
            System.Diagnostics.Debug.WriteLine(localParam);
            System.Diagnostics.Debug.WriteLine(tecnicoParam);

            // Validando parametros

            int anio = int.Parse(anioParam);

            int local = int.Parse(localParam);

            string[] tecnicoParams = tecnicoParam.Split(',');
            List<int> tecnicos = new List<int>();
            foreach (string item in tecnicoParams)
            {
                tecnicos.Add(int.Parse(item));
            }

            // Registrando Plan en BD

            planificacionBL.generar(anio, local, tecnicos);

            TempData["Message"] = "El Plan de Mantenimiento Preventivo se ha registrado satisfactoriamente.";
            TempData["anio"] = anio;

            return RedirectToAction("Index");
        }
    }
}