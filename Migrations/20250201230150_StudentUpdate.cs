using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BridgeRTU.Migrations
{
    /// <inheritdoc />
    public partial class StudentUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FieldOfInterest",
                table: "Student",
                newName: "PersonalInterest");

            migrationBuilder.AddColumn<string>(
                name: "FieldOfStudy",
                table: "Student",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FieldOfStudy",
                table: "Student");

            migrationBuilder.RenameColumn(
                name: "PersonalInterest",
                table: "Student",
                newName: "FieldOfInterest");
        }
    }
}
