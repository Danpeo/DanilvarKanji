using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DanilvarKanji.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFrontEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flashcards_Front_FrontId",
                table: "Flashcards");

            migrationBuilder.DropTable(
                name: "Front");

            migrationBuilder.DropIndex(
                name: "IX_Flashcards_FrontId",
                table: "Flashcards");

            migrationBuilder.DropColumn(
                name: "FrontId",
                table: "Flashcards");

            migrationBuilder.AddColumn<string>(
                name: "Main",
                table: "Flashcards",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sub",
                table: "Flashcards",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Main",
                table: "Flashcards");

            migrationBuilder.DropColumn(
                name: "Sub",
                table: "Flashcards");

            migrationBuilder.AddColumn<string>(
                name: "FrontId",
                table: "Flashcards",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Front",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Main = table.Column<string>(type: "text", nullable: false),
                    Sub = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Front", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flashcards_FrontId",
                table: "Flashcards",
                column: "FrontId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flashcards_Front_FrontId",
                table: "Flashcards",
                column: "FrontId",
                principalTable: "Front",
                principalColumn: "Id");
        }
    }
}
