using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class sc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "ProductsProviders",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(nullable: false),
                    ProviderId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsProviders", x => new { x.ProductId, x.ProviderId });
                    table.ForeignKey(
                        name: "FK_ProductsProviders_Fornecedor_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Fornecedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsProviders_Produto_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vitrine",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductManagementId = table.Column<Guid>(nullable: true),
                    descrição = table.Column<string>(nullable: true),
                    imagem = table.Column<string>(nullable: true),
                    cor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vitrine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vitrine_ControleProdutos_ProductManagementId",
                        column: x => x.ProductManagementId,
                        principalTable: "ControleProdutos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductsProviders_ProviderId",
                table: "ProductsProviders",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Vitrine_ProductManagementId",
                table: "Vitrine",
                column: "ProductManagementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductsProviders");

            migrationBuilder.DropTable(
                name: "Vitrine");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Fornecedor",
                type: "uniqueidentifier",
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
    }
}
