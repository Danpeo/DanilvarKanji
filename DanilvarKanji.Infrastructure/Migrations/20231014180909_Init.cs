﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DanilvarKanji.Migrations
{
  /// <inheritdoc />
  public partial class Init : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
        name: "AspNetRoles",
        columns: table => new
        {
          Id = table.Column<string>(type: "text", nullable: false),
          Name = table.Column<string>(
            type: "character varying(256)",
            maxLength: 256,
            nullable: true
          ),
          NormalizedName = table.Column<string>(
            type: "character varying(256)",
            maxLength: 256,
            nullable: true
          ),
          ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_AspNetRoles", x => x.Id);
        }
      );

      migrationBuilder.CreateTable(
        name: "AspNetUsers",
        columns: table => new
        {
          Id = table.Column<string>(type: "text", nullable: false),
          Photo = table.Column<string>(type: "text", nullable: true),
          JlptLevel = table.Column<int>(type: "integer", nullable: false),
          RegistrationDate = table.Column<DateTime>(
            type: "timestamp with time zone",
            nullable: false
          ),
          LastStudied = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
          UserName = table.Column<string>(
            type: "character varying(256)",
            maxLength: 256,
            nullable: true
          ),
          NormalizedUserName = table.Column<string>(
            type: "character varying(256)",
            maxLength: 256,
            nullable: true
          ),
          Email = table.Column<string>(
            type: "character varying(256)",
            maxLength: 256,
            nullable: true
          ),
          NormalizedEmail = table.Column<string>(
            type: "character varying(256)",
            maxLength: 256,
            nullable: true
          ),
          EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
          PasswordHash = table.Column<string>(type: "text", nullable: true),
          SecurityStamp = table.Column<string>(type: "text", nullable: true),
          ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
          PhoneNumber = table.Column<string>(type: "text", nullable: true),
          PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
          TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
          LockoutEnd = table.Column<DateTimeOffset>(
            type: "timestamp with time zone",
            nullable: true
          ),
          LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
          AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_AspNetUsers", x => x.Id);
        }
      );

      migrationBuilder.CreateTable(
        name: "Characters",
        columns: table => new
        {
          Id = table.Column<string>(type: "text", nullable: false),
          Definition = table.Column<string>(type: "text", nullable: false),
          JlptLevel = table.Column<int>(type: "integer", nullable: true),
          CharacterType = table.Column<int>(type: "integer", nullable: false),
          Mnemonic = table.Column<string>(type: "text", nullable: true),
          StrokeCount = table.Column<int>(type: "integer", nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Characters", x => x.Id);
        }
      );

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

      migrationBuilder.CreateTable(
        name: "AspNetRoleClaims",
        columns: table => new
        {
          Id = table
            .Column<int>(type: "integer", nullable: false)
            .Annotation(
              "Npgsql:ValueGenerationStrategy",
              NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
            ),
          RoleId = table.Column<string>(type: "text", nullable: false),
          ClaimType = table.Column<string>(type: "text", nullable: true),
          ClaimValue = table.Column<string>(type: "text", nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
          table.ForeignKey(
            name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
            column: x => x.RoleId,
            principalTable: "AspNetRoles",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade
          );
        }
      );

      migrationBuilder.CreateTable(
        name: "AspNetUserClaims",
        columns: table => new
        {
          Id = table
            .Column<int>(type: "integer", nullable: false)
            .Annotation(
              "Npgsql:ValueGenerationStrategy",
              NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
            ),
          UserId = table.Column<string>(type: "text", nullable: false),
          ClaimType = table.Column<string>(type: "text", nullable: true),
          ClaimValue = table.Column<string>(type: "text", nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
          table.ForeignKey(
            name: "FK_AspNetUserClaims_AspNetUsers_UserId",
            column: x => x.UserId,
            principalTable: "AspNetUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade
          );
        }
      );

      migrationBuilder.CreateTable(
        name: "AspNetUserLogins",
        columns: table => new
        {
          LoginProvider = table.Column<string>(type: "text", nullable: false),
          ProviderKey = table.Column<string>(type: "text", nullable: false),
          ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
          UserId = table.Column<string>(type: "text", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
          table.ForeignKey(
            name: "FK_AspNetUserLogins_AspNetUsers_UserId",
            column: x => x.UserId,
            principalTable: "AspNetUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade
          );
        }
      );

      migrationBuilder.CreateTable(
        name: "AspNetUserRoles",
        columns: table => new
        {
          UserId = table.Column<string>(type: "text", nullable: false),
          RoleId = table.Column<string>(type: "text", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
          table.ForeignKey(
            name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
            column: x => x.RoleId,
            principalTable: "AspNetRoles",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade
          );
          table.ForeignKey(
            name: "FK_AspNetUserRoles_AspNetUsers_UserId",
            column: x => x.UserId,
            principalTable: "AspNetUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade
          );
        }
      );

      migrationBuilder.CreateTable(
        name: "AspNetUserTokens",
        columns: table => new
        {
          UserId = table.Column<string>(type: "text", nullable: false),
          LoginProvider = table.Column<string>(type: "text", nullable: false),
          Name = table.Column<string>(type: "text", nullable: false),
          Value = table.Column<string>(type: "text", nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey(
            "PK_AspNetUserTokens",
            x => new
            {
              x.UserId,
              x.LoginProvider,
              x.Name
            }
          );
          table.ForeignKey(
            name: "FK_AspNetUserTokens_AspNetUsers_UserId",
            column: x => x.UserId,
            principalTable: "AspNetUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade
          );
        }
      );

      migrationBuilder.CreateTable(
        name: "CharacterLearnings",
        columns: table => new
        {
          Id = table.Column<string>(type: "text", nullable: false),
          AppUserId = table.Column<string>(type: "text", nullable: false),
          CharacterId = table.Column<string>(type: "text", nullable: true),
          LearningState = table.Column<int>(type: "integer", nullable: false),
          LearningProgress = table.Column<float>(type: "real", nullable: false),
          LearnedCount = table.Column<int>(type: "integer", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_CharacterLearnings", x => x.Id);
          table.ForeignKey(
            name: "FK_CharacterLearnings_AspNetUsers_AppUserId",
            column: x => x.AppUserId,
            principalTable: "AspNetUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade
          );
          table.ForeignKey(
            name: "FK_CharacterLearnings_Characters_CharacterId",
            column: x => x.CharacterId,
            principalTable: "Characters",
            principalColumn: "Id"
          );
        }
      );

      migrationBuilder.CreateTable(
        name: "KanjiMeanings",
        columns: table => new
        {
          Id = table.Column<string>(type: "text", nullable: false),
          Definition = table.Column<string>(type: "text", nullable: false),
          Priority = table.Column<float>(type: "real", nullable: true),
          CharacterId = table.Column<string>(type: "text", nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_KanjiMeanings", x => x.Id);
          table.ForeignKey(
            name: "FK_KanjiMeanings_Characters_CharacterId",
            column: x => x.CharacterId,
            principalTable: "Characters",
            principalColumn: "Id"
          );
        }
      );

      migrationBuilder.CreateTable(
        name: "Kunyomis",
        columns: table => new
        {
          Id = table.Column<string>(type: "text", nullable: false),
          JapaneseWriting = table.Column<string>(type: "text", nullable: false),
          Romaji = table.Column<string>(type: "text", nullable: true),
          CharacterId = table.Column<string>(type: "text", nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Kunyomis", x => x.Id);
          table.ForeignKey(
            name: "FK_Kunyomis_Characters_CharacterId",
            column: x => x.CharacterId,
            principalTable: "Characters",
            principalColumn: "Id"
          );
        }
      );

      migrationBuilder.CreateTable(
        name: "Onyomis",
        columns: table => new
        {
          Id = table.Column<string>(type: "text", nullable: false),
          JapaneseWriting = table.Column<string>(type: "text", nullable: false),
          Romaji = table.Column<string>(type: "text", nullable: true),
          CharacterId = table.Column<string>(type: "text", nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Onyomis", x => x.Id);
          table.ForeignKey(
            name: "FK_Onyomis_Characters_CharacterId",
            column: x => x.CharacterId,
            principalTable: "Characters",
            principalColumn: "Id"
          );
        }
      );

      migrationBuilder.CreateTable(
        name: "Words",
        columns: table => new
        {
          Id = table.Column<string>(type: "text", nullable: false),
          Furigana = table.Column<string>(type: "text", nullable: true),
          Romaji = table.Column<string>(type: "text", nullable: false),
          FullJapanese = table.Column<string>(type: "text", nullable: false),
          PartOfSpeach = table.Column<int>(type: "integer", nullable: false),
          CharacterId = table.Column<string>(type: "text", nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Words", x => x.Id);
          table.ForeignKey(
            name: "FK_Words_Characters_CharacterId",
            column: x => x.CharacterId,
            principalTable: "Characters",
            principalColumn: "Id"
          );
        }
      );

      migrationBuilder.CreateTable(
        name: "WordMeanings",
        columns: table => new
        {
          Id = table.Column<string>(type: "text", nullable: false),
          Definition = table.Column<string>(type: "text", nullable: false),
          Priority = table.Column<float>(type: "real", nullable: true),
          WordId = table.Column<string>(type: "text", nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_WordMeanings", x => x.Id);
          table.ForeignKey(
            name: "FK_WordMeanings_Words_WordId",
            column: x => x.WordId,
            principalTable: "Words",
            principalColumn: "Id"
          );
        }
      );

      migrationBuilder.CreateIndex(
        name: "IX_AspNetRoleClaims_RoleId",
        table: "AspNetRoleClaims",
        column: "RoleId"
      );

      migrationBuilder.CreateIndex(
        name: "RoleNameIndex",
        table: "AspNetRoles",
        column: "NormalizedName",
        unique: true
      );

      migrationBuilder.CreateIndex(
        name: "IX_AspNetUserClaims_UserId",
        table: "AspNetUserClaims",
        column: "UserId"
      );

      migrationBuilder.CreateIndex(
        name: "IX_AspNetUserLogins_UserId",
        table: "AspNetUserLogins",
        column: "UserId"
      );

      migrationBuilder.CreateIndex(
        name: "IX_AspNetUserRoles_RoleId",
        table: "AspNetUserRoles",
        column: "RoleId"
      );

      migrationBuilder.CreateIndex(
        name: "EmailIndex",
        table: "AspNetUsers",
        column: "NormalizedEmail"
      );

      migrationBuilder.CreateIndex(
        name: "UserNameIndex",
        table: "AspNetUsers",
        column: "NormalizedUserName",
        unique: true
      );

      migrationBuilder.CreateIndex(
        name: "IX_CharacterLearnings_AppUserId",
        table: "CharacterLearnings",
        column: "AppUserId"
      );

      migrationBuilder.CreateIndex(
        name: "IX_CharacterLearnings_CharacterId",
        table: "CharacterLearnings",
        column: "CharacterId"
      );

      migrationBuilder.CreateIndex(
        name: "IX_KanjiMeanings_CharacterId",
        table: "KanjiMeanings",
        column: "CharacterId"
      );

      migrationBuilder.CreateIndex(
        name: "IX_Kunyomis_CharacterId",
        table: "Kunyomis",
        column: "CharacterId"
      );

      migrationBuilder.CreateIndex(
        name: "IX_Onyomis_CharacterId",
        table: "Onyomis",
        column: "CharacterId"
      );

      migrationBuilder.CreateIndex(
        name: "IX_WordMeanings_WordId",
        table: "WordMeanings",
        column: "WordId"
      );

      migrationBuilder.CreateIndex(
        name: "IX_Words_CharacterId",
        table: "Words",
        column: "CharacterId"
      );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(name: "AspNetRoleClaims");

      migrationBuilder.DropTable(name: "AspNetUserClaims");

      migrationBuilder.DropTable(name: "AspNetUserLogins");

      migrationBuilder.DropTable(name: "AspNetUserRoles");

      migrationBuilder.DropTable(name: "AspNetUserTokens");

      migrationBuilder.DropTable(name: "CharacterLearnings");

      migrationBuilder.DropTable(name: "KanjiMeanings");

      migrationBuilder.DropTable(name: "Kunyomis");

      migrationBuilder.DropTable(name: "Onyomis");

      migrationBuilder.DropTable(name: "Tests");

      migrationBuilder.DropTable(name: "WordMeanings");

      migrationBuilder.DropTable(name: "AspNetRoles");

      migrationBuilder.DropTable(name: "AspNetUsers");

      migrationBuilder.DropTable(name: "Words");

      migrationBuilder.DropTable(name: "Characters");
    }
  }
}
