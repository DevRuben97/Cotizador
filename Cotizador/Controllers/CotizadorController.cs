using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cotizador.Entitys;
using Cotizador.Models;
using System.Data.Entity;

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
        public PartialViewResult Detalle()
        {
            return PartialView();
        }
        [HttpPost]
        public JsonResult GetCotizaciones(DataTable Table)// Obtener Todas las Cotizaciones Del Sistema.
        {
            try
            {
                var cotizador = context.cotizacion.Include(l => l.cliente).ToList();
                int TotalRecords = cotizador.Count();
                int RecordsFiltered;
                
                //Buscar Datos:
                if (!string.IsNullOrEmpty(Table.Search.value))
                {
                    
                }

                //Ordenando los datos
                switch (Table.Order[0].column)
                {
                    case 0:
                        cotizador = (Table.Order[0].dir.Equals("asc")) ?
                            cotizador.OrderBy(x => x.id).ToList() : cotizador.OrderByDescending(x => x.id).ToList();
                        break;
                    case 1:
                        cotizador = (Table.Order[0].dir.Equals("asc")) ?
                             cotizador.OrderBy(x => x.cliente.nombre).ToList() : cotizador.OrderByDescending(x => x.cliente.nombre).ToList();
                        break;
                    case 3:
                        cotizador = (Table.Order[0].dir.Equals("asc")) ?
                            cotizador.OrderBy(x => x.Fecha).ToList() : cotizador.OrderByDescending(x => x.Fecha).ToList();
                        break;
                    case 4:
                        cotizador = (Table.Order[0].dir.Equals("asc")) ?
                            cotizador.OrderBy(x => x.Total).ToList() : cotizador.OrderByDescending(x => x.Total).ToList();
                        break;

                }

                cotizador.Skip(Table.start).Take(cotizador.Count());
                RecordsFiltered = cotizador.Count();

                return Json(new {
                    draw = Table.draw,
                    recordsTotal =TotalRecords,
                    recordsFiltered =RecordsFiltered,
                    data =cotizador
                }, JsonRequestBehavior.AllowGet);
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
        [HttpPost]
        public JsonResult Nuevo(string Ncliente ,string Fecha,decimal Total, DetalleCotizacion[] Detalle)
        {
            try
            {
                
                Clientes Client = context.cliente.ToList()
                    .Where(x => x.nombre.ToLower().Equals(Ncliente.ToLower())).First();
                Total+= 0.00m;

                if (Client!= null)
                {
                    Cotizaciones cotizador = new Cotizaciones
                    {
                        idcliente = Client.id,
                        Total = Total,
                        Fecha = Fecha,
                        Estado = "REALIZADO"
                        
                    };


                    context.cotizacion.Add(cotizador);
                    context.SaveChanges();
                    int CotiID = cotizador.id;


                    for (int i = 0; i < Detalle.Length; i++)
                    {
                        Detalle[i].idcotizacion = CotiID;
                        Detalle[i].PrecioCotizacion += 0.00m;
                        context.detalleCoti.Add(Detalle[i]);
                    }

                    context.SaveChanges();

                    return Json(new { Mensaje = "La Cotizacion fue Creada Correctamente", Error = false }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(new
                    { Mensaje = "Error: No Se Encontro el Cliente En la Base De Datos", Error = false }, JsonRequestBehavior.AllowGet);
                }
                


                
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
        [HttpPost]
        public JsonResult DatosCliente(string nombre)
        {
            try
            {
                var cliente = context.Database.SqlQuery<Clientes>("select * from Clientes where" +
                    " nombre= @nombre", new System.Data.SqlClient.SqlParameter("@nombre", nombre)).ToArray()[0];

                if (cliente != null)
                {
                    return Json(new
                    {
                        TipoCliente = cliente.TipoCliente,
                        dni = cliente.DNI,
                        phone = cliente.telefono,
                        direccion = cliente.direccion,
                        Error = false

                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Error = true, Mensaje = "El Cliente No Fue Encontrado" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Mensaje = "Error Encontrado: " + ex.Message });
            }
        }
        public PartialViewResult ListaServicios()
        {

            return PartialView();
        }
    }
}