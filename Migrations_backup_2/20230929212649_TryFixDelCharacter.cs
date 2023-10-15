using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DanilvarKanji.Migrations
{
    /// <inheritdoc />
    public partial class TryFixDelCharacter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KanjiMeanings_Characters_CharacterId",
                table: "KanjiMeanings");

            migrationBuilder.DropForeignKey(
                name: "FK_Kunyomis_Characters_CharacterId",
                table: "Kunyomis");

            migrationBuilder.DropForeignKey(
                name: "FK_Onyomis_Characters_CharacterId",
                table: "Onyomis");

            migrationBuilder.DropForeignKey(
                name: "FK_WordMeanings_Words_WordId",
                table: "WordMeanings");

            migrationBuilder.DropForeignKey(
                name: "FK_Words_Characters_CharacterId",
                table: "Words");

            migrationBuilder.AlterColumn<int>(
                name: "CharacterId",
                table: "Words",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WordId",
                table: "WordMeanings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CharacterId",
                table: "Onyomis",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CharacterId",
                table: "Kunyomis",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CharacterId",
                table: "KanjiMeanings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_KanjiMeanings_Characters_CharacterId",
                table: "KanjiMeanings",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Kunyomis_Characters_CharacterId",
                table: "Kunyomis",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Onyomis_Characters_CharacterId",
                table: "Onyomis",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WordMeanings_Words_WordId",
                table: "WordMeanings",
                column: "WordId",
                principalTable: "Words",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Words_Characters_CharacterId",
                table: "Words",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KanjiMeanings_Characters_CharacterId",
                table: "KanjiMeanings");

            migrationBuilder.DropForeignKey(
                name: "FK_Kunyomis_Characters_CharacterId",
                table: "Kunyomis");

            migrationBuilder.DropForeignKey(
                name: "FK_Onyomis_Characters_CharacterId",
                table: "Onyomis");

            migrationBuilder.DropForeignKey(
                name: "FK_WordMeanings_Words_WordId",
                table: "WordMeanings");

            migrationBuilder.DropForeignKey(
                name: "FK_Words_Characters_CharacterId",
                table: "Words");

            migrationBuilder.AlterColumn<int>(
                name: "CharacterId",
                table: "Words",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "WordId",
                table: "WordMeanings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CharacterId",
                table: "Onyomis",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CharacterId",
                table: "Kunyomis",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CharacterId",
                table: "KanjiMeanings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_KanjiMeanings_Characters_CharacterId",
                table: "KanjiMeanings",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Kunyomis_Characters_CharacterId",
                table: "Kunyomis",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Onyomis_Characters_CharacterId",
                table: "Onyomis",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WordMeanings_Words_WordId",
                table: "WordMeanings",
                column: "WordId",
                principalTable: "Words",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Words_Characters_CharacterId",
                table: "Words",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id");
        }
    }
}
