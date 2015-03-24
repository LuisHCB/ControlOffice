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
    public class ConsumiblesController : Controller
    {
        Usuarios usuario;
        ConsumiblesModel cm = new ConsumiblesModel();

        /// <summary>
        /// Constructor del controlador
        /// </summary>
        public ConsumiblesController()
        {
            UsuarioModel usuarioModel = new UsuarioModel();
            //Obtenemos usuario actual
            this.usuario = usuarioModel.ObtenerUsuario(ManejadorDeSesiones.ObtenerUsuarioEnSesion());            
        }


        //
        // GET: /Consumibles/
        [Protegido]
        public ActionResult Index()
        {
            ViewBag.NombreUsuario = usuario != null ? usuario.Nombre : "Usuario";
            return View();
        }

       // [ProtegidoVista]
        public PartialViewResult Registro()
        {
            ViewBag.TiposConsumibles = cm.ObtenerTiposConsumibles();            
            return PartialView();
        }

        [ProtegidoVista]
        public PartialViewResult Inventario()
        {
            return PartialView();
        }

        [ProtegidoVista]
        public PartialViewResult InventarioSolicutides()
        {
            return PartialView();
        }

       /* [Protegido]
        public JsonResult RegistrarConsumible(string cantidad = "", int tipo = 0, string clave = "", utilizado=0, DateTime fechaRecepcion = new DateTime(),
            string horaRecepcion ="00:00", string entregado ="",string recibido="", string archivo="")
        {
            string totalErrores = "";
            int tiempoReemplazo = 0;
            if (tipo == 0)
            {
                totalErrores += "<span class='glyphicon glyphicon-exclamation-sign' aria-hidden='true'> </span> Debes seleccionar un tipo de electrónico <br />";
            }
            if (cantidad.Length <= 0)
            {
                totalErrores += "<span class='glyphicon glyphicon-exclamation-sign' aria-hidden='true'> </span> Debes ingresar una cantidad <br />";
            }
            else
            {
                try
                {
                    Convert.ToInt32(cantidad);
                }
                catch
                {
                    totalErrores += "<span class='glyphicon glyphicon-exclamation-sign' aria-hidden='true'> </span> Debes ingresar una cantidad valida <br />";
                }
            }
            if (reemplazo.Length > 0)
            {
                try
                {
                    Convert.ToInt32(cantidad);
                }
                catch
                {
                    totalErrores += "<span class='glyphicon glyphicon-exclamation-sign' aria-hidden='true'> </span> Debes ingresar un número valido como tiempo de reemplazo <br />";
                }
            }
            if (utilizado == 1) { FechaUso = DateTime.Now; }

            if (totalErrores.Length > 0)
            {
                return Json(new { response = false, mensaje = totalErrores });
            }
            else
            {//guarda la informacion
                // if (ManejadorDeSesiones.ExisteUsuarioEnSesion())
                {
                    if (reemplazo.Length > 0)
                    {
                        tiempoReemplazo = Convert.ToInt32(reemplazo);
                        if (unidadReemplazo == 1) { tiempoReemplazo *= 30; }
                        else if (unidadReemplazo == 2) { tiempoReemplazo *= 365; }
                    }
                    return Json(em.RegistrarElectronico(tipo, Convert.ToInt32(cantidad), marca, serie, FechaUso, utilizado, tiempoReemplazo, usuario.Usuario));
                }
               // else
                //{//Debe iniciar sesion, tal vez se ingreso aqui con una copia de la pagina o en cache                    
                  //  RespuestaModel respuesta = new RespuestaModel();
                  //  respuesta.SetRespuesta(false,"Primero debes iniciar sesión");
                  //  respuesta.href = "controloffice/index";
                  //  return Json(respuesta);
               // }
            }



        }*/

    }
}
