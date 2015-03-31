namespace Entidades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tipos_documento")]
    public partial class Tipos_documento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tipos_documento()
        {
            Documentos_enviados = new List<Documentos_enviados>();
        }

        [Key]
        public int Id_tipo_documento { get; set; }

        [StringLength(150)]
        public string Tipo_documento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public  List<Documentos_enviados> Documentos_enviados { get; set; }
    }
}
