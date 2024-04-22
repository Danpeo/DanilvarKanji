using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DanilvarKanji.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveWordsAndChildCharsFromCharacter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Words_Characters_CharacterId",
                table: "Words");

            migrationBuilder.DropIndex(
                name: "IX_Words_CharacterId",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "CharacterId",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "ChildCharacterIds",
                table: "Characters");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CharacterId",
                table: "Words",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "ChildCharacterIds",
                table: "Characters",
                type: "text[]",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Words_CharacterId",
                table: "Words",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Words_Characters_CharacterId",
                table: "Words",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
