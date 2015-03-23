namespace Entidades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tipo_electronico")]
    public partial class Tipo_electronico
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tipo_electronico()
        {
      //      Electronicos = new HashSet<Electronicos>();
            Electronicos = new List<Electronicos>();
        }

        [Key]
        public int Id_tipo_electronico { get; set; }

        [StringLength(100)]
        public string Nombre { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    //    public virtual ICollection<Electronicos> Electronicos { get; set; }
        public List<Electronicos> Electronicos { get; set; }
    }
}
