using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data.Sql;

namespace BussinesEntities
{
    public class PromocionesNegocioEntity
    {
        public int idPromocion { get; set; }

        public string NEGOCIO { get; set; }

        public string PROMOCION { get; set; }

        public DateTime FECHA_ALTA { get; set; }

        public DateTime VENCE { get; set; }

        public int DIAS_BENEFICIO { get; set; }

        public int CANT_DISP { get; set; }

        public string ESTADO { get; set; }
    }
}
