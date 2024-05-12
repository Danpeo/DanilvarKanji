using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DanilvarKanji.Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class AddFlashcard : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(name: "Tests");

      migrationBuilder.AddColumn<int>(
        name: "XP",
        table: "AspNetUsers",
        type: "integer",
        nullable: false,
        defaultValue: 0
      );

      migrationBuilder.CreateTable(
        name: "FlashcardCollections",
        columns: table => new
        {
          Id = table.Column<string>(type: "text", nullable: false),
          Name = table.Column<string>(type: "text", nullable: false),
          AppUserId = table.Column<string>(type: "text", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_FlashcardCollections", x => x.Id);
          table.ForeignKey(
            name: "FK_FlashcardCollections_AspNetUsers_AppUserId",
            column: x => x.AppUserId,
            principalTable: "AspNetUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade
          );
        }
      );

      migrationBuilder.CreateTable(
        name: "Front",
        columns: table => new
        {
          Id = table.Column<string>(type: "text", nullable: false),
          Main = table.Column<string>(type: "text", nullable: false),
          Sub = table.Column<string>(type: "text", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Front", x => x.Id);
        }
      );

      migrationBuilder.CreateTable(
        name: "Flashcards",
        columns: table => new
        {
          Id = table.Column<string>(type: "text", nullable: false),
          FrontId = table.Column<string>(type: "text", nullable: true),
          Back = table.Column<string>(type: "text", nullable: false),
          DoRemember = table.Column<bool>(type: "boolean", nullable: false),
          RememberedInARow = table.Column<int>(type: "integer", nullable: false),
          FlashcardCollectionId = table.Column<string>(type: "text", nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Flashcards", x => x.Id);
          table.ForeignKey(
            name: "FK_Flashcards_FlashcardCollections_FlashcardCollectionId",
            column: x => x.FlashcardCollectionId,
            principalTable: "FlashcardCollections",
            principalColumn: "Id"
          );
          table.ForeignKey(
            name: "FK_Flashcards_Front_FrontId",
            column: x => x.FrontId,
            principalTable: "Front",
            principalColumn: "Id"
          );
        }
      );

      migrationBuilder.CreateIndex(
        name: "IX_FlashcardCollections_AppUserId",
        table: "FlashcardCollections",
        column: "AppUserId"
      );

      migrationBuilder.CreateIndex(
        name: "IX_Flashcards_FlashcardCollectionId",
        table: "Flashcards",
        column: "FlashcardCollectionId"
      );

      migrationBuilder.CreateIndex(
        name: "IX_Flashcards_FrontId",
        table: "Flashcards",
        column: "FrontId"
      );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(name: "Flashcards");

      migrationBuilder.DropTable(name: "FlashcardCollections");

      migrationBuilder.DropTable(name: "Front");

      migrationBuilder.DropColumn(name: "XP", table: "AspNetUsers");

      migrationBuilder.CreateTable(
        name: "Tests",
        columns: table => new
        {
          Id = table.Column<Guid>(type: "uuid", nullable: false),
          Name = table.Column<string>(type: "text", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Tests", x => x.Id);
        }
      );
    }
  }
}
