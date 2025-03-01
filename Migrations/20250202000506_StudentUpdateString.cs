using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BridgeRTU.Migrations
{
    /// <inheritdoc />
    public partial class StudentUpdateString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PersonalInterest",
                table: "Student",
                newName: "PersonalInterests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PersonalInterests",
                table: "Student",
                newName: "PersonalInterest");
        }
    }
}
