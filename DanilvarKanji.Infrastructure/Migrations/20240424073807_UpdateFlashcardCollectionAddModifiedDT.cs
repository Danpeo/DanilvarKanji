using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DanilvarKanji.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFlashcardCollectionAddModifiedDT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlashcardCollections_AspNetUsers_AppUserId",
                table: "FlashcardCollections");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "FlashcardCollections",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "FlashcardCollections",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_FlashcardCollections_AspNetUsers_AppUserId",
                table: "FlashcardCollections",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlashcardCollections_AspNetUsers_AppUserId",
                table: "FlashcardCollections");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                table: "FlashcardCollections");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "FlashcardCollections",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FlashcardCollections_AspNetUsers_AppUserId",
                table: "FlashcardCollections",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
