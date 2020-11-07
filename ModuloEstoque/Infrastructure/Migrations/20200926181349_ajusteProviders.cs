using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ajusteProviders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produto_Fornecedor_ProviderId",
                table: "Produto");

            migrationBuilder.DropIndex(
                name: "IX_Produto_ProviderId",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "ProviderId",
                table: "Produto");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Fornecedor",
                newName: "Nome");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Fornecedor",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedor_ProductId",
                table: "Fornecedor",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fornecedor_Produto_ProductId",
                table: "Fornecedor",
                column: "ProductId",
                principalTable: "Produto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fornecedor_Produto_ProductId",
                table: "Fornecedor");

            migrationBuilder.DropIndex(
                name: "IX_Fornecedor_ProductId",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Fornecedor");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Fornecedor",
                newName: "Name");

            migrationBuilder.AddColumn<Guid>(
                name: "ProviderId",
                table: "Produto",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Produto_ProviderId",
                table: "Produto",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_Fornecedor_ProviderId",
                table: "Produto",
                column: "ProviderId",
                principalTable: "Fornecedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
