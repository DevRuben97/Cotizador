﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cotizador.Models
{
    public class DetalleCotizacion
    {
        [Key]
        public int id { get; set; }
        public int idservicio { get; set; }
        public int idcotizacion { get; set; }
        [Required]
        [StringLength(20)]
        public string producto { get; set; }
        [Required]
        [StringLength(30)]
        public string descripcion { get; set; }
        [Required]
        public int cantidad { get; set; }
        [Required]
        public decimal precioUnitario { get; set; }
        [ForeignKey("idservicio")]
        public Servicios Servicios { get; set; }
        [ForeignKey("idcotizacion")]
        public Cotizaciones cotizador { get; set; }
    }
}