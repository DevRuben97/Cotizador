using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cotizador.Entitys;
using Cotizador.Models;

namespace Cotizador.Controllers
{
    [Security.UserFilter()]
    public class CotizadorController : Controller
    {
        CotizadorContext context = new CotizadorContext();

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
        [HttpGet]
        public ActionResult Nuevo()
        {
            return View();
        }
        public JsonResult Nuevo(string nombre)
        {
            try
            {

                return Json(new {Mensaje= "La Cotizacion fue Creada Correctamente",Error= false }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new {Mensaje= "Error Encontrado: "+ ex.Message, Error=true }, JsonRequestBehavior.AllowGet);
            }
        }
        
        public JsonResult BuscarClientes(string search)
        {// Buscar los Clientes A Seleccionar, para la cotizacion.
            try
            {
                var clientes = context.cliente.ToList()
                    .Where(x => x.nombre.ToLower().Contains(search.ToLower())).Select(x => x.nombre)
                    .Take(5).ToArray();

               if (clientes.Length <= 0)
                {
                    string[] error = { "Sin Resultados" };
                    return Json(new { Clientes = error }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Clientes = clientes }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                string[] error = { "Sin Resultados" };
                return Json(new {Clientes= error }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult FillClientData(string nombre)
        {
            try
            {
                var cliente = context.cliente.Find(nombre);
                if (cliente != null)
                {
                    return Json(new
                    {
                        TipoCliente = cliente.TipoCliente,
                        dni = cliente.DNI,
                        phone = cliente.telefono,
                        direccion = cliente.direccion,
                        Error=false

                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Error = true,Mensaje= "El Cliente No Fue Encontrado" });
                }
            }
            catch(Exception ex)
            {
                return Json(new { Error = true, Mensaje = "Error Encontrado: " + ex.Message });
            }
        }
    }
}