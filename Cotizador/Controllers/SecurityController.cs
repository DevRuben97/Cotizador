using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cotizador.Controllers
{
    //Obtener los accesos al sistema.
    public class SecurityController : Controller
    {
        
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult LoginOut()
        {
            return View();
        }
    }
}