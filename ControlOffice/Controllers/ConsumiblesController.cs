using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlOffice.Controllers
{
    public class ConsumiblesController : Controller
    {
        public ConsumiblesController()
        {

        }
        //
        // GET: /Consumibles/
        public ActionResult Index()
        {
            return View();
        }


    }
}
