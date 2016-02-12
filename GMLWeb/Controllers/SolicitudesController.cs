using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GMLWeb.Models;

namespace GMLWeb.Controllers
{
    public class SolicitudesController : Controller
    {
        private GMLConnectionString db = new GMLConnectionString();

        // GET: Solicitudes
        public ActionResult Index()
        {
            var solicitud = db.Solicitud.Include(s => s.Equipo);
            return View(solicitud.ToList());
        }

        // GET: Solicitudes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitud solicitud = db.Solicitud.Find(id);
            if (solicitud == null)
            {
                return HttpNotFound();
            }
            return View(solicitud);
        }

        // GET: Solicitudes/Create
        public ActionResult Create()
        {
            ViewBag.codigo_equipo = new SelectList(db.Equipo, "codigo", "serie");
            return View();
        }

        // POST: Solicitudes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigo,numero,descripcion,solicitante,codigo_equipo,fecha,estado")] Solicitud solicitud)
        {
            if (ModelState.IsValid)
            {
                db.Solicitud.Add(solicitud);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.codigo_equipo = new SelectList(db.Equipo, "codigo", "serie", solicitud.codigo_equipo);
            return View(solicitud);
        }

        // GET: Solicitudes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitud solicitud = db.Solicitud.Find(id);
            if (solicitud == null)
            {
                return HttpNotFound();
            }
            ViewBag.codigo_equipo = new SelectList(db.Equipo, "codigo", "serie", solicitud.codigo_equipo);
            return View(solicitud);
        }

        // POST: Solicitudes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigo,numero,descripcion,solicitante,codigo_equipo,fecha,estado")] Solicitud solicitud)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitud).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.codigo_equipo = new SelectList(db.Equipo, "codigo", "serie", solicitud.codigo_equipo);
            return View(solicitud);
        }

        // GET: Solicitudes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitud solicitud = db.Solicitud.Find(id);
            if (solicitud == null)
            {
                return HttpNotFound();
            }
            return View(solicitud);
        }

        // POST: Solicitudes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Solicitud solicitud = db.Solicitud.Find(id);
            db.Solicitud.Remove(solicitud);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /* Registro de Solicitudes */
        
        public ActionResult Registrar()
        {
            ViewBag.codigo_equipo = new SelectList(db.Equipo, "codigo", "serie");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrar([Bind(Include = "descripcion,solicitante,codigo_equipo")] Solicitud solicitud)
        {

            solicitud.numero = "SOL-";
            solicitud.fecha = @DateTime.Now;
            solicitud.estado = "P";
                
            if (ModelState.IsValid)
            {
                db.Solicitud.Add(solicitud);
                db.SaveChanges();

                solicitud.numero = "SOL-" + DateTime.Now.ToString("yyyy-MM-") + solicitud.codigo.ToString("D4");
                db.SaveChanges();

                TempData["Message"] = "La solicitud se ha registrado satisfactoriamente.";
                return RedirectToAction("Registrar");
            }

            ViewBag.codigo_equipo = new SelectList(db.Equipo, "codigo", "serie", solicitud.codigo_equipo);
            return View(solicitud);
        }

    }
}
