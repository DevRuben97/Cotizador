using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Cotizador.Models
{
    public class CotizadorContext :DbContext
    {
       public DbSet<Producto> Producto { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Clientes> Cliente { get; set; }
        public DbSet<Factura> Factura { get; set; }
        public DbSet<Detalle_Factura> Detalle_Factura { get; set; }
        public DbSet<Cotizaciones> Cotizacion { get; set; }
       public DbSet<DetalleCotizacion> Detalle_Cotizacion { get; set; }
       public DbSet<Usuarios> Usuario { get; set; }
       public DbSet<Historial_Producto> historial_Producto { get; set; }

        public CotizadorContext() : base("ConectInfo")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configuraciones Adicionales A La Tabla.
            modelBuilder.Entity<Cotizaciones>().Property(x => x.Total).HasPrecision(18, 2);
            modelBuilder.Entity<Producto>().Property(x => x.Precio).HasPrecision(18, 2);
            modelBuilder.Entity<DetalleCotizacion>().Property(x => x.PrecioCotizacion).HasPrecision(5, 3);
            

            //Configuracion de la tabla cotizaciones
            modelBuilder.Entity<Cotizaciones>().Property(x => x.Fecha).HasColumnType("date");
            modelBuilder.Entity<Cotizaciones>().Property(x => x.Estado).HasColumnType("varchar").HasMaxLength(20).IsRequired();

            //Cofiguracion de la tabla Detalle Cotizacion
            modelBuilder.Entity<DetalleCotizacion>().Property(x => x.PrecioCotizacion).HasPrecision(18, 2);

            //Configuracion de la tabla de facturas:
            modelBuilder.Entity<Factura>().Property(x => x.Impuestos).HasPrecision(18, 2);
            modelBuilder.Entity<Factura>().Property(x => x.Total).HasPrecision(18, 2);
            modelBuilder.Entity<Detalle_Factura>().Property(x => x.Precio_facturado).HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);
        }
    }
}