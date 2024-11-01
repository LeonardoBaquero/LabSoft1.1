using GestionInventario.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionInventario.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<MovimientoInventario> MovimientosInventario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la relación Producto-Proveedor
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasOne(p => p.Proveedor)
                .WithMany(p => p.Productos)
                .HasForeignKey(p => p.ProveedorId)
                .IsRequired()  // Hace que la relación sea requerida
                .OnDelete(DeleteBehavior.Restrict);

                // Configurar las propiedades requeridas
                entity.Property(p => p.Nombre).IsRequired();
                entity.Property(p => p.Categoria).IsRequired();
                entity.Property(p => p.PrecioUnitario).HasColumnType("decimal(18,2)");
            });

            // Configuración del Proveedor
            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.Property(p => p.Nombre).IsRequired();
                entity.Property(p => p.Apellido).IsRequired();
                entity.Property(p => p.TipoDocumento).IsRequired();
                entity.Property(p => p.NumeroDocumento).IsRequired();
                entity.Property(p => p.Correo).IsRequired();
            });

            modelBuilder.Entity<MovimientoInventario>(entity =>
                {
                    entity.HasOne(m => m.Producto)
                    .WithMany()
                    .HasForeignKey(m => m.ProductoId)
                    .OnDelete(DeleteBehavior.Restrict);
                });

        }
    }
}