namespace Entidades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    
    public partial class Virtual_documentos_internos
    {
        /*public Tipos_documento_interno Tipos_documento_interno { get; set; }*/

       
        public int Id_documento { get; set; }

        
        public string Descripcion { get; set; }

      
        public string Usuario_registra { get; set; }

        public int? Id_tipo_documento { get; set; }

     
        public string Folio { get; set; }

   
        public string Imagen { get; set; }

        public Tipos_documento_interno Tipo_documento { get; set; }

        

        /*public virtual Usuarios Usuarios { get; set; }*/
    }
}
