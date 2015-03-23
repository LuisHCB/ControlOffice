namespace Entidades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Marca_electronicos")]
    public partial class Marca_electronicos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Marca_electronicos()
        {
            Electronicos = new List<Electronicos>(); //new HashSet<Electronicos>();            
        }

        [Key]
        public int Id_marca { get; set; }

        [StringLength(255)]
        public string Marca { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Electronicos> Electronicos { get; set; }
        public List<Electronicos> Electronicos { get; set; }
    }
}
