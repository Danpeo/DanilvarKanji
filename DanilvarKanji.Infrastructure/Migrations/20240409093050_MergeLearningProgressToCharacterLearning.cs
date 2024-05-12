using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DanilvarKanji.Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class MergeLearningProgressToCharacterLearning : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
        name: "FK_CharacterLearnings_LearningProgresses_LearningProgressId",
        table: "CharacterLearnings"
      );

      migrationBuilder.DropTable(name: "LearningProgresses");

      migrationBuilder.DropIndex(
        name: "IX_CharacterLearnings_LearningProgressId",
        table: "CharacterLearnings"
      );

      migrationBuilder.DropColumn(name: "LearningProgressId", table: "CharacterLearnings");

      migrationBuilder.AddColumn<int>(
        name: "LearningLevel",
        table: "CharacterLearnings",
        type: "integer",
        nullable: false,
        defaultValue: 0
      );

      migrationBuilder.AddColumn<float>(
        name: "LearningLevelValue",
        table: "CharacterLearnings",
        type: "real",
        nullable: false,
        defaultValue: 0f
      );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(name: "LearningLevel", table: "CharacterLearnings");

      migrationBuilder.DropColumn(name: "LearningLevelValue", table: "CharacterLearnings");

      migrationBuilder.AddColumn<string>(
        name: "LearningProgressId",
        table: "CharacterLearnings",
        type: "text",
        nullable: true
      );

      migrationBuilder.CreateTable(
        name: "LearningProgresses",
        columns: table => new
        {
          Id = table.Column<string>(type: "text", nullable: false),
          LearningLevel = table.Column<int>(type: "integer", nullable: false),
          Value = table.Column<float>(type: "real", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_LearningProgresses", x => x.Id);
        }
      );

      migrationBuilder.CreateIndex(
        name: "IX_CharacterLearnings_LearningProgressId",
        table: "CharacterLearnings",
        column: "LearningProgressId"
      );

      migrationBuilder.AddForeignKey(
        name: "FK_CharacterLearnings_LearningProgresses_LearningProgressId",
        table: "CharacterLearnings",
        column: "LearningProgressId",
        principalTable: "LearningProgresses",
        principalColumn: "Id"
      );
    }
  }
}
