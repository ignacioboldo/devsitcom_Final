using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesEntities
{
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

    }

}
