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

            var clientes = context.Database.SqlQuery<Models.Clientes>("select top 100 * from clientes").ToList();
            return View(clientes);
        }
        [HttpGet]
        public ActionResult Nuevo()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Nuevo(Models.Clientes Cliente)
        {
            if (ModelState.IsValid)
            {
                context.cliente.Add(Cliente);
                context.SaveChanges();
                return RedirectToAction("Lista");
            }
            return View();
            

           
        }

    }
}