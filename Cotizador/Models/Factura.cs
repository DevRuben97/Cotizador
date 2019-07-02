using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cotizador.Models
{
    public class Factura
    {
        [Key]
        public int id { get; set; }
        public int idcliente { get; set; }
        public int id_usuario { get; set; }
        public string Codigo { get; set; }  //Codigo de factura.
        public string Cotizacion { get; set; }//Codigo de cotizacion
        public int id_cotizacion { get; set; } //id_cotizacion
        public DateTime Fecha { get; set; }
        [NotMapped]
        public string FormatedDate => Fecha.ToShortDateString();
        [Required]
        [StringLength(25)]
        public string Forma_Pago { get; set; }
        public string NCF { get; set; }
        public decimal Impuestos { get; set; }
        public decimal Total { get; set; }
        [Required]
        [StringLength(20)]
        public string Estado { get; set; }

        //Llaves foraneas y propiedades de navegacion.
        [ForeignKey("idcliente")]
        public Clientes cliente { get; set; }
        [ForeignKey("id_usuario")]
        public Usuarios Usuario { get; set; }

        public List<Detalle_Factura> Detalles { get; set; }
    }
}