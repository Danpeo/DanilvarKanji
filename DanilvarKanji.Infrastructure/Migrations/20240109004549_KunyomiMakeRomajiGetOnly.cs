using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DanilvarKanji.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class KunyomiMakeRomajiGetOnly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Romaji",
                table: "Kunyomis");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Romaji",
                table: "Kunyomis",
                type: "text",
                nullable: true);
        }
    }
}
