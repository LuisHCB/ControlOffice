using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Modelos
{
    public class ElectronicosModel
    {
        /// <summary>
        /// Obtiene todos los tipos de electronicos que existen
        /// </summary>
        /// <returns></returns>
        public List<Tipo_electronico> ObtenerTiposElectronicos()
        {
            using (var context = new DBControlOfficeContext())
            {
                return context.Tipo_electronico.ToList();                
            }
        }

        /// <summary>
        /// Registra un nuevo aparato electronico
        /// </summary>
        /// <param name="tipo">Tipo de aparato</param>
        /// <param name="cantidad">Cantidad que se agrega</param>
        /// <param name="marca">Marca</param>
        /// <param name="serie">Numero de serie</param>
        /// <param name="FechaUso">Fecha en que se comezo a usar</param>
        /// <param name="reemplazo">Dias a los cuales debe reemplazarse</param>
        /// <param name="idUsuario">id se usuario que registra el aparato</param>
        /// <returns></returns>
        public RespuestaModel RegistrarElectronico(int tipo,int cantidad, int marca , string serie, DateTime FechaUso,int utilizado,
            int reemplazo, string idUsuario )
        {
            RespuestaModel respuesta = new Modelos.RespuestaModel();
            using (var context = new DBControlOfficeContext())
            {
                try
                {                    
                  Electronicos nuevoEl = new Electronicos();
                    context.Configuration.ValidateOnSaveEnabled = false;
                                        
                    nuevoEl.Id_tipo_electronico = tipo;
                    nuevoEl.Cantidad = cantidad;
                    if (marca > 0)
                        nuevoEl.Id_marca = marca;
                    nuevoEl.NoSerie = serie;
                    if (utilizado == 1)
                    {
                        nuevoEl.FechaUso = DateTime.Now;
                    }
                    else if (utilizado == 2)
                    {
                        nuevoEl.FechaUso = FechaUso;
                    }
                    nuevoEl.Reemplazo = reemplazo;
                    nuevoEl.Usuario_registra = idUsuario;
                    nuevoEl.Imagen = "";
                    
                    context.Electronicos.Add(nuevoEl);
                    
                    context.SaveChanges();//guarda todos los datos
                    respuesta.SetRespuesta(true);                    
                    respuesta.alerta = "El aparato electrónico se ha registrado correctamente.";
                }
                catch(Exception ex)
                {
                    respuesta.SetRespuesta(false,"Hubo un error en la base de datos o en la conexión a esta. ");
                }
            }
            return respuesta;
            
        }

        public RespuestaModel RegistrarSolicitud(string destino, string descripcion,DateTime fechaEnvio, string folio,string imagen, int envio, string idUsuario )
        {
            RespuestaModel respuesta = new RespuestaModel();
            using(var context = new DBControlOfficeContext())
            {
                try
                {
                    Solicitudes solicitud = new Solicitudes();
                    solicitud.Destino = destino;
                    solicitud.Descripcion = descripcion;
                    if (envio != 0)
                    {
                        solicitud.Fecha_envio = fechaEnvio;
                    }                    
                    solicitud.Folio = folio;
                    solicitud.Id_tipo_solicitud = 1; //solicitud electronica
                    solicitud.Imagen = imagen;
                    solicitud.Usuario_registra = idUsuario;

                    context.Solicitudes.Add(solicitud);
                    context.SaveChanges();
                    respuesta.SetRespuesta(true);
                    respuesta.alerta = "La solicitud se ha resgistrado correctamente.";
                }
                catch (Exception ex)
                {
                    respuesta.SetRespuesta(false, "Hubo un error en la base de datos o en la conexión a esta. ");
                }


                return respuesta;
            }
        }

        public RespuestaModel RegistrarMarca(string marca)
        {
            RespuestaModel respuesta = new RespuestaModel();
            using (var contex = new DBControlOfficeContext())
            {
                if (!ExisteMarca(marca))
                {
                  
                    try
                    {
                        Marca_electronicos nuevaMarca = new Marca_electronicos();
                        nuevaMarca.Marca = marca;
                        contex.Marca_electronicos.Add(nuevaMarca);
                        contex.SaveChanges();
                        respuesta.SetRespuesta(true);
                        respuesta.alerta = "La marca se ha registrado correctamente.";
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
                    respuesta.SetRespuesta(false, "La marca que ingresaste ya existe");
                    return respuesta;
                }
            }
        }

        /// <summary>
        /// Indica si la marca ya existe
        /// </summary>
        /// <param name="marca"></param>
        /// <returns></returns>
        public bool ExisteMarca(string marca)
        {
            using (var context = new DBControlOfficeContext())
            {
                Marca_electronicos marcaE = context.Marca_electronicos.Where(x =>
                                                x.Marca == marca
                                                 ).FirstOrDefault();
                if (marcaE == null)//no existe
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public List<Marca_electronicos> ObtenerMarcas()
        {
            using (var context = new DBControlOfficeContext())
            {
                return context.Marca_electronicos.ToList();
            }
        }
    }
}
