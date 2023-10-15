using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DanilvarKanji.Migrations
{
    /// <inheritdoc />
    public partial class RemovedDateOfBirth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterLearning_AspNetUsers_AppUserId",
                table: "CharacterLearning");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacterLearning_Characters_CharacterId",
                table: "CharacterLearning");

            migrationBuilder.DropForeignKey(
                name: "FK_KanjiMeanings_Characters_CharacterId",
                table: "KanjiMeanings");

            migrationBuilder.DropForeignKey(
                name: "FK_Kunyomis_Characters_CharacterId",
                table: "Kunyomis");

            migrationBuilder.DropForeignKey(
                name: "FK_Onyomis_Characters_CharacterId",
                table: "Onyomis");

            migrationBuilder.DropForeignKey(
                name: "FK_WordMeanings_Words_WordId",
                table: "WordMeanings");

            migrationBuilder.DropForeignKey(
                name: "FK_Words_Characters_CharacterId",
                table: "Words");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CharacterLearning",
                table: "CharacterLearning");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "CharacterLearning",
                newName: "CharacterLearnings");

            migrationBuilder.RenameIndex(
                name: "IX_CharacterLearning_CharacterId",
                table: "CharacterLearnings",
                newName: "IX_CharacterLearnings_CharacterId");

            migrationBuilder.RenameIndex(
                name: "IX_CharacterLearning_AppUserId",
                table: "CharacterLearnings",
                newName: "IX_CharacterLearnings_AppUserId");

            migrationBuilder.AlterColumn<int>(
                name: "CharacterId",
                table: "Words",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "WordId",
                table: "WordMeanings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CharacterId",
                table: "Onyomis",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CharacterId",
                table: "Kunyomis",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CharacterLearnings",
                table: "CharacterLearnings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterLearnings_AspNetUsers_AppUserId",
                table: "CharacterLearnings",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterLearnings_Characters_CharacterId",
                table: "CharacterLearnings",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KanjiMeanings_Characters_CharacterId",
                table: "KanjiMeanings",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Kunyomis_Characters_CharacterId",
                table: "Kunyomis",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Onyomis_Characters_CharacterId",
                table: "Onyomis",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WordMeanings_Words_WordId",
                table: "WordMeanings",
                column: "WordId",
                principalTable: "Words",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Words_Characters_CharacterId",
                table: "Words",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterLearnings_AspNetUsers_AppUserId",
                table: "CharacterLearnings");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacterLearnings_Characters_CharacterId",
                table: "CharacterLearnings");

            migrationBuilder.DropForeignKey(
                name: "FK_KanjiMeanings_Characters_CharacterId",
                table: "KanjiMeanings");

            migrationBuilder.DropForeignKey(
                name: "FK_Kunyomis_Characters_CharacterId",
                table: "Kunyomis");

            migrationBuilder.DropForeignKey(
                name: "FK_Onyomis_Characters_CharacterId",
                table: "Onyomis");

            migrationBuilder.DropForeignKey(
                name: "FK_WordMeanings_Words_WordId",
                table: "WordMeanings");

            migrationBuilder.DropForeignKey(
                name: "FK_Words_Characters_CharacterId",
                table: "Words");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CharacterLearnings",
                table: "CharacterLearnings");

            migrationBuilder.RenameTable(
                name: "CharacterLearnings",
                newName: "CharacterLearning");

            migrationBuilder.RenameIndex(
                name: "IX_CharacterLearnings_CharacterId",
                table: "CharacterLearning",
                newName: "IX_CharacterLearning_CharacterId");

            migrationBuilder.RenameIndex(
                name: "IX_CharacterLearnings_AppUserId",
                table: "CharacterLearning",
                newName: "IX_CharacterLearning_AppUserId");

            migrationBuilder.AlterColumn<int>(
                name: "CharacterId",
                table: "Words",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WordId",
                table: "WordMeanings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CharacterId",
                table: "Onyomis",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CharacterId",
                table: "Kunyomis",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_CharacterLearning",
                table: "CharacterLearning",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterLearning_AspNetUsers_AppUserId",
                table: "CharacterLearning",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterLearning_Characters_CharacterId",
                table: "CharacterLearning",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KanjiMeanings_Characters_CharacterId",
                table: "KanjiMeanings",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Kunyomis_Characters_CharacterId",
                table: "Kunyomis",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Onyomis_Characters_CharacterId",
                table: "Onyomis",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WordMeanings_Words_WordId",
                table: "WordMeanings",
                column: "WordId",
                principalTable: "Words",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
