using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Cotizador.Controllers
{
    [Security.UserFilter()]
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
                            servicos = servicos.OrderBy(x => x.id).ToList();
                        }
                        else
                        {
                            servicos = servicos.OrderByDescending(x => x.id).ToList();
                        }
                    }
                   else if (order.Equals("1"))
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
                   else if (order.Equals("2"))
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
                   else if (order.Equals("3"))
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
                    context.servicio.Add(servicios);
                    context.SaveChanges();
                    return Json(new { Mensaje = "Se Ha  Credado Correctamente El Servicio", Error=false });
               
            }
            catch(Exception ex)
            {
                return Json(new { Mensaje = "Ha Ocurrido Un Error: " + ex.Message, Error= true });
            }
            
        }
        [HttpGet]
        public PartialViewResult Editar(int id)
        {
            try
            {
                var servicio = context.servicio.Find(id);
                return PartialView("Editar",servicio);

            }
            catch(Exception ex)
            {
                ViewBag.Mensaje ="Error: " + ex.Message;
                return PartialView("Editar",null);
            }
        }
        [HttpPost]
        public JsonResult Editar(Models.Servicios servicios)
        {
            try
            {
                if (servicios== null)
                {
                    return Json(new { Mensaje = "Error Al Recibir los datos En El Servidor", Status = false });
                }
                else
                {
                    context.Entry(servicios).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    return Json(new { Mensaje = "Se Ha Editado El Registro Correctamente", Status = true });
                }
            }
            catch(Exception ex)
            {
                return Json(new { Mensaje = "Error: "+ ex.Message, Status = false });
            }
        }
        [HttpPost]
        public JsonResult Eliminar(int id)
        {
            try
            {
                var servicio = context.servicio.Find(id);
                if (servicio == null)
                {
                    return Json(new { Mensaje = "El Servicio No Fue Encontrado, Intente Otra Vez.", Status = false });
                }
                else
                {
                    context.servicio.Remove(servicio);
                    context.SaveChanges();
                    return Json(new { Mensaje = "El Servicio Fue Eliminado Correctamente", Status = true });
                }

            }
            catch( Exception ex)
            {
                return Json(new { Mensaje = "Error: " + ex.Message, Status = false });
            }
        }

    }
}