using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidades;
using Modelos;
using Util;
using ControlOffice.CustomAttributes;


namespace ControlOffice.Controllers
{
    public class UsuariosController : Controller
    {

        Usuarios usuario;
        UsuarioModel usuarioModel;
        /// <summary>
        /// Constructor del controlador
        /// </summary>
        public UsuariosController()
        {
            usuarioModel = new UsuarioModel();
            //Obtenemos usuario actual
            this.usuario = usuarioModel.ObtenerUsuario(ManejadorDeSesiones.ObtenerUsuarioEnSesion());   
        }


        //
        // GET: /Usuarios/
        [SoloAdministrador]
        [Protegido]
        public ActionResult Index()
        {
            ViewBag.NombreUsuario = usuario != null ? usuario.Nombre : "Usuario";
            return View();
        }

        [ProtegidoVista]
        [SoloAdministradorParcial]
        public PartialViewResult modificarPass(string id)
        {
            ViewBag.usuarioActual = usuarioModel.ObtenerUsuario(id);
            return PartialView();
        }

        public PartialViewResult modificarUsuario()
        {
            ViewBag.usuarioActual = usuarioModel.ObtenerUsuario(ManejadorDeSesiones.ObtenerUsuarioEnSesion());
            return PartialView();
        }

        [ProtegidoVista]
        public PartialViewResult verificarAcceso()        
        {
            return PartialView();
        }

        

        [ProtegidoVista]
        [SoloAdministradorParcial]        
        public PartialViewResult NuevoUsuario()
        {
            return PartialView();
        }

        public JsonResult listaUsuarios(JqGrid jq)
        {
            return Json(usuarioModel.ObtenerTodosLosUsuarios(jq), JsonRequestBehavior.AllowGet);
        }

        public JsonResult  verificarUsuario(string pass)
        {
            RespuestaModel respuesta = usuarioModel.verificarUsuarioActual(pass);
            if (respuesta.response)
            {
                respuesta.target = "#modal-modal-general";
                respuesta.html="../usuarios/modificarUsuario";
            }
            return Json( respuesta);
        }
        
        public JsonResult darDeAlta(string id)
        {
            if (usuario.Administrador)
            {
                return Json(usuarioModel.darDeAlta(id));
            }            
            else
            {
                RespuestaModel respuesta = new RespuestaModel();
                respuesta.SetRespuesta(false, "No tienes los permisos necesarios para realizar esta acción");
                respuesta.alerta="No tienes los permisos necesarios para realizar esta acción";
                return Json(respuesta);
            }
        }

        
        public JsonResult darDeBaja(string id)
        {
            if (usuario.Administrador)
            {
                return Json(usuarioModel.darDeBaja(id));
            }
            else
            {
                RespuestaModel respuesta = new RespuestaModel();
                respuesta.SetRespuesta(false, "No tienes los permisos necesarios para realizar esta acción");
                respuesta.alerta = "No tienes los permisos necesarios para realizar esta acción";
                return Json(respuesta);
            }
        }


        
    
        public JsonResult RegistrarUsuario(Usuarios model)
        {
            if (usuario.Administrador)
            {
                return Json(usuarioModel.registrarUsuario(model));            
            }
            else
            {
                RespuestaModel respuesta = new RespuestaModel();
                respuesta.SetRespuesta(false, "No tienes los permisos necesarios para realizar esta acción");                
                return Json(respuesta);
            }
        }

        [HttpPost]
        [SoloAjax]       
        public JsonResult modificarPassUsuario(Usuarios usuarioActual)
        {
            if (usuario.Administrador)
            {
                return Json(usuarioModel.modificarPassUsuario(usuarioActual));
            }
            else
            {
                RespuestaModel respuesta = new RespuestaModel();
                respuesta.SetRespuesta(false, "No tienes los permisos necesarios para realizar esta acción");
                respuesta.alerta="No tienes los permisos necesarios para realizar esta acción";
                return Json(respuesta);
            }
        }

        public JsonResult modificarInfoUsuario(Usuarios usuarioActual)
        {
            //if (usuario.Administrador)
            {
                usuarioActual.Usuario = usuario.Usuario;
                return Json(usuarioModel.modificarInfoUsuario(usuarioActual));
            }
            /*else
            {
                RespuestaModel respuesta = new RespuestaModel();
                respuesta.SetRespuesta(false, "No tienes los permisos necesarios para realizar esta acción");
                respuesta.alerta = "No tienes los permisos necesarios para realizar esta acción";
                return Json(respuesta);
            }*/
        }

    }
}
