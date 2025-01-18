using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Discount.Grpc.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSaleEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaleEventProducts");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "SaleEvents");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "SaleEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SalePercent = table.Column<decimal>(type: "REAL", precision: 5, scale: 2, nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SaleEventProducts",
                columns: table => new
                {
                    SaleEventId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleEventProducts", x => new { x.SaleEventId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_SaleEventProducts_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleEventProducts_SaleEvents_SaleEventId",
                        column: x => x.SaleEventId,
                        principalTable: "SaleEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SaleEventProducts_ProductId",
                table: "SaleEventProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleEventProducts_SaleEventId",
                table: "SaleEventProducts",
                column: "SaleEventId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleEvents_Id",
                table: "SaleEvents",
                column: "Id");
        }
    }
}
