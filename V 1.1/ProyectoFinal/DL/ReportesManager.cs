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





    //    public PromocionesEntity getPromocionById(int id)
    //    {
    //        using(SitcomEntities db = new SitcomEntities())
    //        {
    //            var result = (from p in db.Promociones
    //                         where p.idPromocion == id
    //                         select new PromocionesEntity()
    //                         {
    //                             idPromocion = p.idPromocion,
    //                             titulo = p.titulo,
    //                             descripcion = p.descripcion,
    //                             diasBeneficio = p.diasBeneficio,
    //                             Negocio = (from n in db.Negocio
    //                                        where n.idNegocio==p.idNegocio
    //                                            select n).FirstOrDefault()
    //                         }).FirstOrDefault();

    //            return (PromocionesEntity)result;
    //        }
    //    }
    //    public int addPromocion(PromocionesEntity pro)
    //    {

    //        Promociones p = convert.PromocionesEntityToPromocion(pro);

    //        using(SitcomEntities db = new SitcomEntities())
    //        {

    //            SqlParameter paramTitulo = new SqlParameter("@pTitulo", p.titulo);
    //            SqlParameter paramDesc = new SqlParameter("@pDescripcion", p.descripcion);
    //            SqlParameter paramFechaVenc = new SqlParameter("@pFechaVencimiento", p.fechaVencimiento);
    //            SqlParameter paramDiasBen = new SqlParameter("@pDiasBeneficio", p.diasBeneficio);
    //            SqlParameter paramOfertaMaxima = new SqlParameter("@pOfertaMaxima", p.ofertaMaxima);
    //            SqlParameter paramIdNegocio = new SqlParameter("@pIdNegocio", p.idNegocio);

    //            int result = db.Database.SqlQuery<Int32>("altaPromocion @idNegocio=@pIdNegocio, @titulo=@pTitulo , @fechaVencimiento=@pFechaVencimiento, @diasBeneficio=@pDiasBeneficio, @descripcion=@pDescripcion, @ofertaMaxima=@pOfertaMaxima", paramIdNegocio, paramTitulo, paramFechaVenc, paramDiasBen, paramDesc, paramOfertaMaxima).FirstOrDefault();

    //            return result;

    //        }

    //    }
    //    public List<PromocionesNegocioEntity> getPromociones(int? idNegocio, int esTurista)
    //    {
    //       using (SitcomEntities db = new SitcomEntities())
    //        {

    //            SqlParameter paramIdNegocio = new SqlParameter("@pIdNegocio", idNegocio);
    //            SqlParameter paramEsTurista = new SqlParameter("@pEsTurista", esTurista);

    //            return db.Database.SqlQuery<PromocionesNegocioEntity>("exec getPromociones @idNegocio=@pIdNegocio, @esTurista=@pEsTurista", paramIdNegocio, paramEsTurista).ToList();

    //           // return db.Database.SqlQuery<Promociones>("exec getPromociones2 @idNegocio=@pIdNegocio", paramIdNegocio).FirstOrDefault();
                           
    //        }
    //    }
    //    public int otorgarPromocion(int idUsuario, int idPromocion)
    //    {
    //        using(SitcomEntities db = new SitcomEntities())
    //        {

    //            SqlParameter paramIdUsuario = new SqlParameter("@pIdUsuario", idUsuario);
    //            SqlParameter paramIdPromocion = new SqlParameter("@pIdPromocion", idPromocion);

    //            int result = db.Database.SqlQuery<Int32>("otorgarPromocion @idUsuario=@pIdUsuario, @idPromocion=@pIdPromocion", paramIdUsuario, paramIdPromocion).FirstOrDefault();

    //            return result;
    //        }
    //    }

    //    public int finalizarPromocion(int idPromocion)
    //    {
    //        using (SitcomEntities db = new SitcomEntities())
    //        {
    //            SqlParameter paramIdPromocion = new SqlParameter("@pIdPromocion", idPromocion);

    //            int result = db.Database.SqlQuery<Int32>("cancelPromociones @idPromocion=@pIdPromocion", paramIdPromocion).FirstOrDefault();

    //            return result;
    //        }
    //    }
    //    public PromocionesOtorgadasUsuario getUltimaPromocionOtorgada(int idUsuario)
    //    {
    //        using (SitcomEntities db = new SitcomEntities())
    //        {
    //            SqlParameter paramIdUsuario = new SqlParameter("@pIdUsuario",idUsuario);
    //            PromocionesOtorgadasUsuario pro = db.Database.SqlQuery<PromocionesOtorgadasUsuario>("getUltimaPromocionOtorgada @idUsuario=@pIdUsuario", paramIdUsuario).FirstOrDefault();

    //            return (PromocionesOtorgadasUsuario)pro;
    //        }
    //    }
    //    public List<PromocionesOtorgadasUsuario> getPromocionesOtorgadas(int idUsuario)
    //    {
    //        using (SitcomEntities db = new SitcomEntities())
    //        {
    //            SqlParameter paramIdUsuario = new SqlParameter("@pIdUsuario", idUsuario);
    //            List<PromocionesOtorgadasUsuario> pro = db.Database.SqlQuery<PromocionesOtorgadasUsuario>("getPromocionesOtorgadas @idUsuario=@pIdUsuario", paramIdUsuario).ToList();

    //            return pro;
    //        }
    //    }
    //    public PromocionesOtorgadasUsuario getPromocionOtorgada(string codigo)
    //    {
    //        using (SitcomEntities db = new SitcomEntities())
    //        {
    //            SqlParameter paramCodigo = new SqlParameter("@pCodigo", codigo);
    //            PromocionesOtorgadasUsuario pro = db.Database.SqlQuery<PromocionesOtorgadasUsuario>("getPromocionOtorgada @codigo=@pCodigo", paramCodigo).FirstOrDefault();

    //            return pro;
    //        }
    //   }
    //    public int regUsoPromocion(string codigo)
    //    {
    //        using (SitcomEntities db = new SitcomEntities())
    //        {

    //            SqlParameter paramCodigo = new SqlParameter("@pCodigo", codigo);
                
    //            int result = db.Database.SqlQuery<Int32>("regUsoPromocion @codigo=@pCodigo", paramCodigo).FirstOrDefault();

    //            return result;
    //        }
    //    }
    }
}
