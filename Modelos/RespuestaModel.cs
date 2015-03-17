using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class RespuestaModel
    {
        public bool response { get; set; }
        public List<object> results { get; set; }
        public object result { get; set; }
        public string mensaje { get; set; }
        public string funcion { get; set; }
        public string href { get; set; }
        public string alerta { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        public RespuestaModel()
        {
            this.response = false;
            this.mensaje = "Error inesperado";
        }

        public void SetRespuesta(bool respuesta, string msj = null)
        {
            if (!respuesta && msj == "")
            {
                mensaje = "Ocurrio un error inesperado";
            }
            else
            {
                this.mensaje = msj;
            }
            this.response = respuesta;
        }

    }
}
