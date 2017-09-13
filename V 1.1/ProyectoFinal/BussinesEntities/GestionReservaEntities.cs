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
}
