﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BussinesEntities
{
    using System;
    using System.Collections.Generic;
    using DAL;

    public partial class TiposRespuestaEntity
    {
        public TiposRespuestaEntity()
        {
            this.Preguntas = new HashSet<Preguntas>();
            this.RespuestasPosibles = new HashSet<RespuestasPosibles>();
        }

        public int idTipoRespuesta { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }

        public virtual ICollection<Preguntas> Preguntas { get; set; }
        public virtual ICollection<RespuestasPosibles> RespuestasPosibles { get; set; }
        public virtual RespuestasPosibles[] RespuestasPosiblesArray { get; set; }

    }
}
