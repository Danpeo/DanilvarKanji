using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DanilvarKanji.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameExerciseTypeToReviewTypeAndAddExerciseType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReviewType",
                table: "Exercises",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewType",
                table: "Exercises");
        }
    }
}
