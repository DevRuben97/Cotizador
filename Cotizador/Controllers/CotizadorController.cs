using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cotizador.Entitys;

namespace Cotizador.Controllers
{
    [Security.UserFilter()]
    public class CotizadorController : Controller
    {
        Models.CotizadorContext context = new Models.CotizadorContext();

        public ActionResult Lista() // Lista de Contizaciones Hechas.
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetCotizaciones(DataTable Table)// Obtener Todas las Cotizaciones Del Sistema.
        {
            try
            {

                return Json(new { }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { },JsonRequestBehavior.AllowGet);
            }

        }
    }
}