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
    public class PlanMantenimientosController : Controller
    {
        private GMLConnectionString db = new GMLConnectionString();

        // GET: PlanMantenimientos
        public ActionResult Index()
        {
            var planMantenimiento = db.PlanMantenimiento.Include(p => p.Local);
            return View(planMantenimiento.ToList());
        }

        // GET: PlanMantenimientos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanMantenimiento planMantenimiento = db.PlanMantenimiento.Find(id);
            if (planMantenimiento == null)
            {
                return HttpNotFound();
            }
            return View(planMantenimiento);
        }

        // GET: PlanMantenimientos/Create
        public ActionResult Create()
        {
            ViewBag.codigo_local = new SelectList(db.Local, "codigo", "nombre");
            return View();
        }

        // POST: PlanMantenimientos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigo,anio,codigo_local")] PlanMantenimiento planMantenimiento)
        {
            if (ModelState.IsValid)
            {
                db.PlanMantenimiento.Add(planMantenimiento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.codigo_local = new SelectList(db.Local, "codigo", "nombre", planMantenimiento.codigo_local);
            return View(planMantenimiento);
        }

        // GET: PlanMantenimientos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanMantenimiento planMantenimiento = db.PlanMantenimiento.Find(id);
            if (planMantenimiento == null)
            {
                return HttpNotFound();
            }
            ViewBag.codigo_local = new SelectList(db.Local, "codigo", "nombre", planMantenimiento.codigo_local);
            return View(planMantenimiento);
        }

        // POST: PlanMantenimientos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigo,anio,codigo_local")] PlanMantenimiento planMantenimiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planMantenimiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.codigo_local = new SelectList(db.Local, "codigo", "nombre", planMantenimiento.codigo_local);
            return View(planMantenimiento);
        }

        // GET: PlanMantenimientos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanMantenimiento planMantenimiento = db.PlanMantenimiento.Find(id);
            if (planMantenimiento == null)
            {
                return HttpNotFound();
            }
            return View(planMantenimiento);
        }

        // POST: PlanMantenimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlanMantenimiento planMantenimiento = db.PlanMantenimiento.Find(id);
            db.PlanMantenimiento.Remove(planMantenimiento);
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
