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
    public class PlanMantenimientoDetallesController : Controller
    {
        private GMLConnectionString db = new GMLConnectionString();

        // GET: PlanMantenimientoDetalles
        public ActionResult Index()
        {
            var planMantenimientoDetalle = db.PlanMantenimientoDetalle.Include(p => p.CronogramaDetalle).Include(p => p.Disponibilidad).Include(p => p.PlanMantenimiento);
            return View(planMantenimientoDetalle.ToList());
        }

        // GET: PlanMantenimientoDetalles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanMantenimientoDetalle planMantenimientoDetalle = db.PlanMantenimientoDetalle.Find(id);
            if (planMantenimientoDetalle == null)
            {
                return HttpNotFound();
            }
            return View(planMantenimientoDetalle);
        }

        // GET: PlanMantenimientoDetalles/Create
        public ActionResult Create()
        {
            ViewBag.codigo_cronogramadetalle = new SelectList(db.CronogramaDetalle, "codigo", "actividad");
            ViewBag.codigo_disponibilidad = new SelectList(db.Disponibilidad, "codigo", "codigo");
            ViewBag.codigo_planmantenimiento = new SelectList(db.PlanMantenimiento, "codigo", "codigo");
            return View();
        }

        // POST: PlanMantenimientoDetalles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigo,codigo_planmantenimiento,codigo_cronogramadetalle,codigo_disponibilidad")] PlanMantenimientoDetalle planMantenimientoDetalle)
        {
            if (ModelState.IsValid)
            {
                db.PlanMantenimientoDetalle.Add(planMantenimientoDetalle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.codigo_cronogramadetalle = new SelectList(db.CronogramaDetalle, "codigo", "actividad", planMantenimientoDetalle.codigo_cronogramadetalle);
            ViewBag.codigo_disponibilidad = new SelectList(db.Disponibilidad, "codigo", "codigo", planMantenimientoDetalle.codigo_disponibilidad);
            ViewBag.codigo_planmantenimiento = new SelectList(db.PlanMantenimiento, "codigo", "codigo", planMantenimientoDetalle.codigo_planmantenimiento);
            return View(planMantenimientoDetalle);
        }

        // GET: PlanMantenimientoDetalles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanMantenimientoDetalle planMantenimientoDetalle = db.PlanMantenimientoDetalle.Find(id);
            if (planMantenimientoDetalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.codigo_cronogramadetalle = new SelectList(db.CronogramaDetalle, "codigo", "actividad", planMantenimientoDetalle.codigo_cronogramadetalle);
            ViewBag.codigo_disponibilidad = new SelectList(db.Disponibilidad, "codigo", "codigo", planMantenimientoDetalle.codigo_disponibilidad);
            ViewBag.codigo_planmantenimiento = new SelectList(db.PlanMantenimiento, "codigo", "codigo", planMantenimientoDetalle.codigo_planmantenimiento);
            return View(planMantenimientoDetalle);
        }

        // POST: PlanMantenimientoDetalles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigo,codigo_planmantenimiento,codigo_cronogramadetalle,codigo_disponibilidad")] PlanMantenimientoDetalle planMantenimientoDetalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planMantenimientoDetalle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.codigo_cronogramadetalle = new SelectList(db.CronogramaDetalle, "codigo", "actividad", planMantenimientoDetalle.codigo_cronogramadetalle);
            ViewBag.codigo_disponibilidad = new SelectList(db.Disponibilidad, "codigo", "codigo", planMantenimientoDetalle.codigo_disponibilidad);
            ViewBag.codigo_planmantenimiento = new SelectList(db.PlanMantenimiento, "codigo", "codigo", planMantenimientoDetalle.codigo_planmantenimiento);
            return View(planMantenimientoDetalle);
        }

        // GET: PlanMantenimientoDetalles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanMantenimientoDetalle planMantenimientoDetalle = db.PlanMantenimientoDetalle.Find(id);
            if (planMantenimientoDetalle == null)
            {
                return HttpNotFound();
            }
            return View(planMantenimientoDetalle);
        }

        // POST: PlanMantenimientoDetalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlanMantenimientoDetalle planMantenimientoDetalle = db.PlanMantenimientoDetalle.Find(id);
            db.PlanMantenimientoDetalle.Remove(planMantenimientoDetalle);
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
