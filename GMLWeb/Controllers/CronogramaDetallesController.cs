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
    public class CronogramaDetallesController : Controller
    {
        private GMLConnectionString db = new GMLConnectionString();

        // GET: CronogramaDetalles
        public ActionResult Index()
        {
            var cronogramaDetalle = db.CronogramaDetalle.Include(c => c.Cronograma);
            return View(cronogramaDetalle.ToList());
        }

        // GET: CronogramaDetalles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CronogramaDetalle cronogramaDetalle = db.CronogramaDetalle.Find(id);
            if (cronogramaDetalle == null)
            {
                return HttpNotFound();
            }
            return View(cronogramaDetalle);
        }

        // GET: CronogramaDetalles/Create
        public ActionResult Create()
        {
            ViewBag.codigo_cronograma = new SelectList(db.Cronograma, "codigo", "codigo");
            return View();
        }

        // POST: CronogramaDetalles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigo,codigo_cronograma,semana,actividad,asignada")] CronogramaDetalle cronogramaDetalle)
        {
            if (ModelState.IsValid)
            {
                db.CronogramaDetalle.Add(cronogramaDetalle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.codigo_cronograma = new SelectList(db.Cronograma, "codigo", "codigo", cronogramaDetalle.codigo_cronograma);
            return View(cronogramaDetalle);
        }

        // GET: CronogramaDetalles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CronogramaDetalle cronogramaDetalle = db.CronogramaDetalle.Find(id);
            if (cronogramaDetalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.codigo_cronograma = new SelectList(db.Cronograma, "codigo", "codigo", cronogramaDetalle.codigo_cronograma);
            return View(cronogramaDetalle);
        }

        // POST: CronogramaDetalles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigo,codigo_cronograma,semana,actividad,asignada")] CronogramaDetalle cronogramaDetalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cronogramaDetalle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.codigo_cronograma = new SelectList(db.Cronograma, "codigo", "codigo", cronogramaDetalle.codigo_cronograma);
            return View(cronogramaDetalle);
        }

        // GET: CronogramaDetalles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CronogramaDetalle cronogramaDetalle = db.CronogramaDetalle.Find(id);
            if (cronogramaDetalle == null)
            {
                return HttpNotFound();
            }
            return View(cronogramaDetalle);
        }

        // POST: CronogramaDetalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CronogramaDetalle cronogramaDetalle = db.CronogramaDetalle.Find(id);
            db.CronogramaDetalle.Remove(cronogramaDetalle);
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
