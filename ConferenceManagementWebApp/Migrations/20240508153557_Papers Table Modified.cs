
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConferenceManagementWebApp.Migrations
{
    /// <inheritdoc />
    public partial class PapersTableModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Papers",
                newName: "Recommendation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Recommendation",
                table: "Papers",
                newName: "Status");
        }
    }
}

