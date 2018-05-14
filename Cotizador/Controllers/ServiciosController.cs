using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Cotizador.Controllers
{
    public class ServiciosController : Controller
    {
        Models.CotizadorContext context = new Models.CotizadorContext();
        public ActionResult Lista()
        {
            return View();
        }
        public JsonResult GetList()
        {
            var Datos = context.servicio.ToList();
            return Json(new { data = Datos }, JsonRequestBehavior.AllowGet);
        }
    }
}