using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BussinesEntities;
using System.Data.SqlClient;

namespace BL
{
    public class ReportesManager
    {

        private ConvertidorEntitiesToDAL convert = new ConvertidorEntitiesToDAL();


        public List<ReportesDataTableEntitiesFechaValor> getReporteCampoFecha(DateTime FechaDesde, DateTime FechaHasta, String Vista)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramFechaDesde = new SqlParameter("@pFechaDesde", FechaDesde);
                SqlParameter paramFechaHasta = new SqlParameter("@pFechaHasta", FechaHasta);
                SqlParameter paramVista = new SqlParameter("@pVista", Vista);


                return db.Database.SqlQuery<ReportesDataTableEntitiesFechaValor>("EXEC [dbo].[ObtenerSolicitudesDeReservasNoConfirmadas] @fechaDesde=@pfechaDesde , @fechaHasta=@pfechaHasta  , @vista=@pVista", paramFechaDesde, paramFechaHasta, paramVista).ToList();



            }
        }

        public List<ReportesCampoFechaValor> ObtenerReservasPorOrigen(DateTime FechaDesde, DateTime FechaHasta, String Vista)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramFechaDesde = new SqlParameter("@pFechaDesde", FechaDesde);
                SqlParameter paramFechaHasta = new SqlParameter("@pFechaHasta", FechaHasta);
                SqlParameter paramVista = new SqlParameter("@pVista", Vista);


                return db.Database.SqlQuery<ReportesCampoFechaValor>("EXEC [Reportes].[ObtenerOrigenDeReservasPorFecha] @fechaDesde=@pfechaDesde , @fechaHasta=@pfechaHasta  , @vista=@pVista", paramFechaDesde, paramFechaHasta, paramVista).ToList();



            }
        }

        public List<ReportesCampoFechaValor> ObtenerReservasPorOrigenPorNegocio(DateTime FechaDesde, DateTime FechaHasta, String Vista, Int32 Negocio)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramFechaDesde = new SqlParameter("@pFechaDesde", FechaDesde);
                SqlParameter paramFechaHasta = new SqlParameter("@pFechaHasta", FechaHasta);
                SqlParameter paramVista = new SqlParameter("@pVista", Vista);
                SqlParameter paramNegocio = new SqlParameter("@pNegocio", Negocio);

                return db.Database.SqlQuery<ReportesCampoFechaValor>("EXEC [Reportes].[ObtenerOrigenDeReservasPorNegocio] @fechaDesde=@pfechaDesde , @fechaHasta=@pfechaHasta  , @vista=@pVista, @idNegocio=@pNegocio", paramFechaDesde, paramFechaHasta, paramVista, paramNegocio).ToList();



            }
        }
        public List<ReportesCampoValorDinamico> ObtenerReservasPorOrigenGrafico(DateTime FechaDesde, DateTime FechaHasta, String Vista)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramFechaDesde = new SqlParameter("@pFechaDesde", FechaDesde);
                SqlParameter paramFechaHasta = new SqlParameter("@pFechaHasta", FechaHasta);
                SqlParameter paramVista = new SqlParameter("@pVista", Vista);


                return db.Database.SqlQuery<ReportesCampoValorDinamico>("EXEC [Reportes].[ObtenerOrigenDeReservasPorFecha_Grafico] @fechaDesde=@pfechaDesde , @fechaHasta=@pfechaHasta  , @vista=@pVista", paramFechaDesde, paramFechaHasta, paramVista).ToList();



            }
        }

        public List<ReportesCampoFechaValor> ObtenerReservasPorOrigenGrafico_2(string FechaDesde, string FechaHasta, String Vista, int? idProv1, int? idProv2, int? idProv3)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramFechaDesde = new SqlParameter("@pFechaDesde", FechaDesde);
                SqlParameter paramFechaHasta = new SqlParameter("@pFechaHasta", FechaHasta);
                SqlParameter paramVista = new SqlParameter("@pVista", Vista);
                SqlParameter paramProv1 = new SqlParameter("@pProv1", idProv1);
                SqlParameter paramProv2 = new SqlParameter("@pProv2", idProv2);
                SqlParameter paramProv3 = new SqlParameter("@pProv3", idProv3);


                return db.Database.SqlQuery<ReportesCampoFechaValor>("EXEC [Reportes].[ObtenerOrigenDeReservasPorFecha_Grafico] @fechaDesdeP=@pfechaDesde , @fechaHastaP=@pfechaHasta  , @vista=@pVista, @idProv1=@pProv1, @idProv2=@pProv2, @idProv3=@pProv3", paramFechaDesde, paramFechaHasta, paramVista,paramProv1,paramProv2,paramProv3).ToList();



            }
        }

