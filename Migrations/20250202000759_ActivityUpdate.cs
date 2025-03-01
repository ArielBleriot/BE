using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BridgeRTU.Migrations
{
    /// <inheritdoc />
    public partial class ActivityUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: 1,
                column: "Interest",
                value: "[\"Wellness\"]");

            migrationBuilder.UpdateData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: 2,
                column: "Interest",
                value: "[\"Technology\"]");

            migrationBuilder.UpdateData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: 3,
                column: "Interest",
                value: "[\"Photography\"]");

            migrationBuilder.UpdateData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: 4,
                column: "Interest",
                value: "[\"Cooking\"]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: 1,
                column: "Interest",
                value: "Wellness");

            migrationBuilder.UpdateData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: 2,
                column: "Interest",
                value: "Technology");

            migrationBuilder.UpdateData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: 3,
                column: "Interest",
                value: "Photography");

            migrationBuilder.UpdateData(
                table: "Activity",
                keyColumn: "Id",
                keyValue: 4,
                column: "Interest",
                value: "Cooking");
        }
    }
}
