using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cotizador.Models
{
    public class Cotizaciones
    {
        [Key]
        public int id { get; set; }
        public int idcliente { get; set; }
        public int iddetalle { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public decimal Total { get; set; }
        [Required]
        [ForeignKey("idcliente")]
        public Clientes cliente { get; set; }
        [Required]
        public List<DetalleCotizacion> Detalles { get; set; }
    }
}