public List<ReportesCampoFechaValor> ObtenerReservasPorOrigenGrafico_ProvNegocio(string FechaDesde, string FechaHasta, String Vista, int? idProv1, int? idProv2, int? idProv3, int idNegocio)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramFechaDesde = new SqlParameter("@pFechaDesde", FechaDesde);
                SqlParameter paramFechaHasta = new SqlParameter("@pFechaHasta", FechaHasta);
                SqlParameter paramVista = new SqlParameter("@pVista", Vista);
                SqlParameter paramProv1 = new SqlParameter("@pProv1", idProv1);
                SqlParameter paramProv2 = new SqlParameter("@pProv2", idProv2);
                SqlParameter paramProv3 = new SqlParameter("@pProv3", idProv3);
                SqlParameter paramIdNegocio = new SqlParameter("@pIdNegocio", idNegocio);


                return db.Database.SqlQuery<ReportesCampoFechaValor>("EXEC [Reportes].[ObtenerOrigenDeReservasNegocioPorFecha_Grafico] @fechaDesdeP=@pfechaDesde , @fechaHastaP=@pfechaHasta  , @vista=@pVista, @idProv1=@pProv1, @idProv2=@pProv2, @idProv3=@pProv3, @idNegocio=@pIdNegocio", paramFechaDesde, paramFechaHasta, paramVista,paramProv1,paramProv2,paramProv3,paramIdNegocio).ToList();
            }
        }

        public List<ReportesCampoValorDinamico> ObtenerPorcentajeOcupacionGrafico(DateTime FechaDesde, DateTime FechaHasta, String Vista)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramFechaDesde = new SqlParameter("@pFechaDesde", FechaDesde);
                SqlParameter paramFechaHasta = new SqlParameter("@pFechaHasta", FechaHasta);
                SqlParameter paramVista = new SqlParameter("@pVista", Vista);


                return db.Database.SqlQuery<ReportesCampoValorDinamico>("EXEC [Reportes].[ObtenerPorcentajeOcupacion_Grafico] @fechaDesde=@pfechaDesde , @fechaHasta=@pfechaHasta  , @vista=@pVista", paramFechaDesde, paramFechaHasta, paramVista).ToList();



            }
        }
        public List<ReportesCampoValorDinamico> ObtenerPorcentajeOcupacionNegocioGrafico(DateTime FechaDesde, DateTime FechaHasta, String Vista, Int32 Negocio)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramFechaDesde = new SqlParameter("@pFechaDesde", FechaDesde);
                SqlParameter paramFechaHasta = new SqlParameter("@pFechaHasta", FechaHasta);
                SqlParameter paramVista = new SqlParameter("@pVista", Vista);
                SqlParameter paramNegocio = new SqlParameter("@pNegocio", Negocio);


                return db.Database.SqlQuery<ReportesCampoValorDinamico>("EXEC [Reportes].[ObtenerPorcentajeOcupacionPorNegocio_Grafico] @fechaDesde=@pfechaDesde , @fechaHasta=@pfechaHasta  , @vista=@pVista, @idNegocio=@pNegocio", paramFechaDesde, paramFechaHasta, paramVista, paramNegocio).ToList();



            }
        }

        public List<ReportesCampoValorDinamico> ObtenerReservasPorCategoriaGrafico(DateTime FechaDesde, DateTime FechaHasta, String Vista)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramFechaDesde = new SqlParameter("@pFechaDesde", FechaDesde);
                SqlParameter paramFechaHasta = new SqlParameter("@pFechaHasta", FechaHasta);
                SqlParameter paramVista = new SqlParameter("@pVista", Vista);


                return db.Database.SqlQuery<ReportesCampoValorDinamico>("EXEC [Reportes].[obtenerReservasPorCategoria_Grafico] @fechaDesde=@pfechaDesde , @fechaHasta=@pfechaHasta  , @vista=@pVista", paramFechaDesde, paramFechaHasta, paramVista).ToList();



            }
        }

        public List<ReportesCampoValorDinamico> ObtenerReservasPorProvinciaNegocioGrafico(DateTime FechaDesde, DateTime FechaHasta, String Vista, Int32 Negocio)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramFechaDesde = new SqlParameter("@pFechaDesde", FechaDesde);
                SqlParameter paramFechaHasta = new SqlParameter("@pFechaHasta", FechaHasta);
                SqlParameter paramVista = new SqlParameter("@pVista", Vista);
                SqlParameter paramNegocio = new SqlParameter("@pNegocio", Negocio);


                return db.Database.SqlQuery<ReportesCampoValorDinamico>("EXEC [Reportes].[ObtenerOrigenDeReservasPorNegocio_Grafico] @fechaDesde=@pfechaDesde , @fechaHasta=@pfechaHasta  , @vista=@pVista, @idNegocio=@pNegocio", paramFechaDesde, paramFechaHasta, paramVista, paramNegocio).ToList();



            }
        }
        public List<ReportesCampoValor> ObtenerReservasPorOrigenPorNegocioGrafico(DateTime FechaDesde, DateTime FechaHasta, String Vista, Int32 Negocio)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramFechaDesde = new SqlParameter("@pFechaDesde", FechaDesde);
                SqlParameter paramFechaHasta = new SqlParameter("@pFechaHasta", FechaHasta);
                SqlParameter paramVista = new SqlParameter("@pVista", Vista);
                SqlParameter paramNegocio = new SqlParameter("@pNegocio", Negocio);

                return db.Database.SqlQuery<ReportesCampoValor>("EXEC [Reportes].[ObtenerOrigenDeReservasPorNegocio] @fechaDesde=@pfechaDesde , @fechaHasta=@pfechaHasta  , @vista=@pVista, @idNegocio=@pNegocio", paramFechaDesde, paramFechaHasta, paramVista, paramNegocio).ToList();



            }
        }


        public List<ReportesCampoValor> ObtenerCantidadPasajerosGrafico(DateTime FechaDesde, DateTime FechaHasta, String Vista)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramFechaDesde = new SqlParameter("@pFechaDesde", FechaDesde);
                SqlParameter paramFechaHasta = new SqlParameter("@pFechaHasta", FechaHasta);
                SqlParameter paramVista = new SqlParameter("@pVista", Vista);


                return db.Database.SqlQuery<ReportesCampoValor>("EXEC [Reportes].[ObtenerCantidadPasajerosHospedados_Grafico] @fechaDesde=@pfechaDesde , @fechaHasta=@pfechaHasta  , @vista=@pVista", paramFechaDesde, paramFechaHasta, paramVista).ToList();



            }
        }
        public List<ReportesCampoFechaValor> ObtenerReservasPorSolicitud(DateTime FechaDesde, DateTime FechaHasta, String Vista)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramFechaDesde = new SqlParameter("@pFechaDesde", FechaDesde);
                SqlParameter paramFechaHasta = new SqlParameter("@pFechaHasta", FechaHasta);
                SqlParameter paramVista = new SqlParameter("@pVista", Vista);


                return db.Database.SqlQuery<ReportesCampoFechaValor>("EXEC [Reportes].[obtenerSolicitudesReservaVsReservaDirecta] @fechaDesde=@pfechaDesde , @fechaHasta=@pfechaHasta  , @vista=@pVista", paramFechaDesde, paramFechaHasta, paramVista).ToList();



            }
        }
        public List<ReportesCampoValorValor> ObtenerReservasPorSolicitudGrafico(DateTime FechaDesde, DateTime FechaHasta, String Vista)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramFechaDesde = new SqlParameter("@pFechaDesde", FechaDesde);
                SqlParameter paramFechaHasta = new SqlParameter("@pFechaHasta", FechaHasta);
                SqlParameter paramVista = new SqlParameter("@pVista", Vista);


                return db.Database.SqlQuery<ReportesCampoValorValor>("EXEC [Reportes].[obtenerSolicitudesReservaVsReservaDirecta_Grafico] @fechaDesde=@pfechaDesde , @fechaHasta=@pfechaHasta  , @vista=@pVista", paramFechaDesde, paramFechaHasta, paramVista).ToList();



            }
        }


        public List<ReportesCampoValorDinamico> ObtenerReservasPorSolicitudNegocioGrafico(DateTime FechaDesde, DateTime FechaHasta, String Vista, Int32 Negocio)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramFechaDesde = new SqlParameter("@pFechaDesde", FechaDesde);
                SqlParameter paramFechaHasta = new SqlParameter("@pFechaHasta", FechaHasta);
                SqlParameter paramVista = new SqlParameter("@pVista", Vista);
                SqlParameter paramNegocio = new SqlParameter("@pNegocio", Negocio);

                return db.Database.SqlQuery<ReportesCampoValorDinamico>("EXEC [Reportes].[obtenerSolicitudesReservaVsReservaDirectaNegocio_Grafico] @fechaDesde=@pfechaDesde , @fechaHasta=@pfechaHasta  , @vista=@pVista, @idNegocio=@pNegocio", paramFechaDesde, paramFechaHasta, paramVista, paramNegocio).ToList();



            }
        }

        public List<ReportesCampoFechaValor> ObtenerReservasPorSolicitudNegocio(DateTime FechaDesde, DateTime FechaHasta, String Vista, Int32 Negocio)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramFechaDesde = new SqlParameter("@pFechaDesde", FechaDesde);
                SqlParameter paramFechaHasta = new SqlParameter("@pFechaHasta", FechaHasta);
                SqlParameter paramVista = new SqlParameter("@pVista", Vista);
                SqlParameter paramNegocio = new SqlParameter("@pNegocio", Negocio);

                return db.Database.SqlQuery<ReportesCampoFechaValor>("EXEC [Reportes].[obtenerSolicitudesReservaVsReservaDirectaNegocio] @fechaDesde=@pfechaDesde , @fechaHasta=@pfechaHasta  , @vista=@pVista, @idNegocio=@pNegocio", paramFechaDesde, paramFechaHasta, paramVista, paramNegocio).ToList();



            }
        }



        public List<ReportesCampoValorValor> ObtenerReservasPorSolicitudPorNegocio(DateTime FechaDesde, DateTime FechaHasta, String Vista, Int32 Negocio)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramFechaDesde = new SqlParameter("@pFechaDesde", FechaDesde);
                SqlParameter paramFechaHasta = new SqlParameter("@pFechaHasta", FechaHasta);
                SqlParameter paramVista = new SqlParameter("@pVista", Vista);
                SqlParameter paramNegocio = new SqlParameter("@pNegocio", Negocio);

                return db.Database.SqlQuery<ReportesCampoValorValor>("EXEC [Reportes].[obtenerSolicitudesReservaVsReservaDirectaPorNegocio] @fechaDesde=@pfechaDesde , @fechaHasta=@pfechaHasta  , @vista=@pVista, @idNegocio=@pNegocio", paramFechaDesde, paramFechaHasta, paramVista, paramNegocio).ToList();



            }
        }



        public List<ReportesCampoValorDinamico> ObtenerReservasPorSolicitudPorNegocioGrafico(DateTime FechaDesde, DateTime FechaHasta, String Vista, Int32 Negocio)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramFechaDesde = new SqlParameter("@pFechaDesde", FechaDesde);
                SqlParameter paramFechaHasta = new SqlParameter("@pFechaHasta", FechaHasta);
                SqlParameter paramVista = new SqlParameter("@pVista", Vista);
                SqlParameter paramNegocio = new SqlParameter("@pNegocio", Negocio);

                return db.Database.SqlQuery<ReportesCampoValorDinamico>("EXEC [Reportes].[obtenerSolicitudesReservaVsReservaDirectaPorNegocio] @fechaDesde=@pfechaDesde , @fechaHasta=@pfechaHasta  , @vista=@pVista, @idNegocio=@pNegocio", paramFechaDesde, paramFechaHasta, paramVista, paramNegocio).ToList();



            }
        }

        

        public List<ReportesCampoValor> ObtenerPromocionesNoUtilizadasPorNegocio(DateTime FechaDesde, DateTime FechaHasta, String Vista, Int32 Negocio)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramFechaDesde = new SqlParameter("@pFechaDesde", FechaDesde);
                SqlParameter paramFechaHasta = new SqlParameter("@pFechaHasta", FechaHasta);
                SqlParameter paramVista = new SqlParameter("@pVista", Vista);
                SqlParameter paramNegocio = new SqlParameter("@pNegocio", Negocio);

                return db.Database.SqlQuery<ReportesCampoValor>("EXEC [Reportes].[ObtenerPromocionesNoUtilizadasPorNegocio] @fechaDesde=@pfechaDesde , @fechaHasta=@pfechaHasta  , @vista=@pVista, @idNegocio=@pNegocio", paramFechaDesde, paramFechaHasta, paramVista, paramNegocio).ToList();
            }
        }

        public List<ReportesCampoValorDinamico> ObtenerPromocionesNoUtilizadasPorNegocioGrafico(DateTime FechaDesde, DateTime FechaHasta, String Vista, Int32 Negocio)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramFechaDesde = new SqlParameter("@pFechaDesde", FechaDesde);
                SqlParameter paramFechaHasta = new SqlParameter("@pFechaHasta", FechaHasta);
                SqlParameter paramVista = new SqlParameter("@pVista", Vista);
                SqlParameter paramNegocio = new SqlParameter("@pNegocio", Negocio);

                return db.Database.SqlQuery<ReportesCampoValorDinamico>("EXEC [Reportes].[ObtenerPromocionesNoUtilizadasPorNegocio_Grafico] @fechaDesde=@pfechaDesde , @fechaHasta=@pfechaHasta  , @vista=@pVista, @idNegocio=@pNegocio", paramFechaDesde, paramFechaHasta, paramVista, paramNegocio).ToList();



            }
        }

        

        public List<ReportesCampoValor> ObtenerPromocionesVencidasPorNegocio(DateTime FechaDesde, DateTime FechaHasta, String Vista, Int32 Negocio)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramFechaDesde = new SqlParameter("@pFechaDesde", FechaDesde);
                SqlParameter paramFechaHasta = new SqlParameter("@pFechaHasta", FechaHasta);
                SqlParameter paramVista = new SqlParameter("@pVista", Vista);
                SqlParameter paramNegocio = new SqlParameter("@pNegocio", Negocio);

                return db.Database.SqlQuery<ReportesCampoValor>("EXEC [Reportes].[ObtenerPromocionesVencidasPorNegocio] @fechaDesde=@pfechaDesde , @fechaHasta=@pfechaHasta  , @vista=@pVista, @idNegocio=@pNegocio", paramFechaDesde, paramFechaHasta, paramVista, paramNegocio).ToList();



            }
        }

        public List<ReportesCampoValor> ObtenerPromocionesNegocioPorProvincia(string FechaDesde, string FechaHasta, int idNegocio)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramFechaDesde = new SqlParameter("@pFechaDesde", FechaDesde);
                SqlParameter paramFechaHasta = new SqlParameter("@pFechaHasta", FechaHasta);
                SqlParameter paramIdNegocio = new SqlParameter("@pIdNegocio", idNegocio);

                return db.Database.SqlQuery<ReportesCampoValor>("EXEC [Reportes].[GetPromocionesNegocioPorProvincia] @idNegocio=@pIdNegocio, @fechaDesdeP=@pfechaDesde, @fechaHastaP=@pfechaHasta", paramIdNegocio, paramFechaDesde, paramFechaHasta).ToList();
            }
        }

        public List<ReportesCampoValor> ObtenerPromocionesPorProvincia(string FechaDesde, string FechaHasta)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramFechaDesde = new SqlParameter("@pFechaDesde", FechaDesde);
                SqlParameter paramFechaHasta = new SqlParameter("@pFechaHasta", FechaHasta);

                return db.Database.SqlQuery<ReportesCampoValor>("EXEC [Reportes].[GetPromocionesPorProvincia] @fechaDesdeP=@pfechaDesde, @fechaHastaP=@pfechaHasta", paramFechaDesde, paramFechaHasta).ToList();
            }
        }


        public List<ReportesCampoFechaValor> ObtenerReservasPorCategoria(DateTime FechaDesde, DateTime FechaHasta, String Vista)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramFechaDesde = new SqlParameter("@pFechaDesde", FechaDesde);
                SqlParameter paramFechaHasta = new SqlParameter("@pFechaHasta", FechaHasta);
                SqlParameter paramVista = new SqlParameter("@pVista", Vista);


                return db.Database.SqlQuery<ReportesCampoFechaValor>("EXEC [Reportes].[obtenerReservasPorCategoria] @fechaDesde=@pfechaDesde , @fechaHasta=@pfechaHasta  , @vista=@pVista", paramFechaDesde, paramFechaHasta, paramVista).ToList();



            }
        }
        public List<ReportesCampoFechaValor> ObtenerPromocionesPorComercio_old(DateTime FechaDesde, DateTime FechaHasta, String Vista)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramFechaDesde = new SqlParameter("@pFechaDesde", FechaDesde);
                SqlParameter paramFechaHasta = new SqlParameter("@pFechaHasta", FechaHasta);
                SqlParameter paramVista = new SqlParameter("@pVista", Vista);


                return db.Database.SqlQuery<ReportesCampoFechaValor>("EXEC [Reportes].[obtenerPromocionesPorComercio] @fechaDesde=@pfechaDesde , @fechaHasta=@pfechaHasta  , @vista=@pVista", paramFechaDesde, paramFechaHasta, paramVista).ToList();



            }
        }

 		public List<ReportesCampoValor> ObtenerPromocionesPorComercio(string FechaDesde, string FechaHasta, int idNegocio)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramFechaDesde = new SqlParameter("@pFechaDesde", FechaDesde);
                SqlParameter paramFechaHasta = new SqlParameter("@pFechaHasta", FechaHasta);
                SqlParameter paramIdNegocio = new SqlParameter("@pIdNegocio", idNegocio);


                return db.Database.SqlQuery<ReportesCampoValor>("EXEC [Reportes].[obtenerPromocionesPorComercio] @fechaDesdeP=@pfechaDesde , @fechaHastaP=@pfechaHasta  , @idNegocio=@pIdNegocio", paramFechaDesde, paramFechaHasta, paramIdNegocio).ToList();
            }
        }

        public List<ReportesCampoValor> ObtenerReservasPorProvinciaGeneral(string FechaDesde, string FechaHasta, int idTipoHospedaje)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramFechaDesde = new SqlParameter("@pFechaDesde", FechaDesde);
                SqlParameter paramFechaHasta = new SqlParameter("@pFechaHasta", FechaHasta);
                SqlParameter paramIdTipoHospedaje = new SqlParameter("@pIdTipoHospedaje", idTipoHospedaje);

                return db.Database.SqlQuery<ReportesCampoValor>("EXEC [Reportes].[ObtenerOrigenDeReservasPorFechaGeneral] @fechaDesdeP=@pfechaDesde , @fechaHastaP=@pfechaHasta  , @idTipoLugarHospedaje=@pIdTipoHospedaje", paramFechaDesde, paramFechaHasta, paramIdTipoHospedaje).ToList();
            }
        }
        public List<ReportesCampoValor> ObtenerPromocionesNoUtilizadasPorComercio(DateTime FechaDesde, DateTime FechaHasta, String Vista, Int32 Negocio)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramFechaDesde = new SqlParameter("@pFechaDesde", FechaDesde);
                SqlParameter paramFechaHasta = new SqlParameter("@pFechaHasta", FechaHasta);
                SqlParameter paramVista = new SqlParameter("@pVista", Vista);
                SqlParameter paramNegocio = new SqlParameter("@pNegocio", Negocio);

                return db.Database.SqlQuery<ReportesCampoValor>("EXEC [Reportes].[ObtenerPromocionesNoUtilizadasPorNegocio] @fechaDesde=@pfechaDesde , @fechaHasta=@pfechaHasta  , @vista=@pVista, @idNegocio=@pNegocio", paramFechaDesde, paramFechaHasta, paramVista, paramNegocio).ToList();



            }
        }

        public List <ReportesCampoValor>ObtenerRespuestasPorPreguntaNegocio(int idEncuesta, int idPregunta, int idNegocio, string fechaDesde, string fechaHasta)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramIdEncuesta = new SqlParameter("@pIdEncuesta", idEncuesta);
                SqlParameter paramIdPregunta = new SqlParameter("@pIdPregunta", idPregunta);
                SqlParameter paramIdNegocio = new SqlParameter("@pIdNegocio", idNegocio);
                SqlParameter paramFechaDesde = new SqlParameter("@pFechaDesde", fechaDesde);
                SqlParameter paramFechaHasta = new SqlParameter("@pFechaHasta", fechaHasta);

                return db.Database.SqlQuery<ReportesCampoValor>("EXEC [Reportes].[RespuestasEncuesta] @idEncuesta=@pIdEncuesta , @idPregunta=@pIdPregunta  , @idNegocio=@pIdNegocio, @fechaDesdeP=@pFechaDesde, @fechaHastaP=@pFechaHasta", paramIdEncuesta, paramIdPregunta, paramIdNegocio, paramFechaDesde, paramFechaHasta).ToList();
            }

        }

        public List<ReportesCampoValor> ObtenerRespuestasPorPregunta(int idEncuesta, int idPregunta, string fechaDesde, string fechaHasta)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramIdEncuesta = new SqlParameter("@pIdEncuesta", idEncuesta);
                SqlParameter paramIdPregunta = new SqlParameter("@pIdPregunta", idPregunta);
                SqlParameter paramFechaDesde = new SqlParameter("@pFechaDesde", fechaDesde);
                SqlParameter paramFechaHasta = new SqlParameter("@pFechaHasta", fechaHasta);

                return db.Database.SqlQuery<ReportesCampoValor>("EXEC [Reportes].[RespuestasEncuestaSecretaria] @idEncuesta=@pIdEncuesta , @idPregunta=@pIdPregunta, @fechaDesdeP=@pfechaDesde, @fechaHastaP=@pFechaHasta", paramIdEncuesta, paramIdPregunta, paramFechaDesde, paramFechaHasta).ToList();
            }
        }

        public List<ReportesCampoValor> ObtenerEncuestasPorEstado(string fechaDesde, string fechaHasta)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramFechaDesde = new SqlParameter("@pFechaDesde", fechaDesde);
                SqlParameter paramFechaHasta = new SqlParameter("@pFechaHasta", fechaHasta);

                return db.Database.SqlQuery<ReportesCampoValor>("EXEC GetEncuestasPorEstado @fechaDesdeP=@pfechaDesde, @fechaHastaP=@pFechaHasta", paramFechaDesde, paramFechaHasta).ToList();
            }
        }

 		public List<ReportesCampoValor> ObtenerPromocionesPorEstado(string fechaDesde, string fechaHasta)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramFechaDesde = new SqlParameter("@pFechaDesde", fechaDesde);
                SqlParameter paramFechaHasta = new SqlParameter("@pFechaHasta", fechaHasta);

                return db.Database.SqlQuery<ReportesCampoValor>("EXEC Reportes.GetPromocionesPorEstado @fechaDesdeP=@pfechaDesde, @fechaHastaP=@pFechaHasta", paramFechaDesde, paramFechaHasta).ToList();
            }
        }

        public List<ReportesCampoCantidadValor> ObtenerReservasPorTipoHospedaje(string fechaDesde, string fechaHasta)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                SqlParameter paramFechaDesde = new SqlParameter("@pFechaDesde", fechaDesde);
                SqlParameter paramFechaHasta = new SqlParameter("@pFechaHasta", fechaHasta);

                return db.Database.SqlQuery<ReportesCampoCantidadValor>("EXEC [Reportes].[ObtenerReservasPorTipoHospedajeConCantidad] @fechaDesdeP=@pfechaDesde, @fechaHastaP=@pFechaHasta", paramFechaDesde, paramFechaHasta).ToList();
            }
        }

    }
}
