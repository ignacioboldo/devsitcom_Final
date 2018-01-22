using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BussinesEntities
{
    public class PromocionesOtorgadasEntity
    {
        public int idPromocion { get; set; }
        public Nullable<int> idUsuario { get; set; }
        public Nullable<System.DateTime> fechaOtorgamiento { get; set; }
        public Nullable<System.DateTime> fechaVencimiento { get; set; }
        public string codigo { get; set; }
        public Nullable<int> utilizada { get; set; }

        public virtual Promociones Promociones { get; set; }
        public virtual Usuarios Usuarios { get; set; }
    }
}
