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
    public class DisponibilidadesController : Controller
    {
        private GMLConnectionString db = new GMLConnectionString();

        // GET: Disponibilidades
        public ActionResult Index()
        {
            var disponibilidad = db.Disponibilidad.Include(d => d.Tecnico);
            return View(disponibilidad.ToList());
        }

        // GET: Disponibilidades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disponibilidad disponibilidad = db.Disponibilidad.Find(id);
            if (disponibilidad == null)
            {
                return HttpNotFound();
            }
            return View(disponibilidad);
        }

        // GET: Disponibilidades/Create
        public ActionResult Create()
        {
            ViewBag.codigo_tecnico = new SelectList(db.Tecnico, "codigo", "dni");
            return View();
        }

        // POST: Disponibilidades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigo,codigo_tecnico,anio,semana")] Disponibilidad disponibilidad)
        {
            if (ModelState.IsValid)
            {
                db.Disponibilidad.Add(disponibilidad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.codigo_tecnico = new SelectList(db.Tecnico, "codigo", "dni", disponibilidad.codigo_tecnico);
            return View(disponibilidad);
        }

        // GET: Disponibilidades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disponibilidad disponibilidad = db.Disponibilidad.Find(id);
            if (disponibilidad == null)
            {
                return HttpNotFound();
            }
            ViewBag.codigo_tecnico = new SelectList(db.Tecnico, "codigo", "dni", disponibilidad.codigo_tecnico);
            return View(disponibilidad);
        }

        // POST: Disponibilidades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigo,codigo_tecnico,anio,semana")] Disponibilidad disponibilidad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(disponibilidad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.codigo_tecnico = new SelectList(db.Tecnico, "codigo", "dni", disponibilidad.codigo_tecnico);
            return View(disponibilidad);
        }

        // GET: Disponibilidades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disponibilidad disponibilidad = db.Disponibilidad.Find(id);
            if (disponibilidad == null)
            {
                return HttpNotFound();
            }
            return View(disponibilidad);
        }

        // POST: Disponibilidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Disponibilidad disponibilidad = db.Disponibilidad.Find(id);
            db.Disponibilidad.Remove(disponibilidad);
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
