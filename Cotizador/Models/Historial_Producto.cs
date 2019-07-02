using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cotizador.Models
{
    public class Historial_Producto
    {
        public int id { get; set; }
        public int id_producto { get; set; }
        public int id_usuario { get; set; }
        public DateTime Fecha { get; set; }
        [NotMapped]
        public string Formated_Date => Fecha.Date.ToShortDateString();

        public int Cantidad_Modificada { get; set; } //Registra la cantidad de stock cambiado del producto
        [ForeignKey("id_producto")]
        public Producto Producto { get; set; }
        [ForeignKey("id_usuario")]
        public Usuarios Usuario { get; set; }

    }
}