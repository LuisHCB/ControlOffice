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
            return PartialView();
        }

        [ProtegidoVista]
        public PartialViewResult Inventario()
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

        public JsonResult ObtenerTiposDocEnv()
        {
            string opciones = "<option value='0'>Selecciona una tipo de documento</option>";
            List<Tipos_documento> docs = docEnv.ObtenerTiposDocEnviados();
            foreach (Tipos_documento d in docs)
            {
                opciones += "<option value='" + d.Id_tipo_documento + "'>" + d.Tipo_documento + "</option>";
            }
            opciones = "<select class=' form-control col-sm-9' id='listaTipoDocumentos' name='marca' style='border-color:white'>" + opciones + "</select>";

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

    }
}
