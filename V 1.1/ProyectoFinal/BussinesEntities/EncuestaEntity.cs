﻿﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using DAL;
using System.ComponentModel.DataAnnotations;

namespace BussinesEntities
{
    public partial class EncuestaEntity
    {
        public EncuestaEntity()
        {
            this.EncuestasAsignadas = new HashSet<EncuestasAsignadas>();
        }

        public int idEncuesta { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public Nullable<int> idTipoEncuesta { get; set; }
        public Nullable<bool> esActiva { get; set; }
        public bool setearComoActiva { get; set; }

        public virtual TiposEncuesta TiposEncuesta { get; set; }
        public virtual ICollection<EncuestasAsignadas> EncuestasAsignadas { get; set; }
        public virtual Preguntas Preguntas { get; set; }
        public virtual TiposEncuesta TiposEncuesta1 { get; set; }
        public virtual PreguntasEntity[] Preguntas1 { get; set; }
    }
}