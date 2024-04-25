using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DanilvarKanji.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeStringDefinitionOwned : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StringDefinitions");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.CreateTable(
                name: "Characters_Mnemonics",
                columns: table => new
                {
                    CharacterId = table.Column<string>(type: "text", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "text", nullable: false),
                    Culture = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters_Mnemonics", x => new { x.CharacterId, x.Id });
                    table.ForeignKey(
                        name: "FK_Characters_Mnemonics_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KanjiMeanings_Definitions",
                columns: table => new
                {
                    KanjiMeaningId = table.Column<string>(type: "text", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "text", nullable: false),
                    Culture = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KanjiMeanings_Definitions", x => new { x.KanjiMeaningId, x.Id });
                    table.ForeignKey(
                        name: "FK_KanjiMeanings_Definitions_KanjiMeanings_KanjiMeaningId",
                        column: x => x.KanjiMeaningId,
                        principalTable: "KanjiMeanings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Characters_Mnemonics");

            migrationBuilder.DropTable(
                name: "KanjiMeanings_Definitions");

            migrationBuilder.CreateTable(
                name: "StringDefinitions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CharacterId = table.Column<string>(type: "text", nullable: true),
                    Culture = table.Column<int>(type: "integer", nullable: false),
                    KanjiMeaningId = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StringDefinitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StringDefinitions_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StringDefinitions_KanjiMeanings_KanjiMeaningId",
                        column: x => x.KanjiMeaningId,
                        principalTable: "KanjiMeanings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    A = table.Column<int>(type: "integer", nullable: false),
                    InTest_B = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StringDefinitions_CharacterId",
                table: "StringDefinitions",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_StringDefinitions_KanjiMeaningId",
                table: "StringDefinitions",
                column: "KanjiMeaningId");
        }
    }
}
