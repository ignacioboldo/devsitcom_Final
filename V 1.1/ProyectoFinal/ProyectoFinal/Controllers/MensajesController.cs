﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoFinal.Controllers
{
    public class MensajesController : Controller
    {
        // GET: Mensajes
        public ActionResult Index(string codigo)
        {
            ViewBag.codigo = codigo;
            return View();
        }
    }
}