using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL;

namespace BussinesEntities
{   

    public partial class PreguntasEntity
    {
        public PreguntasEntity()
        {
            this.RtasXEncuestasAsignadas = new HashSet<RtasXEncuestasAsignadas>();
        }

        public int idPregunta { get; set; }

        [Required(ErrorMessage = "Se debe completar el texto de la pregunta.")]
        [StringLength(500, MinimumLength = 20, ErrorMessage="¡El texto de la pregunta debe tener un mínimo de 20 caractéres!")]
        public string textoPregunta { get; set; }
        public Nullable<int> idEncuesta { get; set; }
        public Nullable<int> idClasifPregunta { get; set; }
        public Nullable<int> idTipoRespuesta { get; set; }
        public Nullable<int> respuesta { get; set; }
        public virtual ICollection<RtasXEncuestasAsignadas> RtasXEncuestasAsignadas { get; set; }
        public virtual TiposRespuestaEntity TiposRespuesta { get; set; }
        public virtual ClasifPregunta ClasifPregunta { get; set; }

        public Encuestas Encuesta { get; set; }

        public List<TiposRespuesta> TiposRespuestaList { get; set; }
        public List<ClasifPregunta> ClasifPreguntaList { get; set; }
    }
}
