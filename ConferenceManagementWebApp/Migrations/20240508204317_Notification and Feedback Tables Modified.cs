using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConferenceManagementWebApp.Migrations
{
    /// <inheritdoc />
    public partial class NotificationandFeedbackTablesModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_UserId",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Notifications",
                newName: "ReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                newName: "IX_Notifications_ReceiverId");

            migrationBuilder.AlterColumn<int>(
                name: "Recommendation",
                table: "Reviews",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_ReceiverId",
                table: "Notifications",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_ReceiverId",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "Notifications",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_ReceiverId",
                table: "Notifications",
                newName: "IX_Notifications_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "Recommendation",
                table: "Reviews",
                type: "int",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_UserId",
                table: "Notifications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
