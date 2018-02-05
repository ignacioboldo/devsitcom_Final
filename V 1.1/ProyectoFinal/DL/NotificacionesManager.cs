    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinesEntities;
using DAL;
using System.Data.SqlClient;


namespace BL
{
  public  class NotificacionesManager
    {

       
      public List<NotificacionesEntity> consultarNotificaciones(int? idUsuario, int? idPerfil)
      {
          List<NotificacionesEntity> lstNotif = new List<NotificacionesEntity>();


          using (SitcomEntities db = new SitcomEntities())
          {

              SqlParameter paramIdUsuario = new SqlParameter("@pidUsuario", idUsuario);
              SqlParameter paramIdPerfil = new SqlParameter("@pidPerfil", idPerfil);



              return db.Database.SqlQuery<NotificacionesEntity>("ConsultarNotificacionesPorUsuario @idUsuario=@pidUsuario, @idPerfil= @pidPerfil ", paramIdUsuario, paramIdPerfil).ToList();


          }


 

       
    }
}
}

