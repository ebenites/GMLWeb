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
    
    public partial class PlanMantenimiento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PlanMantenimiento()
        {
            this.Disponibilidad = new HashSet<Disponibilidad>();
        }
    
        public int codigo { get; set; }
        public int anio { get; set; }
        public int codigo_local { get; set; }
    
        public virtual Local Local { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Disponibilidad> Disponibilidad { get; set; }
    }
}
