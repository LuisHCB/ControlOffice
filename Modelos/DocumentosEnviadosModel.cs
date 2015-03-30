using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Modelos
{
   public   class DocumentosEnviadosModel
    {
       public List<Tipos_documento> ObtenerTiposDocEnviados()
       {
           using (var context = new DBControlOfficeContext())
           {
               return context.Tipos_documento.ToList();
           }
       }

       public RespuestaModel RegistrarNuevoTipoDocEnv(string tipoDocumento)
       {
           RespuestaModel respuesta = new RespuestaModel();
           using (var contex = new DBControlOfficeContext())
           {
               if (!ExisteTipoDocumento(tipoDocumento))
               {
                   try
                   {
                       Tipos_documento doc = new Tipos_documento();
                       doc.Tipo_documento = tipoDocumento;                       
                       contex.Tipos_documento.Add(doc);
                       contex.SaveChanges();
                       respuesta.SetRespuesta(true);                       
                       return respuesta;
                   }
                   catch
                   {
                       respuesta.SetRespuesta(false, "Error al ingresar el registro, verifique información o conexión");
                       return respuesta;
                   }
               }
               else
               {
                   respuesta.SetRespuesta(false, "El tipo de documento -"+tipoDocumento+"- ya existe");
                   return respuesta;
               }
           }
       }


       public RespuestaModel RegistrarDocumentoEnviado(string destino, string asunto, int tipoDocumento, DateTime fechaEnvio, string folio, string imagen, int opcionEnvio ,
           string idUsuario)
       {
           RespuestaModel respuesta = new RespuestaModel();
           using (var context = new DBControlOfficeContext())
           {
               try
               {
                   Documentos_enviados doc = new Documentos_enviados();
                   doc.Destino = destino;
                   doc.Asunto = asunto;
                   if (opcionEnvio != 0)
                   {
                       doc.Fecha_envio = fechaEnvio;
                   }
                   doc.Folio = folio;
                   if (tipoDocumento != 0)
                   {
                       doc.Id_tipo_documento = tipoDocumento; //<---
                   }
                   doc.Imagen = imagen;
                   doc.Usuario_registra = idUsuario;

                   context.Documentos_enviados.Add(doc);
                   context.SaveChanges();
                   respuesta.SetRespuesta(true);
                   respuesta.alerta = "El documento se ha resgistrado correctamente.";
               }
               catch (Exception ex)
               {
                   respuesta.SetRespuesta(false, "Hubo un error en la base de datos o en la conexión a esta. ");
               }
               return respuesta;
           }
       }


       public bool ExisteTipoDocumento(string tipoDocumento)
       {
           using (var context = new DBControlOfficeContext())
            {
                Tipos_documento doc = context.Tipos_documento.Where(x =>
                                                x.Tipo_documento == tipoDocumento
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
               Documentos_enviados ultimoDoc = /*context.Solicitudes.Where(x =>
                                           x.Folio.Contains(fecha)
                                           ).LastOrDefault();*/
               context.Documentos_enviados.SqlQuery("select TOP 1 * from Documentos_enviados where (folio LIKE '" + fecha + "%' ) ").SingleOrDefault();
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
       public bool ExisteDocumentoEnviado(string folio)
       {
           using (var context = new DBControlOfficeContext())
           {
               Documentos_enviados doc = context.Documentos_enviados.Where(x =>
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

    }
}
