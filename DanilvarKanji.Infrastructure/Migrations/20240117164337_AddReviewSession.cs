using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DanilvarKanji.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReviewSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReviewSessionId",
                table: "Exercises",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ReviewSessions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    AppUserId = table.Column<string>(type: "text", nullable: false),
                    ReviewDataTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewSessions_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_ReviewSessionId",
                table: "Exercises",
                column: "ReviewSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewSessions_AppUserId",
                table: "ReviewSessions",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_ReviewSessions_ReviewSessionId",
                table: "Exercises",
                column: "ReviewSessionId",
                principalTable: "ReviewSessions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_ReviewSessions_ReviewSessionId",
                table: "Exercises");

            migrationBuilder.DropTable(
                name: "ReviewSessions");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_ReviewSessionId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "ReviewSessionId",
                table: "Exercises");
        }
    }
}
