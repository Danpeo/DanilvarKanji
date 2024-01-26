using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DanilvarKanji.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLearnCharQtyForDayAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QtyOfCharsForLearningForDay",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QtyOfCharsForLearningForDay",
                table: "AspNetUsers");
        }
    }
}
