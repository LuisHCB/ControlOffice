using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Util;
using Modelos;
using Entidades;
using ControlOffice.CustomAttributes;

namespace ControlOffice.Controllers
{
    public class DocumentosController : Controller
    {

        Usuarios usuario;

        /// <summary>
        /// Constructor del controlador
        /// </summary>
        public DocumentosController()
        {
            UsuarioModel usuarioModel = new UsuarioModel();
            //Obtenemos usuario actual
            this.usuario = usuarioModel.ObtenerUsuario(ManejadorDeSesiones.ObtenerUsuarioEnSesion());            
        }

        //
        // GET: /Documentos/
        [Protegido]
        public ActionResult Index()
        {
            ViewBag.NombreUsuario = usuario != null ? usuario.Nombre : "Usuario";
            return View();
        }

    }
}
