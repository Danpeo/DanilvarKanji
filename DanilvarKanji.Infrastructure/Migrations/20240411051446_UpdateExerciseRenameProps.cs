using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DanilvarKanji.Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class UpdateExerciseRenameProps : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.RenameColumn(
        name: "ReviewType",
        table: "Exercises",
        newName: "ExerciseSubject"
      );

      migrationBuilder.CreateTable(
        name: "Tests",
        columns: table => new
        {
          Id = table.Column<string>(type: "text", nullable: false),
          A = table.Column<int>(type: "integer", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Tests", x => x.Id);
        }
      );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(name: "Tests");

      migrationBuilder.RenameColumn(
        name: "ExerciseSubject",
        table: "Exercises",
        newName: "ReviewType"
      );
    }
  }
}
