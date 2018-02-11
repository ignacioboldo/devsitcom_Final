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
        public EncuestasManager em = new EncuestasManager();
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
                case "Reservas por Solicitud":
                    ViewBag.valor1 = "RESERVAS DIRECTAS";
                    ViewBag.valor2 = "RESERVAS CON SOLICITUD";
                    break;

                case "Porcentaje Ocupacion":
                     
                    ViewBag.valor1 = "OCUPADO";
                    ViewBag.valor2 = "LIBRE";
                    
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

                case "Porcentaje Ocupacion Negocio":
                     ViewBag.valor1 = "OCUPADO";
                    ViewBag.valor2 = "LIBRE";
                    ViewBag.valor3 = null;
                    break;

                case "Promociones no Utilizadas Negocio":
                    ViewBag.valor1 = "NO UTILIZADAS";
                    ViewBag.valor2 = "UTILIZADAS";
                    ViewBag.valor3 = null;
                    break;

                case "Reservas por Solicitud Negocio":
                    ViewBag.valor1 = "RESERVAS DIRECTAS";
                    ViewBag.valor2 = "RESERVAS CON SOLICITUD";
                    ViewBag.valor3 = null;
                    break;

                default:
                    break;
            
            }
          
            return View();

        }

        public ActionResult ReporteCampoFechaValor(string tipo_reporte, string nombre_reporte, String fecha_desde, String fecha_hasta)
        {

            var lista = new List<ReportesCampoFechaValor>();
            var listaValor = new List<ReportesCampoValor>();
            
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

                case "Reservas por Solicitud":
                    lista = rm.ObtenerReservasPorSolicitud(Convert.ToDateTime(fecha_desde), Convert.ToDateTime(fecha_hasta), tipo_reporte);
                    ViewBag.data = lista;
                    ViewBag.nombre_campo = "Reservas Directas";
                    ViewBag.nombre_valor = "Reservas con Solicitud";
                    ViewBag.clase_reporte = "ReportesCampoFechaValor";
                    vista_reporte = "CampoFechaValor_Tabla";
                    break;
                  
                case "Cantidad Pasajeros Hospedados":
                    listaValor = rm.ObtenerCantidadPasajerosGrafico(Convert.ToDateTime(fecha_desde), Convert.ToDateTime(fecha_hasta), tipo_reporte);
                    ViewBag.data = listaValor;
                    ViewBag.nombre_campo = "Fecha";
                    ViewBag.nombre_valor = "Cantidad Pasajeros Hospedados";
                    ViewBag.clase_reporte = "ReportesCampoValor";
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
                    return Json(resultNegocio, JsonRequestBehavior.AllowGet);
                    break;

                case "Reservas por Solicitud Negocio":
                    resultNegocio = rm.ObtenerReservasPorSolicitudNegocioGrafico(Convert.ToDateTime(fecha_desde), Convert.ToDateTime(fecha_hasta), tipo_reporte, Convert.ToInt32(negocio));
                    return Json(resultNegocio, JsonRequestBehavior.AllowGet);
                    break;

                case "Promociones no Utilizadas Negocio":
                    resultNegocio = rm.ObtenerPromocionesNoUtilizadasPorNegocioGrafico(Convert.ToDateTime(fecha_desde), Convert.ToDateTime(fecha_hasta), tipo_reporte, Convert.ToInt32(negocio));
                   
                    return Json(resultNegocio, JsonRequestBehavior.AllowGet);
                    break;

                
                case "Promociones Vencidas Negocio":

                    return Json(result, JsonRequestBehavior.AllowGet);
                    break;

                case "Promociones por Provincia Negocio":
                    lista = rm.ObtenerPromocionesPorProvincia(Convert.ToDateTime(fecha_desde), Convert.ToDateTime(fecha_hasta), tipo_reporte, Convert.ToInt32(negocio));
                    return Json(lista, JsonRequestBehavior.AllowGet);
                    break;

                case "Porcentaje Ocupacion Negocio":

                    resultNegocio = rm.ObtenerPorcentajeOcupacionNegocioGrafico(Convert.ToDateTime(fecha_desde), Convert.ToDateTime(fecha_hasta), tipo_reporte, Convert.ToInt32(negocio));
                    return Json(resultNegocio, JsonRequestBehavior.AllowGet);

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
            var resultValor = new List<ReportesCampoValorValor>();
            var resultCampo = new List<ReportesCampoValor>();


            switch (nombre_reporte)
            {
                case "Reservas por Origen":
                    result = rm.ObtenerReservasPorOrigenGrafico(Convert.ToDateTime(fecha_desde), Convert.ToDateTime(fecha_hasta), tipo_reporte);
                    return Json(result, JsonRequestBehavior.AllowGet);
                    break;

                case "Porcentaje Ocupacion":
                    result = rm.ObtenerPorcentajeOcupacionGrafico(Convert.ToDateTime(fecha_desde), Convert.ToDateTime(fecha_hasta), tipo_reporte);
                    return Json(result, JsonRequestBehavior.AllowGet);
                    break;

                case "Reservas por Categoria":
                    result = rm.ObtenerReservasPorCategoriaGrafico(Convert.ToDateTime(fecha_desde), Convert.ToDateTime(fecha_hasta), tipo_reporte);
                    return Json(result, JsonRequestBehavior.AllowGet);
                    break;

                case "Reservas por Solicitud":
                    resultValor = rm.ObtenerReservasPorSolicitudGrafico(Convert.ToDateTime(fecha_desde), Convert.ToDateTime(fecha_hasta), tipo_reporte);
                    return Json(resultValor, JsonRequestBehavior.AllowGet);
                    break;
                case "Cantidad Pasajeros Hospedados":
                    resultCampo = rm.ObtenerCantidadPasajerosGrafico(Convert.ToDateTime(fecha_desde), Convert.ToDateTime(fecha_hasta), tipo_reporte);
                    return Json(resultCampo, JsonRequestBehavior.AllowGet);
                    
                    break;

                default:
                    return Json(result, JsonRequestBehavior.AllowGet);
                    break;
            }

            ViewBag.nombre_reporte = nombre_reporte;
            ViewBag.tipo_reporte = tipo_reporte;

            
        }



                    return Json(rcvd, JsonRequestBehavior.AllowGet);
                    break;
           

                default:
                    return Json(result, JsonRequestBehavior.AllowGet);
                    break;
            }

            ViewBag.nombre_reporte = nombre_reporte;
            ViewBag.tipo_reporte = tipo_reporte;


        }
   public JsonResult DataGraficoPregEncuesta(int idPregunta, int idEncuesta, int idNegocio, string fechaDesde, string fechaHasta)
        {
            List<ReportesCampoValor> result = new List<ReportesCampoValor>();

            result = rm.ObtenerRespuestasPorPreguntaNegocio(idEncuesta, idPregunta, idNegocio, fechaDesde, fechaHasta);

            return Json(result, JsonRequestBehavior.AllowGet);

        }

  public JsonResult DataGraficoPregEncuestaSecretaria(int idPregunta, int idEncuesta, string fechaDesde, string fechaHasta)
        {
            List<ReportesCampoValor> result = new List<ReportesCampoValor>();

            result = rm.ObtenerRespuestasPorPregunta(idEncuesta, idPregunta, fechaDesde, fechaHasta);

            return Json(result, JsonRequestBehavior.AllowGet);

        }

   public JsonResult DataGraficoEncuestasPorEstado(string fechaDesde, string fechaHasta)
        {
          List<ReportesCampoValor> result = new List<ReportesCampoValor>();

          result = rm.ObtenerEncuestasPorEstado(fechaDesde, fechaHasta);

          return Json(result, JsonRequestBehavior.AllowGet);
        }

  public ActionResult RespuestasEncuestaNegocio(int? idEncuesta, int? idNegocio, string fechaDesde, string fechaHasta)
        {
            ObtenerUsuarioActual();
            List<Negocio> negocios = nm.GetNegocioByUsuario(usuarioActual.idUsuario);
            ViewBag.lista_negocios = negocios;
            ViewBag.nombre_reporte = "Encuesta de Negocio";

            if (idNegocio != null)
            {
                List<EncuestasRespEntity> encuestas = em.GetEncuestasRespondidasNegocio((int)idNegocio);
                ViewBag.IdNegocio = idNegocio;
                ViewBag.EncuestasNegocio = encuestas;
            }            

            if(idEncuesta != null)
            { 
                 List<PreguntasEntity> pregs = em.GetPreguntasEncuesta((int)idEncuesta);

                 string fecha_desde = fechaDesde == null ? "" : fechaDesde.ToString();
                 string fecha_hasta = fechaHasta == null ? "" : fechaHasta.ToString();

                 Session["fecha_desde"] = fecha_desde;
                 Session["fecha_hasta"] = fecha_hasta;

                 List<ReportesCampoValor> result = rm.ObtenerRespuestasPorPreguntaNegocio((int)idEncuesta, (int)pregs.FirstOrDefault().idPregunta, (int)idNegocio, fecha_desde, fecha_hasta);

                 int cantEnc = 0;
                 foreach (var item in result)
                 {
                     cantEnc += int.Parse(item.Valor);
                 }

                 ViewBag.CantEncuestasResp = cantEnc;
                 ViewBag.IdEncuesta = idEncuesta; 
                 ViewBag.IdNegocio = idNegocio;
                 
                 
                 return View(pregs);
            }            
            
            return View();
        }

  public ActionResult RespuestasEncuestaSecretaria(int? idEncuesta, string fechaDesde, string fechaHasta)
        {
            ObtenerUsuarioActual();
            ViewBag.nombre_reporte = "Encuestas";

            
            List<EncuestasRespEntity> encuestas = em.GetEncuestasRespondidas();
            ViewBag.Encuestas = encuestas;


            if (idEncuesta != null)
            {
                Session["fecha_desde"] = fechaDesde;
                Session["fecha_hasta"] = fechaHasta;

                List<PreguntasEntity> pregs = em.GetPreguntasEncuesta((int)idEncuesta);

                List<ReportesCampoValor> result = rm.ObtenerRespuestasPorPregunta((int)idEncuesta, (int)pregs.FirstOrDefault().idPregunta, fechaDesde, fechaHasta);

                int cantEnc = 0;
                foreach (var item in result)
                {
                    cantEnc += int.Parse(item.Valor);
                }

                ViewBag.CantEncuestasResp = cantEnc;
                ViewBag.IdEncuesta = idEncuesta;
                return View(pregs);
            }

            return View();
        }

   public ActionResult ReporteEncuestasCampoValor()
   {
       return View();
    }

   public ActionResult CampoFechaValor_ReservasOrigen(string nombre_reporte) 
        {
            List<Provincia> listProvincias = dm.getProvinciaPaisSeleccionado(1);
            ViewBag.nombre_reporte = nombre_reporte;
            ViewBag.Provincias = listProvincias;

            return View();
        }

    }
}