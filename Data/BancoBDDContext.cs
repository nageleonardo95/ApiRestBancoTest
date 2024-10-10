using ApiRestBancoTest.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiRestBancoTest.Data
{
    public class BancoBDDContext : DbContext
    {
        public BancoBDDContext(DbContextOptions<BancoBDDContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Map las entidades 
            modelBuilder.Entity<Cliente>().ToTable("Clientes");
            modelBuilder.Entity<Cuenta>().ToTable("Cuentas");
            modelBuilder.Entity<Movimiento>().ToTable("Movimientos");

            // Configurar las PK
            modelBuilder.Entity<Cliente>().HasKey(c => c.intClienteId);
            modelBuilder.Entity<Cuenta>().HasKey(c => c.intCuentaId);
            modelBuilder.Entity<Movimiento>().HasKey(m => m.intMovimientoId);

            // Configurar relacion
            modelBuilder.Entity<Cuenta>()
                .HasOne(c => c.Cliente)
                .WithMany()
                .HasForeignKey(c => c.intClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Movimiento>()
                .HasOne(m => m.Cuenta)
                .WithMany()
                .HasForeignKey(m => m.intCuentaId)
                .OnDelete(DeleteBehavior.Cascade);


        }

    }
}


