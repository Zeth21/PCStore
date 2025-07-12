using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCStore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class OrderProductPriceUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductCost",
                table: "OrderProductLists");

            migrationBuilder.AddColumn<decimal>(
                name: "ProductPrice",
                table: "OrderProductLists",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProductTotalCost",
                table: "OrderProductLists",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductPrice",
                table: "OrderProductLists");

            migrationBuilder.DropColumn(
                name: "ProductTotalCost",
                table: "OrderProductLists");

            migrationBuilder.AddColumn<int>(
                name: "ProductCost",
                table: "OrderProductLists",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
