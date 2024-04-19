using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConferenceManagementWebApp.Migrations
{
    /// <inheritdoc />
    public partial class EditSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Papers_AspNetUsers_AuthorId",
                table: "Papers");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "Papers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Papers_AspNetUsers_AuthorId",
                table: "Papers",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Papers_AspNetUsers_AuthorId",
                table: "Papers");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "Papers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Papers_AspNetUsers_AuthorId",
                table: "Papers",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
