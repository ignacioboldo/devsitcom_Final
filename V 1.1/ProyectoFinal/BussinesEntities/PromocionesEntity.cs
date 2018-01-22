using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.ComponentModel.DataAnnotations;

namespace BussinesEntities
{   
        public class PromocionesEntity
        {
            public PromocionesEntity()
            {
                this.PromocionesOtorgadas = new HashSet<PromocionesOtorgadas>();
            }
            public int idPromocion { get; set; }
            public Nullable<System.DateTime> fechaAlta { get; set; }

            [Required(ErrorMessage = "Se debe completar la fecha de vencimiento.")]
            public Nullable<System.DateTime> fechaVencimiento { get; set; }

            [Required(ErrorMessage = "Se debe completar el titulo.")]
            public string titulo { get; set; }

            [Required(ErrorMessage = "Se debe completar la descripcion.")]
            public string descripcion { get; set; }

            [Required(ErrorMessage = "Se deben los completar los dias del beneficio. Dias durante los cuales la promocion sera valida para el cliente.")]
            [Range(2, 90, ErrorMessage = "Los dias del beneficio no pueden ser menores a 2 ni mayores a 90")]
            public Nullable<int> diasBeneficio { get; set; }

            [Required(ErrorMessage = "Se debe la oferta máxima de la promoción.")]
            [Range(2, 100, ErrorMessage = "El stock de promociones no puede ser menor a 2 ni mayor a 100")]
            public Nullable<int> ofertaMaxima { get; set; }
            public Nullable<int> idNegocio { get; set; }
            public virtual Negocio Negocio { get; set; }
            public virtual ICollection<PromocionesOtorgadas> PromocionesOtorgadas { get; set; }
        }
}
