using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Modelos
{
    public class MobiliariosModel
    {
        /// <summary>
        /// Obtiene todos los tipos de mobiliarios que existen
        /// </summary>
        /// <returns></returns>
        public List<Tipo_mobiliario> ObtenerTiposMobiliarios()
        {
            using (var context = new DBControlOfficeContext() )
            {
                return context.Tipo_mobiliario.ToList();
            }
        }

        public List<Marca_mobiliario> ObtenerMarcas()
        {
            using (var context = new DBControlOfficeContext())
            {
                return context.Marca_mobiliario.ToList();
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
                Marca_mobiliario marcaE = context.Marca_mobiliario.Where(x =>
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

        public RespuestaModel RegistrarMarca(string marca)
        {
            RespuestaModel respuesta = new RespuestaModel();
            using (var contex = new DBControlOfficeContext())
            {
                if (!ExisteMarca(marca))
                {

                    try
                    {
                        Marca_mobiliario nuevaMarca = new Marca_mobiliario();
                        nuevaMarca.Marca = marca;
                        contex.Marca_mobiliario.Add(nuevaMarca);
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
        public RespuestaModel RegistrarMobiliario(int tipo, int cantidad, int marca, string serie, DateTime FechaUso, int utilizado,
            int reemplazo, string idUsuario, string archivo)
        {
            RespuestaModel respuesta = new Modelos.RespuestaModel();
            using (var context = new DBControlOfficeContext())
            {
                try
                {
                    Mobiliarios nuevoMo = new Mobiliarios();
                    context.Configuration.ValidateOnSaveEnabled = false;

                    nuevoMo.Id_tipo_mobiliario = tipo;
                    nuevoMo.Cantidad = cantidad;
                    if (marca > 0)
                        nuevoMo.Id_marca = marca;
                    nuevoMo.NoSerie = serie;
                    if (utilizado == 1)
                    {
                        nuevoMo.FechaUso = DateTime.Now;
                    }
                    else if (utilizado == 2)
                    {
                        nuevoMo.FechaUso = FechaUso;
                    }
                    nuevoMo.Reemplazo = reemplazo;
                    nuevoMo.Usuario_registra = idUsuario;
                    nuevoMo.Imagen = archivo;

                    context.Mobiliarios.Add(nuevoMo);

                    context.SaveChanges();//guarda todos los datos
                    respuesta.SetRespuesta(true);
                    respuesta.alerta = "El mobiliario se ha registrado correctamente.";
                }
                catch (Exception ex)
                {
                    respuesta.SetRespuesta(false, "Hubo un error en la base de datos o en la conexión a esta. ");
                }
            }
            return respuesta;

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
                    solicitud.Id_tipo_solicitud = 2; //solicitud mobiliario
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

        public JqGridModel<VirtualSolicitudes> ObtenerSolicitudesMobiliarios(JqGrid jq)
        {
            JqGridModel<VirtualSolicitudes> jqm = new JqGridModel<VirtualSolicitudes>();

            using (var ctx = new DBControlOfficeContext())
            {
                // Traemos la cantidad de registros
                jq.count = ctx.Solicitudes.Where(x =>
                     x.Id_tipo_solicitud == 2).Count(); //solo solicitudes de mobiliarios

                // Configuramos el JqGridModel
                jqm.Config(jq);

                //Esta consulta solo sirve para Sql serve 2012 en adelante
                // consulta ="SELECT * FROM Electronicos ORDER BY  Id_electronico  OFFSET 10 ROWS FETCH NEXT 3 ROWS ONLY;",
                //try
                {
                    string consulta = "select top " + jqm.limit + " * from (select *, ROW_NUMBER() over (order by " + jqm.sord +
                           " ) as limites from Solicitudes where Id_tipo_solicitud = 2 ) xx where limites >=" + jqm.start;
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

        public JqGridModel<Mobiliarios> ObtenerTodosLosMobiliarios(JqGrid jq)
        {
            JqGridModel<Mobiliarios> jqm = new JqGridModel<Mobiliarios>();

            using (var ctx = new DBControlOfficeContext())
            {
                // Traemos la cantidad de registros
                jq.count = ctx.Mobiliarios.Count();

                // Configuramos el JqGridModel
                jqm.Config(jq);

                //Esta consulta solo sirve para Sql serve 2012 en adelante
                // consulta ="SELECT * FROM Electronicos ORDER BY  Id_electronico  OFFSET 10 ROWS FETCH NEXT 3 ROWS ONLY;",
                //try
                {
                    string consulta = "select top " + jqm.limit + " * from (select *, ROW_NUMBER() over (order by " + jqm.sord +
                           " ) as limites from Mobiliarios ) xx where limites >=" + jqm.start;
                    List<Mobiliarios> l = ctx.Database.SqlQuery<Mobiliarios>(consulta).ToList();
                    int t = 0; int m = 0;
                    for (int i = 0; i < l.Count(); i++)
                    {//obtengo relaciones por cada electronico. Este algoritmo no es nada recomendado, debido al tiempo que toma,
                        //pero en un mapeo directo habria que modificar la entidad del modelo e ingresar los datos directamente
                        t = l[i].Id_tipo_mobiliario;
                        m = (int)(l[i].Id_marca == null ? -1 : l[i].Id_marca);
                        l[i].Tipo_mobiliario = ctx.Tipo_mobiliario.Where(x => x.Id_tipo_mobiliario == t).SingleOrDefault();
                        l[i].Marca_mobiliario = ctx.Marca_mobiliario.Where(x => x.Id_marca == m).SingleOrDefault();
                    }

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
    }
}
