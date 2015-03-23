namespace Entidades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class VirtualSolicitudes
    {
      
        public int Id_solicitud { get; set; }

        public int Id_tipo_solicitud { get; set; }

       
        public string Destino { get; set; }

        public string Descripcion { get; set; }

        
        public DateTime? Fecha_envio { get; set; }

        public string Fecha_envio_texto { get; set; }

        
        public string Folio { get; set; }

        
        public string Imagen { get; set; }

        [StringLength(20)]
        public string Usuario_registra { get; set; }

        
        /*
        public virtual Tipo_solicitudes Tipo_solicitudes { get; set; }

        public virtual Usuarios Usuarios { get; set; }*/
    }
}
