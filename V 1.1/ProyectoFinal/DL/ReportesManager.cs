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


        public List<ReportesDataTableEntitiesFechaValor> getReporteCampoFecha(DateTime FechaDesde , DateTime FechaHasta , String Vista)
        {
            using (SitcomEntities db = new SitcomEntities())
            {
                            SqlParameter paramFechaDesde = new SqlParameter("@pFechaDesde", FechaDesde);
                            SqlParameter paramFechaHasta = new SqlParameter("@pFechaHasta", FechaHasta);
                            SqlParameter paramVista = new SqlParameter("@pVista", Vista);


                return db.Database.SqlQuery<ReportesDataTableEntitiesFechaValor>("EXEC [dbo].[ObtenerSolicitudesDeReservasNoConfirmadas] @fechaDesde=@pfechaDesde , @fechaHasta=@pfechaHasta  , @vista=@pVista", paramFechaDesde, paramFechaHasta,paramVista).ToList();

               

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


         
    }
}
