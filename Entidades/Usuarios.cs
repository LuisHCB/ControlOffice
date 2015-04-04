namespace Entidades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Usuarios")]
    public partial class Usuarios
    {
     /*   [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuarios()
        {
            Consumibles = new HashSet<Consumibles>();
            Documentos_enviados = new HashSet<Documentos_enviados>();
            Documentos_internos = new HashSet<Documentos_internos>();
            Documentos_recibidos = new HashSet<Documentos_recibidos>();
            Electronicos = new HashSet<Electronicos>();
            Mobiliarios = new HashSet<Mobiliarios>();
            Solicitudes = new HashSet<Solicitudes>();
        }*/

        [Key]
        [StringLength(20)]
        [Required (ErrorMessage="Debes ingresar tu correo")]
        [EmailAddress (ErrorMessage="Es necesaria una dirección de email")]
        public string Usuario { get; set; }

        [Required (ErrorMessage="Ingresa tu contraseña")]
        [StringLength(50)]
        public string Pass { get; set; }

        [StringLength(255)]        
        public string Nombre { get; set; }

        
        public bool? Activo { get; set; }

        
        public bool Administrador { get; set; }


        /*
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Consumibles> Consumibles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Documentos_enviados> Documentos_enviados { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Documentos_internos> Documentos_internos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Documentos_recibidos> Documentos_recibidos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Electronicos> Electronicos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Mobiliarios> Mobiliarios { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Solicitudes> Solicitudes { get; set; }*/
    }
}
