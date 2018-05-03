using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cotizador.Controllers
{
    public class HomeController : Controller
    {
        //Controlador de Inicio de la Aplicación.
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        
    }
}