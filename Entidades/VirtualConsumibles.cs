namespace Entidades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    
    public class VirtualConsumibles
    {
    
        public int Id_consumible { get; set; }

        public int Id_tipo_consumible { get; set; }

        public int Cantidad { get; set; }

        public int? Id_marca { get; set; }

    
        public string Clave { get; set; }

    
        public DateTime? Fecha_recepcion { get; set; }

        public string Fecha_recepcion_texto { get; set; }

    
        public string Entrega { get; set; }

    
        public string Usuario_recibe { get; set; }

    
        public string Imagen { get; set; }

        public virtual Tipo_consumible Tipo_consumible { get; set; }

      /*  public virtual Usuarios Usuario_recibe { get; set; }*/
    }
}
