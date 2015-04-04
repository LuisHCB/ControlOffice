using System;
using System.Collections.Generic;
using System.Linq;
using Entidades;
using System.Text;
using System.Threading.Tasks;
using Util;
using System.Web;

namespace Modelos
{
    public class UsuarioModel
    {
        /// <summary>
        /// Metodo que verifica si se permite o no el acceso
        /// </summary>
        /// <param name="usuario"></param>
        public RespuestaModel Acceder(Usuarios usuario)
        {
            using (var context = new DBControlOfficeContext())            
         {
             RespuestaModel respuesta = new RespuestaModel();
             try
             {
                 var us = context.Usuarios.Where( x => 
                                         x.Usuario == usuario.Usuario &&
                                         x.Pass == usuario.Pass ).SingleOrDefault();//solo uno
                 if (us != null)
                 {
                     if (us.Activo == true)
                     {
                         ManejadorDeSesiones.AgregarUsuarioEnSesion(usuario.Usuario);
                         respuesta.SetRespuesta(true, "Acceso permitido");
                         respuesta.href = "controlOffice/menu";
                         return respuesta;
                     }
                     else
                     {
                         respuesta.response = false;
                         respuesta.mensaje = "Lo lamento "+us.Nombre+", ya no tienes permitido acceder al sistema";
                         return respuesta;
                     }
                 }
                 else
                 {
                     respuesta.response = false;
                     respuesta.mensaje = "Error en usuario o contraseña";
                     return respuesta;
                 }
             }
             catch
             {
                 respuesta.response = false;
                 respuesta.mensaje = "Error inesperado. Verifique la conexión";
                 return respuesta;
             }
             
         }

        }

        /// <summary>
        /// Cierra la sesion actual
        /// </summary>
        public void CerrarSesion()
        {
            ManejadorDeSesiones.EliminarSesion();
            HttpContext.Current.Response.Redirect("/controlOffice",true);            
        }

        /// <summary>
        /// Obtiene al usuario indicado por su llave primaria
        /// </summary>
        /// <param name="pk"></param>
        /// <returns></returns>
        public Usuarios ObtenerUsuario(string pk)
        {
            using (var context = new DBControlOfficeContext())
            {
                Usuarios usuario = context.Usuarios.Where(x=>
                             x.Usuario == pk
                    ).SingleOrDefault();
                if (usuario != null)
                {
                    return usuario;
                }
                else
                {
                    return null;
                }

            }
        }

        public JqGridModel<Usuarios> ObtenerTodosLosUsuarios(JqGrid jq)
        {
            JqGridModel<Usuarios> jqm = new JqGridModel<Usuarios>();

            using (var ctx = new DBControlOfficeContext())
            {
                // Traemos la cantidad de registros
                jq.count = ctx.Usuarios.Count();

                // Configuramos el JqGridModel
                jqm.Config(jq);

                //Esta consulta solo sirve para Sql serve 2012 en adelante
                // consulta ="SELECT * FROM Electronicos ORDER BY  Id_electronico  OFFSET 10 ROWS FETCH NEXT 3 ROWS ONLY;",
                //try
                {
                    string consulta = "select top " + jqm.limit + " * from (select *, ROW_NUMBER() over (order by " + jqm.sord +
                           " ) as limites from Usuarios ) xx where limites >=" + jqm.start;
                    List<Usuarios> l = ctx.Database.SqlQuery<Usuarios>(consulta).ToList();
                    
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

        public RespuestaModel darDeAlta(string idUsuario)
        {
            RespuestaModel respuesta = new RespuestaModel();
            try
            {
                using(var context = new DBControlOfficeContext())
                {
                    Usuarios usuario = context.Usuarios.Where(x=>
                                                x.Usuario == idUsuario).FirstOrDefault();
                    if (usuario != null)
                    {
                        usuario.Activo = true;
                        context.SaveChanges();
                        respuesta.SetRespuesta(true);
                        respuesta.alerta = "El usuario se ha dado de alta correctamente";
                    }
                    else
                    {
                        respuesta.SetRespuesta(false,"El usuario no existe");
                        
                    }

                }
            }
            catch (Exception ex)
            {
                respuesta.SetRespuesta(false,"Error inesperado: "+ex.Message);                
            }
            return respuesta;
        }

        public RespuestaModel darDeBaja(string idUsuario)
        {
            RespuestaModel respuesta = new RespuestaModel();
            try
            {
                using (var context = new DBControlOfficeContext())
                {
                    Usuarios usuario = context.Usuarios.Where(x =>
                                                x.Usuario == idUsuario).FirstOrDefault();
                    if (usuario != null)
                    {
                        usuario.Activo = false;
                        context.SaveChanges();
                        respuesta.SetRespuesta(true);
                        respuesta.alerta = "El usuario se ha dado de baja correctamente";
                    }
                    else
                    {
                        respuesta.SetRespuesta(false, "El usuario no existe");                        
                    }

                }
            }
            catch (Exception ex)
            {
                respuesta.SetRespuesta(false, "Error inesperado: " + ex.Message);
            }
            return respuesta;
        }

        public RespuestaModel registrarUsuario(Usuarios usuario)
        {
            RespuestaModel respuesta = new RespuestaModel();
            try
            {
                using (var context = new DBControlOfficeContext())
                {
                    Usuarios us = context.Usuarios.Where(x =>
                                                         x.Usuario == usuario.Usuario).FirstOrDefault();
                    if (us == null)
                    {
                        usuario.Activo = true;
                        context.Usuarios.Add(usuario);
                        context.SaveChanges();
                        respuesta.SetRespuesta(true);
                        respuesta.alerta = "El usuario se ha registrado correctamente";
                        respuesta.funcion = "$('#form-usuario').modal('hide'); $('#tabla1').trigger('reloadGrid'); ";
                    }
                    else
                    {
                        respuesta.SetRespuesta(false, "Lo lamento, el usuario "+usuario.Usuario+" ya existe");
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.SetRespuesta(false, "Error inesperado: " + ex.Message);
            }
            return respuesta;
        }

        public RespuestaModel modificarPassUsuario(Usuarios usuario)
        {
            RespuestaModel respuesta = new RespuestaModel();
            try
            {
                if (usuario.Pass == null)
                {
                    respuesta.SetRespuesta(false, "Es necesario que ingreses una contraseña");
                }
                else
                {
                    using (var context = new DBControlOfficeContext())
                    {
                        Usuarios us = context.Usuarios.Where(x =>
                                                    x.Usuario == usuario.Usuario).SingleOrDefault();
                        if (us != null)
                        {
                            us.Pass = usuario.Pass;

                            context.SaveChanges();
                            respuesta.SetRespuesta(true);
                            respuesta.alerta = "La información se modificó correctamente";
                            respuesta.funcion = "$('#form-usuario-info').modal('hide'); $('#tabla1').trigger('reloadGrid'); ";
                        }
                        else
                        {
                            respuesta.SetRespuesta(false, "Lo lamento, el usuario " + usuario.Usuario + " no existe");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.SetRespuesta(false, "Error inesperado: " + ex.Message);
            }
            return respuesta;

        }
    }
}
