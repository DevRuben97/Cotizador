using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Cotizador.Models
{
    public class Usuarios
    {
        [Key]
        public int id { get; set; }
        [Required]
        [StringLength(30)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(30)]
        public string Apellido { get; set; }
        [Required]
        [StringLength(15)]
        public string Telefono { get; set; }
        [Required]
        [StringLength(50)]
        public string Direccion { get; set; }
        [Required]
        [StringLength(20)]
        public string Usuario { get; set; }
        [Required]
        [StringLength(20)]
        public string Clave { get; set; }
        [Required]
        [StringLength(20)]
        public string Rol { get; set; } //Rol de usuario: {Administrador,Consulta}

        public List<Factura> Facturas { get; set; }
        public List<Cotizaciones> Cotizaciones { get; set; }
    }
}