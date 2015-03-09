namespace Entidades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Documentos_enviados
    {
        [Key]
        public int Id_documento { get; set; }

        [Required]
        [StringLength(255)]
        public string Destino { get; set; }

        [Required]
        [StringLength(500)]
        public string Asunto { get; set; }

        public int? Id_tipo_documento { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? Fecha_envio { get; set; }

        [StringLength(50)]
        public string Folio { get; set; }

        [StringLength(200)]
        public string Imagen { get; set; }

        [StringLength(20)]
        public string Usuario_registra { get; set; }

        public virtual Tipos_documento Tipos_documento { get; set; }

        public virtual Usuarios Usuarios { get; set; }
    }
}
