using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidades;
using Modelos;
using ControlOffice.CustomAttributes;
using Util;

namespace ControlOffice.Controllers
{
    public class ControlOfficeController : Controller
    {
        private UsuarioModel usuarioModel = new UsuarioModel();
        
        //
        // GET: /ControlOffice/

        public ActionResult Index()
        {
            return View();
        }

        [Protegido] //solo con sesion activa
        public ActionResult Menu()
        {
            //Obtenemos usuario actual
            Usuarios usuario = usuarioModel.ObtenerUsuario(ManejadorDeSesiones.ObtenerUsuarioEnSesion());
            ViewBag.NombreUsuario = usuario != null ? usuario.Nombre : "Usuario";
            return View();
        }



        [HttpPost] //solo accede por el metodo post
        [SoloAjax]     // definida en customattributes
        public JsonResult Acceder(string Usuario, string Pass)
        {   
            if (ModelState.IsValid)//solo si es valida la informacion
            {
                //crea un objeto json y lo devuelve a partir de la clase dada
                return Json(usuarioModel.Acceder(new Usuarios { Usuario = Usuario , Pass = Pass})); // new Usuarios {usuario = usuarios, etc}
            }
            else
            {
                //Crea un objeto json y lo devuelve
                return Json(new { response = false, mensaje = "Hay un error en el usuario o contraseña, verifique los datos" });
            }
        }

        [HttpGet]
        public ActionResult CerrarSesion()
        {
            usuarioModel.CerrarSesion();//cierra la sesion
            return View();
        }

    }
}
