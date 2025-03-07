using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Discount.Grpc.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOncreatingModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductAffectedIds",
                table: "SaleEvents");

            migrationBuilder.AlterColumn<decimal>(
                name: "SalePercent",
                table: "SaleEvents",
                type: "REAL",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "Coupons",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Coupons",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

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
                name: "IX_SaleEvents_Id",
                table: "SaleEvents",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_Id",
                table: "Coupons",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SaleEventProducts_ProductId",
                table: "SaleEventProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleEventProducts_SaleEventId",
                table: "SaleEventProducts",
                column: "SaleEventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaleEventProducts");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropIndex(
                name: "IX_SaleEvents_Id",
                table: "SaleEvents");

            migrationBuilder.DropIndex(
                name: "IX_Coupons_Id",
                table: "Coupons");

            migrationBuilder.AlterColumn<decimal>(
                name: "SalePercent",
                table: "SaleEvents",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "REAL",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AddColumn<string>(
                name: "ProductAffectedIds",
                table: "SaleEvents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "Coupons",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Coupons",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "REAL");
        }
    }
}
