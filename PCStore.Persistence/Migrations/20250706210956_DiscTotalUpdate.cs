using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCStore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DiscTotalUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DiscountTotal",
                table: "DiscountUsages",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountTotal",
                table: "CouponUsages",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountTotal",
                table: "DiscountUsages");

            migrationBuilder.DropColumn(
                name: "DiscountTotal",
                table: "CouponUsages");
        }
    }
}
