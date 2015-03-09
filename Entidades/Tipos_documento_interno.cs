namespace Entidades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public partial class Tipos_documento_interno
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tipos_documento_interno()
        {
            Documentos_internos = new HashSet<Documentos_internos>();
        }

        [Key]
        public int Id_tipo_documento_interno { get; set; }

        [StringLength(150)]
        public string Tipo_documento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Documentos_internos> Documentos_internos { get; set; }
    }
}
