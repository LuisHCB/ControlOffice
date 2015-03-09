namespace Entidades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
 

    public partial class Marca_mobiliario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Marca_mobiliario()
        {
            Mobiliarios = new HashSet<Mobiliarios>();
        }

        [Key]
        public int Id_marca { get; set; }

        [StringLength(255)]
        public string Marca { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Mobiliarios> Mobiliarios { get; set; }
    }
}
