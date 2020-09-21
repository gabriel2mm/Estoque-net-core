using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AjustesGerais : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotaFiscal_Produto_ProductId",
                table: "NotaFiscal");

            migrationBuilder.DropIndex(
                name: "IX_NotaFiscal_ProductId",
                table: "NotaFiscal");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "NotaFiscal");

            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "NotaFiscal");

            migrationBuilder.AddColumn<Guid>(
                name: "InvoiceId",
                table: "Produto",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuantidadeEspecifica",
                table: "Produto",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produto_NotaFiscal_InvoiceId",
                table: "Produto");

            migrationBuilder.DropIndex(
                name: "IX_Produto_InvoiceId",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "QuantidadeEspecifica",
                table: "Produto");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "NotaFiscal",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantidade",
                table: "NotaFiscal",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_NotaFiscal_ProductId",
                table: "NotaFiscal",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotaFiscal_Produto_ProductId",
                table: "NotaFiscal",
                column: "ProductId",
                principalTable: "Produto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
