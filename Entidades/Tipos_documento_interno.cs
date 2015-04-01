namespace Entidades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tipos_documento_interno")]
    public class Tipos_documento_interno
    {
       // [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
       /* public List<Documentos_internos> Documentos_internos { get; set; }*/

        [Key]
        public int Id_tipo_documento_interno { get; set; }

       // [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        /*public Tipos_documento_interno()
        {
            Documentos_internos  = new List<Documentos_internos>();
        }*/

        

        [StringLength(150)]
        public string Tipo_documento { get; set; }

        
    }
}
