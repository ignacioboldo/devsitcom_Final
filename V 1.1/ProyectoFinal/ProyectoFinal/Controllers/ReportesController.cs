﻿using BussinesEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using BL;

namespace ProyectoFinal.Controllers
{
    public class ReportesController : Controller
    {
        // GET: Reportes
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BuscarReporte(string nombre_reporte, DateTime? fecha_desde, DateTime? fecha_hasta)
        {
            var vista_reporte = "";

            switch (nombre_reporte)
            {
                case "GRAFICO_PIE":

                    vista_reporte = "GraficoPie";
                    break;
                case "GRAFICO_BAR":

                    vista_reporte = "GraficoBar";
                    break;
                case "DATA_TABLA":

                     var lista = new List<ReportesDataTableEntitiesFechaValor>();
                   //  ReportesDataTableEntities rDataTable = new ReportesDataTableEntities();
                     ReportesManager rm = new ReportesManager();
                     lista =  rm.getReporteCampoFecha(Convert.ToDateTime("1/1/2017"),Convert.ToDateTime("1/1/2020"),"Diaria");
                    // rDataTable.descripcion = "Prueba";
                     //lista.Add(rDataTable);
                        
                     ViewBag.nombre_reporte = "Solicitudes de Reserva por Negocio";
                     ViewBag.data = lista;

                     vista_reporte = "DataTable";
                    break;
                default:
                    vista_reporte = "SinDatos";
                    break;
            }

            return PartialView(vista_reporte);
        }



    }
}