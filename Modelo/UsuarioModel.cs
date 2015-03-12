using System;
using System.Collections.Generic;
using System.Linq;
using Entidades;
using System.Text;
using System.Threading.Tasks;
using Util;
using System.Web;

namespace Modelo
{
    public class UsuarioModel
    {
        /// <summary>
        /// Metodo que verifica si se permite o no el acceso
        /// </summary>
        /// <param name="usuario"></param>
        public RespuestaModel Acceder(Usuarios usuario)
        {
         using(var context = new DBContolOficceContext ())            
         {
             RespuestaModel respuesta = new RespuestaModel();
             try
             {
                 var us = context.Usuarios.Where( x => 
                                         x.Usuario == usuario.Usuario &&
                                         x.Pass == usuario.Pass ).SingleOrDefault();//solo uno
                 if (us != null)
                 {
                     ManejadorDeSesiones.AgregarUsuarioEnSesion(usuario.Usuario);
                     respuesta.SetRespuesta(true,"Acceso permitido");                     
                     respuesta.href = "controlOffice/menu";
                     return respuesta;
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
            using (var context = new DBContolOficceContext())
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
    }
}
