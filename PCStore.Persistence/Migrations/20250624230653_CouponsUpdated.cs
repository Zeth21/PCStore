using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCStore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CouponsUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CouponUsageTime",
                table: "CouponUsages");

            migrationBuilder.RenameColumn(
                name: "CouponUsageId",
                table: "CouponUsages",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "CouponCode",
                table: "Coupons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CouponMinOrderAmount",
                table: "Coupons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Coupons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CouponCode",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "CouponMinOrderAmount",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Coupons");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CouponUsages",
                newName: "CouponUsageId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CouponUsageTime",
                table: "CouponUsages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
