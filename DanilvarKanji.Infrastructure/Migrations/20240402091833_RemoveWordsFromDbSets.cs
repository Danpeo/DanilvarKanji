using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DanilvarKanji.Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class RemoveWordsFromDbSets : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
        name: "FK_StringDefinitions_Words_WordId",
        table: "StringDefinitions"
      );

      migrationBuilder.DropTable(name: "Words");

      migrationBuilder.DropIndex(name: "IX_StringDefinitions_WordId", table: "StringDefinitions");

      migrationBuilder.DropColumn(name: "WordId", table: "StringDefinitions");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<string>(
        name: "WordId",
        table: "StringDefinitions",
        type: "text",
        nullable: true
      );

      migrationBuilder.CreateTable(
        name: "Words",
        columns: table => new
        {
          Id = table.Column<string>(type: "text", nullable: false),
          FullJapanese = table.Column<string>(type: "text", nullable: false),
          Furigana = table.Column<string>(type: "text", nullable: true),
          PartOfSpeach = table.Column<int>(type: "integer", nullable: false),
          Romaji = table.Column<string>(type: "text", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Words", x => x.Id);
        }
      );

      migrationBuilder.CreateIndex(
        name: "IX_StringDefinitions_WordId",
        table: "StringDefinitions",
        column: "WordId"
      );

      migrationBuilder.AddForeignKey(
        name: "FK_StringDefinitions_Words_WordId",
        table: "StringDefinitions",
        column: "WordId",
        principalTable: "Words",
        principalColumn: "Id"
      );
    }
  }
}
