using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DanilvarKanji.Migrations
{
    /// <inheritdoc />
    public partial class TryFixDelCharacter_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WordMeanings_Words_WordId",
                table: "WordMeanings");

            migrationBuilder.AlterColumn<int>(
                name: "WordId",
                table: "WordMeanings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WordMeanings_Words_WordId",
                table: "WordMeanings",
                column: "WordId",
                principalTable: "Words",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WordMeanings_Words_WordId",
                table: "WordMeanings");

            migrationBuilder.AlterColumn<int>(
                name: "WordId",
                table: "WordMeanings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_WordMeanings_Words_WordId",
                table: "WordMeanings",
                column: "WordId",
                principalTable: "Words",
                principalColumn: "Id");
        }
    }
}
