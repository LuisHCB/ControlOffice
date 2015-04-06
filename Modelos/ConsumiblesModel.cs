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


        public RespuestaModel RegistrarConsumible(int cantidad , int tipo, string clave , int recepcion, DateTime fechaRecepcion ,
             string entregado , string archivo , string idUsuario)
        {
            RespuestaModel respuesta = new Modelos.RespuestaModel();
            using (var context = new DBControlOfficeContext())
            {
                try
                {
                    Consumibles nuevoCo = new Consumibles();
                    context.Configuration.ValidateOnSaveEnabled = false;

                    nuevoCo.Id_tipo_consumible = tipo;
                    nuevoCo.Cantidad = cantidad;
                    nuevoCo.Clave = clave;
                    nuevoCo.Fecha_recepcion = fechaRecepcion;
                    /*if (marca > 0)
                        nuevoEl.Id_marca = marca;*/                    
                    if (recepcion == 1)
                    {
                        nuevoCo.Fecha_recepcion = DateTime.Now;
                    }
                    else if (recepcion == 2)
                    {
                        nuevoCo.Fecha_recepcion = fechaRecepcion;
                    }
                    nuevoCo.Entrega = entregado;
                    nuevoCo.Usuario_recibe = idUsuario;
                    nuevoCo.Imagen = archivo;

                    context.Consumibles.Add(nuevoCo);

                    context.SaveChanges();//guarda todos los datos
                    respuesta.SetRespuesta(true);
                    respuesta.alerta = "El consumible se ha registrado correctamente.";
                }
                catch (Exception ex)
                {
                    respuesta.SetRespuesta(false, "Hubo un error en la base de datos o en la conexión a esta. ");
                }
            }
            return respuesta;
        }


        public JqGridModel<VirtualConsumibles> ObtenerTodosLosConsumibles(JqGrid jq)
        {
            JqGridModel<VirtualConsumibles> jqm = new JqGridModel<VirtualConsumibles>();

            using (var ctx = new DBControlOfficeContext())
            {
                // Traemos la cantidad de registros
                jq.count = ctx.Consumibles.Count();

                // Configuramos el JqGridModel
                jqm.Config(jq);

                //Esta consulta solo sirve para Sql serve 2012 en adelante
                // consulta ="SELECT * FROM Electronicos ORDER BY  Id_electronico  OFFSET 10 ROWS FETCH NEXT 3 ROWS ONLY;",
                try
                {
                    string consulta = "select top " + jqm.limit + " * from (select *, ROW_NUMBER() over (order by " + jqm.sord +
                           " ) as limites from Consumibles ) xx where limites >=" + jqm.start;
                    List<Consumibles> listaConsumibles = ctx.Database.SqlQuery<Consumibles>(consulta).ToList();
                    List<VirtualConsumibles> lista = new List<VirtualConsumibles>();
                    VirtualConsumibles consumibleVirtual;
                    int t = 0; string usuario = "";
                    //Realizo copia para enviarla con formato a la tabla                    
                    //Da formato a la fecha y otros parametros para ser enviada como un objeto json
                    for (int i = 0; i < listaConsumibles.Count(); i++)
                    {//obtengo relaciones por cada consumible. Este algoritmo no es nada recomendado, debido al tiempo que toma,
                        //pero en un mapeo directo habria que modificar la entidad del modelo e ingresar los datos directamente
                        t = listaConsumibles[i].Id_tipo_consumible;                        
                        listaConsumibles[i].Tipo_consumible = ctx.Tipo_consumible.Where(x => x.Id_tipo_consumible == t ).SingleOrDefault();  
                      
                        //copia
                        consumibleVirtual = new VirtualConsumibles();
                        consumibleVirtual.Cantidad = listaConsumibles[i].Cantidad;
                        consumibleVirtual.Clave = listaConsumibles[i].Clave;
                        consumibleVirtual.Entrega = listaConsumibles[i].Entrega;
                        consumibleVirtual.Fecha_recepcion_texto = listaConsumibles[i].Fecha_recepcion.ToString();
                        consumibleVirtual.Fecha_recepcion = listaConsumibles[i].Fecha_recepcion;
                        consumibleVirtual.Id_consumible = listaConsumibles[i].Id_consumible;
                        consumibleVirtual.Id_marca = listaConsumibles[i].Id_marca;
                        consumibleVirtual.Id_tipo_consumible = listaConsumibles[i].Id_tipo_consumible;
                        consumibleVirtual.Imagen = listaConsumibles[i].Imagen;
                        consumibleVirtual.Tipo_consumible = listaConsumibles[i].Tipo_consumible;
                        usuario = listaConsumibles[i].Usuario_recibe;// esto es asi por que la expresion lamda no permite evaluaciones dentro del where
                        consumibleVirtual.Usuario_recibe = (ctx.Usuarios.Where(x => x.Usuario == usuario).SingleOrDefault()).Nombre;
                        lista.Add(consumibleVirtual);
                    }
                    //Realizo copia para enviarla con formato a la tabla                    
                    //Da formato a la fecha y otros parametros para ser enviada como un objeto json                    
                    jqm.DataSource(lista);//ctx.Database.SqlQuery<Electronicos>(consulta).ToList());
                    /*jqm.DataSource(ctx.Database.SqlQuery<Electronicos>(consulta,
                            new SqlParameter("OFFSET", jqm.start),
                            new SqlParameter("FETCH", jqm.limit)).ToList());*/
                }
                catch(Exception ex)
                {

                }
            }

            return jqm;
        }

        public RespuestaModel RegistrarSolicitud(string destino, string descripcion, DateTime fechaEnvio, string folio, string imagen, int envio, string idUsuario)
        {
            RespuestaModel respuesta = new RespuestaModel();
            using (var context = new DBControlOfficeContext())
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
                    solicitud.Id_tipo_solicitud = 3; //solicitud mobiliario
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

        public JqGridModel<VirtualSolicitudes> ObtenerSolicitudesConsumibles(JqGrid jq)
        {
            JqGridModel<VirtualSolicitudes> jqm = new JqGridModel<VirtualSolicitudes>();

            using (var ctx = new DBControlOfficeContext())
            {
                // Traemos la cantidad de registros
                jq.count = 10;// ctx.Solicitudes.Where(x =>
                     //x.Id_tipo_solicitud == 3 ).Count(); //solo solicitudes de consumibles

                // Configuramos el JqGridModel
                jqm.Config(jq);

                //Esta consulta solo sirve para Sql serve 2012 en adelante
                // consulta ="SELECT * FROM Electronicos ORDER BY  Id_electronico  OFFSET 10 ROWS FETCH NEXT 3 ROWS ONLY;",
                //try
                {
                    string consulta = "select top " + jqm.limit + " * from (select *, ROW_NUMBER() over (order by " + jqm.sord +
                           " ) as limites from Solicitudes where Id_tipo_solicitud = 3 ) xx where limites >=" + jqm.start;
                    List<Solicitudes> listaSolicitudes = ctx.Database.SqlQuery<Solicitudes>(consulta).ToList();
                    List<VirtualSolicitudes> lista = new List<VirtualSolicitudes>();
                    VirtualSolicitudes solicitudVirtual;
                    //int t = 0; int m = 0;
                    //Da formato a la fecha y otros parametros para ser enviada como un objeto json
                    for (int i = 0; i < listaSolicitudes.Count(); i++)
                    {
                        solicitudVirtual = new VirtualSolicitudes();
                        solicitudVirtual.Descripcion = listaSolicitudes[i].Descripcion;
                        solicitudVirtual.Destino = listaSolicitudes[i].Destino;
                        solicitudVirtual.Fecha_envio = listaSolicitudes[i].Fecha_envio;
                        solicitudVirtual.Fecha_envio_texto = listaSolicitudes[i].Fecha_envio.ToString();
                        solicitudVirtual.Folio = listaSolicitudes[i].Folio;
                        solicitudVirtual.Id_solicitud = listaSolicitudes[i].Id_solicitud;
                        solicitudVirtual.Id_tipo_solicitud = listaSolicitudes[i].Id_tipo_solicitud;
                        solicitudVirtual.Imagen = listaSolicitudes[i].Imagen;
                        solicitudVirtual.Usuario_registra = listaSolicitudes[i].Usuario_registra;

                        lista.Add(solicitudVirtual);

                    }
                    jqm.DataSource(lista);//ctx.Database.SqlQuery<Electronicos>(consulta).ToList());
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

        public RespuestaModel eliminar(string id)
        {
            int idConsumible = Convert.ToInt32(id);
            RespuestaModel respuesta = new RespuestaModel();
            try
            {
                using (var context = new DBControlOfficeContext())
                {
                    Consumibles consumible = context.Consumibles.Where(x =>
                                                               x.Id_consumible == idConsumible).FirstOrDefault();
                    context.Consumibles.Remove(consumible);
                    context.SaveChanges();
                    respuesta.SetRespuesta(true, "El elemento se ha eliminado correctamente");
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

        public RespuestaModel RegistrarTipoConsumible(string tipo)
        {
            RespuestaModel respuesta = new RespuestaModel();
            using (var contex = new DBControlOfficeContext())
            {
                if (!ExisteTipo(tipo))
                {

                    try
                    {
                        Tipo_consumible nuevoTipo = new Tipo_consumible();
                        nuevoTipo.Nombre = tipo;
                        contex.Tipo_consumible.Add(nuevoTipo);
                        contex.SaveChanges();
                        respuesta.SetRespuesta(true);
                        respuesta.alerta = "El tipo de consumible se ha registrado correctamente.";
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
                    respuesta.SetRespuesta(false, "La tipo de conusmible que ingresaste ya existe");
                    return respuesta;
                }
            }
        }

        /// <summary>
        /// Indica si la el tipo de consumible ya existe
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public bool ExisteTipo(string tipo)
        {
            using (var context = new DBControlOfficeContext())
            {
                Tipo_consumible tipoE = context.Tipo_consumible.Where(x =>
                                                x.Nombre == tipo
                                                 ).FirstOrDefault();
                if (tipoE == null)//no existe
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public List<Tipo_consumible> ObtenerTipos()
        {
            using (var context = new DBControlOfficeContext())
            {
                return context.Tipo_consumible.ToList();
            }
        }
    }
}
