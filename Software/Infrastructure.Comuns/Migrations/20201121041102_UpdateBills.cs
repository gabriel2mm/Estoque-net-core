using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stock.Infrastructure.Migrations
{
    public partial class UpdateBills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "tipoOperacao",
                table: "TransaçãoContas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Discount",
                table: "ContasAReceber",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Fine",
                table: "ContasAReceber",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Interest",
                table: "ContasAReceber",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Guid>(
                name: "NewTitleId",
                table: "ContasAReceber",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Discount",
                table: "ContasAPagar",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Fine",
                table: "ContasAPagar",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Interest",
                table: "ContasAPagar",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_ContasAReceber_NewTitleId",
                table: "ContasAReceber",
                column: "NewTitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContasAReceber_ContasAReceber_NewTitleId",
                table: "ContasAReceber",
                column: "NewTitleId",
                principalTable: "ContasAReceber",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContasAReceber_ContasAReceber_NewTitleId",
                table: "ContasAReceber");

            migrationBuilder.DropIndex(
                name: "IX_ContasAReceber_NewTitleId",
                table: "ContasAReceber");

            migrationBuilder.DropColumn(
                name: "tipoOperacao",
                table: "TransaçãoContas");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "ContasAReceber");

            migrationBuilder.DropColumn(
                name: "Fine",
                table: "ContasAReceber");

            migrationBuilder.DropColumn(
                name: "Interest",
                table: "ContasAReceber");

            migrationBuilder.DropColumn(
                name: "NewTitleId",
                table: "ContasAReceber");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "ContasAPagar");

            migrationBuilder.DropColumn(
                name: "Fine",
                table: "ContasAPagar");

            migrationBuilder.DropColumn(
                name: "Interest",
                table: "ContasAPagar");
        }
    }
}
