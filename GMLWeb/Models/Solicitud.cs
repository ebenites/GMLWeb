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
    
    public partial class Solicitud
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Solicitud()
        {
            this.OrdenServicio = new HashSet<OrdenServicio>();
        }
    
        public int codigo { get; set; }
        public string numero { get; set; }
        public string descripcion { get; set; }
        public string solicitante { get; set; }
        public int codigo_equipo { get; set; }
        public System.DateTime fecha { get; set; }
        public string estado { get; set; }
    
        public virtual Equipo Equipo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrdenServicio> OrdenServicio { get; set; }
    }
}
