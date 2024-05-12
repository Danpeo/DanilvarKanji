using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DanilvarKanji.Migrations
{
  /// <inheritdoc />
  public partial class AddStringDefinitions : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(name: "Definition", table: "WordMeanings");

      migrationBuilder.DropColumn(name: "Definition", table: "KanjiMeanings");

      migrationBuilder.DropColumn(name: "Definition", table: "Characters");

      migrationBuilder.CreateTable(
        name: "StringDefinitions",
        columns: table => new
        {
          Id = table.Column<string>(type: "text", nullable: false),
          Value = table.Column<string>(type: "text", nullable: false),
          Culture = table.Column<int>(type: "integer", nullable: false),
          CharacterId = table.Column<string>(type: "text", nullable: true),
          KanjiMeaningId = table.Column<string>(type: "text", nullable: true),
          WordMeaningId = table.Column<string>(type: "text", nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_StringDefinitions", x => x.Id);
          table.ForeignKey(
            name: "FK_StringDefinitions_Characters_CharacterId",
            column: x => x.CharacterId,
            principalTable: "Characters",
            principalColumn: "Id"
          );
          table.ForeignKey(
            name: "FK_StringDefinitions_KanjiMeanings_KanjiMeaningId",
            column: x => x.KanjiMeaningId,
            principalTable: "KanjiMeanings",
            principalColumn: "Id"
          );
          table.ForeignKey(
            name: "FK_StringDefinitions_WordMeanings_WordMeaningId",
            column: x => x.WordMeaningId,
            principalTable: "WordMeanings",
            principalColumn: "Id"
          );
        }
      );

      migrationBuilder.CreateIndex(
        name: "IX_StringDefinitions_CharacterId",
        table: "StringDefinitions",
        column: "CharacterId"
      );

      migrationBuilder.CreateIndex(
        name: "IX_StringDefinitions_KanjiMeaningId",
        table: "StringDefinitions",
        column: "KanjiMeaningId"
      );

      migrationBuilder.CreateIndex(
        name: "IX_StringDefinitions_WordMeaningId",
        table: "StringDefinitions",
        column: "WordMeaningId"
      );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(name: "StringDefinitions");

      migrationBuilder.AddColumn<string>(
        name: "Definition",
        table: "WordMeanings",
        type: "text",
        nullable: false,
        defaultValue: ""
      );

      migrationBuilder.AddColumn<string>(
        name: "Definition",
        table: "KanjiMeanings",
        type: "text",
        nullable: false,
        defaultValue: ""
      );

      migrationBuilder.AddColumn<string>(
        name: "Definition",
        table: "Characters",
        type: "text",
        nullable: false,
        defaultValue: ""
      );
    }
  }
}
