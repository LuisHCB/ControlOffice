using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using System.Threading.Tasks;

namespace Modelos
{
    public class SolicitudesModel
    {

        /// <summary>
        /// Indica si la solicitud indicada por el folio ya existe
        /// </summary>
        /// <param name="folio"></param>
        /// <returns></returns>
        public bool ExisteSolicitud(string folio)
        {
            using (var context = new DBControlOfficeContext())
            {
                Solicitudes solicitud = context.Solicitudes.Where(x =>
                                                x.Folio == folio
                                                 ).FirstOrDefault();
                if (solicitud == null)//no existe
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Obtiene el ultimo indice del folio agregado correctamente
        /// </summary>
        /// <returns></returns>
        public int ObtenerUltimoFolio()
        {
            //Este algoritmo no es optimo, pero por cuestiones de tiempo se realiza de esta manera, sin embargo contiene varios
            // posibles errores que en su momento pueden llegar a ocurrir
            int indice = 0;
            string fecha = DateTime.Now.Year + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
            using (var context = new DBControlOfficeContext())
            {
                Solicitudes ultimaSolicitud = /*context.Solicitudes.Where(x =>
                                           x.Folio.Contains(fecha)
                                           ).LastOrDefault();*/
                context.Solicitudes.SqlQuery("select TOP 1 * from solicitudes where (folio LIKE '" + fecha + "%' ) ").SingleOrDefault();
                if (ultimaSolicitud != null)
                {
                    try
                    {
                        indice = Convert.ToInt32(ultimaSolicitud.Folio.Substring(8));
                    }
                    catch
                    {

                    }
                }
            }
            return indice;
        }
    }
}
