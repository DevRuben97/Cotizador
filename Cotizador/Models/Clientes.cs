using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Cotizador.Models
{
    public class Clientes
    {
        [Key]
        public int id { get; set; }
        [Required]
        [StringLength(20)]
        public string nombre { get; set; }
        [Required]
        [StringLength(20)]
        public string apellido { get; set; }
        [Required]
        [StringLength(20)]
        public string TipoCliente { get; set; }
        [Required]
        [StringLength(20)]
        public string TipoDNI { get; set; }
        [Required]
        [StringLength(20)]
        public string DNI { get; set; }
        [Required]
        [StringLength(40)]
        public string direccion { get; set; }
        [Required]
        [StringLength(20)]
        public string telefono { get; set; }
        [Required]
        [StringLength(20)]
        public string email { get; set; }

        public List<Cotizaciones> Cotizaciones;
    }
}