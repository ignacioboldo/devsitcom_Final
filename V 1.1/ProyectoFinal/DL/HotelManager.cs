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
  public  class HotelManager
    {


      public Hotel buscarHotelById(int idHot)
      {
          using (var db = new SitcomEntities())
          {
              var result = (from u in db.Hotel
                            where u.idHotel == idHot
                            select u).FirstOrDefault();
                            

              return result;
          }
      }


      public List<Habitacion> getHabitacionesByHotel(int? idHot)
  {
      using (var db = new SitcomEntities())
      {
          var result = (from u in db.Habitacion
                        where u.idHotel == idHot
                        select u).ToList<Habitacion>();


          return result;

      }
  
  
  
  }


      public void registrarDisponibilidad(DisponibilidadEntity dispo)
      {
          using (SitcomEntities db = new SitcomEntities())
          {
              var dis = new Disponibilidad()
              {
                  fechaDesde= dispo.fechaDesde,
                  fechaHasta=dispo.fechaHasta,
                  idHabitacion=dispo.idHabitacion,
                  habilitado=dispo.habilitado,
                  idEstado=dispo.idEstado
                  
              };

              db.Disponibilidad.Add(dis);
              db.SaveChanges();
          }
      }

      public List<DisponibilidadEntity> consultarDisponibilidad(int? idHot, int? cantHab, int? cantPers, int? anio, int? mes)
      {
          List<DisponibilidadEntity> lstDispo = new List<DisponibilidadEntity>();


          using (SitcomEntities db = new SitcomEntities())
          {

              SqlParameter paramComplejo = new SqlParameter("@pHotel", idHot);
              SqlParameter paramCantHab = new SqlParameter("@pCantHab", cantHab);
              SqlParameter paramCantPers = new SqlParameter("@pCantPers", cantPers);
              SqlParameter paramAnio = new SqlParameter("@pAnio", anio);
              SqlParameter paramMes = new SqlParameter("@pMes", mes);



              return db.Database.SqlQuery<DisponibilidadEntity>("getDisponibilidadHotel @idHotel=@pHotel, @cantidadHabitacionesSolicitadas= @pCantHab , @cantidadPersonasSolicitadas=@pCantPers , @anio=@pAnio , @mes= @pMes ", paramComplejo, paramCantHab, paramCantPers, paramAnio, paramMes).ToList();


          }



      }


      public string consultarDisponibilidadPorNegocio(DateTime fechaDesde, DateTime fechaHasta, int cantPersonas, int cantHabitaciones, int idNegocio)
      {


          using (SitcomEntities db = new SitcomEntities())
          {

              return db.Database.SqlQuery<string>("ConsultarDisponibilidadPorNegocio @fechaDesde='" + fechaDesde + "',@fechaHasta='" + fechaHasta + "',@cantPersonas=" + cantPersonas + " ,@cantHabitaciones=" + cantHabitaciones + " ,@idNegocio= " + idNegocio).First();

          }


      }


      public List<ReservasEntities> disponiblidad(DateTime? fechaDesde, DateTime? fechaHasta, int? cantPersonas, int? cantHabitaciones, string tipoHospedaje)
      {

          List<ReservasEntities> lstDispo = new List<ReservasEntities>();


          using (SitcomEntities db = new SitcomEntities())
          {
    
              SqlParameter paramfechaHasta = new SqlParameter("@pfechaHasta", fechaHasta);
              SqlParameter paramfechaDesde = new SqlParameter("@pfechaDesde", fechaDesde);
              SqlParameter paramcantPersonas = new SqlParameter("@pcantPersonas", cantPersonas);
              SqlParameter paramcantHabitaciones = new SqlParameter("@pcantHabitaciones", cantHabitaciones);
              SqlParameter paramtipoHospedaje = new SqlParameter("@ptipoHospedaje", tipoHospedaje);

              if (fechaDesde == null)
                  paramfechaDesde = new SqlParameter("@pfechaDesde", DBNull.Value);

              if (fechaHasta == null)
                  paramfechaHasta = new SqlParameter("@pfechaHasta", DBNull.Value);

              if (cantPersonas == null)
                  paramcantPersonas = new SqlParameter("@pcantPersonas", DBNull.Value);

              if (cantHabitaciones == null)
                  paramcantHabitaciones = new SqlParameter("@pcantHabitaciones", DBNull.Value);

              if (tipoHospedaje == null)
                  paramtipoHospedaje = new SqlParameter("@ptipoHospedaje", DBNull.Value);
  

              return db.Database.SqlQuery<ReservasEntities>("ConsultarLugaresHospedaje @fechaDesde=@pfechaDesde, @fechaHasta= @pfechaHasta , @cantPersonas=@pcantPersonas , @cantHabitaciones=@pcantHabitaciones , @tipoHospedaje= @ptipoHospedaje ", paramfechaDesde, paramfechaHasta,paramcantPersonas,paramcantHabitaciones, paramtipoHospedaje).ToList();



          }

      }

      public List<ClientesEntity> buscarClientes(string buscar, int idNegocio)
      {

        
          using (SitcomEntities db = new SitcomEntities())
          {

              return db.Database.SqlQuery<ClientesEntity>("Persona_s_By_IdNegocio @idNegocio=" + idNegocio + " , @buscar= '" + buscar + "'").ToList();

          }

         
      }

      public List<CalendarioEntities> calendario(DateTime fecha_desde, DateTime fecha_hasta,int? idNegocio)
      {


          using (SitcomEntities db = new SitcomEntities())
          {

              return db.Database.SqlQuery<CalendarioEntities>("ConsultarHabitacionesDisponiblesPorFechaYNegocio_Planning @fechaDesde='" + fecha_desde + "',@fechaHasta='" + fecha_hasta + "',@idNegocio=" + idNegocio).ToList();

          }


      }

      public List<CalendarioHabitacionesEntities> calendarioNombresHabitacion(DateTime fecha_desde, DateTime fecha_hasta, int? idNegocio)
      {


          using (SitcomEntities db = new SitcomEntities())
          {

              return db.Database.SqlQuery<CalendarioHabitacionesEntities>("ConsultarHabitacionesDisponiblesPorFechaYNegocio @fechaDesde='" + fecha_desde + "',@fechaHasta='" + fecha_hasta + "',@idNegocio=" + idNegocio).ToList();

          }


      }




      public List<CalendarioEntities> planoReserva(DateTime fecha_desde, DateTime fecha_hasta, int? idNegocio)
      {


          using (SitcomEntities db = new SitcomEntities())
          {

              return db.Database.SqlQuery<CalendarioEntities>("ConsultarHabitacionesDisponiblesPorFechaYNegocio_Planning @fechaDesde='" + fecha_desde + "',@fechaHasta='" + fecha_hasta + "',@idNegocio=" + idNegocio).ToList();

          }


      }

      public List<ListadoReservasPlanoEntities> listadoReservasPlaning(DateTime fecha_desde, DateTime fecha_hasta, int? idNegocio, bool tipo_comentario)
      {


          using (SitcomEntities db = new SitcomEntities())
          {

              return db.Database.SqlQuery<ListadoReservasPlanoEntities>("ConsultarListadReservasPorFechaYNegocio_Planning @tipoComentario = 0, @fechaDesde='" + fecha_desde + "',@fechaHasta='" + fecha_hasta + "',@idNegocio=" + idNegocio).ToList();

          }


      }


      public List<ListadoDisponibilidadPlanoEntities> listadoDisponibilidadPlaning(DateTime fecha_desde, DateTime fecha_hasta, int? idNegocio, bool tipo_comentario)
      {


          using (SitcomEntities db = new SitcomEntities())
          {

              return db.Database.SqlQuery<ListadoDisponibilidadPlanoEntities>("ConsultarListadDisponibilidadPorFechaYNegocio_Planning @tipoComentario = 0, @fechaDesde='" + fecha_desde + "',@fechaHasta='" + fecha_hasta + "',@idNegocio=" + idNegocio).ToList();

          }


      }



      public List<HabitacionesDisponiblesEntities> buscarDisponibilidadHabitaciones(DateTime fecha_desde, DateTime fecha_hasta, int idNegocio)
      {

          using (SitcomEntities db = new SitcomEntities())
          {

              return db.Database.SqlQuery<HabitacionesDisponiblesEntities>("ConsultarHabitacionesDisponiblesPorFechaYNegocio_FiltroReserva @fechaDesde='" + fecha_desde + "',@fechaHasta= '" + fecha_hasta + "',@idNegocio=" + idNegocio + "").ToList();

          }


      }

      public decimal comentariosSolicitud_i(string comentario, string rutaAdjunto, int idSolicitud, int idReserva , bool comentarioCliente)
      {

          using (SitcomEntities db = new SitcomEntities())
          {

              return db.Database.SqlQuery<decimal>("ComentariosSolicitud_i @comentarioCliente='" + comentarioCliente + "', @comentario='" + comentario + "',@rutaAdjunto= '" + rutaAdjunto + "',@idSolicitud=" + idSolicitud + " ,@idReserva=" + idReserva + "").First();

          }



      }

      public List<ReservasUsuarioEntities> consultarListadoReservasPorPersona(int idPersona)
      {

          using (SitcomEntities db = new SitcomEntities())
          {

              return db.Database.SqlQuery<ReservasUsuarioEntities>("ConsultarListadoReservasPorPersona @idPersona='" + idPersona + "'").ToList();

          }


      }


      public List<SolicitudesUsuarioEntities> consultarListadoSolicitudesPorPersona(int idPersona)
      {

          using (SitcomEntities db = new SitcomEntities())
          {

              return db.Database.SqlQuery<SolicitudesUsuarioEntities>("ConsultarListadoSolicitudesPorPersona @idPersona='" + idPersona + "'").ToList();

          }


      }

    
public List<SolicitudesNegociosEntities> consultarListadoSolicitudesPorNegocio(int idNegocio)
      {

          using (SitcomEntities db = new SitcomEntities())
          {

              return db.Database.SqlQuery<SolicitudesNegociosEntities>("ConsultarListadoSolicitudesPorNegocio @idNegocio='" + idNegocio + "'").ToList();

          }


      }













  public List<ListadoHabitacionesEnCkeckInEntities> listarHabitacionesPorNroReserva_CheckIn(int idReserva)
      {

          using (SitcomEntities db = new SitcomEntities())
          {

              return db.Database.SqlQuery<ListadoHabitacionesEnCkeckInEntities>("ListarHabitacionesPorNroReserva_CheckIn @idReserva='" + idReserva + "'").ToList();

          }


      }


      public List<ReservasComentariosEntities> consultarComentariosSolicitud(int idSolicitud, int idReserva)
      {

          using (SitcomEntities db = new SitcomEntities())
          {

              return db.Database.SqlQuery<ReservasComentariosEntities>("ConsultarComentariosSolicitud @idSolicitud='" + idSolicitud + "' , @idReserva='" + idReserva +"' ").ToList();

          }


      }

      public decimal reserva_i(int? idPersonaSolicitante, int? idNegocio, int? idSolicitud)
      {


          using (SitcomEntities db = new SitcomEntities())
          {

              return db.Database.SqlQuery<decimal>("Reserva_i @idPersonaSolicitante='" + idPersonaSolicitante + "',@idNegocio= '" + idNegocio + "',@idSolicitud=" + idSolicitud + "").First();
      
          }


      }

      public decimal detalleDisponibilidad_i(int idDisponibilidad, int idPersona)
      {


          using (SitcomEntities db = new SitcomEntities())
          {

              return db.Database.SqlQuery<decimal>("DetalleDisponibilidad_i @idDisponibilidad='" + idDisponibilidad + "',@idPersona= '" + idPersona + "'").First();

          }


      }


      public decimal disponibilidad_i(DateTime fechaDesde, DateTime fechaHasta, int? idHabitacion, int? idCasaODpto, int? idReserva)
      {
      


          using (SitcomEntities db = new SitcomEntities())
          {

              if (idHabitacion != null) {
                  return db.Database.SqlQuery<decimal>("Disponibilidad_i @fechaDesde='" + fechaDesde + "',@fechaHasta= '" + fechaHasta + "',@idHabitacion='" + idHabitacion + "',@idCasaODpto=NULL,@idReserva=" + idReserva + "").First();

              }
              else
              {

                  return db.Database.SqlQuery<decimal>("Disponibilidad_i @fechaDesde='" + fechaDesde + "',@fechaHasta= '" + fechaHasta + "',@idHabitacion=NULL,@idCasaODpto='" + idCasaODpto + "',@idReserva=" + idReserva + "").First();

              }
    
          
          }


      }

    

      //public void reserva_Anular(int idReserva)
      //{


      //    using (SitcomEntities db = new SitcomEntities())
      //    {

      //       db.Database.ExecuteSqlCommand("Reserva_Anular @idReserva=" + idReserva + "");

      //    }


      //}


      public void actualizar_Comentarios_Leidos(int? idReserva, int? idSolicitud, bool cliente)
      {


          using (SitcomEntities db = new SitcomEntities())
          {

              db.Database.ExecuteSqlCommand("Actualizar_Comentarios_Leidos @cliente=" + cliente + ", @idSolicitud=" + idSolicitud + ", @idReserva=" + idReserva + "");

          }


      }


      public void reserva_CheckOut(int idReserva)
      {


          using (SitcomEntities db = new SitcomEntities())
          {

              db.Database.ExecuteSqlCommand("Reserva_CheckOut @idReserva=" + idReserva + "");
          }


      }

      public void reserva_Anular(int idReserva)
      {


          using (SitcomEntities db = new SitcomEntities())
          {

              db.Database.ExecuteSqlCommand("Reserva_Anular @idReserva=" + idReserva + "");
          }


      }


      public void disponibilidad_Anular(int idReserva)
      {


          using (SitcomEntities db = new SitcomEntities())
          {

              db.Database.ExecuteSqlCommand("Disponibilidad_d @idDisponibilidad=" + idReserva + "");
          }


      }

     

      public void reserva_CheckIn(int idReserva)
      {


          using (SitcomEntities db = new SitcomEntities())
          {
              db.Database.ExecuteSqlCommand("Reserva_CheckIn @idReserva=" + idReserva + "");
    
          }


      }

      public List<PlanoReservaEntities> planoReserva()
      {

          List<PlanoReservaEntities> lstClie = new List<PlanoReservaEntities>();

          PlanoReservaEntities p = new PlanoReservaEntities();

          p.NroHabitacion = 1;

          PlanoReservaEntities p2 = new PlanoReservaEntities();

          p2.NroHabitacion = 2;


          lstClie.Add(p);
          lstClie.Add(p2);

          return lstClie;

      }


       
    }
}
