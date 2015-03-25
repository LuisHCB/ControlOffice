namespace Entidades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tipo_consumible")]
    public partial class Tipo_consumible
    {
       // [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tipo_consumible()
        {
            Consumibles = new List <Consumibles>();
        }

        [Key]
        public int Id_tipo_consumible { get; set; }

        [StringLength(100)]
        public string Nombre { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public  List<Consumibles> Consumibles { get; set; }
    }
}
