﻿// <auto-generated />
using System;
using DanilvarKanji.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DanilvarKanji.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DanilvarKanji.Shared.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<int>("JlptLevel")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LastStudied")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("ProfileImageId")
                        .HasColumnType("text");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.HasIndex("ProfileImageId");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Models.Character", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("CharacterType")
                        .HasColumnType("integer");

                    b.Property<string>("Definition")
                        .HasColumnType("text");

                    b.Property<int>("JlptLevel")
                        .HasColumnType("integer");

                    b.Property<int?>("StrokeCount")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Models.CharacterLearning", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("AppUserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CharacterId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("LearnedCount")
                        .HasColumnType("integer");

                    b.Property<string>("LearningProgressId")
                        .HasColumnType("text");

                    b.Property<int>("LearningState")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("CharacterId");

                    b.HasIndex("LearningProgressId");

                    b.ToTable("CharacterLearnings");
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Models.Image", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("PublicId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Models.KanjiMeaning", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("CharacterId")
                        .HasColumnType("text");

                    b.Property<float?>("Priority")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.ToTable("KanjiMeanings");
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Models.Kunyomi", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("CharacterId")
                        .HasColumnType("text");

                    b.Property<string>("JapaneseWriting")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Romaji")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.ToTable("Kunyomis");
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Models.LearningProgress", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("LearningLevel")
                        .HasColumnType("integer");

                    b.Property<float>("Value")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("LearningProgresses");
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Models.Onyomi", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("CharacterId")
                        .HasColumnType("text");

                    b.Property<string>("JapaneseWriting")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Romaji")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.ToTable("Onyomis");
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Models.StringDefinition", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("CharacterId")
                        .HasColumnType("text");

                    b.Property<int>("Culture")
                        .HasColumnType("integer");

                    b.Property<string>("KanjiMeaningId")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("WordMeaningId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.HasIndex("KanjiMeaningId");

                    b.HasIndex("WordMeaningId");

                    b.ToTable("StringDefinitions");
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Models.TEST", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Models.Word", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("CharacterId")
                        .HasColumnType("text");

                    b.Property<string>("FullJapanese")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Furigana")
                        .HasColumnType("text");

                    b.Property<int>("PartOfSpeach")
                        .HasColumnType("integer");

                    b.Property<string>("Romaji")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.ToTable("Words");
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Models.WordMeaning", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<float?>("Priority")
                        .HasColumnType("real");

                    b.Property<string>("WordId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("WordId");

                    b.ToTable("WordMeanings");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Models.AppUser", b =>
                {
                    b.HasOne("DanilvarKanji.Shared.Models.Image", "ProfileImage")
                        .WithMany()
                        .HasForeignKey("ProfileImageId");

                    b.Navigation("ProfileImage");
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Models.CharacterLearning", b =>
                {
                    b.HasOne("DanilvarKanji.Shared.Models.AppUser", "AppUser")
                        .WithMany("CharacterLearnings")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DanilvarKanji.Shared.Models.Character", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DanilvarKanji.Shared.Models.LearningProgress", "LearningProgress")
                        .WithMany()
                        .HasForeignKey("LearningProgressId");

                    b.Navigation("AppUser");

                    b.Navigation("Character");

                    b.Navigation("LearningProgress");
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Models.KanjiMeaning", b =>
                {
                    b.HasOne("DanilvarKanji.Shared.Models.Character", null)
                        .WithMany("KanjiMeanings")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Models.Kunyomi", b =>
                {
                    b.HasOne("DanilvarKanji.Shared.Models.Character", null)
                        .WithMany("Kunyomis")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Models.Onyomi", b =>
                {
                    b.HasOne("DanilvarKanji.Shared.Models.Character", null)
                        .WithMany("Onyomis")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Models.StringDefinition", b =>
                {
                    b.HasOne("DanilvarKanji.Shared.Models.Character", null)
                        .WithMany("Mnemonics")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DanilvarKanji.Shared.Models.KanjiMeaning", null)
                        .WithMany("Definitions")
                        .HasForeignKey("KanjiMeaningId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DanilvarKanji.Shared.Models.WordMeaning", null)
                        .WithMany("Definitions")
                        .HasForeignKey("WordMeaningId");
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Models.Word", b =>
                {
                    b.HasOne("DanilvarKanji.Shared.Models.Character", null)
                        .WithMany("Words")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Models.WordMeaning", b =>
                {
                    b.HasOne("DanilvarKanji.Shared.Models.Word", null)
                        .WithMany("WordMeanings")
                        .HasForeignKey("WordId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DanilvarKanji.Shared.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DanilvarKanji.Shared.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DanilvarKanji.Shared.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("DanilvarKanji.Shared.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Models.AppUser", b =>
                {
                    b.Navigation("CharacterLearnings");
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Models.Character", b =>
                {
                    b.Navigation("KanjiMeanings");

                    b.Navigation("Kunyomis");

                    b.Navigation("Mnemonics");

                    b.Navigation("Onyomis");

                    b.Navigation("Words");
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Models.KanjiMeaning", b =>
                {
                    b.Navigation("Definitions");
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Models.Word", b =>
                {
                    b.Navigation("WordMeanings");
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Models.WordMeaning", b =>
                {
                    b.Navigation("Definitions");
                });
#pragma warning restore 612, 618
        }
    }
}
