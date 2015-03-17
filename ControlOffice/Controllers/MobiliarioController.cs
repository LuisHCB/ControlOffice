using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Modelos;
using Entidades;
using Util;
using ControlOffice.CustomAttributes;

namespace ControlOffice.Controllers
{
    public class MobiliarioController : Controller
    {
        Usuarios usuario;

        /// <summary>
        /// Constructor del controlador
        /// </summary>
        public MobiliarioController()
        {
            UsuarioModel usuarioModel = new UsuarioModel();
            //Obtenemos usuario actual
            this.usuario = usuarioModel.ObtenerUsuario(ManejadorDeSesiones.ObtenerUsuarioEnSesion());            
        }


        //
        // GET: /Mobiliario/
        [Protegido]
        public ActionResult Index()
        {
            ViewBag.NombreUsuario = usuario != null ? usuario.Nombre : "Usuario";
            return View();
        }

    }
}
