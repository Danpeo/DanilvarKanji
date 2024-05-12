using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DanilvarKanji.Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class AddExercise : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
        name: "Exercises",
        columns: table => new
        {
          Id = table.Column<string>(type: "text", nullable: false),
          CharacterId = table.Column<string>(type: "text", nullable: false),
          AppUserId = table.Column<string>(type: "text", nullable: false),
          IsCorrect = table.Column<bool>(type: "boolean", nullable: false),
          ExerciseType = table.Column<int>(type: "integer", nullable: false),
          ExcerciseDateTime = table.Column<DateTime>(
            type: "timestamp with time zone",
            nullable: false
          )
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Exercises", x => x.Id);
          table.ForeignKey(
            name: "FK_Exercises_AspNetUsers_AppUserId",
            column: x => x.AppUserId,
            principalTable: "AspNetUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade
          );
          table.ForeignKey(
            name: "FK_Exercises_Characters_CharacterId",
            column: x => x.CharacterId,
            principalTable: "Characters",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade
          );
        }
      );

      migrationBuilder.CreateIndex(
        name: "IX_Exercises_AppUserId",
        table: "Exercises",
        column: "AppUserId"
      );

      migrationBuilder.CreateIndex(
        name: "IX_Exercises_CharacterId",
        table: "Exercises",
        column: "CharacterId"
      );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(name: "Exercises");
    }
  }
}
