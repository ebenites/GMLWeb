//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GMLWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PlanMantenimientoDetalle
    {
        public int codigo { get; set; }
        public int codigo_planmantenimiento { get; set; }
        public int codigo_cronogramadetalle { get; set; }
        public int codigo_disponibilidad { get; set; }
    
        public virtual CronogramaDetalle CronogramaDetalle { get; set; }
        public virtual Disponibilidad Disponibilidad { get; set; }
        public virtual PlanMantenimiento PlanMantenimiento { get; set; }
    }
}