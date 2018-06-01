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
        public JsonResult GetClientes(DataTable table)//Obtener Todos Los 100 primeros clientes de la Base de Datos.
        {
            try
            {
                var clientes = context.cliente.ToList();
                int TotalRecords = clientes.Count();
                int RecordsFiltered;
                
                //Buscando Datos:
                if (!string.IsNullOrEmpty(table.Search.value))
                {
                    clientes = clientes.Where(x => x.nombre.ToLower().Contains(table.Search.value.ToLower())
                     || x.DNI.ToLower().Equals(table.Search.value)).ToList();
                }
                //Ordenando Los Datos:
                switch (table.Order[0].column)
                {
                    case 0:
                        clientes = (table.Order[0].dir.Equals("asc")) ?
                        clientes.OrderBy(x => x.id).ToList() : clientes.OrderByDescending(x => x.id).ToList();
                        break;
                    case 1:
                        clientes = (table.Order[0].dir.Equals("asc")) ?
                        clientes.OrderBy(x => x.nombre).ToList() : clientes.OrderByDescending(x => x.nombre).ToList();
                        break;
                    case 2:
                        clientes = (table.Order[0].dir.Equals("asc")) ?
                        clientes.OrderBy(x => x.apellido).ToList() : clientes.OrderByDescending(x => x.apellido).ToList();
                        break;
                    case 3:
                        clientes = (table.Order[0].dir.Equals("asc")) ?
                        clientes.OrderBy(x => x.TipoCliente).ToList() : clientes.OrderByDescending(x => x.TipoCliente).ToList();
                        break;
                    case 4:
                        clientes = (table.Order[0].dir.Equals("asc")) ?
                        clientes.OrderBy(x => x.DNI).ToList() : clientes.OrderByDescending(x => x.DNI).ToList();
                        break;
                    case 5:
                        clientes = (table.Order[0].dir.Equals("asc")) ?
                        clientes.OrderBy(x => x.direccion).ToList() : clientes.OrderByDescending(x => x.direccion).ToList();
                        break;
                    case 6:
                        clientes = (table.Order[0].dir.Equals("asc")) ?
                        clientes.OrderBy(x => x.telefono).ToList() : clientes.OrderByDescending(x => x.telefono).ToList();
                        break;
                    case 7:
                        clientes = (table.Order[0].dir.Equals("asc")) ?
                        clientes.OrderBy(x => x.email).ToList() : clientes.OrderByDescending(x => x.email).ToList();
                        break;
                    default:
                        clientes = (table.Order[0].dir.Equals("asc")) ?
                        clientes.OrderBy(x => x.id).ToList() : clientes.OrderByDescending(x => x.id).ToList();
                        break;
                }

                clientes.Skip(table.start).Take(clientes.Count());
                RecordsFiltered = clientes.Count();

                return Json(new
                { draw=table.draw,recordsTotal= TotalRecords, recordsFiltered= RecordsFiltered,data= clientes }
                ,JsonRequestBehavior.AllowGet);


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
        [HttpPost]
        public JsonResult Eliminar(int id)//Confirmar la Eliminacacion
        {
            try
            {
                var cliente = context.cliente.Find(id);
                if (cliente == null)
                {
                    return Json(new { Mensaje = "No Se Ha Podido Encotrar El Cliente En la Base de datos", Error = true });
                }
                context.cliente.Remove(cliente);
                context.SaveChanges();
                return Json(new { Mensaje = "Cliente Eliminado Correctamente", Error = false });
            }
            catch(Exception ex)
            {
                return Json(new { Mensaje = "Error Encontrado: " + ex.Message, Error = true });
            }
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
        public JsonResult Editar(Clientes model)
        {
            try
            {
                context.Entry(model).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return Json(new { Mensaje = "El Cliente Fue Editado Correctamente", Error = false });
            }
            catch(Exception ex)
            {
                return Json(new { Mensaje = "Error Encontrado: " + ex.Message, Error= true });
            }
            
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