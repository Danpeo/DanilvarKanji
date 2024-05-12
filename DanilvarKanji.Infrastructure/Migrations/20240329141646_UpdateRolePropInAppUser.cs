using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DanilvarKanji.Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class UpdateRolePropInAppUser : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
        name: "FK_AspNetUsers_AppUserRole_AppUserRoleId",
        table: "AspNetUsers"
      );

      migrationBuilder.DropTable(name: "AppUserRole");

      migrationBuilder.DropIndex(name: "IX_AspNetUsers_AppUserRoleId", table: "AspNetUsers");

      migrationBuilder.DropColumn(name: "AppUserRoleId", table: "AspNetUsers");

      migrationBuilder.AddColumn<string>(
        name: "Role",
        table: "AspNetUsers",
        type: "text",
        nullable: false,
        defaultValue: ""
      );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(name: "Role", table: "AspNetUsers");

      migrationBuilder.AddColumn<string>(
        name: "AppUserRoleId",
        table: "AspNetUsers",
        type: "text",
        nullable: true
      );

      migrationBuilder.CreateTable(
        name: "AppUserRole",
        columns: table => new
        {
          Id = table.Column<string>(type: "text", nullable: false),
          Name = table.Column<string>(type: "text", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_AppUserRole", x => x.Id);
        }
      );

      migrationBuilder.CreateIndex(
        name: "IX_AspNetUsers_AppUserRoleId",
        table: "AspNetUsers",
        column: "AppUserRoleId"
      );

      migrationBuilder.AddForeignKey(
        name: "FK_AspNetUsers_AppUserRole_AppUserRoleId",
        table: "AspNetUsers",
        column: "AppUserRoleId",
        principalTable: "AppUserRole",
        principalColumn: "Id"
      );
    }
  }
}
