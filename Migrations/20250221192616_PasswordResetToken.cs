using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BridgeRTU.Migrations
{
    /// <inheritdoc />
    public partial class PasswordResetToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Project_ProjectId",
                table: "Comment");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Comment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "PasswordResetToken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordResetToken", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Project_ProjectId",
                table: "Comment",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Project_ProjectId",
                table: "Comment");

            migrationBuilder.DropTable(
                name: "PasswordResetToken");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Comment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Project_ProjectId",
                table: "Comment",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id");
        }
    }
}
