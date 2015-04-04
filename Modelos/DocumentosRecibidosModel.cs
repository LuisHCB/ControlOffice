using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Modelos
{
    public class DocumentosRecibidosModel
    {
        public RespuestaModel RegistrarDocumentoRecibico(string destino , string asunto , int opcionRecepcion, DateTime fechaRecepcion ,
              string folioRecepcion , string imagenRecepcion , DateTime fechaEnvioRecepcion , int opcionEnvio,
            int opcionLlegada , DateTime fechaEnvioLlegada , int requiereRespuesta , int opcionRequiereRespuesta ,
             DateTime fechaEnvioRespuesta , string respuesta, string idUsuario)
        {
            RespuestaModel resp = new RespuestaModel();
            using (var context = new DBControlOfficeContext())
            {                
                try
                {
                    context.Configuration.ValidateOnSaveEnabled = false;
                    Documentos_recibidos doc = new Documentos_recibidos();
                    doc.Usuario_recibe = idUsuario;
                    doc.Destino = destino;
                    doc.Asunto = asunto;
                    if (opcionRecepcion != 0)
                    {
                        doc.Fecha_recibe = fechaRecepcion;
                    }
                    doc.Folio = folioRecepcion;

                    if (opcionEnvio != 0)
                    {
                        doc.Fecha_envio = fechaEnvioRecepcion;
                    }

                    if (opcionLlegada == 2)
                    {
                        doc.Fecha_esperada = fechaEnvioLlegada;
                    }

                    doc.Requiere_respuesta =  Convert.ToBoolean(requiereRespuesta);
                    if (requiereRespuesta == 1)
                    {
                        if (opcionRequiereRespuesta != 0)
                        {
                            doc.Fecha_respuesta = fechaEnvioRespuesta;
                        }
                    }
                    doc.Respuesta = respuesta;


                    doc.Imagen = imagenRecepcion;
                    

                    context.Documentos_recibidos.Add(doc);
                    context.SaveChanges();
                    resp.SetRespuesta(true);
                    resp.alerta = "El documento recbido se ha resgistrado correctamente.";
                }
                catch (Exception ex)
                {
                    resp.SetRespuesta(false, "Hubo un error en la base de datos o en la conexión a esta. ");
                }
                return resp;
            }
        }

        public JqGridModel<Documentos_recibidos> ObtenerDocumentosRecibidos(JqGrid jq)
        {
            JqGridModel<Documentos_recibidos> jqm = new JqGridModel<Documentos_recibidos>();

            using (var ctx = new DBControlOfficeContext())
            {
                // Traemos la cantidad de registros
                jq.count = ctx.Documentos_recibidos.Count();

                // Configuramos el JqGridModel
                jqm.Config(jq);

                //Esta consulta solo sirve para Sql serve 2012 en adelante
                // consulta ="SELECT * FROM Electronicos ORDER BY  Id_electronico  OFFSET 10 ROWS FETCH NEXT 3 ROWS ONLY;",
                //try
                {
                    string consulta = "select top " + jqm.limit + " * from (select *, ROW_NUMBER() over (order by " + jqm.sord +
                           " ) as limites from Documentos_recibidos ) xx where limites >=" + jqm.start;
                    List<Documentos_recibidos> l = ctx.Database.SqlQuery<Documentos_recibidos>(consulta).ToList();
                    
                    jqm.DataSource(l);//ctx.Database.SqlQuery<Electronicos>(consulta).ToList());
                    /*jqm.DataSource(ctx.Database.SqlQuery<Electronicos>(consulta,
                            new SqlParameter("OFFSET", jqm.start),
                            new SqlParameter("FETCH", jqm.limit)).ToList());*/
                }
                //catch(Exception ex)
                {

                }
            }

            return jqm;
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
                Documentos_recibidos ultimoDoc = /*context.Solicitudes.Where(x =>
                                           x.Folio.Contains(fecha)
                                           ).LastOrDefault();*/
                context.Documentos_recibidos.SqlQuery("select TOP 1 * from Documentos_recibidos where (folio LIKE '" + fecha + "%' ) ").SingleOrDefault();
                if (ultimoDoc != null)
                {
                    try
                    {
                        indice = Convert.ToInt32(ultimoDoc.Folio.Substring(8));
                    }
                    catch
                    {

                    }
                }
            }
            return indice;
        }

        /// <summary>
        /// Indica si la solicitud indicada por el folio ya existe
        /// </summary>
        /// <param name="folio"></param>
        /// <returns></returns>
        public bool ExisteDocumentoRecibido(string folio)
        {
            using (var context = new DBControlOfficeContext())
            {
                Documentos_recibidos doc = context.Documentos_recibidos.Where(x =>
                                                x.Folio == folio
                                                 ).FirstOrDefault();
                if (doc == null)//no existe
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public RespuestaModel eliminar(string id)
        {
            int idDoc = Convert.ToInt32(id);
            RespuestaModel respuesta = new RespuestaModel();
            try
            {
                using (var context = new DBControlOfficeContext())
                {
                    Documentos_recibidos doc = context.Documentos_recibidos.Where(x =>
                                                               x.Id_documento == idDoc).FirstOrDefault();
                    context.Documentos_recibidos.Remove(doc);
                    context.SaveChanges();
                    respuesta.SetRespuesta(true, "El documento se ha eliminado correctamente");
                    return respuesta;
                }
            }
            catch (Exception ex)
            {
                respuesta.SetRespuesta(false);
                respuesta.alerta = "Error: " + ex;
                return respuesta;
            }
        }


    }



}
