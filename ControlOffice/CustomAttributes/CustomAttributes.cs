using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Routing;
using Util;
using Entidades;
using Modelos;


namespace ControlOffice.CustomAttributes
{
    
    /// <summary>
    /// Declara mi propia definicion para un anotation
    /// En este caso solo permite acceso por ajax
    /// </summary>
    public class SoloAjax : ActionFilterAttribute //hereda
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAjaxRequest())//Es una peticion ajax??
            {
                filterContext.HttpContext.Response.Write("Accesos no permitido");
                //aqui redireccionar a una pagina de error
                filterContext.HttpContext.Response.End();
            }

            //base.OnActionExecuted(filterContext);
        }
    }


    /// <summary>
    /// Protege la acción de usuarios no registrados
    /// </summary>
    public class Protegido : ActionFilterAttribute //hereda
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (ManejadorDeSesiones.ExisteUsuarioEnSesion())
            {
                base.OnActionExecuted(filterContext);
            }
            else //pide que ingrese al sistema primero
            {                
                HttpContext.Current.Response.Redirect("/controlOffice", true);
            }
        }
    }

    public class SoloAdministradorParcial : ActionFilterAttribute //hereda
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (ManejadorDeSesiones.ExisteUsuarioEnSesion())
            {               
                string us = ManejadorDeSesiones.ObtenerUsuarioEnSesion();
                using (var context = new DBControlOfficeContext() )
                {
                    Usuarios usuario = context.Usuarios.Where(x =>
                                                               x.Usuario == us).FirstOrDefault();
                    if (usuario != null)
                    {
                        if (usuario.Administrador == false)
                        {   
                            //aqui redireccionar a una pagina de error                            
                            HttpContext.Current.Response.Redirect("/controlOffice/denegado", true);
                            filterContext.HttpContext.Response.End();
                        }
                    }
                    else
                    {
                        
                    }
                }
                base.OnActionExecuted(filterContext);
            }
            else //pide que ingrese al sistema primero
            {
                HttpContext.Current.Response.Redirect("/controlOffice", true);
            }
        }
    }

    public class SoloAdministrador : ActionFilterAttribute //hereda
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (ManejadorDeSesiones.ExisteUsuarioEnSesion())
            {
                string us = ManejadorDeSesiones.ObtenerUsuarioEnSesion();
                using (var context = new DBControlOfficeContext())
                {
                    Usuarios usuario = context.Usuarios.Where(x =>
                                                               x.Usuario == us).FirstOrDefault();
                    if (usuario != null)
                    {
                        if (usuario.Administrador == false)
                        {
                            //aqui redireccionar a una pagina de error                            
                            HttpContext.Current.Response.Redirect("/controlOffice/denegadoV", true);                           
                            filterContext.HttpContext.Response.End();
                        }
                    }
                    else
                    {

                    }
                }
                base.OnActionExecuted(filterContext);
            }
            else //pide que ingrese al sistema primero
            {
                HttpContext.Current.Response.Redirect("/controlOffice", true);
            }
        }
    }
    
    public class ProtegidoVista : ActionFilterAttribute //hereda
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (ManejadorDeSesiones.ExisteUsuarioEnSesion())
            {
                base.OnActionExecuted(filterContext);
            }
            else //pide que ingrese al sistema primero
            {
                HttpContext.Current.Response.Redirect("/controlOffice/inicioSesion", true);
            }
        }
    }
}