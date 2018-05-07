using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cotizador.Controllers
{
    public class CotizadorController : Controller
    {
        Models.CotizadorContext context = new Models.CotizadorContext();

        public ActionResult Lista() // Lista de Contizaciones Hechas.
        {
            var lista = context.Database.SqlQuery<Models.Cotizaciones>("select * from Cotizaciones").ToList();
            return View(lista);
        }
        public ActionResult Cotizacion()
        {

            return View();
        }
    }
}