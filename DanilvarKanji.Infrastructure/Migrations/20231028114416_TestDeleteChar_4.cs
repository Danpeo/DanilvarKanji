using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DanilvarKanji.Migrations
{
  /// <inheritdoc />
  public partial class TestDeleteChar_4 : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(name: "FK_Onyomis_Characters_CharacterId", table: "Onyomis");

      migrationBuilder.DropForeignKey(
        name: "FK_StringDefinitions_Characters_CharacterId",
        table: "StringDefinitions"
      );

      migrationBuilder.DropForeignKey(
        name: "FK_StringDefinitions_KanjiMeanings_KanjiMeaningId",
        table: "StringDefinitions"
      );

      migrationBuilder.DropForeignKey(name: "FK_WordMeanings_Words_WordId", table: "WordMeanings");

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
        name: "FK_KanjiMeanings_Characters_CharacterId",
        table: "KanjiMeanings",
        column: "CharacterId",
        principalTable: "Characters",
        principalColumn: "Id",
        onDelete: ReferentialAction.Cascade
      );

      migrationBuilder.AddForeignKey(
        name: "FK_Onyomis_Characters_CharacterId",
        table: "Onyomis",
        column: "CharacterId",
        principalTable: "Characters",
        principalColumn: "Id",
        onDelete: ReferentialAction.Cascade
      );

      migrationBuilder.AddForeignKey(
        name: "FK_StringDefinitions_Characters_CharacterId",
        table: "StringDefinitions",
        column: "CharacterId",
        principalTable: "Characters",
        principalColumn: "Id",
        onDelete: ReferentialAction.Cascade
      );

      migrationBuilder.AddForeignKey(
        name: "FK_StringDefinitions_KanjiMeanings_KanjiMeaningId",
        table: "StringDefinitions",
        column: "KanjiMeaningId",
        principalTable: "KanjiMeanings",
        principalColumn: "Id",
        onDelete: ReferentialAction.Cascade
      );

      migrationBuilder.AddForeignKey(
        name: "FK_WordMeanings_Words_WordId",
        table: "WordMeanings",
        column: "WordId",
        principalTable: "Words",
        principalColumn: "Id",
        onDelete: ReferentialAction.Cascade
      );

      migrationBuilder.AddForeignKey(
        name: "FK_Words_Characters_CharacterId",
        table: "Words",
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
        name: "FK_KanjiMeanings_Characters_CharacterId",
        table: "KanjiMeanings"
      );

      migrationBuilder.DropForeignKey(name: "FK_Onyomis_Characters_CharacterId", table: "Onyomis");

      migrationBuilder.DropForeignKey(
        name: "FK_StringDefinitions_Characters_CharacterId",
        table: "StringDefinitions"
      );

      migrationBuilder.DropForeignKey(
        name: "FK_StringDefinitions_KanjiMeanings_KanjiMeaningId",
        table: "StringDefinitions"
      );

      migrationBuilder.DropForeignKey(name: "FK_WordMeanings_Words_WordId", table: "WordMeanings");

      migrationBuilder.DropForeignKey(name: "FK_Words_Characters_CharacterId", table: "Words");

      migrationBuilder.DropIndex(name: "IX_Words_CharacterId", table: "Words");

      migrationBuilder.DropIndex(name: "IX_KanjiMeanings_CharacterId", table: "KanjiMeanings");

      migrationBuilder.DropColumn(name: "CharacterId", table: "Words");

      migrationBuilder.DropColumn(name: "CharacterId", table: "KanjiMeanings");

      migrationBuilder.AddForeignKey(
        name: "FK_Onyomis_Characters_CharacterId",
        table: "Onyomis",
        column: "CharacterId",
        principalTable: "Characters",
        principalColumn: "Id"
      );

      migrationBuilder.AddForeignKey(
        name: "FK_StringDefinitions_Characters_CharacterId",
        table: "StringDefinitions",
        column: "CharacterId",
        principalTable: "Characters",
        principalColumn: "Id"
      );

      migrationBuilder.AddForeignKey(
        name: "FK_StringDefinitions_KanjiMeanings_KanjiMeaningId",
        table: "StringDefinitions",
        column: "KanjiMeaningId",
        principalTable: "KanjiMeanings",
        principalColumn: "Id"
      );

      migrationBuilder.AddForeignKey(
        name: "FK_WordMeanings_Words_WordId",
        table: "WordMeanings",
        column: "WordId",
        principalTable: "Words",
        principalColumn: "Id"
      );
    }
  }
}
