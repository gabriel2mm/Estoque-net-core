using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ajusteTabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produto_NotaFiscal_InvoiceId",
                table: "Produto");

            migrationBuilder.DropIndex(
                name: "IX_Produto_InvoiceId",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "Custo",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "QuantidadeEspecifica",
                table: "Produto");

            migrationBuilder.AddColumn<string>(
                name: "CodProduto",
                table: "Produto",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DepositoCode",
                table: "Deposito",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Custo total",
                table: "ControleProdutos",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "TransacaoProdutos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataDaTransicao = table.Column<DateTime>(nullable: false),
                    TipoTransacao = table.Column<int>(nullable: false),
                    CustoUnitario = table.Column<double>(nullable: false),
                    Quantidade = table.Column<int>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    WarehouseId = table.Column<Guid>(nullable: false),
                    InvoiceId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransacaoProdutos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransacaoProdutos_NotaFiscal_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "NotaFiscal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransacaoProdutos_Produto_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransacaoProdutos_Deposito_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Deposito",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransacaoProdutos_InvoiceId",
                table: "TransacaoProdutos",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TransacaoProdutos_ProductId",
                table: "TransacaoProdutos",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TransacaoProdutos_WarehouseId",
                table: "TransacaoProdutos",
                column: "WarehouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransacaoProdutos");

            migrationBuilder.DropColumn(
                name: "CodProduto",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "DepositoCode",
                table: "Deposito");

            migrationBuilder.DropColumn(
                name: "Custo total",
                table: "ControleProdutos");

            migrationBuilder.AddColumn<double>(
                name: "Custo",
                table: "Produto",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Guid>(
                name: "InvoiceId",
                table: "Produto",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuantidadeEspecifica",
                table: "Produto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Produto_InvoiceId",
                table: "Produto",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_NotaFiscal_InvoiceId",
                table: "Produto",
                column: "InvoiceId",
                principalTable: "NotaFiscal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
