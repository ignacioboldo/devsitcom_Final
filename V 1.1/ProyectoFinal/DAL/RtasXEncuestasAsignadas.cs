//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class RtasXEncuestasAsignadas
    {
        public int idEncuestaAsignada { get; set; }
        public int idPregunta { get; set; }
        public int respuesta { get; set; }
    
        public virtual Preguntas Preguntas { get; set; }
    }
}
