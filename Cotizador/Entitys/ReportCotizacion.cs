using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cotizador.Entitys
{
    public class ReportCotizacion
    {
        public decimal Total { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Expira { get; set; }
        public string Cliente { get; set; }
        public string Dirección { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Cedula { get; set; }
        public int Codigo { get; set; }
        public string Servicio { get; set; }
        public string Descripción { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }

    }
}