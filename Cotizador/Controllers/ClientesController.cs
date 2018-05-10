using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

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
        [HttpGet]
        public ActionResult Eliminar(int? id)//Confirmar la Eliminacacion
        {
            if (id== null)
            {
                ViewBag.Error = "Tienes Que Pasar Un ID";
            }
            var cliente = context.cliente.Find(id);

            if (cliente == null)
            {
                ViewBag.Error = "No Se Pudo Encontrar El Cliente Solicitado";
            }
            return PartialView(cliente);
        }
        [HttpPost]
        public JsonResult ConfDelete(int id)//Eliminar El Cliente seleccionado.
        {

            try
            {
                var Cliente = context.cliente.Find(id);

                if (Cliente == null)
                {
                    return Json(new { Mensaje = "No Se Pudo Encotrar el Cliente Solicitado" });
                }
                else
                {
                    context.cliente.Remove(Cliente);
                    context.SaveChanges();
                    return Json(new { Mensaje = "El Cliente Fue Eliminado Correctamente" });
                }
            }
            catch(Exception ex)
            {
                return Json(new { Mensaje = "Error Ocurrido: " + ex.Message });
            }

            
        }
        [HttpPost]
        public JsonResult Buscar(string nombre)
        {
           var contenido= context.cliente.ToList().Where(x => x.nombre.Equals(nombre)).ToList();
           
            return Json("");
        }
        public PartialViewResult Detalles(int id )
        {
           
                var cliente = context.cliente.Find(id);
                if (cliente == null)
                {
                    ViewBag.Error = "No Se Pudo Encontrar Los Datos Requeridos";
                }
                return PartialView("_PDetalles", cliente);
            
           
           
        }

    }
}