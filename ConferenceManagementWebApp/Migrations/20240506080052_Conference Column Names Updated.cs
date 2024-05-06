using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConferenceManagementWebApp.Migrations
{
    /// <inheritdoc />
    public partial class ConferenceColumnNamesUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Conferences",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "Conferences",
                newName: "EndDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Conferences",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Conferences",
                newName: "EndTime");
        }
    }
}
