namespace Util
{
    public static class ManejadorDeCookies
    {
        private static string keyCookie = "key?for:cookie";
        /// <summary>
        /// Pregunta si un usuario ha iniciado sesion en el sistema
        /// </summary>
        /// <returns></returns>
        public static bool ExisteUsuarioEnSesion()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }

        public static void AgregarGalletita(string nombre, int expiraDias)
        {
            Tools tools = new Tools();
            string nomCookie = tools.EncriptarCadena(nombre, keyCookie);  
            //bool persiste = true;
            HttpCookie cookie = new HttpCookie(nomCookie);            
            cookie.Expires = DateTime.Now.AddDays(expiraDias);//expira en N dias meses
            
            //var ticket = FormsAuthentication.Decrypt(cookie.Value);//desencripta y crear un ticket
            ////creamos un ticket que contiene la cookie
            //var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, value);
            //cookie.Value = FormsAuthentication.Encrypt(newTicket);
            HttpContext.Current.Response.Cookies.Add(cookie);//agregamnos la nueva cookie
        }

        public static void EliminarGalletita(string nombre)
        {
            if (ExisteUsuarioEnSesion())
            {
                Tools tools = new Tools();
                string nomCookie = tools.EncriptarCadena(nombre, keyCookie);
                HttpCookie cookie = HttpContext.Current.Request.Cookies[nomCookie];
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        public static void AgregarValorEnGalletita(string nombreCookie, string key, string value)
        {
            Tools tools = new Tools();
            string nomCookie = tools.EncriptarCadena(nombreCookie, keyCookie);
            HttpCookie cookie = HttpContext.Current.Request.Cookies[nomCookie];
            cookie.Values.Add(tools.EncriptarCadena(key, keyCookie), tools.EncriptarCadena(value, keyCookie));
            HttpContext.Current.Response.Cookies.Add(cookie);//agreganos la nueva cookie
        }

        public static string ObtenerValorEnGalletita(string nombreCookie, string key)
        {
            string resp = null;
            Tools tools = new Tools();
            string nomCookie = tools.EncriptarCadena(nombreCookie, keyCookie);
            HttpCookie cookie = HttpContext.Current.Request.Cookies[nomCookie];
            resp = cookie.Values[tools.EncriptarCadena(key, keyCookie)];
            return tools.DesencriptarCadena(resp, keyCookie); 
        }

        public static HttpCookieCollection ObtenerFrascoDeGalletas()
        {
             return  HttpContext.Current.Request.Cookies;
            
        }

        ///// <summary>
        ///// Elimina toda la informacion de la sesion actual
        ///// En este caso solo hace que expire la cookie, ya que no es posible eliminarla directamente
        ///// </summary>
        //public static void EliminarSesion()
        //{
        //    if (ExisteUsuarioEnSesion())
        //    {
        //        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName);
        //        cookie.Expires = DateTime.Now.AddDays(-1);
        //        HttpContext.Current.Response.Cookies.Add(cookie);
        //    }
        //}

        ///// <summary>
        ///// Obtiene el usuario que actualmente esta logueado
        ///// </summary>
        ///// <returns></returns>
        //public static string ObtenerUsuarioEnSesion()
        //{
        //    string usuario = null;

        //    if (HttpContext.Current.User != null && HttpContext.Current.User.Identity is FormsIdentity)
        //    {
        //        //Obtengo entonces la cookie
        //        FormsAuthenticationTicket ticket = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket;
        //        if (ticket != null)//si hay ticket
        //        {
        //            usuario = ticket.UserData;
        //        }
        //    }
        //    return usuario;
        //}

        //public void ObtenerValor(string key, string value, int expiraDias)
        //{
        //    string valor = null;

        //    if (HttpContext.Current.User != null && HttpContext.Current.User.Identity is FormsIdentity)
        //    {
        //        //Obtengo entonces la cookie
        //        FormsAuthenticationTicket ticket = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket;
        //        if (ticket != null)//si hay ticket
        //        {
        //            usuario = ticket.UserData;
        //        }
        //    }
        //    return usuario;
        //}

        //public static void AgregarUsuarioEnSesion(string pk)
        //{
        //    bool persiste = true;
        //    var cookie = FormsAuthentication.GetAuthCookie("usuario", persiste);//creamos la cookie
        //    cookie.Name = FormsAuthentication.FormsCookieName;//nombre de la cookie, este es configurado
        //    cookie.Expires = DateTime.Now.AddMonths(3);//expira en tres meses

        //    var ticket = FormsAuthentication.Decrypt(cookie.Value);//desencripta y crear un ticket
        //    //creamos un ticket que contiene la cookie
        //    var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, ticket.IsPersistent, pk);
        //    cookie.Value = FormsAuthentication.Encrypt(newTicket);
        //    HttpContext.Current.Response.Cookies.Add(cookie);//agregamnos la nuevva cookie
        //}



    }
}
