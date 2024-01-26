using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DanilvarKanji.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeWordMeaningsStringsDefinitions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WordMeanings_Words_WordId",
                table: "WordMeanings");

            migrationBuilder.DropIndex(
                name: "IX_WordMeanings_WordId",
                table: "WordMeanings");

            migrationBuilder.DropColumn(
                name: "WordId",
                table: "WordMeanings");

            migrationBuilder.AddColumn<string>(
                name: "WordId",
                table: "StringDefinitions",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StringDefinitions_WordId",
                table: "StringDefinitions",
                column: "WordId");

            migrationBuilder.AddForeignKey(
                name: "FK_StringDefinitions_Words_WordId",
                table: "StringDefinitions",
                column: "WordId",
                principalTable: "Words",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StringDefinitions_Words_WordId",
                table: "StringDefinitions");

            migrationBuilder.DropIndex(
                name: "IX_StringDefinitions_WordId",
                table: "StringDefinitions");

            migrationBuilder.DropColumn(
                name: "WordId",
                table: "StringDefinitions");

            migrationBuilder.AddColumn<string>(
                name: "WordId",
                table: "WordMeanings",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WordMeanings_WordId",
                table: "WordMeanings",
                column: "WordId");

            migrationBuilder.AddForeignKey(
                name: "FK_WordMeanings_Words_WordId",
                table: "WordMeanings",
                column: "WordId",
                principalTable: "Words",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
