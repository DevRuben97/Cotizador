using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Cotizador.Models
{
    public class Servicios
    {
        [Key]
        public int id { get; set; }
        [Required]
        [StringLength(20)]
        public string nombre { get; set; }
        [Required]
        [StringLength(40)]
        public string Descripcion { get; set; }
        [Required]
        public decimal Costo { get; set; }
        public List<DetalleCotizacion> detalles { get; set; }
    }
}