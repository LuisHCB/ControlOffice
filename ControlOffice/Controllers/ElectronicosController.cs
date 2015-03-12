using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entidades;
using Modelo;
using Util;
using ControlOffice.CustomAttributes;

namespace ControlOffice.Controllers
{
    public class ElectronicosController : Controller
    {
        Usuarios usuario;

        /// <summary>
        /// Constructor del controlador
        /// </summary>
        public ElectronicosController()
        {
            UsuarioModel usuarioModel = new UsuarioModel();
            //Obtenemos usuario actual
            this.usuario = usuarioModel.ObtenerUsuario(ManejadorDeSesiones.ObtenerUsuarioEnSesion());            
        }

        //
        // GET: /Electronicos/
        [Protegido]
        public ActionResult Index()
        {
            ViewBag.NombreUsuario = usuario != null ? usuario.Nombre : "Usuario";
            return View();
        }



    }
}
