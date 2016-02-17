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
    public class CronogramasController : Controller
    {
        private GMLConnectionString db = new GMLConnectionString();

        // GET: Cronogramas
        public ActionResult Index()
        {
            var cronograma = db.Cronograma.Include(c => c.Equipo);
            return View(cronograma.ToList());
        }

        // GET: Cronogramas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cronograma cronograma = db.Cronograma.Find(id);
            if (cronograma == null)
            {
                return HttpNotFound();
            }
            return View(cronograma);
        }

        // GET: Cronogramas/Create
        public ActionResult Create()
        {
            ViewBag.codigo_equipo = new SelectList(db.Equipo, "codigo", "serie");
            return View();
        }

        // POST: Cronogramas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigo,anio,codigo_equipo")] Cronograma cronograma)
        {
            if (ModelState.IsValid)
            {
                db.Cronograma.Add(cronograma);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.codigo_equipo = new SelectList(db.Equipo, "codigo", "serie", cronograma.codigo_equipo);
            return View(cronograma);
        }

        // GET: Cronogramas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cronograma cronograma = db.Cronograma.Find(id);
            if (cronograma == null)
            {
                return HttpNotFound();
            }
            ViewBag.codigo_equipo = new SelectList(db.Equipo, "codigo", "serie", cronograma.codigo_equipo);
            return View(cronograma);
        }

        // POST: Cronogramas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigo,anio,codigo_equipo")] Cronograma cronograma)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cronograma).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.codigo_equipo = new SelectList(db.Equipo, "codigo", "serie", cronograma.codigo_equipo);
            return View(cronograma);
        }

        // GET: Cronogramas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cronograma cronograma = db.Cronograma.Find(id);
            if (cronograma == null)
            {
                return HttpNotFound();
            }
            return View(cronograma);
        }

        // POST: Cronogramas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cronograma cronograma = db.Cronograma.Find(id);
            db.Cronograma.Remove(cronograma);
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
