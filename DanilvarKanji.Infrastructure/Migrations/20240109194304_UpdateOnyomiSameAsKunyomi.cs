using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DanilvarKanji.Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class UpdateOnyomiSameAsKunyomi : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AlterColumn<string>(
        name: "Romaji",
        table: "Onyomis",
        type: "text",
        nullable: false,
        defaultValue: "",
        oldClrType: typeof(string),
        oldType: "text",
        oldNullable: true
      );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AlterColumn<string>(
        name: "Romaji",
        table: "Onyomis",
        type: "text",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "text"
      );
    }
  }
}
