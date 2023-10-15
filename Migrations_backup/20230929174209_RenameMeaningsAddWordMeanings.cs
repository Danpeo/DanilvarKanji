using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DanilvarKanji.Migrations
{
    /// <inheritdoc />
    public partial class RenameMeaningsAddWordMeanings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kunyomis_Characters_CharacterId",
                table: "Kunyomis");

            migrationBuilder.DropForeignKey(
                name: "FK_Meanings_Words_WordId",
                table: "Meanings");

            migrationBuilder.DropIndex(
                name: "IX_Meanings_WordId",
                table: "Meanings");

            migrationBuilder.DropColumn(
                name: "JlptLevel",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "WordId",
                table: "Meanings");

            migrationBuilder.AlterColumn<int>(
                name: "CharacterId",
                table: "Kunyomis",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "WordMeaning",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Definition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<float>(type: "real", nullable: true),
                    WordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordMeaning", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordMeaning_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WordMeaning_WordId",
                table: "WordMeaning",
                column: "WordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kunyomis_Characters_CharacterId",
                table: "Kunyomis",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kunyomis_Characters_CharacterId",
                table: "Kunyomis");

            migrationBuilder.DropTable(
                name: "WordMeaning");

            migrationBuilder.AddColumn<int>(
                name: "JlptLevel",
                table: "Words",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WordId",
                table: "Meanings",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CharacterId",
                table: "Kunyomis",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meanings_WordId",
                table: "Meanings",
                column: "WordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kunyomis_Characters_CharacterId",
                table: "Kunyomis",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meanings_Words_WordId",
                table: "Meanings",
                column: "WordId",
                principalTable: "Words",
                principalColumn: "Id");
        }
    }
}
