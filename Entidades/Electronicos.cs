namespace Entidades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Electronicos")]
    public class Electronicos
    {
        [Key]        
        public int Id_electronico { get; set; }

        public int Id_tipo_electronico { get; set; }

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

      /*  public Marca_electronicos Marca_electronicos { get; set; }

        public Tipo_electronico Tipo_electronico { get; set; }
        
        public Usuarios Usuarios { get; set; }*/
    }
}
