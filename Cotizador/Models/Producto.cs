using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cotizador.Models
{
    /// <summary>
    /// Productos del negocio
    /// </summary>
    public class Producto
    {
        [Key]
        public int id { get; set; }
        [Required]
        [StringLength(20)]
        public string Codigo { get; set; }
        public string nombre { get; set; }
        [Required]
        [StringLength(40)]
        public string Descripcion { get; set; }
        [Required]
        public decimal Precio { get; set; }
        [Required]
        public decimal Costo { get; set; }
        [Required]
        public bool Gravado { get; set; }
        [Required]
        [StringLength(20)]
        public string Tipo_Producto { get; set; } //{Servicio, Articulo}
        public int Stock { get; set; } //Cantidad de stock requerido (Si es un articulo)
        [Required]
        public string ImageString { get; set; }
        [ForeignKey("Categoria")]
        public int ID_Categoria { get; set; }
        public Categoria Categoria { get; set; }
        public List<DetalleCotizacion> Cotizaciones { get; set; }
        public List<Detalle_Factura>   Facturas { get; set; }
    }
}