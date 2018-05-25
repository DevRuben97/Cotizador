using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Cotizador.Models;
using Cotizador.Entitys;

namespace Cotizador.Controllers
{
    public class ClientesController : Controller
    {
        CotizadorContext context = new CotizadorContext();

        //Clientes Que Contiene El Sistema.
        public ActionResult Lista()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetClientes(DataTable data)//Obtener Todos Los 100 primeros clientes de la Base de Datos.
        {
            try
            {
                var val = "";


                return Json(new { });
            }
            catch(Exception ex)
            {
                return Json(new { Error = ex.Message });
            }
        }
        [HttpGet]
        public PartialViewResult Nuevo()
        {
            return PartialView();
        }
        [HttpPost]
        public JsonResult Nuevo(Clientes Cliente)
        {

            try
            {
                context.cliente.Add(Cliente);
                context.SaveChanges();
                return Json(new { Mensaje = "El Cliente Fue Creado Correctamente", Error = false });
            }
            catch(Exception ex)
            {
                return Json(new { Mensaje = "A Ocurrido Un Error: " + ex.Message, Error = true });
            }
           
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
                    return Json(new { Mensaje = "El Cliente Fue Eliminado Correctamente"});
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
        [HttpGet]
        public PartialViewResult Editar(int id)
        {
            var cliente = context.cliente.Find(id);
            if (cliente == null)
            {
                ViewBag.Mensaje = "No Se Pudo Encontrar el Cliente Solicitado";
            }
            return PartialView("_Editar",cliente);
        }
        [HttpPost]
        public JsonResult Editar(Models.Clientes model)
        {

            return Json(new { });
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