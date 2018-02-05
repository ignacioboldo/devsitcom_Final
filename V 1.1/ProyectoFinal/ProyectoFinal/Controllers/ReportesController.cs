﻿using BussinesEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using DAL;
using BL;

namespace ProyectoFinal.Controllers
{
    public class ReportesController : Controller
    {
        public UsuarioEntity usuarioActual;
        public UsuariosManager um = new UsuariosManager();
        public NegociosManager nm = new NegociosManager();
        ReportesManager rm = new ReportesManager();


        public List<Negocio> obtenerHospedajesPorUsuario()
        {
            var lista = nm.GetHospedajeByUsuario(usuarioActual.idUsuario);
            return lista;
        }
        public void ObtenerUsuarioActual()
        {
            usuarioActual = (UsuarioEntity)Session["User"];
        }
        // GET: Reportes
        public ActionResult IndexReportes()
        {
            ObtenerUsuarioActual();

            if (usuarioActual.idUsuario == 0)
                usuarioActual.idPerfil = 0;

            ViewBag.idPerfil = usuarioActual.idPerfil;
            ViewBag.countComercio = nm.GetComercioByUsuario(usuarioActual.idUsuario).Count;
            ViewBag.countHospedaje = nm.GetHospedajeByUsuario(usuarioActual.idUsuario).Count;
            return View();

        }

 


        public ActionResult CampoFechaValor_Index(string nombre_reporte)
        {
            ViewBag.nombre_reporte = nombre_reporte;
            

            switch(nombre_reporte)
            {
                case "Reservas por Origen":
                    
                            ViewBag.valor1 = "BUENOS AIRES";
                            ViewBag.valor2 = "CORDOBA";
                            ViewBag.valor3 = "ENTRE RIOS";
                    break;

                case "Reservas por Categoria":
                    
                    ViewBag.valor1 = "3 ESTRELLAS";
                    ViewBag.valor2 = "4 ESTRELLAS";
                    ViewBag.valor3 = "5 ESTRELLAS";
                    break;

                default:
                    break;
            }

            return View();

        }

        public ActionResult CampoFechaValorNegocio_Index(string nombre_reporte, int idTipoReporte)
        {
            ObtenerUsuarioActual();
            var listaNegocio = new List<Negocio>();

            if (idTipoReporte == 1)
                listaNegocio = nm.GetHospedajeByUsuario(usuarioActual.idUsuario);
            else if (idTipoReporte == 2)
                listaNegocio = nm.GetComercioByUsuario(usuarioActual.idUsuario);

            ViewBag.lista_negocios = listaNegocio;
            ViewBag.nombre_reporte = nombre_reporte;
            

            switch(nombre_reporte){
                case "Reservas por Origen Negocio":
                   ViewBag.valor1 = "BUENOS AIRES";
                    ViewBag.valor2 = "CORDOBA";
                    ViewBag.valor3 = "ENTRE RIOS";
                    break;
            
            }
          
            return View();

        }

        public ActionResult ReporteCampoFechaValor(string tipo_reporte, string nombre_reporte, String fecha_desde, String fecha_hasta)
        {

            var lista = new List<ReportesCampoFechaValor>();
            
            var vista_reporte = "";


            switch (nombre_reporte)
            {
                case "Reservas por Categoria":
                    lista = rm.ObtenerReservasPorCategoria(Convert.ToDateTime(fecha_desde), Convert.ToDateTime(fecha_hasta), tipo_reporte);
                    ViewBag.nombre_campo = "Categoria";
                    ViewBag.nombre_valor = "Porcentaje de Reservas";
                    ViewBag.data = lista;
                    ViewBag.clase_reporte = "ReportesCampoFechaValor";
                    vista_reporte = "CampoFechaValor_Tabla";
                    break;
                case "Reservas por Origen":
                    lista = rm.ObtenerReservasPorOrigen(Convert.ToDateTime(fecha_desde), Convert.ToDateTime(fecha_hasta), tipo_reporte);
                    ViewBag.nombre_campo = "Provincia";
                    ViewBag.nombre_valor = "Porcentaje de Reservas";
                    ViewBag.data = lista;
                    ViewBag.clase_reporte = "ReportesCampoFechaValor";
                    vista_reporte = "CampoFechaValor_Tabla";
                    break;


                case "Promociones por Comercio":
                    lista = rm.ObtenerPromocionesPorComercio(Convert.ToDateTime(fecha_desde), Convert.ToDateTime(fecha_hasta), tipo_reporte);
                    ViewBag.nombre_campo = "Comercio";
                    ViewBag.nombre_valor = "Porcentaje de Promociones";
                    ViewBag.data = lista;
                    ViewBag.clase_reporte = "ReportesCampoFechaValor";
                    vista_reporte = "CampoFechaValor_Tabla";

                    break;


                default:

                    break;
            }


            ViewBag.nombre_reporte = nombre_reporte;
            ViewBag.tipo_reporte = tipo_reporte;

            return PartialView(vista_reporte);
        }

