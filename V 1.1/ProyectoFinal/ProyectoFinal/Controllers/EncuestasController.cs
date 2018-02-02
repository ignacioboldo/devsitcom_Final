﻿﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using BussinesEntities;
using BL;

namespace ProyectoFinal
{
    public class EncuestasController : Controller
    {
        private SitcomEntities db = new SitcomEntities();
        private EncuestasManager em = new EncuestasManager();
        private UsuarioEntity usuarioActual = new UsuarioEntity();
        private UsuariosManager um = new UsuariosManager();

        private string nomController = "";

        public EncuestasController()
        {
            //ObtenerUsuarioActual();
            nomController = this.GetType().Name.Substring(0, this.GetType().Name.IndexOf("Controller"));
        }

        // GET: /Encuestas/
        public ActionResult EncuestasIndex()
        {

            ObtenerUsuarioActual();
            List<EncuestaEntityIndexUsuario> encuestasUsuario = em.GetEncuestasAsignadas(usuarioActual.idUsuario);

            //ViewBag.IdEncuesta = idEncuesta;
            ViewBag.idPerfil = usuarioActual.idPerfil;
            List<EncuestaEntityIndex> encuestas = em.GetAllEncuestas();
            return View(encuestas);
        }

        // GET: /Encuestas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Encuestas encuestas = db.Encuestas.Find(id);
            if (encuestas == null)
            {
                return HttpNotFound();
            }
            return View(encuestas);
        }


        // GET: /Encuestas/Edit/5
        public ActionResult EditEncuesta(int? idEncuesta)
        {

       
            if (idEncuesta == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EncuestaEntity en = em.GetEncuestaById(idEncuesta);
            if (en == null)
            {
                return HttpNotFound();
            }

            ViewBag.idTipoEncuesta = new SelectList(db.TiposEncuesta, "idTipoEncuesta", "nombre", en.idTipoEncuesta);
            //ViewBag.idEncuesta = new SelectList(db.TiposEncuesta, "idTipoEncuesta", "nombre", en.idEncuesta);

            return View(en);
        }


        public ActionResult NuevaEncuesta()
        {
           
            ViewBag.idTipoEncuesta = new SelectList(db.TiposEncuesta, "idTipoEncuesta", "nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NuevaEncuesta([Bind(Include = "nombre,descripcion,idTipoEncuesta,setearComoActiva")] EncuestaEntity en)
        {
            if (ModelState.IsValid)
            {
                em.CrearEncuesta(en);
                return RedirectToAction("EncuestasIndex");
            }
            ViewBag.idTipoEncuesta = new SelectList(db.TiposEncuesta, "idTipoEncuesta", "nombre", en.idTipoEncuesta);

            return View(en);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEncuesta([Bind(Include = "idEncuesta,nombre,descripcion,idTipoEncuesta,setearComoActiva")] EncuestaEntity en)
        {
            if (ModelState.IsValid)
            {
                em.EditEncuesta(en);
                return RedirectToAction("EncuestasIndex");
            }
            ViewBag.idTipoEncuesta = new SelectList(db.TiposEncuesta, "idTipoEncuesta", "nombre", en.idTipoEncuesta);

            return View(en);
        }

        // GET: /Encuestas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Encuestas encuestas = db.Encuestas.Find(id);
            if (encuestas == null)
            {
                return HttpNotFound();
            }
            return View(encuestas);
        }

        // POST: /Encuestas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Encuestas encuestas = db.Encuestas.Find(id);
            db.Encuestas.Remove(encuestas);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PreguntasEncuesta(int idEncuesta)
        {
            
            List<PreguntasEntity> pregsEncuesta = em.GetPreguntasEncuesta(idEncuesta);

            ViewBag.IdEncuesta = idEncuesta;
            return View(pregsEncuesta);
        }

        public ActionResult ResponderEncuesta(string tipoEncuesta, int idEncuestaAsignada)
        {
            
            int idTipoEncuesta = 0;
            switch (tipoEncuesta)
            {
                case "Lugares de Hospedaje": idTipoEncuesta = 1;
                    break;

                case "Comercio": idTipoEncuesta = 2;
                    break;

                case "Ciudad": idTipoEncuesta = 3;
                    break;

                default:
                    break;
            }

            int idEncuesta = em.GetEncuestaVigente(idTipoEncuesta);

            EncuestaEntity enc = new EncuestaEntity();
            enc.idEncuesta = idEncuesta;
            enc.Preguntas1 = em.GetPreguntasEncuesta(idEncuesta).ToArray();

            ViewBag.IdEncuestaAsignada = idEncuestaAsignada;
            return View(enc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResponderEncuesta(EncuestaEntity respuestas, int idEncuestaAsignada)
        {
            int noresp = 0;
            foreach (var item in respuestas.Preguntas1)
            {
                if (item.respuesta == null)
                    noresp++;
            }

            if (noresp > 0)
            {
                ViewBag.Error = "Faltan preguntas por responder!";
                ViewBag.IdEncuestaAsignada = idEncuestaAsignada;
                return View(respuestas);
            }

            em.AgregarRespuestas(respuestas.Preguntas1, idEncuestaAsignada);

            return RedirectToAction("EncuestasUsuario");
        }

        public ActionResult EncuestasUsuario()
        {
            
            ObtenerUsuarioActual();
            List<EncuestaEntityIndexUsuario> encuestasUsuario = em.GetEncuestasAsignadas(usuarioActual.idUsuario);

            //ViewBag.IdEncuesta = idEncuesta;
            ViewBag.idPerfil = usuarioActual.idPerfil;

            return View(encuestasUsuario);
        }

        public ActionResult VerEncuestaUsuario(int idEncuestaAsignada)
        {
            List<RespuestasEncuestaUsuarioEntity> respuestas = em.GetRespuestaUsuarioEntity(idEncuestaAsignada);
            return View(respuestas);
        }

        // GET: /Preguntas/Create
        public ActionResult NuevaPregunta(int idEncuesta)
        {

            ViewBag.idTipoRespuesta = new SelectList(db.TiposRespuesta, "idTipoRespuesta", "nombre");
            ViewBag.idClasifPregunta = new SelectList(db.ClasifPregunta, "idClasifPregunta", "nombre");
            ViewBag.IdEncuesta = idEncuesta;
            return View();
        }

        // POST: /Encuestas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NuevaPregunta(PreguntasEntity pregunta)
        {
            if (ModelState.IsValid)
            {
                em.CrearPregunta(pregunta);
                return RedirectToAction("PreguntasEncuesta", new { idEncuesta = pregunta.idEncuesta });
            }

            ViewBag.idTipoRespuesta = new SelectList(db.TiposRespuesta, "idTipoRespuesta", "nombre");
            ViewBag.idClasifPregunta = new SelectList(db.ClasifPregunta, "idClasifPregunta", "nombre");
            ViewBag.IdEncuesta = pregunta.idEncuesta;

            return View(pregunta);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public void ObtenerUsuarioActual()
        {
            usuarioActual = (UsuarioEntity)Session["User"];
        }
    }
}