namespace Entidades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
  

    public partial class Documentos_recibidos
    {
        [Key]
        public int Id_documento { get; set; }

        [Required]
        [StringLength(255)]
        public string Destino { get; set; }

        [Required]
        [StringLength(20)]
        public string Usuario_recibe { get; set; }

        [Required]
        [StringLength(500)]
        public string Asunto { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Fecha_recibe { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? Fecha_envio { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? Fecha_esperada { get; set; }

        [StringLength(50)]
        public string Folio { get; set; }

        public bool? Requiere_respuesta { get; set; }

        [StringLength(200)]
        public string Imagen { get; set; }

        public virtual Usuarios Usuarios { get; set; }
    }
}
