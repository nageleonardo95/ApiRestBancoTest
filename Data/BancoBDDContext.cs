using ApiRestBancoTest.Models;
using Microsoft.AspNetCore.Mvc;
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

            modelBuilder.Entity<Cliente>().HasKey(c => c.intClienteId);
            modelBuilder.Entity<Cuenta>().HasKey(c => c.intCuentaId);
            modelBuilder.Entity<Movimiento>().HasKey(c => c.intMovimientoId);
        }
    }
}

