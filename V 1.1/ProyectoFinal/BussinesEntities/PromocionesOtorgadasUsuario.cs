using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesEntities
{
    public class PromocionesOtorgadasUsuario
    {

        public int? ID { get; set; }

        public string NEGOCIO { get; set; }

        public string PROMOCION { get; set; }

        public DateTime OBTENIDA { get; set; }

        public DateTime VENCE { get; set; }

        public string CODIGO { get; set; }

        public string ESTADO { get; set; }

        public int USUARIO { get; set; }

    }
}
