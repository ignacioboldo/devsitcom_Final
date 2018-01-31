using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BussinesEntities
{
    public class SolicitudEntity
    {
        public int idSolicitud { get; set; }
        public Nullable<int> idUsuarioSolicitante { get; set; }
        public Nullable<System.DateTime> fechaDesde { get; set; }
        public Nullable<System.DateTime> fechaHasta { get; set; }
        public Nullable<int> cantidadPersonas { get; set; }
        public Nullable<int> cantidadLugares { get; set; }
        public string observacion { get; set; }
        public Nullable<int> idNegocio { get; set; }

        public virtual Negocio Negocio { get; set; }
        public virtual Usuarios Usuarios { get; set; }
        public Nullable<bool> Expirar { get; set; }
        public virtual Nullable<System.DateTime> FechaExpiracion { get; set; }
        public virtual Nullable<System.DateTime> FechaCreacion { get; set;  }
        //public Nullable<int> mensajesSinLeer { get; set; }




    }
}