        public ActionResult ReporteCampoFechaValorPorNegocio(string tipo_reporte, string nombre_reporte, String fecha_desde, String fecha_hasta, string negocio)
        {

            var lista = new List<ReportesCampoFechaValor>();

            var vista_reporte = "";

            switch (nombre_reporte)
            {

                case "Reservas por Origen Negocio":
                    lista = rm.ObtenerReservasPorOrigenPorNegocio(Convert.ToDateTime(fecha_desde), Convert.ToDateTime(fecha_hasta), tipo_reporte, Convert.ToInt32(negocio));
                    ViewBag.nombre_campo = "Provincia";
                    ViewBag.nombre_valor = "Porcentaje de Reservas";
                    ViewBag.data = lista;
                    ViewBag.clase_reporte = "ReportesCampoFechaValor";

                    vista_reporte = "CampoFechaValor_Tabla";

                    break;

                case "Promociones no Utilizadas Negocio":
                    lista = rm.ObtenerPromocionesNoUtilizadasPorNegocio(Convert.ToDateTime(fecha_desde), Convert.ToDateTime(fecha_hasta), tipo_reporte, Convert.ToInt32(negocio));
                    ViewBag.nombre_campo = "Utilizada";
                    ViewBag.nombre_valor = "Promociones No Utilizadas";
                    ViewBag.data = lista;
                    ViewBag.clase_reporte = "ReportesFechaValor";

                    vista_reporte = "CampoFechaValor_Tabla";

                    break;

                case "Promociones Vencidas Negocio":
                    lista = rm.ObtenerPromocionesVencidasPorNegocio(Convert.ToDateTime(fecha_desde), Convert.ToDateTime(fecha_hasta), tipo_reporte, Convert.ToInt32(negocio));
                    ViewBag.nombre_campo = "Vencida";
                    ViewBag.nombre_valor = "Promociones Vencidas";
                    ViewBag.data = lista;
                    ViewBag.clase_reporte = "ReportesFechaValor";

                    vista_reporte = "CampoFechaValor_Tabla";

                    break;

                case "Promociones por Provincia Negocio":
                    lista = rm.ObtenerPromocionesPorProvincia(Convert.ToDateTime(fecha_desde), Convert.ToDateTime(fecha_hasta), tipo_reporte, Convert.ToInt32(negocio));
                    ViewBag.nombre_campo = "Provincia";
                    ViewBag.nombre_valor = "Porcentaje Promociones";
                    ViewBag.data = lista;
                    
                    ViewBag.clase_reporte = "ReportesCampoFechaValor";

                    vista_reporte = "CampoFechaValor_Tabla";

                    break;
                default:

                    break;
            }


            ViewBag.nombre_reporte = nombre_reporte;
            ViewBag.tipo_reporte = tipo_reporte;

            return PartialView(vista_reporte);
        }



        public JsonResult DataGraficoTortaNegocio(string tipo_reporte, string nombre_reporte, String fecha_desde, String fecha_hasta, string negocio)
        {
            var result = new List<ReportesCampoValor>();
            var resultNegocio = new List<ReportesCampoValorDinamico>();
           

            switch (nombre_reporte)
            {

                case "Reservas por Origen Negocio":
                    resultNegocio = rm.ObtenerReservasPorProvinciaNegocioGrafico(Convert.ToDateTime(fecha_desde), Convert.ToDateTime(fecha_hasta), tipo_reporte, Convert.ToInt32(negocio));
                  
                    break;

                case "Promociones no Utilizadas Negocio":
                    result = rm.ObtenerPromocionesNoUtilizadasPorComercio(Convert.ToDateTime(fecha_desde), Convert.ToDateTime(fecha_hasta), tipo_reporte, Convert.ToInt32(negocio));
                    ViewBag.clase_reporte = "ReportesCampoValor";
                    break;

                case "Promociones Vencidas Negocio":
                   

                    break;

                case "Promociones por Provincia Negocio":
                   

                    

                    break;
                default:

                    break;
            }


            ViewBag.nombre_reporte = nombre_reporte;
            ViewBag.tipo_reporte = tipo_reporte;

            return Json(resultNegocio, JsonRequestBehavior.AllowGet);
            
        }

        public JsonResult DataGraficoTortaDinamico(string tipo_reporte, string nombre_reporte, String fecha_desde, String fecha_hasta)
        {
            var result = new List<ReportesCampoValorDinamico>();


            switch (nombre_reporte)
            {
                case "Reservas por Origen":
                    result = rm.ObtenerReservasPorOrigenGrafico(Convert.ToDateTime(fecha_desde), Convert.ToDateTime(fecha_hasta), tipo_reporte);
                    
                    break;

                case "Reservas por Categoria":
                    result = rm.ObtenerReservasPorCategoriaGrafico(Convert.ToDateTime(fecha_desde), Convert.ToDateTime(fecha_hasta), tipo_reporte);
                  
                    break;

                default:
                    break;
            }

            ViewBag.nombre_reporte = nombre_reporte;
            ViewBag.tipo_reporte = tipo_reporte;

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}