using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Cotizador.Models
{
    public class Categoria
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(30)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }

        public List<Producto> Productos { get; set; }
    }
}