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
    
    public partial class Reserva
    {
        public int idReserva { get; set; }
        public Nullable<int> idPersonaSolicitante { get; set; }
        public Nullable<int> idNegocio { get; set; }
        public Nullable<int> idSolicitud { get; set; }
        public Nullable<int> idEstado { get; set; }
    }
}