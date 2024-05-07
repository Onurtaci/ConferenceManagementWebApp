using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConferenceManagementWebApp.Migrations
{
    /// <inheritdoc />
    public partial class Columnnameseditedandsomespecificationsarechanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reviews_PaperId",
                table: "Reviews");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PaperId",
                table: "Reviews",
                column: "PaperId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Reviews_PaperId",
                table: "Reviews");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PaperId",
                table: "Reviews",
                column: "PaperId");
        }
    }
}
