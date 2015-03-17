namespace Entidades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tipo_mobiliario")]
    public partial class Tipo_mobiliario
    {
        /*[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tipo_mobiliario()
        {
            Mobiliarios = new HashSet<Mobiliarios>();
        }*/

        [Key]
        public int Id_tipo_mobiliario { get; set; }

        [StringLength(100)]
        public string Nombre { get; set; }

     /*  [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Mobiliarios> Mobiliarios { get; set; }*/
    }
}
