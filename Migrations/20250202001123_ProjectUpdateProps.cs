using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BridgeRTU.Migrations
{
    /// <inheritdoc />
    public partial class ProjectUpdateProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Interests",
                table: "Project",
                newName: "FieldOfStudy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FieldOfStudy",
                table: "Project",
                newName: "Interests");
        }
    }
}
