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
        public DateTime Fecha { get; set; }
        public DateTime Expiracion { get; set; }
        [NotMapped]
        public string FormatedDate => Fecha.ToShortDateString();
        [NotMapped]
        public string FormatedExpiracion => Expiracion.ToShortDateString();
        [Required]
        public string Estado { get; set; }
        public decimal Total { get; set; }

        //Llaves foraneas y propiedades de navegacion.
        [ForeignKey("idcliente")]
        public Clientes cliente { get; set; }

        public List<DetalleCotizacion> Detalles { get; set; }
    }
}