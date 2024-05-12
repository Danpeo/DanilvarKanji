using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DanilvarKanji.Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class RemoveWordMeaningsFromDbSets : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
        name: "FK_StringDefinitions_WordMeanings_WordMeaningId",
        table: "StringDefinitions"
      );

      migrationBuilder.DropTable(name: "WordMeanings");

      migrationBuilder.DropIndex(
        name: "IX_StringDefinitions_WordMeaningId",
        table: "StringDefinitions"
      );

      migrationBuilder.DropColumn(name: "WordMeaningId", table: "StringDefinitions");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<string>(
        name: "WordMeaningId",
        table: "StringDefinitions",
        type: "text",
        nullable: true
      );

      migrationBuilder.CreateTable(
        name: "WordMeanings",
        columns: table => new
        {
          Id = table.Column<string>(type: "text", nullable: false),
          Priority = table.Column<float>(type: "real", nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_WordMeanings", x => x.Id);
        }
      );

      migrationBuilder.CreateIndex(
        name: "IX_StringDefinitions_WordMeaningId",
        table: "StringDefinitions",
        column: "WordMeaningId"
      );

      migrationBuilder.AddForeignKey(
        name: "FK_StringDefinitions_WordMeanings_WordMeaningId",
        table: "StringDefinitions",
        column: "WordMeaningId",
        principalTable: "WordMeanings",
        principalColumn: "Id"
      );
    }
  }
}
