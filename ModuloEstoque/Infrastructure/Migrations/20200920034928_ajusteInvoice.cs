using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ajusteInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "NotaFiscal",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantidade",
                table: "NotaFiscal",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "NotaFiscal");

            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "NotaFiscal");
        }
    }
}
