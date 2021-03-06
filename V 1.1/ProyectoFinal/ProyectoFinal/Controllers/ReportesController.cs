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
        public EncuestasManager em = new EncuestasManager();
        public DomicilioManager dm = new DomicilioManager();
        ReportesManager rm = new ReportesManager();
        SitcomEntities db = new SitcomEntities();


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


            switch (nombre_reporte)
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


            switch (nombre_reporte)
            {
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
                    ViewBag.valor1 = "RESERVAS CON SOLICITUD";
                    ViewBag.valor2 = "RESERVAS DIRECTAS";
                    
                    ViewBag.valor3 = null;
                    break;

                default:
                    break;

            }

            return View();

        }


        public ActionResult CampoFechaValorNegocio_PromVencidas(string nombre_reporte, int idTipoReporte)
        {
            ObtenerUsuarioActual();
            var listaNegocio = new List<Negocio>();

            if (idTipoReporte == 1)
                listaNegocio = nm.GetHospedajeByUsuario(usuarioActual.idUsuario);
            else if (idTipoReporte == 2)
                listaNegocio = nm.GetComercioByUsuario(usuarioActual.idUsuario);

            ViewBag.lista_negocios = listaNegocio;
            ViewBag.nombre_reporte = nombre_reporte;

            List<ReportesCampoValor> lista = new List<ReportesCampoValor>();

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


                //case "Promociones por Comercio":
                //    lista = rm.ObtenerPromocionesPorComercio(Convert.ToDateTime(fecha_desde), Convert.ToDateTime(fecha_hasta), tipo_reporte);
                //    ViewBag.nombre_campo = "Comercio";
                //    ViewBag.nombre_valor = "Porcentaje de Promociones";
                //    ViewBag.data = lista;
                //    ViewBag.clase_reporte = "ReportesCampoFechaValor";
                //    vista_reporte = "CampoFechaValor_Tabla";

                    //break;

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
            var result = new List<ReportesCampoValor>();

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
                    result = rm.ObtenerPromocionesNoUtilizadasPorNegocio(Convert.ToDateTime(fecha_desde), Convert.ToDateTime(fecha_hasta), tipo_reporte, Convert.ToInt32(negocio));
                    ViewBag.nombre_campo = "Utilizada";
                    ViewBag.nombre_valor = "Promociones No Utilizadas";
                    ViewBag.data = result;
                    ViewBag.clase_reporte = "ReportesFechaValor";

                    vista_reporte = "CampoFechaValor_Tabla";

                    break;

                case "Promociones Vencidas Negocio":
                    result = rm.ObtenerPromocionesVencidasPorNegocio(Convert.ToDateTime(fecha_desde), Convert.ToDateTime(fecha_hasta), tipo_reporte, Convert.ToInt32(negocio));
                    ViewBag.nombre_campo = "Vencida";
                    ViewBag.nombre_valor = "Promociones Vencidas";
                    ViewBag.data = lista;
                    ViewBag.clase_reporte = "ReportesFechaValor";

                    vista_reporte = "CampoFechaValor_Tabla";

                    break;

                case "Reservas por Solicitud Negocio":
                    lista = rm.ObtenerReservasPorSolicitudNegocio(Convert.ToDateTime(fecha_desde), Convert.ToDateTime(fecha_hasta), tipo_reporte, Convert.ToInt32(negocio));
                    ViewBag.nombre_campo = "Reservas con Solicitud";
                    ViewBag.nombre_valor = "Reservas sin Solicitud";
                    ViewBag.data = lista;
                    ViewBag.clase_reporte = "ReportesFechaValor";

                    vista_reporte = "CampoFechaValor_Tabla";
                    break;
                //case "Promociones por Provincia Negocio":
                //    lista = rm.ObtenerPromocionesPorProvincia(Convert.ToDateTime(fecha_desde), Convert.ToDateTime(fecha_hasta), tipo_reporte, Convert.ToInt32(negocio));
                //    ViewBag.nombre_campo = "Provincia";
                //    ViewBag.nombre_valor = "Porcentaje Promociones";
                //    ViewBag.data = lista;

                //    ViewBag.clase_reporte = "ReportesCampoFechaValor";

                //    vista_reporte = "CampoFechaValor_Tabla";

                //    break;
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
            var lista = new List<ReportesCampoFechaValor>();
           

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
                    result = rm.ObtenerPromocionesVencidasPorNegocio(Convert.ToDateTime(fecha_desde), Convert.ToDateTime(fecha_hasta), tipo_reporte, Convert.ToInt32(negocio));
                    return Json(result, JsonRequestBehavior.AllowGet);
                    break;

                case "Promociones por Provincia Negocio":
                    int idNeg = int.Parse(negocio);
                    result = rm.ObtenerPromocionesNegocioPorProvincia(fecha_desde, fecha_hasta, idNeg);
                    return Json(result, JsonRequestBehavior.AllowGet);
                    break;

                case "Promociones por Provincia Secretaria":
                    result = rm.ObtenerPromocionesPorProvincia(fecha_desde, fecha_hasta);
                    return Json(result, JsonRequestBehavior.AllowGet);
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




        public JsonResult DataGraficoTortaDinamicoReservas(string tipo_reporte, string nombre_reporte, String fecha_desde, String fecha_hasta, int? idProv1, int? idProv2, int? idProv3, int? idNegocio)
        {
            var result = new List<ReportesCampoValorDinamico>();
            var resultValor = new List<ReportesCampoValorValor>();
            var resultCampo = new List<ReportesCampoValor>();
            var result_2 = new List<ReportesCampoFechaValor>();

            switch (nombre_reporte)
            {
                case "Reservas por Origen":

                    idProv1 = idProv1 == null ? 0 : idProv1;
                    idProv2 = idProv2 == null ? 0 : idProv2;
                    idProv3 = idProv3 == null ? 0 : idProv3;

                    result_2 = rm.ObtenerReservasPorOrigenGrafico_2(fecha_desde, fecha_hasta, tipo_reporte, (int)idProv1, (int)idProv2, (int)idProv3);

                    List<ReportesCampoValorDinamico> rcvd = new List<ReportesCampoValorDinamico>();

                    ReportesCampoFechaValor[] valores = result_2.ToArray();

                    int i = 0;
                    string valorSet = "";
                    string valor2Set = "";
                    string valor3Set = "";
                    Session["Etiqueta_1"] = null;
                    Session["Etiqueta_2"] = null;
                    Session["Etiqueta_3"] = null;
                    while (i < valores.Length)
                    {
                        string fecha = valores[i].fecha;
                        ReportesCampoValorDinamico rc = new ReportesCampoValorDinamico();
                        foreach (var item2 in valores)
                        {
                            if (item2.fecha == fecha)
                            {
                                if (rc.Campo == null)
                                {
                                    rc.Campo = fecha;
                                }

                                if (rc.Valor == null && (valorSet == "" || valorSet == item2.campo))
                                {
                                    if (valorSet == "")
                                    {
                                        valorSet = item2.campo;
                                        rc.Etiqueta_1 = item2.campo;
                                        //ViewBag.Etiqueta_1 = item2.campo;
                                    }

                                    rc.Valor = item2.valor;
                                }
                                else
                                {
                                    if (rc.Valor_2 == null && (valor2Set == "" || valor2Set == item2.campo))
                                    {
                                        if (valor2Set == "")
                                        {
                                            valor2Set = item2.campo;
                                            rc.Etiqueta_2 = item2.campo;
                                            //ViewBag.Etiqueta_2 = item2.campo;
                                        }


                                        rc.Valor_2 = item2.valor;
                                    }
                                    else
                                    {
                                        if (rc.Valor_3 == null && (valor3Set == "" || valor3Set == item2.campo))
                                        {
                                            if (valor3Set == "")
                                            {
                                                valor3Set = item2.campo;
                                                rc.Etiqueta_3 = item2.campo;
                                                //ViewBag.Etiqueta_3 = item2.campo;
                                            }

                                            rc.Valor_3 = item2.valor;
                                        }

                                    }
                                }
                                i++;
                            }
                        }

                        rcvd.Add(rc);
                    }


                    return Json(rcvd, JsonRequestBehavior.AllowGet);
                    break;

                case "Reservas por Origen Negocio":

                    idProv1 = idProv1 == null ? 0 : idProv1;
                    idProv2 = idProv2 == null ? 0 : idProv2;
                    idProv3 = idProv3 == null ? 0 : idProv3;

                    result_2 = rm.ObtenerReservasPorOrigenGrafico_ProvNegocio(fecha_desde, fecha_hasta, tipo_reporte, (int)idProv1, (int)idProv2, (int)idProv3, (int)idNegocio);

                    List<ReportesCampoValorDinamico> rcvd_1 = new List<ReportesCampoValorDinamico>();

                    ReportesCampoFechaValor[] valores_1 = result_2.ToArray();

                    int j = 0;
                    string valorSet_1 = "";
                    string valor2Set_1 = "";
                    string valor3Set_1 = "";
                    Session["Etiqueta_1"] = null;
                    Session["Etiqueta_2"] = null;
                    Session["Etiqueta_3"] = null;

                    while (j < valores_1.Length)
                    {
                        string fecha = valores_1[j].fecha;
                        ReportesCampoValorDinamico rc = new ReportesCampoValorDinamico();
                        foreach (var item2 in valores_1)
                        {
                            if (item2.fecha == fecha)
                            {
                                if (rc.Campo == null)
                                {
                                    rc.Campo = fecha;
                                }

                                if (rc.Valor == null && (valorSet_1 == "" || valorSet_1 == item2.campo))
                                {
                                    if (valorSet_1 == "")
                                    {
                                        valorSet_1 = item2.campo;
                                        rc.Etiqueta_1 = item2.campo;
                                        //ViewBag.Etiqueta_1 = item2.campo;
                                    }

                                    rc.Valor = item2.valor;
                                }
                                else
                                {
                                    if (rc.Valor_2 == null && (valor2Set_1 == "" || valor2Set_1 == item2.campo))
                                    {
                                        if (valor2Set_1 == "")
                                        {
                                            valor2Set_1 = item2.campo;
                                            rc.Etiqueta_2 = item2.campo;
                                            //ViewBag.Etiqueta_2 = item2.campo;
                                        }
                                        rc.Valor_2 = item2.valor;
                                    }
                                    else
                                    {
                                        if (rc.Valor_3 == null && (valor3Set_1 == "" || valor3Set_1 == item2.campo))
                                        {
                                            if (valor3Set_1 == "")
                                            {
                                                valor3Set_1 = item2.campo;
                                                rc.Etiqueta_3 = item2.campo;
                                                //ViewBag.Etiqueta_3 = item2.campo;
                                            }

                                            rc.Valor_3 = item2.valor;
                                        }

                                    }
                                }
                                j++;
                            }
                        }

                        rcvd_1.Add(rc);
                    }


                    return Json(rcvd_1, JsonRequestBehavior.AllowGet);
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
        public JsonResult DataGraficoPromocionesPorEstado(string fechaDesde, string fechaHasta)
        {
            List<ReportesCampoValor> result = new List<ReportesCampoValor>();

            result = rm.ObtenerPromocionesPorEstado(fechaDesde, fechaHasta);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

       public JsonResult DataGraficoReservasPorTipoHospedaje(string fechaDesde, string fechaHasta)
        {
            List<ReportesCampoCantidadValor> result = new List<ReportesCampoCantidadValor>();   

            result = rm.ObtenerReservasPorTipoHospedaje(fechaDesde, fechaHasta);
      

            List<ReportesCampoValor> resultParaGraficar = new List<ReportesCampoValor>();

            foreach (var item in result)
            {
                ReportesCampoValor r = new ReportesCampoValor()
                {
                    Campo = item.campo,
                    Valor = item.valor
                };

                resultParaGraficar.Add(r);
            }      
                       

            return Json(resultParaGraficar, JsonRequestBehavior.AllowGet);
        }

		public JsonResult DataGraficoReservasPorTipoHospedajeConCantidad(string fechaDesde, string fechaHasta)
        {
            List<ReportesCampoCantidadValor> result = new List<ReportesCampoCantidadValor>();

            result = rm.ObtenerReservasPorTipoHospedaje(fechaDesde, fechaHasta);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DataGraficoPromocionesPorComercio(string fechaDesde, string fechaHasta, int idNegocio)
        {
            List<ReportesCampoValor> result = new List<ReportesCampoValor>();

            result = rm.ObtenerPromocionesPorComercio(fechaDesde, fechaHasta, idNegocio);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DataGraficoReservasPorProvinciaGeneral(string fechaDesde, string fechaHasta, int idTipoLugarHospedaje)
        {
            List<ReportesCampoValor> result = new List<ReportesCampoValor>();

            result = rm.ObtenerReservasPorProvinciaGeneral(fechaDesde, fechaHasta, idTipoLugarHospedaje);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
       
        public JsonResult DataGraficoCantPasajerosHospedados(string tipo_reporte, string fecha_desde, string fecha_hasta)
        {
            List<ReportesCampoValor> resultCampo = new List<ReportesCampoValor>();
            resultCampo = rm.ObtenerCantidadPasajerosGrafico(Convert.ToDateTime(fecha_desde), Convert.ToDateTime(fecha_hasta), tipo_reporte);
            return Json(resultCampo, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DataGraficoPromocionesPorProvincia(string fecha_desde, string fecha_hasta)
        {
            List<ReportesCampoValor> result = new List<ReportesCampoValor>();
            result = rm.ObtenerPromocionesPorProvincia(fecha_desde, fecha_hasta);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DataGraficoPromocionesPorProvinciaNegocio(string fecha_desde, string fecha_hasta, string negocio)
        {
            List<ReportesCampoValor> result = new List<ReportesCampoValor>();
            int idNeg = int.Parse(negocio);
            result = rm.ObtenerPromocionesNegocioPorProvincia(fecha_desde, fecha_hasta, idNeg);
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

            if (idEncuesta != null)
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

        public ActionResult ReporteCantidadPasajerosHospedados(string nombre_reporte)
        {
            ViewBag.nombre_reporte = nombre_reporte;
            return View();
        }
        public ActionResult ReporteEncuestasCampoValor()
        {
            return View();
        }

        public ActionResult ReportePromocionesCampoValor()
        {
            return View();
        }

        public ActionResult ReportePromocionesPorComercioCampoValor()
        {
            ObtenerUsuarioActual();
            var listaNegocio = new List<Negocio>();

            listaNegocio = nm.GetComercioByUsuario(usuarioActual.idUsuario);

            ViewBag.lista_negocios = listaNegocio;

            return View();
        }
        public ActionResult ReporteReservasPorProvinciaGeneral()
        {
            ObtenerUsuarioActual();

            List<TipoLugarHospedaje> tiposHospedaje = db.TipoLugarHospedaje.ToList();

            tiposHospedaje.Add(new TipoLugarHospedaje() { idTipoLugarHospedaje = 0, nombre = "TODOS" });

            SelectList listaTiposHospedaje = new SelectList(tiposHospedaje, "idTipoLugarHospedaje", "nombre", 0);

            ViewBag.ListaTiposHospedaje = listaTiposHospedaje;

            return View();
        }

        public ActionResult ReportePorcentajeReservasTipoHospedaje()
        {
            ViewBag.nombre_reporte = "PORCENTAJE DE RESERVAS POR TIPO DE HOSPEDAJE";
           return View();
        }

        public ActionResult CampoFechaValor_ReservasOrigen(string nombre_reporte)
        {
            List<Provincia> listProvincias = dm.getProvinciaPaisSeleccionado(1);
            ViewBag.nombre_reporte = nombre_reporte;
            ViewBag.Provincias = listProvincias;

            return View();
        }

public ActionResult CampoFechaValor_ReservasOrigenNegocio(string nombre_reporte,int idTipoReporte, int idNegocio)
        {
            ObtenerUsuarioActual();
            List<Provincia> listProvincias = dm.getProvinciaPaisSeleccionado(1);
            ViewBag.nombre_reporte = nombre_reporte;
            ViewBag.Provincias = listProvincias;
            ViewBag.IdNegocio = idNegocio;

            var listaNegocio = new List<Negocio>();

            if (idTipoReporte == 1)
                listaNegocio = nm.GetHospedajeByUsuario(usuarioActual.idUsuario);
            else if (idTipoReporte == 2)
                listaNegocio = nm.GetComercioByUsuario(usuarioActual.idUsuario);

            ViewBag.lista_negocios = listaNegocio;
            ViewBag.nombre_reporte = nombre_reporte;

            return View();
        }


        public ActionResult PromocionesNegocioPorProvincia(string nombre_reporte, int idTipoReporte)
        {
            ObtenerUsuarioActual();
            var listaNegocio = new List<Negocio>();

            if (idTipoReporte == 1)
                listaNegocio = nm.GetHospedajeByUsuario(usuarioActual.idUsuario);
            else if (idTipoReporte == 2)
                listaNegocio = nm.GetComercioByUsuario(usuarioActual.idUsuario);

            ViewBag.lista_negocios = listaNegocio;
            ViewBag.nombre_reporte = nombre_reporte;

         
            return View();

        }

        public ActionResult PromocionesPorProvincia(string nombre_reporte, int idTipoReporte)
        {
           
            ViewBag.nombre_reporte = nombre_reporte;
            return View();

        }
    }
}