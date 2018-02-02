using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesEntities
{

    public class ReportesDataTableEntities
    {

        public string descripcion { get; set; }
        public DateTime? fecha { get; set; }

    }

    public class ReportesGraficoPieEntities
    {

        public string label { get; set; }
        public int valor { get; set; }


    }

    public class ReportesGraficoBarEntities
    {

        public string label { get; set; }
        public int valor { get; set; }

    }

    public class ReservasEntities
    {
        public int? idNegocio { get; set; }
        public string NombreNegocio { get; set; }
        public string TipoLugarHospedaje { get; set; }
        public int? idCategoria { get; set; }
        public string descripcion { get; set; }
        public byte[] FotoPrincipal { get; set; }
        public int? idFoto { get; set; }

    }

    public class HabitacionesDisponiblesEntities
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }

    }

    public class ReservasUsuarioEntity
    {
        public List<ReservasUsuarioEntities> reservas { get; set; }

        public List<SolicitudesUsuarioEntities> solicitudes { get; set; }
    }


    public class ReservasUsuarioEntities
    {
        public int idPersona { get; set; }
        public int idReserva { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime fechaHasta { get; set; }
        public string EstadoReserva { get; set; }
        public string TipoHabitacion { get; set; }
        public string Negocio { get; set; }
        public int idSolicitud { get; set; }
    }

    public class SolicitudesUsuarioEntities
    {
        public int idSolicitud { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime fechaHasta { get; set; }
        public int cantidadPersonas { get; set; }
        public int cantidadLugares { get; set; }  
        public string observacion { get; set; }
        public Boolean Expirar { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string Negocio { get; set; }
        public int idPersona { get; set; }
        public String Mensaje { get; set; }
        public int MensajesSinLeer { get; set; }
    }

    public class ListadoHabitacionesEnCkeckInEntities
    {
        public string Nombre { get; set; }
        public int idReserva { get; set; }
        public int idDisponibilidad { get; set; }
    }

    public class SolicitudesNegociosEntities
    {
        public int idSolicitud { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime fechaHasta { get; set; }
        public int cantidadPersonas { get; set; }
        public int cantidadLugares { get; set; }
        public string observacion { get; set; }
        public Boolean Expirar { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string NombreApellido { get; set; }
        public int documento { get; set; }
        public String Mensaje { get; set; }
        public int idNegocio { get; set; }
        public int MensajesSinLeer { get; set; }
    }


    public class ReservasComentariosEntities
    {
        public string comentario { get; set; }
        public string rutaAdjunto { get; set; }
        public int idSolicitud { get; set; }
        public DateTime fechaEnvioComentario { get; set; }
        public Boolean? comentarioCliente { get; set; }
    }

    public class InsetReservaEntities
    {
        public int? Id { get; set; }
    }

    public class PlanoReservaEntities
    {
        public int? NroHabitacion { get; set; }
    }

    public class CalendarioEntities
    {
        public DateTime Fecha { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Estado { get; set; }
        public int? idReserva { get; set; }
        public List<CalendarioHabitacionesEntities> nombresHabitacion { get; set; }
       

    }

    public class CalendarioHabitacionesEntities
    {
      
        public string Nombre { get; set; }
        public int Id { get; set; }
        public string tipo { get; set; }
         

    }

    public class ListadoReservasPlanoEntities
    {
        public DateTime FechaDesde { get; set; }
        public DateTime fechaHasta { get; set; }
        public int idReserva { get; set; }
        public string apellido { get; set; }
        public string nombre { get; set; }
        public string Telefono { get; set; }
        public int mensajesSinLeer { get; set; }
        public int idEstado { get; set; }
        public string nombreEstado { get; set; }
        public int idSolicitud { get; set; }
        
    }

    

        }

