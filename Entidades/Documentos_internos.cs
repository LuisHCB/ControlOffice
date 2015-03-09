namespace Entidades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public partial class Documentos_internos
    {
        [Key]
        public int Id_documento { get; set; }

        [Required]
        [StringLength(2048)]
        public string Descripcion { get; set; }

        [StringLength(20)]
        public string Usuario_registra { get; set; }

        public int? Id_tipo_documento { get; set; }

        [StringLength(50)]
        public string Folio { get; set; }

        [StringLength(200)]
        public string Imagen { get; set; }

        public virtual Tipos_documento_interno Tipos_documento_interno { get; set; }

        public virtual Usuarios Usuarios { get; set; }
    }
}
