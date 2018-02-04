using BL;
using BussinesEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GraficoTorta()
        {
            return View();
        }

        public JsonResult DataGraficoTorta(string un_parametro)
        {
            GraficoTestManager gm = new GraficoTestManager();

            var result = gm.DatosGraficoTest();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GraficoBarra()
        {
            return View();
        }

        public JsonResult DataGraficoBarra(string un_parametro)
        {
           
            GraficoTestManager gm = new GraficoTestManager();

            var result = gm.DatosGraficoTest();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}