using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DanilvarKanji.Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class UpdateCharacterRemoveNullables : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AlterColumn<int>(
        name: "StrokeCount",
        table: "Characters",
        type: "integer",
        nullable: false,
        defaultValue: 0,
        oldClrType: typeof(int),
        oldType: "integer",
        oldNullable: true
      );

      migrationBuilder.AlterColumn<string>(
        name: "Definition",
        table: "Characters",
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
      migrationBuilder.AlterColumn<int>(
        name: "StrokeCount",
        table: "Characters",
        type: "integer",
        nullable: true,
        oldClrType: typeof(int),
        oldType: "integer"
      );

      migrationBuilder.AlterColumn<string>(
        name: "Definition",
        table: "Characters",
        type: "text",
        nullable: true,
        oldClrType: typeof(string),
        oldType: "text"
      );
    }
  }
}
