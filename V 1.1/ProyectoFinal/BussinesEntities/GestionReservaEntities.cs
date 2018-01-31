using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesEntities
{
    public class GestionReservaEntities
    {
        public string HabitacionText { get; set; }
        public int? IdHabitacion { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
    }

    public class ClientesAgregadosEntities
    {
        public string nombre { get; set; }
        public int idPersona { get; set; }
        public int idNegocio { get; set; }
        public int idHabitacion { get; set; }
        public int nroReserva { get; set; }
    }
}
