using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DanilvarKanji.Migrations
{
  /// <inheritdoc />
  public partial class TestDeleteChar_2 : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
        name: "FK_CharacterLearnings_Characters_CharacterId",
        table: "CharacterLearnings"
      );

      migrationBuilder.DropForeignKey(
        name: "FK_KanjiMeanings_Characters_CharacterId",
        table: "KanjiMeanings"
      );

      migrationBuilder.DropForeignKey(name: "FK_Words_Characters_CharacterId", table: "Words");

      migrationBuilder.DropIndex(name: "IX_Words_CharacterId", table: "Words");

      migrationBuilder.DropIndex(name: "IX_KanjiMeanings_CharacterId", table: "KanjiMeanings");

      migrationBuilder.DropColumn(name: "CharacterId", table: "Words");

      migrationBuilder.DropColumn(name: "CharacterId", table: "KanjiMeanings");

      migrationBuilder.AlterColumn<string>(
        name: "CharacterId",
        table: "CharacterLearnings",
        type: "text",
        nullable: false,
        defaultValue: "",
        oldClrType: typeof(string),
        oldType: "text",
        oldNullable: true
      );

      migrationBuilder.AddForeignKey(
        name: "FK_CharacterLearnings_Characters_CharacterId",
        table: "CharacterLearnings",
        column: "CharacterId",
        principalTable: "Characters",
        principalColumn: "Id",
        onDelete: ReferentialAction.Cascade
      );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
        name: "FK_CharacterLearnings_Characters_CharacterId",
        table: "CharacterLearnings"
      );

      migrationBuilder.AddColumn<string>(
        name: "CharacterId",
        table: "Words",
        type: "text",
        nullable: true
      );

      migrationBuilder.AddColumn<string>(
        name: "CharacterId",
        table: "KanjiMeanings",
        type: "text",
        nullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "CharacterId",
        table: "CharacterLearnings",
        type: "text",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "text"
      );

      migrationBuilder.CreateIndex(
        name: "IX_Words_CharacterId",
        table: "Words",
        column: "CharacterId"
      );

      migrationBuilder.CreateIndex(
        name: "IX_KanjiMeanings_CharacterId",
        table: "KanjiMeanings",
        column: "CharacterId"
      );

      migrationBuilder.AddForeignKey(
        name: "FK_CharacterLearnings_Characters_CharacterId",
        table: "CharacterLearnings",
        column: "CharacterId",
        principalTable: "Characters",
        principalColumn: "Id"
      );

      migrationBuilder.AddForeignKey(
        name: "FK_KanjiMeanings_Characters_CharacterId",
        table: "KanjiMeanings",
        column: "CharacterId",
        principalTable: "Characters",
        principalColumn: "Id"
      );

      migrationBuilder.AddForeignKey(
        name: "FK_Words_Characters_CharacterId",
        table: "Words",
        column: "CharacterId",
        principalTable: "Characters",
        principalColumn: "Id"
      );
    }
  }
}
