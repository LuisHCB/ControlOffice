namespace Entidades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public partial class Tipo_solicitudes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tipo_solicitudes()
        {
            Solicitudes = new HashSet<Solicitudes>();
        }

        [Key]
        public int Id_tipo_solicitud { get; set; }

        [StringLength(100)]
        public string Tipo_solicitud { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Solicitudes> Solicitudes { get; set; }
    }
}
