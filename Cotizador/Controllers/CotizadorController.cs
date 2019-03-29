using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cotizador.Entitys;
using Cotizador.Models;
using System.Data.Entity;
using Microsoft.Reporting.WebForms;
using System.IO;


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
        [HttpGet]
        public PartialViewResult Detalle(int CotID)//Devolver el detalle correspondiente 
        {

            var detalle = context.detalleCoti.Where(x => x.idcotizacion.Equals(CotID))
                .Include(c => c.Servicios).ToList();
            return PartialView(detalle);
        }
        [HttpPost]
        public JsonResult GetCotizaciones(DataTable Table)// Obtener Todas las Cotizaciones Del Sistema.
        {
            try
            {
                var cotizador = new List<Cotizaciones>();
                int TotalRecords;
                int RecordsFiltered;
                
                //Buscar Datos:
                if (!string.IsNullOrEmpty(Table.Search.value)){

                    var fechas = Table.Search.value.Split('/');
                    var Desde = Convert.ToDateTime(fechas[0]);
                    var Hasta = Convert.ToDateTime(fechas[1]);
                    cotizador = context.cotizacion.Where(x => x.Fecha>= Desde && x.Fecha <=Hasta.Date)
                        .Include(x => x.cliente).ToList();
                }
                else
                {
                    cotizador= context.cotizacion.Include(l => l.cliente).ToList();
                }
                TotalRecords = cotizador.Count();
                //Ordenando los datos
                switch (Table.Order[0].column)
                {
                    case 0:
                        cotizador = (Table.Order[0].dir.Equals("asc")) ?
                            cotizador.OrderBy(x => x.id).ToList() : cotizador.OrderByDescending(x => x.id).ToList();
                        break;
                    case 1:
                        cotizador = (Table.Order[1].dir.Equals("asc")) ?
                             cotizador.OrderBy(x => x.cliente.nombre).ToList() : cotizador.OrderByDescending(x => x.cliente.nombre).ToList();
                        break;
                    case 3:
                        cotizador = (Table.Order[2].dir.Equals("asc")) ?
                            cotizador.OrderBy(x => x.Fecha).ToList() : cotizador.OrderByDescending(x => x.Fecha).ToList();
                        break;
                    case 4:
                        cotizador = (Table.Order[3].dir.Equals("asc")) ?
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
            catch(Exception ex)
            {
                var errorList = new List<Cotizaciones>();
                return Json(new
                {
                    draw = Table.draw,
                    recordsTotal = errorList.Count,
                    recordsFiltered = errorList.Count,
                    data = errorList
                }, JsonRequestBehavior.AllowGet);
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
                    //Obtener la fecha de la cotizacion convirtiendo el string en un datetime.
                    var dateData = Fecha.Split('/');
                    DateTime Date = new DateTime(Convert.ToInt32(dateData[2]),
                        Convert.ToInt32(dateData[1]), Convert.ToInt32(dateData[0]));

                    Cotizaciones cotizador = new Cotizaciones
                    {
                        idcliente = Client.id,
                        Total = Total,
                        Fecha = Date,
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
        [HttpPost]
        public JsonResult Anular(int Id)
        {
            try
            {
                var model = context.cotizacion.Where(x => x.id == Id).First();
                if(model!= null)
                {
                    model.Estado = "ANULADO";
                    context.Entry(model).State = EntityState.Modified;
                    context.SaveChanges();
                    
                    return Json(new { Mensaje = "Se Anulo la Cotización correctamente", Error = false }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Mensaje = "No se Encontro la Cotización Seleccionada", Error = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                return Json(new { Mensaje = "Error Encontrado: "+ ex.Message, Error = true }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Report(int id)// Devolver el pdf del Reporte de Cotizacion.
        {
            LocalReport report = new LocalReport();
            List<Entitys.ReportCotizacion> DataLists = new List<ReportCotizacion>();
            string path = Path.Combine(Server.MapPath("~/Reports"), "Coti.rdlc");

            if (System.IO.File.Exists(path))
            {
                report.ReportPath = path;
            }
            else
            {
                return RedirectToAction("Lista");
            }
            //Solicitar los datos al servidor por medio de un procedimiento almacenado
            DataLists = context.Database.SqlQuery<ReportCotizacion>("EXEC ReportCotizacion @idcotizacion",
               new System.Data.SqlClient.SqlParameter("@idcotizacion", id)).ToList();

           ReportDataSource dataSource = new ReportDataSource("CotiData", DataLists);
           var parameter = new ReportParameter("Impuesto", Convert.ToString((DataLists[0].Total * 0.18m)),true);
           report.DataSources.Add(dataSource);
           //report.SetParameters(parameter);
            

            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension= "pdf";
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = report.Render(
                reportType,
                "",
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            return File(renderedBytes, "application/pdf","Cotizacion-" + DataLists[0].Fecha+".pdf");
        }
    }
}