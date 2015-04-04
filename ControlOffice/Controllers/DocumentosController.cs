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
        DocumentosEnviadosModel docEnv = new DocumentosEnviadosModel();
        DocumentosRecibidosModel docRec = new DocumentosRecibidosModel();
        DocumentosInternosModal docInt = new DocumentosInternosModal();

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

        [ProtegidoVista]
        public PartialViewResult Registro()
        {            
            ViewBag.NombreUsuario = usuario != null ? usuario.Nombre : "No definido";
            ViewBag.tiposDocumentosEnviados = docEnv.ObtenerTiposDocEnviados();
            ViewBag.tiposDocumentosEstandar = docInt.ObtenerTiposDocInternos();
            return PartialView();
        }

        [ProtegidoVista]
        public PartialViewResult InventarioDocumentosEnviados()
        {
            return PartialView();
        }

        [ProtegidoVista]
        public PartialViewResult InventarioDocumentosInternos()
        {
            return PartialView();
        }
        
        [ProtegidoVista]
        public PartialViewResult InventarioDocumentosRecibidos()
        {
            return PartialView();
        }

        public JsonResult registrarDocumentoEnviado(string destino = "", string asunto = "", int opcionEnvio = 0, int tipoDocumento = 0, DateTime fechaEnvio = new DateTime(), string horaEnvio = "",
             string crearFolio = "", string folio = "", string imagen = "")
        {
            try
            {
                string totalErrores = "";
                if (destino.Length <= 0)
                {
                    totalErrores += "<span class='glyphicon glyphicon-exclamation-sign' aria-hidden='true'> </span> Debes indicar para quien es este documento. <br />";
                }
                if (asunto.Length <= 0)
                {
                    totalErrores += "<span class='glyphicon glyphicon-exclamation-sign' aria-hidden='true'> </span> Ingresa una descripción o asunto para este documento. <br />";
                }
                if (opcionEnvio == 1) { fechaEnvio = DateTime.Now; }
                else if (opcionEnvio == 2)
                {
                    if (horaEnvio.Length <= 0) { horaEnvio = "00:00"; }
                    fechaEnvio = new DateTime(fechaEnvio.Year, fechaEnvio.Month, fechaEnvio.Day, Convert.ToInt32(horaEnvio.Substring(0, 2)), Convert.ToInt32(horaEnvio.Substring(3, 2)), 0);
                }

                if (crearFolio == "on")
                {
                    folio = DateTime.Now.Year + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
                    int f = docEnv.ObtenerUltimoFolio();
                    int conta = 20;//intentos de asignacion de folio
                    while (conta > 0)
                    {
                        if (docEnv.ExisteDocumentoEnviado(folio + f)) { f++; }
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
                    return Json(docEnv.RegistrarDocumentoEnviado(destino, asunto,tipoDocumento, fechaEnvio, folio, imagen, opcionEnvio , usuario.Usuario));
                }
            }
            catch (Exception ex)
            {
                return Json(new { response = false, mensaje = "Error inesperado del sistema, verifique la información. " + ex.Message });
            }
        }

        public JsonResult registrarDocumentoRecibido(string destinoRecibido = "", string asuntoRecibido = "", int opcionRecepcion = 0, DateTime fechaRecepcion = new DateTime(), string horaRecepcion = "",
             string crearFolioRecepcion = "", string folioRecepcion = "", string imagenRecepcion = "", DateTime fechaEnvioRecepcion = new DateTime(), string horaEnvioRecepcion = "", int opcionEnvio=0,
            int opcionLlegada = 0, DateTime fechaEnvioLlegada = new DateTime(), string horaEnvioLlegada = "",int requiereRespuesta=0, int opcionRequiereRespuesta =0,
             DateTime fechaEnvioRespuesta = new DateTime(), string horaEnvioRespuesta = "", string respuesta ="")
        {
            try
            {
                string totalErrores = "";
                if (destinoRecibido.Length <= 0)
                {
                    totalErrores += "<span class='glyphicon glyphicon-exclamation-sign' aria-hidden='true'> </span> Debes indicar para quien es este documento. <br />";
                }
                if (asuntoRecibido.Length <= 0)
                {
                    totalErrores += "<span class='glyphicon glyphicon-exclamation-sign' aria-hidden='true'> </span> Ingresa una descripción o asunto para este documento. <br />";
                }
                if (opcionRecepcion == 1) { fechaRecepcion = DateTime.Now; }
                else if (opcionRecepcion == 2)
                {
                    if (horaRecepcion.Length <= 0) { horaRecepcion = "00:00"; }
                    fechaRecepcion = new DateTime(fechaRecepcion.Year, fechaRecepcion.Month, fechaRecepcion.Day, Convert.ToInt32(horaRecepcion.Substring(0, 2)), Convert.ToInt32(horaRecepcion.Substring(3, 2)), 0);
                }

                if (crearFolioRecepcion == "on")
                {
                    folioRecepcion = DateTime.Now.Year + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
                    int f = docRec.ObtenerUltimoFolio();
                    int conta = 20;//intentos de asignacion de folio
                    while (conta > 0)
                    {
                        if (docRec.ExisteDocumentoRecibido(folioRecepcion + f)) { f++; }
                        else { folioRecepcion += f; conta = -1; }
                        conta--;
                    }
                    if (conta == 0)//fue imposible asignar el folio
                    {
                        totalErrores += "<span class='glyphicon glyphicon-exclamation-sign' aria-hidden='true'> </span> Error: Imposible asignar el folio automaticamente; Por favor, insegresa el folio manualmente <br />";
                    }
                }

                if (opcionEnvio == 1) { fechaEnvioRecepcion = DateTime.Now; }
                else if (opcionEnvio == 2)
                {
                    if (horaEnvioRecepcion.Length <= 0) { horaEnvioRecepcion = "00:00"; }
                    fechaEnvioRecepcion = new DateTime(fechaEnvioRecepcion.Year, fechaEnvioRecepcion.Month, fechaEnvioRecepcion.Day, Convert.ToInt32(horaEnvioRecepcion.Substring(0, 2)), Convert.ToInt32(horaEnvioRecepcion.Substring(3, 2)), 0);
                }

                if (opcionLlegada == 1) { fechaEnvioLlegada = DateTime.Now; }
                else if (opcionLlegada == 2)
                {
                    if (horaEnvioLlegada.Length <= 0) { horaEnvioLlegada = "00:00"; }
                    fechaEnvioLlegada = new DateTime(fechaEnvioLlegada.Year, fechaEnvioLlegada.Month, fechaEnvioLlegada.Day, Convert.ToInt32(horaEnvioLlegada.Substring(0, 2)), Convert.ToInt32(horaEnvioLlegada.Substring(3, 2)), 0);
                }

                if (requiereRespuesta == 1)
                {
                    if (opcionRequiereRespuesta == 1) { fechaEnvioRespuesta = DateTime.Now; }
                    else if (opcionRequiereRespuesta == 2)
                    {
                        if (horaEnvioRespuesta.Length <= 0) { horaEnvioRespuesta = "00:00"; }
                        fechaEnvioRespuesta = new DateTime(fechaEnvioRespuesta.Year, fechaEnvioRespuesta.Month, fechaEnvioRespuesta.Day, Convert.ToInt32(horaEnvioRespuesta.Substring(0, 2)), Convert.ToInt32(horaEnvioRespuesta.Substring(3, 2)), 0);
                    }
                }

                if (totalErrores.Length > 0)
                {
                    return Json(new { response = false, mensaje = totalErrores });
                }
                else
                {
                    return Json(docRec.RegistrarDocumentoRecibico(destinoRecibido, asuntoRecibido, opcionRecepcion, fechaRecepcion, folioRecepcion, imagenRecepcion,
                        fechaEnvioRecepcion, opcionEnvio,opcionLlegada,fechaEnvioLlegada,requiereRespuesta,opcionRequiereRespuesta, fechaEnvioRespuesta, respuesta, usuario.Usuario));
                }
            }
            catch (Exception ex)
            {
                return Json(new { response = false, mensaje = "Error inesperado del sistema, verifique la información. "  });
            }
        }

        public JsonResult registrarDocumentoEstandar(string asuntoEstandar = "", int tipoDocumentoEstandar = 0, string folioEstandar = "", string imagenEstandar = "")
        {
            try
            {
                string totalErrores = "";                
                if (asuntoEstandar.Length <= 0)
                {
                    totalErrores += "<span class='glyphicon glyphicon-exclamation-sign' aria-hidden='true'> </span> Ingresa una descripción o asunto para este documento. <br />";
                }
               
                
                if (totalErrores.Length > 0)
                {
                    return Json(new { response = false, mensaje = totalErrores });
                }
                else
                {
                    return Json(docInt.registrarDocumentoEstandar( asuntoEstandar, tipoDocumentoEstandar, folioEstandar, imagenEstandar, usuario.Usuario));
                }
            }
            catch (Exception ex)
            {
                return Json(new { response = false, mensaje = "Error inesperado del sistema, verifique la información. " + ex.Message });
            }
        }

        public JsonResult ObtenerTiposDocEnv()
        {
            string opciones = "<option value='0'>Selecciona una tipo de documento</option>";
            List<Tipos_documento> docs = docEnv.ObtenerTiposDocEnviados();
            foreach (Tipos_documento d in docs)
            {
                opciones += "<option value='" + d.Id_tipo_documento + "'>" + d.Tipo_documento + "</option>";
            }
            opciones = "<select class=' form-control col-sm-9' id='listaTipoDocumentos' name='tipoDocumento' style='border-color:white'>" + opciones + "</select>";

            return Json(new { response = true, mensaje = opciones });
        }

        public JsonResult registrarNuevoTipoDocEnv(string tipoDocumento="")
        {
            if (tipoDocumento.Length > 0)
            {
                return Json(docEnv.RegistrarNuevoTipoDocEnv(tipoDocumento));
            }
            else
            {
                return Json(new { response = false });
            }
        }

        public JsonResult ObtenerTiposDocInt()
        {
            string opciones = "<option value='0'>Selecciona una tipo de documento</option>";
            List<Tipos_documento_interno> docs = docInt.ObtenerTiposDocInternos();
            foreach (Tipos_documento_interno d in docs)
            {
                opciones += "<option value='" + d.Id_tipo_documento_interno + "'>" + d.Tipo_documento + "</option>";
            }
            opciones = "<select class=' form-control col-sm-9' id='listaTipoDocumentosEstandar' name='tipoDocumentoEstandar' style='border-color:white'>" + opciones + "</select>";

            return Json(new { response = true, mensaje = opciones });
        }

        public JsonResult registrarNuevoTipoDocInt(string tipoDocumento = "")
        {
            if (tipoDocumento.Length > 0)
            {
                return Json(docInt.RegistrarNuevoTipoDocInt(tipoDocumento));
            }
            else
            {
                return Json(new { response = false });
            }
        }

        public JsonResult listaDocumentosEnviados(JqGrid jq)
        {
            try
            {
                return Json(docEnv.ObtenerDocumentosEnviados(jq), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { response = false, mensaje = "Error: " + ex.Message });
            }
        }
        
        public JsonResult listaDocumentosRecibidos(JqGrid jq)
        {
            try
            {
                return Json(docRec.ObtenerDocumentosRecibidos(jq), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { response = false, mensaje = "Error: " + ex.Message });
            }
        }

        public JsonResult listaDocumentosInternos(JqGrid jq)
        {
            try
            {
                return Json(docInt.ObtenerDocumentosInternos(jq), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { response = false, mensaje = "Error: " + ex.Message });
            }
        }

        public JsonResult eliminarDocEnviado(string id)
        {
            if (usuario.Administrador)
            {
                return Json(docEnv.eliminar(id));
            }
            else
            {
                RespuestaModel respuesta = new RespuestaModel();
                respuesta.SetRespuesta(false);
                respuesta.alerta = "No tienes los permisos necesarios para realizar esta acción";
                return Json(respuesta);
            }
        }

        public JsonResult eliminarDocInterno(string id)
        {
            if(usuario.Administrador)
            {
             return Json(docInt.eliminar(id));
            }
            else
            {
                RespuestaModel respuesta = new RespuestaModel();
                respuesta.SetRespuesta(false);
                respuesta.alerta = "No tienes los permisos necesarios para realizar esta acción";
                return Json(respuesta);
            }
        }

        public JsonResult eliminarDocRecibido(string id)
        {
            if (usuario.Administrador)
            {
                return Json(docRec.eliminar(id));
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
