using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DanilvarKanji.Migrations
{
  /// <inheritdoc />
  public partial class TestDeleteChar_3 : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
        name: "FK_Kunyomis_Characters_CharacterId",
        table: "Kunyomis"
      );

      migrationBuilder.AddForeignKey(
        name: "FK_Kunyomis_Characters_CharacterId",
        table: "Kunyomis",
        column: "CharacterId",
        principalTable: "Characters",
        principalColumn: "Id",
        onDelete: ReferentialAction.Cascade
      );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
        name: "FK_Kunyomis_Characters_CharacterId",
        table: "Kunyomis"
      );

      migrationBuilder.AddForeignKey(
        name: "FK_Kunyomis_Characters_CharacterId",
        table: "Kunyomis",
        column: "CharacterId",
        principalTable: "Characters",
        principalColumn: "Id"
      );
    }
  }
}
