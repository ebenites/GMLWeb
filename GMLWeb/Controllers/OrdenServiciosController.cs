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
    public class OrdenServiciosController : Controller
    {
        private GMLConnectionString db = new GMLConnectionString();

        // GET: OrdenServicios
        public ActionResult Index()
        {
            var ordenServicio = db.OrdenServicio.Include(o => o.Equipo).Include(o => o.Solicitud).Include(o => o.Tecnico);
            return View(ordenServicio.ToList());
        }

        // GET: OrdenServicios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdenServicio ordenServicio = db.OrdenServicio.Find(id);
            if (ordenServicio == null)
            {
                return HttpNotFound();
            }
            return View(ordenServicio);
        }

        // GET: OrdenServicios/Create
        public ActionResult Create()
        {
            ViewBag.codigo_equipo = new SelectList(db.Equipo, "codigo", "serie");
            ViewBag.codigo_solicitud = new SelectList(db.Solicitud, "codigo", "numero");
            ViewBag.codigo_tecnico = new SelectList(db.Tecnico, "codigo", "dni");
            return View();
        }

        // POST: OrdenServicios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigo,numero,observacion,fecha,estado,codigo_equipo,codigo_tecnico,codigo_solicitud")] OrdenServicio ordenServicio)
        {
            if (ModelState.IsValid)
            {
                db.OrdenServicio.Add(ordenServicio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.codigo_equipo = new SelectList(db.Equipo, "codigo", "serie", ordenServicio.codigo_equipo);
            ViewBag.codigo_solicitud = new SelectList(db.Solicitud, "codigo", "numero", ordenServicio.codigo_solicitud);
            ViewBag.codigo_tecnico = new SelectList(db.Tecnico, "codigo", "dni", ordenServicio.codigo_tecnico);
            return View(ordenServicio);
        }

        // GET: OrdenServicios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdenServicio ordenServicio = db.OrdenServicio.Find(id);
            if (ordenServicio == null)
            {
                return HttpNotFound();
            }
            ViewBag.codigo_equipo = new SelectList(db.Equipo, "codigo", "serie", ordenServicio.codigo_equipo);
            ViewBag.codigo_solicitud = new SelectList(db.Solicitud, "codigo", "numero", ordenServicio.codigo_solicitud);
            ViewBag.codigo_tecnico = new SelectList(db.Tecnico, "codigo", "dni", ordenServicio.codigo_tecnico);
            return View(ordenServicio);
        }

        // POST: OrdenServicios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigo,numero,observacion,fecha,estado,codigo_equipo,codigo_tecnico,codigo_solicitud")] OrdenServicio ordenServicio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ordenServicio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.codigo_equipo = new SelectList(db.Equipo, "codigo", "serie", ordenServicio.codigo_equipo);
            ViewBag.codigo_solicitud = new SelectList(db.Solicitud, "codigo", "numero", ordenServicio.codigo_solicitud);
            ViewBag.codigo_tecnico = new SelectList(db.Tecnico, "codigo", "dni", ordenServicio.codigo_tecnico);
            return View(ordenServicio);
        }

        // GET: OrdenServicios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdenServicio ordenServicio = db.OrdenServicio.Find(id);
            if (ordenServicio == null)
            {
                return HttpNotFound();
            }
            return View(ordenServicio);
        }

        // POST: OrdenServicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrdenServicio ordenServicio = db.OrdenServicio.Find(id);
            db.OrdenServicio.Remove(ordenServicio);
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
    }
}
