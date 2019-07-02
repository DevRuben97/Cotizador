using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cotizador.Models
{
    public class Detalle_Factura
    {
        [Key]
        public int id { get; set; }
        public int idproducto { get; set; }
        public int idfactura { get; set; }
        [Required]
        public int cantidad { get; set; } //Cantidad a facturar.
        [Required]
        public decimal Precio_facturado { get; set; }
        public int descuento { get; set; }
        [ForeignKey("idproducto")]
        public Producto Servicios { get; set; }
        [ForeignKey("idfactura")]
        public Factura  factura { get; set; }
    }
}