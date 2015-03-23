namespace Entidades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Mobiliarios")]
    public partial class Mobiliarios
    {
        [Key]
        public int Id_mobiliario { get; set; }

        public int Id_tipo_mobiliario { get; set; }

        public int Cantidad { get; set; }

        [Required]
        [StringLength(50)]
        public string NoSerie { get; set; }

        public int? Id_marca { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaUso { get; set; }

        public int? Reemplazo { get; set; }

        [StringLength(200)]
        public string Imagen { get; set; }

        [Required]
        [StringLength(20)]
        public string Usuario_registra { get; set; }

        public virtual Marca_mobiliario Marca_mobiliario { get; set; }

        public virtual Tipo_mobiliario Tipo_mobiliario { get; set; }

        /*public virtual Usuarios Usuarios { get; set; }*/
    }
}
