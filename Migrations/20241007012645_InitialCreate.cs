using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiRestBancoTest.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    intClienteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    intContraseña = table.Column<int>(type: "int", nullable: false),
                    blEstado = table.Column<bool>(type: "bit", nullable: false),
                    strNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    intEdad = table.Column<int>(type: "int", nullable: false),
                    intIdentificacion = table.Column<int>(type: "int", nullable: false),
                    strDireccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    intelefono = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.intClienteId);
                });

            migrationBuilder.CreateTable(
                name: "Cuentas",
                columns: table => new
                {
                    intCuentaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    intNumeroCuenta = table.Column<int>(type: "int", nullable: false),
                    strTipoCuenta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    intSaldoInicial = table.Column<int>(type: "int", nullable: false),
                    blEstadoCuenta = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuentas", x => x.intCuentaId);
                });

            migrationBuilder.CreateTable(
                name: "Movimientos",
                columns: table => new
                {
                    intMovimientoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dtimeFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    strTipoMovimiento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    intValor = table.Column<int>(type: "int", nullable: false),
                    intSaldo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimientos", x => x.intMovimientoId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Cuentas");

            migrationBuilder.DropTable(
                name: "Movimientos");
        }
    }
}
