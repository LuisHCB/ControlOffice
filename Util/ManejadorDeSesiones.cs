using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Configuration;
using System.Web.UI.HtmlControls;


namespace Util
{
    public class ManejadorDeSesiones
    {
        /// <summary>
        /// Pregunta si un usuario ha iniciado sesion en el sistema
        /// </summary>
        /// <returns></returns>
        public static bool ExisteUsuarioEnSesion()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }

        /// <summary>
        /// Elimina toda la informacion de la sesion actual
        /// En este caso solo hace que expire la cookie, ya que no es posible eliminarla directamente
        /// </summary>
        public static void EliminarSesion()
        {
            if (ExisteUsuarioEnSesion())
            {
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName);
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        /// <summary>
        /// Obtiene el usuario que actualmente esta logueado
        /// </summary>
        /// <returns></returns>
        public static string ObtenerUsuarioEnSesion()
        {
            string usuario = null;

            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity is FormsIdentity)
            {
                //Obtengo entonces la cookie
                FormsAuthenticationTicket ticket = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket;
                if (ticket != null)//si hay ticket
                {
                    usuario = ticket.UserData;
                }
            }
             return usuario;
        }

        public static void AgregarUsuarioEnSesion(string pk)
        {
            bool persiste = true;
            var cookie = FormsAuthentication.GetAuthCookie("usuario",persiste);//creamos la cookie
            cookie.Name = FormsAuthentication.FormsCookieName;//nombre de la cookie, este es configurado
            cookie.Expires = DateTime.Now.AddMonths(3);//expira en tres meses

            var ticket = FormsAuthentication.Decrypt(cookie.Value);//desencripta y crear un ticket
            //creamos un ticket que ocntiene la cookie
            var newTicket = new FormsAuthenticationTicket(ticket.Version,ticket.Name,ticket.IssueDate,ticket.Expiration,ticket.IsPersistent,pk);
            cookie.Value = FormsAuthentication.Encrypt(newTicket);
            HttpContext.Current.Response.Cookies.Add(cookie);//agregamnos la nuevva cookie
        }


    }
}
