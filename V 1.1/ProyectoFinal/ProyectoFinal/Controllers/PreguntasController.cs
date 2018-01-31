using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;

namespace ProyectoFinal
{
    public class PreguntasController : Controller
    {
        private SitcomEntities db = new SitcomEntities();

        // GET: /Preguntas/
        public ActionResult Index()
        {
            var preguntas = db.Preguntas.Include(p => p.Encuestas).Include(p => p.Encuestas1).Include(p => p.Preguntas1).Include(p => p.Preguntas2).Include(p => p.TiposRespuesta).Include(p => p.ClasifPregunta);
            return View(preguntas.ToList());
        }

        // GET: /Preguntas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Preguntas preguntas = db.Preguntas.Find(id);
            if (preguntas == null)
            {
                return HttpNotFound();
            }
            return View(preguntas);
        }

        // GET: /Preguntas/Create
        public ActionResult Create()
        {
            ViewBag.idPregunta = new SelectList(db.Encuestas, "idEncuesta", "nombre");
            ViewBag.idEncuesta = new SelectList(db.Encuestas, "idEncuesta", "nombre");
            ViewBag.idPregunta = new SelectList(db.Preguntas, "idPregunta", "textoPregunta");
            ViewBag.idPregunta = new SelectList(db.Preguntas, "idPregunta", "textoPregunta");
            ViewBag.idTipoRespuesta = new SelectList(db.TiposRespuesta, "idTipoRespuesta", "nombre");
            ViewBag.idClasifPregunta = new SelectList(db.ClasifPregunta, "idClasifPregunta", "nombre");
            return View();
        }

        // POST: /Preguntas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="idPregunta,textoPregunta,idEncuesta,idClasifPregunta,idTipoRespuesta")] Preguntas preguntas)
        {
            if (ModelState.IsValid)
            {
                db.Preguntas.Add(preguntas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idPregunta = new SelectList(db.Encuestas, "idEncuesta", "nombre", preguntas.idPregunta);
            ViewBag.idEncuesta = new SelectList(db.Encuestas, "idEncuesta", "nombre", preguntas.idEncuesta);
            ViewBag.idPregunta = new SelectList(db.Preguntas, "idPregunta", "textoPregunta", preguntas.idPregunta);
            ViewBag.idPregunta = new SelectList(db.Preguntas, "idPregunta", "textoPregunta", preguntas.idPregunta);
            ViewBag.idTipoRespuesta = new SelectList(db.TiposRespuesta, "idTipoRespuesta", "nombre", preguntas.idTipoRespuesta);
            ViewBag.idClasifPregunta = new SelectList(db.ClasifPregunta, "idClasifPregunta", "nombre", preguntas.idClasifPregunta);
            return View(preguntas);
        }

        // GET: /Preguntas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Preguntas preguntas = db.Preguntas.Find(id);
            if (preguntas == null)
            {
                return HttpNotFound();
            }
            ViewBag.idPregunta = new SelectList(db.Encuestas, "idEncuesta", "nombre", preguntas.idPregunta);
            ViewBag.idEncuesta = new SelectList(db.Encuestas, "idEncuesta", "nombre", preguntas.idEncuesta);
            ViewBag.idPregunta = new SelectList(db.Preguntas, "idPregunta", "textoPregunta", preguntas.idPregunta);
            ViewBag.idPregunta = new SelectList(db.Preguntas, "idPregunta", "textoPregunta", preguntas.idPregunta);
            ViewBag.idTipoRespuesta = new SelectList(db.TiposRespuesta, "idTipoRespuesta", "nombre", preguntas.idTipoRespuesta);
            ViewBag.idClasifPregunta = new SelectList(db.ClasifPregunta, "idClasifPregunta", "nombre", preguntas.idClasifPregunta);
            return View(preguntas);
        }

        // POST: /Preguntas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="idPregunta,textoPregunta,idEncuesta,idClasifPregunta,idTipoRespuesta")] Preguntas preguntas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(preguntas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idPregunta = new SelectList(db.Encuestas, "idEncuesta", "nombre", preguntas.idPregunta);
            ViewBag.idEncuesta = new SelectList(db.Encuestas, "idEncuesta", "nombre", preguntas.idEncuesta);
            ViewBag.idPregunta = new SelectList(db.Preguntas, "idPregunta", "textoPregunta", preguntas.idPregunta);
            ViewBag.idPregunta = new SelectList(db.Preguntas, "idPregunta", "textoPregunta", preguntas.idPregunta);
            ViewBag.idTipoRespuesta = new SelectList(db.TiposRespuesta, "idTipoRespuesta", "nombre", preguntas.idTipoRespuesta);
            ViewBag.idClasifPregunta = new SelectList(db.ClasifPregunta, "idClasifPregunta", "nombre", preguntas.idClasifPregunta);
            return View(preguntas);
        }

        // GET: /Preguntas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Preguntas preguntas = db.Preguntas.Find(id);
            if (preguntas == null)
            {
                return HttpNotFound();
            }
            return View(preguntas);
        }

        // POST: /Preguntas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Preguntas preguntas = db.Preguntas.Find(id);
            db.Preguntas.Remove(preguntas);
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
