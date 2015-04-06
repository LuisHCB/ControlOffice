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
        SolicitudesModel sm = new SolicitudesModel();

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

        [ProtegidoVista]
        public PartialViewResult Registro()
        {
            ViewBag.TiposConsumibles = cm.ObtenerTiposConsumibles();
            ViewBag.NombreUsuario = usuario != null ? usuario.Nombre : "No definido";
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

        [ProtegidoVista]        
        public JsonResult RegistrarSolicitud(int envio, string destino = "", string descripcion = "", DateTime fechaEnvio = new DateTime(), string horaEnvio = "",
            string crearFolio = "", string folio = "", string imagen = "")
        {
            try
            {
                string totalErrores = "";
                if (destino.Length <= 0)
                {
                    totalErrores += "<span class='glyphicon glyphicon-exclamation-sign' aria-hidden='true'> </span> Debes indicar para quien es esta solicitud. <br />";
                }
                if (descripcion.Length <= 0)
                {
                    totalErrores += "<span class='glyphicon glyphicon-exclamation-sign' aria-hidden='true'> </span> Ingresa una descripción para esta solicitud. <br />";
                }
                if (envio == 1) { fechaEnvio = DateTime.Now; }
                else if (envio == 2)
                {
                    if (horaEnvio.Length <= 0) { horaEnvio = "00:00"; }
                    fechaEnvio = new DateTime(fechaEnvio.Year, fechaEnvio.Month, fechaEnvio.Day, Convert.ToInt32(horaEnvio.Substring(0, 2)), Convert.ToInt32(horaEnvio.Substring(3, 2)), 0);
                }

                if (crearFolio == "on")
                {
                    folio = DateTime.Now.Year + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
                    int f = sm.ObtenerUltimoFolio();
                    int conta = 20;//intentos de asignacion de folio
                    while (conta > 0)
                    {
                        if (sm.ExisteSolicitud(folio + f)) { f++; }
                        else { folio += f; conta = -1; }
                        conta--;
                    }
                    if (conta == 0)//fue imposible asignar el folio
                    {
                        totalErrores += "<span class='glyphicon glyphicon-exclamation-sign' aria-hidden='true'> </span> Error: Imposible asignar el folio automaticamente; Por favor, insegresa el folio manualmente <br />";
                    }
                }
                if (totalErrores.Length > 0)
                {
                    return Json(new { response = false, mensaje = totalErrores });
                }
                else
                {
                    return Json(cm.RegistrarSolicitud(destino, descripcion, fechaEnvio, folio, imagen, envio, usuario.Usuario));
                }
            }
            catch (Exception ex)
            {
                return Json(new { response = false, mensaje = "Error inesperado del sistema, verifique la información. " + ex.Message });
            }
        }

        [ProtegidoVista]
        public JsonResult RegistrarConsumible(string cantidad = "", int tipo = 0, string clave = "", int recibido = 0, DateTime fechaRecepcion = new DateTime(),
            string horaRecepcion = "00:00", string entregado = "", string archivo = "")
        {
            try
            {
                string totalErrores = "";                
                if (tipo == 0)
                {
                    totalErrores += "<span class='glyphicon glyphicon-exclamation-sign' aria-hidden='true'> </span> Debes seleccionar un tipo de consumible <br />";
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

                if (recibido == 1) { fechaRecepcion = DateTime.Now; }
                else if (recibido == 2) {
                    if (horaRecepcion.Length <= 0) { horaRecepcion = "00:00"; }
                    fechaRecepcion = new DateTime(fechaRecepcion.Year, fechaRecepcion.Month, fechaRecepcion.Day, Convert.ToInt32(horaRecepcion.Substring(0, 2)), Convert.ToInt32(horaRecepcion.Substring(3, 2)), 0); }

                if (totalErrores.Length > 0)
                {
                    return Json(new { response = false, mensaje = totalErrores });
                }
                else
                {//guarda la informacion
                    // if (ManejadorDeSesiones.ExisteUsuarioEnSesion())
                    {
                        return Json(cm.RegistrarConsumible(Convert.ToInt32(cantidad), tipo, clave, recibido, fechaRecepcion, entregado, archivo, usuario.Usuario));
                    }
                    // else
                    //{//Debe iniciar sesion, tal vez se ingreso aqui con una copia de la pagina o en cache                    
                    //  RespuestaModel respuesta = new RespuestaModel();
                    //  respuesta.SetRespuesta(false,"Primero debes iniciar sesión");
                    //  respuesta.href = "controloffice/index";
                    //  return Json(respuesta);
                    // }
                }
            }
            catch (Exception ex)
            {
                return Json(new { response = false, mensaje = "Error inesperado del sistema, verifique la información. " + ex.Message });
            }

        }

        [Protegido]
        public JsonResult RegistrarTipoConsumible(string nuevoTipo = "")
        {

            if (nuevoTipo.Length <= 0)
            {
                return Json(new { Response = false, mensaje = "<span class='glyphicon glyphicon-exclamation-sign' aria-hidden='true'> </span> Ingresa el nombre del nuevo tipo de consumible que quieres registrar. <br />" });
            }
            else
            {
                RespuestaModel respuesta = cm.RegistrarTipoConsumible(nuevoTipo);
                respuesta.funcion = "actualizarTipos()";
                return Json(respuesta);
            }
        }

        public JsonResult ObtenerTipos()
        {
            string opciones = "<option value='0'>Seleccionar</option>";
            List<Tipo_consumible> tipos = cm.ObtenerTipos();
            foreach (Tipo_consumible t in tipos)
            {
                opciones += "<option value='" + t.Id_tipo_consumible + "'>" + t.Nombre + "</option>";
            }
            opciones = "<select class='form-control' id='FORM-entrega_LB-tipo' name='tipo' style='border-color:white' required>" + opciones + "</select>";

            return Json(new { response = true, mensaje = opciones });
        }

        [ProtegidoVista]        
        public JsonResult listaConsumibles(JqGrid jq)
        {
            try
            {
                return Json(cm.ObtenerTodosLosConsumibles(jq), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { response = false, mensaje = "Error: " + ex.Message });
            }
        }

        [ProtegidoVista]        
        public JsonResult listaSolicitudConsumibles(JqGrid jq)
        {
            return Json(cm.ObtenerSolicitudesConsumibles(jq), JsonRequestBehavior.AllowGet);
        }

        [ProtegidoVista]
        [SoloAdministrador]
        public JsonResult eliminarSolicitud(string id)
        {
            if(usuario.Administrador)
            {
             return Json(sm.eliminarSolicitud(id));
            }
            else
            {
                RespuestaModel respuesta = new RespuestaModel();
                respuesta.SetRespuesta(false);
                respuesta.alerta = "No tienes los permisos necesarios para realizar esta acción";
                return Json(respuesta);
            }
        }

        [ProtegidoVista]
        [SoloAdministrador]
        public JsonResult eliminar(string id)
        {
            if(usuario.Administrador)
            {
             return Json(cm.eliminar(id));
            }
            else
            {
                RespuestaModel respuesta = new RespuestaModel();
                respuesta.SetRespuesta(false);
                respuesta.alerta = "No tienes los permisos necesarios para realizar esta acción";
                return Json(respuesta);
            }
        }
    }
}
