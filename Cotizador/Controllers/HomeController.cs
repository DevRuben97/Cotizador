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
        [Security.UserFilter()]
        public ActionResult Index()
        {
            var datos = (Models.Usuarios)Session["UserInfo"];
            ViewBag.User = datos.Usuario;
            ViewBag.Nombre = datos.Nombre;
            ViewBag.Apellido = datos.Apellido;
            return View();
        }
        
    }
}