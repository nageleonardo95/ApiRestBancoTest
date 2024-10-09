using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiRestBancoTest.Migrations
{
    public partial class MigracionV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "strGenero",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "strGenero",
                table: "Clientes");
        }
    }
}
