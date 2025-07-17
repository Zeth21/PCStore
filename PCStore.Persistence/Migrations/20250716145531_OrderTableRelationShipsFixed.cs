using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCStore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class OrderTableRelationShipsFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OrderProductLists_OrderId",
                table: "OrderProductLists");

            migrationBuilder.DropIndex(
                name: "IX_DiscountUsages_OrderId",
                table: "DiscountUsages");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProductLists_OrderId",
                table: "OrderProductLists",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountUsages_OrderId",
                table: "DiscountUsages",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OrderProductLists_OrderId",
                table: "OrderProductLists");

            migrationBuilder.DropIndex(
                name: "IX_DiscountUsages_OrderId",
                table: "DiscountUsages");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProductLists_OrderId",
                table: "OrderProductLists",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiscountUsages_OrderId",
                table: "DiscountUsages",
                column: "OrderId",
                unique: true);
        }
    }
}
