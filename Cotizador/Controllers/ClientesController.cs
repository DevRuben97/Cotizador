using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cotizador.Controllers
{
    public class ClientesController : Controller
    {
        Models.CotizadorContext context = new Models.CotizadorContext();

        //Clientes Que Contiene El Sistema.
        public ActionResult Lista()
        {
            var lista = context.cliente.ToList();
            return View(lista);
        }
        [HttpGet]
        public ActionResult Nuevo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Nuevo(Models.Clientes Cliente)
        {
            context.cliente.Add(Cliente);
            context.SaveChanges();

            return RedirectToAction("Lista");
        }

    }
}