using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fornecedor",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Local",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(maxLength: 20, nullable: false),
                    UF = table.Column<string>(maxLength: 2, nullable: true),
                    Cidade = table.Column<string>(nullable: true),
                    Bairro = table.Column<string>(nullable: true),
                    Endereço = table.Column<string>(maxLength: 50, nullable: true),
                    CEP = table.Column<string>(maxLength: 8, nullable: true),
                    Numero = table.Column<int>(nullable: false),
                    Complemento = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Local", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    Unidade = table.Column<string>(nullable: false),
                    Medida = table.Column<string>(nullable: false),
                    ProviderId = table.Column<Guid>(nullable: false),
                    Entrada = table.Column<DateTime>(nullable: false),
                    Saida = table.Column<DateTime>(nullable: false),
                    Preço = table.Column<double>(nullable: false),
                    Custo = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produto_Fornecedor_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Fornecedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Deposito",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DeTerceiros = table.Column<bool>(nullable: false),
                    LocationWareHouseId = table.Column<Guid>(nullable: true),
                    WareHousesID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposito", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deposito_Local_LocationWareHouseId",
                        column: x => x.LocationWareHouseId,
                        principalTable: "Local",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotaFiscal",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    NumeroIdentificacao = table.Column<string>(nullable: true),
                    Emissao = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: true),
                    Tipo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotaFiscal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotaFiscal_Produto_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ControleProdutos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: true),
                    CustoMedio = table.Column<double>(nullable: false),
                    Quantidade = table.Column<int>(nullable: false),
                    WareHouseProductControlId = table.Column<Guid>(nullable: true),
                    ProductsControlWareHouseIDs = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControleProdutos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ControleProdutos_Produto_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ControleProdutos_Deposito_WareHouseProductControlId",
                        column: x => x.WareHouseProductControlId,
                        principalTable: "Deposito",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ControleProdutos_ProductId",
                table: "ControleProdutos",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ControleProdutos_WareHouseProductControlId",
                table: "ControleProdutos",
                column: "WareHouseProductControlId");

            migrationBuilder.CreateIndex(
                name: "IX_Deposito_LocationWareHouseId",
                table: "Deposito",
                column: "LocationWareHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_NotaFiscal_ProductId",
                table: "NotaFiscal",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_ProviderId",
                table: "Produto",
                column: "ProviderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ControleProdutos");

            migrationBuilder.DropTable(
                name: "NotaFiscal");

            migrationBuilder.DropTable(
                name: "Deposito");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Local");

            migrationBuilder.DropTable(
                name: "Fornecedor");
        }
    }
}
