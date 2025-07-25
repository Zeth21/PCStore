using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCStore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NotificationFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_NotificationUserId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_OrderStatuses_OrderId",
                table: "OrderStatuses");

            migrationBuilder.AlterColumn<string>(
                name: "NotificationUserId",
                table: "Notifications",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NotificationContent",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NotificationTitle",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatuses_OrderId_StatusNameId",
                table: "OrderStatuses",
                columns: new[] { "OrderId", "StatusNameId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_NotificationUserId",
                table: "Notifications",
                column: "NotificationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_NotificationUserId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_OrderStatuses_OrderId_StatusNameId",
                table: "OrderStatuses");

            migrationBuilder.DropColumn(
                name: "NotificationTitle",
                table: "Notifications");

            migrationBuilder.AlterColumn<string>(
                name: "NotificationUserId",
                table: "Notifications",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "NotificationContent",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatuses_OrderId",
                table: "OrderStatuses",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_NotificationUserId",
                table: "Notifications",
                column: "NotificationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
