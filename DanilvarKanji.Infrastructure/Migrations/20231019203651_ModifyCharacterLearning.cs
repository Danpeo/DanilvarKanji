using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DanilvarKanji.Migrations
{
  /// <inheritdoc />
  public partial class ModifyCharacterLearning : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(name: "LearningProgress", table: "CharacterLearnings");

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

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
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

      migrationBuilder.AddColumn<float>(
        name: "LearningProgress",
        table: "CharacterLearnings",
        type: "real",
        nullable: false,
        defaultValue: 0f
      );
    }
  }
}
