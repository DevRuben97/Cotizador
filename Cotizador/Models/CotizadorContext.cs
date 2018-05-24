using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Cotizador.Models
{
    public class CotizadorContext :DbContext
    {
       public DbSet<Servicios> servicio { get; set; }
       public DbSet<Clientes> cliente { get; set; }
       public DbSet<Cotizaciones> cotizacion { get; set; }
       public DbSet<DetalleCotizacion> detalleCoti { get; set; }

        public CotizadorContext() : base("ConectInfo")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configuraciones Adicionales A La Tabla.
            modelBuilder.Entity<Cotizaciones>().Property(x => x.Total).HasPrecision(5, 3);
            modelBuilder.Entity<Servicios>().Property(x => x.Costo).HasPrecision(5, 3);
            

            base.OnModelCreating(modelBuilder);
        }
    }
}