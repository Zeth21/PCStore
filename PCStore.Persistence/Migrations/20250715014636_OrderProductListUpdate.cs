using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCStore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class OrderProductListUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StatusNameString",
                table: "StatusNames",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ProductOldTotalCost",
                table: "OrderProductLists",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductOldTotalCost",
                table: "OrderProductLists");

            migrationBuilder.AlterColumn<string>(
                name: "StatusNameString",
                table: "StatusNames",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
