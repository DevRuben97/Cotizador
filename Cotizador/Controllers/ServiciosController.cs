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
        [HttpPost]
        public JsonResult GetServices()
        {
            JsonResult jsonResult = new JsonResult();
            try
            {
                //Obtener Los Parametros Que Envia Datatable Al Servidor:
                string Buscador = Request.Form.GetValues("search[value]")[0];
                string draw = Request.Form.GetValues("draw")[0];
                string order = Request.Form.GetValues("order[0][column]")[0];
                string orderDir = Request.Form.GetValues("order[0][dir]")[0];
                int startRec = Convert.ToInt32(Request.Form.GetValues("start")[0]);
                int pageSize = Convert.ToInt32(Request.Form.GetValues("length")[0]);
                //Carga De Datos:
                var servicos = context.servicio.ToList();
                //Total De Registros:
                int totalRecords = servicos.Count();

                //Aplicar El Buscador Si Este Es Requerdido:
                if (!(string.IsNullOrEmpty(Buscador) || string.IsNullOrEmpty(order)))
                {
                   servicos= servicos.Where(x => x.nombre.ToLower().Contains(Buscador)
                    || x.Descripcion.ToLower().Contains(Buscador)
                    || x.Costo.Equals(Buscador)).ToList();
                }
                //Ordenar los datos en Base A Los Datos:
                if (!string.IsNullOrEmpty(order) && !string.IsNullOrEmpty(orderDir))
                {
                   if (order.Equals("0"))
                    {
                        if (orderDir.Equals("asc"))
                        {
                            servicos = servicos.OrderBy(x => x.nombre).ToList();
                        }
                        else
                        {
                            servicos = servicos.OrderByDescending(x => x.nombre).ToList();
                        }
                    }
                   else if (order.Equals("1"))
                    {
                        if (orderDir.Equals("asc"))
                        {
                            servicos = servicos.OrderBy(x => x.Descripcion).ToList();
                        }
                        else 
                        {
                            servicos = servicos.OrderByDescending(x => x.Descripcion).ToList();
                        }
                    }
                   else if (order.Equals("2"))
                    {
                        if (orderDir.Equals("asc"))
                        {
                            servicos = servicos.OrderBy(x => x.Costo).ToList();
                        }     
                        else
                        {
                            servicos = servicos.OrderByDescending(x => x.Costo).ToList();
                        }
                    }
                }

                int recFilter = servicos.Count();
                servicos.Skip(startRec).Take(pageSize);

                //Retornar Los Datos:
                jsonResult = this.Json(new
                {
                    draw = Convert.ToInt32(draw),
                    recordsTotal = totalRecords,
                    recordsFiltered = recFilter,
                    data = servicos
                }, JsonRequestBehavior.AllowGet);

                return jsonResult;

            }
            catch(Exception ex)
            {
                return Json(new { Error = ex.Message });
            }
        }
        [HttpGet]
        public PartialViewResult Nuevo()
        {
        
            return PartialView("Nuevo");
        }
        [HttpPost]
        public JsonResult Nuevo(Models.Servicios servicios)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.servicio.Add(servicios);
                    context.SaveChanges();
                    return Json(new { Mensaje = "Se Ha  Credado Correctamente El Servicio" });
                }
                else { return Json(new { Mensaje = "El Modelo Es Invalido" }); }
            }
            catch(Exception ex)
            {
                return Json(new { Mensaje = "Ha Ocurrido Un Error: " + ex.Message });
            }
            
        }

    }
}