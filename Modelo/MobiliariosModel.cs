using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Modelo
{
    public class MobiliariosModel
    {
        /// <summary>
        /// Obtiene todos los tipos de mobiliarios que existen
        /// </summary>
        /// <returns></returns>
        public List<Tipo_mobiliario> ObtenerTiposMobiliarios()
        {
            using (var context = new DBContolOficceContext())
            {
                return context.Tipo_mobiliario.ToList();
            }
        }
    }
}
