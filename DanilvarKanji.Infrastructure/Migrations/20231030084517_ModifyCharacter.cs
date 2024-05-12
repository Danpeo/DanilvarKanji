using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DanilvarKanji.Migrations
{
  /// <inheritdoc />
  public partial class ModifyCharacter : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.RenameColumn(name: "Mnemonic", table: "Characters", newName: "Definition");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.RenameColumn(name: "Definition", table: "Characters", newName: "Mnemonic");
    }
  }
}
