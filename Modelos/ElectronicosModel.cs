using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public JqGridModel<VirtualSolicitudes> ObtenerSolicitudesElectronicos(JqGrid jq)
        {
            JqGridModel<VirtualSolicitudes> jqm = new JqGridModel<VirtualSolicitudes>();

            using (var ctx = new DBControlOfficeContext())
            {
                // Traemos la cantidad de registros
                jq.count = ctx.Solicitudes.Where(x=>
                     x.Id_tipo_solicitud == 1).Count();

                // Configuramos el JqGridModel
                jqm.Config(jq);

                //Esta consulta solo sirve para Sql serve 2012 en adelante
                // consulta ="SELECT * FROM Electronicos ORDER BY  Id_electronico  OFFSET 10 ROWS FETCH NEXT 3 ROWS ONLY;",
                //try
                {
                    string consulta = "select top " + jqm.limit + " * from (select *, ROW_NUMBER() over (order by " + jqm.sord +
                           " ) as limites from Solicitudes where Id_tipo_solicitud = 1 ) xx where limites >=" + jqm.start;
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
                    jqm.DataSource(lista );//ctx.Database.SqlQuery<Electronicos>(consulta).ToList());
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

        public JqGridModel<Electronicos> ObtenerTodosLosElectronicos(JqGrid jq)
        {
            JqGridModel<Electronicos> jqm = new JqGridModel<Electronicos>();

            using (var ctx = new DBControlOfficeContext())
            {
                // Traemos la cantidad de registros
                jq.count = ctx.Electronicos.Count();

                // Configuramos el JqGridModel
                jqm.Config(jq);

                //Esta consulta solo sirve para Sql serve 2012 en adelante
               // consulta ="SELECT * FROM Electronicos ORDER BY  Id_electronico  OFFSET 10 ROWS FETCH NEXT 3 ROWS ONLY;",
                //try
                {
                    string consulta = "select top " + jqm.limit + " * from (select *, ROW_NUMBER() over (order by " + jqm.sord +
                           " ) as limites from Electronicos ) xx where limites >=" + jqm.start;
                    List <Electronicos> l = ctx.Database.SqlQuery<Electronicos>(consulta).ToList();
                    int t = 0; int m=0;
                    for (int i = 0; i < l.Count(); i++)
                    {//obtengo relaciones por cada electronico. Este algoritmo no es nada recomendado, debido al tiempo que toma,
                       //pero en un mapeo directo habria que modificar la entidad del modelo e ingresar los datos directamente
                        t=l[i].Id_tipo_electronico;
                        m = (int) (l[i].Id_marca == null ? -1 : l[i].Id_marca);
                        l[i].Tipo_electronico = ctx.Tipo_electronico.Where(x => x.Id_tipo_electronico == t).SingleOrDefault();
                        l[i].Marca_electronicos = ctx.Marca_electronicos.Where(x => x.Id_marca == m).SingleOrDefault();
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
