namespace Entidades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Consumibles")]
    public partial class Consumibles
    {
        [Key]
        public int Id_consumible { get; set; }

        public int Id_tipo_consumible { get; set; }

        public int Cantidad { get; set; }

        public int? Id_marca { get; set; }

        [StringLength(50)]
        public string Clave { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? Fecha_recepcion { get; set; }

        [StringLength(255)]
        public string Entrega { get; set; }

        [Required]
        [StringLength(20)]
        public string Usuario_recibe { get; set; }

        [StringLength(200)]
        public string Imagen { get; set; }

        public virtual Tipo_consumible Tipo_consumible { get; set; }

        public virtual Usuarios Usuarios { get; set; }
    }
}
