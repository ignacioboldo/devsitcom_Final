using BussinesEntities;
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
            return View();

        }

        public ActionResult CampoFechaValorNegocio_Index(string nombre_reporte, int idTipoReporte)
        {
            ObtenerUsuarioActual();
            var listaNegocio = new List<Negocio>();

            if (idTipoReporte == 1)
                listaNegocio = nm.GetHospedajeByUsuario(usuarioActual.idUsuario);
            else if(idTipoReporte == 2)
                listaNegocio = nm.GetComercioByUsuario(usuarioActual.idUsuario);

            ViewBag.lista_negocios = listaNegocio;
            ViewBag.nombre_reporte = nombre_reporte;
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
                default:

                    break;
            }


            ViewBag.nombre_reporte = nombre_reporte;
            ViewBag.tipo_reporte = tipo_reporte;
           
            return PartialView(vista_reporte);
        }

    }
}