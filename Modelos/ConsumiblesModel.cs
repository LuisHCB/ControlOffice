using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Modelos
{
    public class ConsumiblesModel
    {
        /// <summary>
        /// Obtiene todos los tipos de consumibles que existen
        /// </summary>
        /// <returns></returns>
        public List<Tipo_consumible> ObtenerTiposConsumibles()
        {
            using (var context = new DBControlOfficeContext())
            {
                return context.Tipo_consumible.ToList();
            }
        }

        
    }
}
