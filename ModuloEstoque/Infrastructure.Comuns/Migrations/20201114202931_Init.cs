using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stock.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Filial",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filial", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fornecedor",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: false)
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
                    Nome = table.Column<string>(maxLength: 20, nullable: true),
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
                name: "NotaFiscal",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    NumeroIdentificacao = table.Column<string>(nullable: true),
                    Emissao = table.Column<DateTime>(nullable: false),
                    Transacao = table.Column<DateTime>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    Tipo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotaFiscal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pagamentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Payment = table.Column<string>(nullable: true),
                    Card = table.Column<string>(nullable: true),
                    DueDate = table.Column<string>(nullable: true),
                    CCV = table.Column<string>(nullable: true),
                    Plots = table.Column<string>(nullable: true),
                    BilletNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CodProduto = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: false),
                    Unidade = table.Column<string>(nullable: false),
                    Medida = table.Column<string>(nullable: false),
                    Entrada = table.Column<DateTime>(nullable: false),
                    Saida = table.Column<DateTime>(nullable: false),
                    Preço = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Deposito",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DepositoCode = table.Column<string>(nullable: false),
                    BranchOfficeId = table.Column<Guid>(nullable: true),
                    DeTerceiros = table.Column<bool>(nullable: false),
                    LocationWareHouseId = table.Column<Guid>(nullable: true),
                    WareHousesID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposito", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deposito_Filial_BranchOfficeId",
                        column: x => x.BranchOfficeId,
                        principalTable: "Filial",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Deposito_Local_LocationWareHouseId",
                        column: x => x.LocationWareHouseId,
                        principalTable: "Local",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    codPedido = table.Column<string>(nullable: true),
                    nome = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    CPF = table.Column<string>(nullable: false),
                    LocationId = table.Column<Guid>(nullable: false),
                    PaymentId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedido_Local_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Local",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pedido_Pagamentos_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Pagamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoFornecedor",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(nullable: false),
                    ProviderId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoFornecedor", x => new { x.ProductId, x.ProviderId });
                    table.ForeignKey(
                        name: "FK_ProdutoFornecedor_Fornecedor_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Fornecedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdutoFornecedor_Produto_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoControle",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: true),
                    CustoMedio = table.Column<double>(nullable: false),
                    Custototal = table.Column<double>(name: "Custo total", nullable: false),
                    Quantidade = table.Column<int>(nullable: false),
                    WareHouseProductControlId = table.Column<Guid>(nullable: true),
                    ProductsControlWareHouseIDs = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoControle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProdutoControle_Produto_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProdutoControle_Deposito_WareHouseProductControlId",
                        column: x => x.WareHouseProductControlId,
                        principalTable: "Deposito",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransacaoProduto",
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
                    table.PrimaryKey("PK_TransacaoProduto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransacaoProduto_NotaFiscal_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "NotaFiscal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransacaoProduto_Produto_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransacaoProduto_Deposito_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Deposito",
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
                    cor = table.Column<string>(nullable: true),
                    quantidade = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vitrine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vitrine_ProdutoControle_ProductManagementId",
                        column: x => x.ProductManagementId,
                        principalTable: "ProdutoControle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Campanha",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ImageBanner = table.Column<string>(nullable: true),
                    ShowcaseId = table.Column<Guid>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campanha", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Campanha_Vitrine_ShowcaseId",
                        column: x => x.ShowcaseId,
                        principalTable: "Vitrine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PedidosVitrine",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(nullable: false),
                    ShowcaseId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidosVitrine", x => new { x.OrderId, x.ShowcaseId });
                    table.ForeignKey(
                        name: "FK_PedidosVitrine_Pedido_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Pedido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidosVitrine_Vitrine_ShowcaseId",
                        column: x => x.ShowcaseId,
                        principalTable: "Vitrine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Campanha_ShowcaseId",
                table: "Campanha",
                column: "ShowcaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Deposito_BranchOfficeId",
                table: "Deposito",
                column: "BranchOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Deposito_LocationWareHouseId",
                table: "Deposito",
                column: "LocationWareHouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_LocationId",
                table: "Pedido",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_PaymentId",
                table: "Pedido",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidosVitrine_ShowcaseId",
                table: "PedidosVitrine",
                column: "ShowcaseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoControle_ProductId",
                table: "ProdutoControle",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoControle_WareHouseProductControlId",
                table: "ProdutoControle",
                column: "WareHouseProductControlId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoFornecedor_ProviderId",
                table: "ProdutoFornecedor",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_TransacaoProduto_InvoiceId",
                table: "TransacaoProduto",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TransacaoProduto_ProductId",
                table: "TransacaoProduto",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TransacaoProduto_WarehouseId",
                table: "TransacaoProduto",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Vitrine_ProductManagementId",
                table: "Vitrine",
                column: "ProductManagementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Campanha");

            migrationBuilder.DropTable(
                name: "PedidosVitrine");

            migrationBuilder.DropTable(
                name: "ProdutoFornecedor");

            migrationBuilder.DropTable(
                name: "TransacaoProduto");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropTable(
                name: "Vitrine");

            migrationBuilder.DropTable(
                name: "Fornecedor");

            migrationBuilder.DropTable(
                name: "NotaFiscal");

            migrationBuilder.DropTable(
                name: "Pagamentos");

            migrationBuilder.DropTable(
                name: "ProdutoControle");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Deposito");

            migrationBuilder.DropTable(
                name: "Filial");

            migrationBuilder.DropTable(
                name: "Local");
        }
    }
}
