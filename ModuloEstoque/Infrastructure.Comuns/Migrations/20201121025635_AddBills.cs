using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stock.Infrastructure.Migrations
{
    public partial class AddBills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContasAPagar",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    InvoiceId = table.Column<Guid>(nullable: true),
                    DueDate = table.Column<DateTime>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    BeneficiaryNumber = table.Column<int>(nullable: false),
                    Beneficiary = table.Column<string>(nullable: true),
                    NewTitleId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContasAPagar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContasAPagar_NotaFiscal_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "NotaFiscal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContasAPagar_ContasAPagar_NewTitleId",
                        column: x => x.NewTitleId,
                        principalTable: "ContasAPagar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContasAReceber",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    InvoiceId = table.Column<Guid>(nullable: true),
                    DueDate = table.Column<DateTime>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    NumeroSacado = table.Column<int>(nullable: false),
                    Sacado = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContasAReceber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContasAReceber_NotaFiscal_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "NotaFiscal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransaçãoContas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataDaTransacao = table.Column<DateTime>(nullable: false),
                    TipoTransacao = table.Column<int>(nullable: false),
                    codTransacao = table.Column<string>(nullable: true),
                    Valor = table.Column<double>(nullable: false),
                    Juros = table.Column<double>(nullable: false),
                    Multa = table.Column<double>(nullable: false),
                    Disconto = table.Column<double>(nullable: false),
                    BillsToReceiveId = table.Column<Guid>(nullable: true),
                    BillsToPayId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransaçãoContas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransaçãoContas_ContasAPagar_BillsToPayId",
                        column: x => x.BillsToPayId,
                        principalTable: "ContasAPagar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransaçãoContas_ContasAReceber_BillsToReceiveId",
                        column: x => x.BillsToReceiveId,
                        principalTable: "ContasAReceber",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContasAPagar_InvoiceId",
                table: "ContasAPagar",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ContasAPagar_NewTitleId",
                table: "ContasAPagar",
                column: "NewTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_ContasAReceber_InvoiceId",
                table: "ContasAReceber",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TransaçãoContas_BillsToPayId",
                table: "TransaçãoContas",
                column: "BillsToPayId");

            migrationBuilder.CreateIndex(
                name: "IX_TransaçãoContas_BillsToReceiveId",
                table: "TransaçãoContas",
                column: "BillsToReceiveId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransaçãoContas");

            migrationBuilder.DropTable(
                name: "ContasAPagar");

            migrationBuilder.DropTable(
                name: "ContasAReceber");
        }
    }
}
