﻿// <auto-generated />
using System;
using System.Collections.Generic;
using DanilvarKanji.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DanilvarKanji.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240118071301_testvps3")]
    partial class testvps3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DanilvarKanji.Domain.Entities.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("AppUserRoleId")
                        .HasColumnType("text");

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

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime>("RefreshTokenExpiry")
                        .HasColumnType("timestamp with time zone");

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

                    b.HasIndex("AppUserRoleId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.HasIndex("ProfileImageId");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("DanilvarKanji.Domain.Entities.Character", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("CharacterType")
                        .HasColumnType("integer");

                    b.Property<List<string>>("ChildCharacterIds")
                        .HasColumnType("text[]");

                    b.Property<string>("Definition")
                        .HasColumnType("text");

                    b.Property<int>("JlptLevel")
                        .HasColumnType("integer");

                    b.Property<int?>("StrokeCount")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("DanilvarKanji.Domain.Entities.CharacterLearning", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("AppUserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CharacterId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("LastReviewDateTime")
                        .HasColumnType("timestamp with time zone");

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

            modelBuilder.Entity("DanilvarKanji.Domain.Entities.Exercises.Exercise", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("AppUserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CharacterId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ExcerciseDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ExerciseType")
                        .HasColumnType("integer");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("boolean");

                    b.Property<string>("ReviewSessionId")
                        .HasColumnType("text");

                    b.Property<int>("ReviewType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("CharacterId");

                    b.HasIndex("ReviewSessionId");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("DanilvarKanji.Domain.Entities.Image", b =>
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

            modelBuilder.Entity("DanilvarKanji.Domain.Entities.KanjiMeaning", b =>
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

            modelBuilder.Entity("DanilvarKanji.Domain.Entities.Kunyomi", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("CharacterId")
                        .HasColumnType("text");

                    b.Property<string>("JapaneseWriting")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Romaji")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.ToTable("Kunyomis");
                });

            modelBuilder.Entity("DanilvarKanji.Domain.Entities.LearningProgress", b =>
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

            modelBuilder.Entity("DanilvarKanji.Domain.Entities.Onyomi", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("CharacterId")
                        .HasColumnType("text");

                    b.Property<string>("JapaneseWriting")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Romaji")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.ToTable("Onyomis");
                });

            modelBuilder.Entity("DanilvarKanji.Domain.Entities.ReviewSession", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("AppUserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ReviewDataTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.ToTable("ReviewSessions");
                });

            modelBuilder.Entity("DanilvarKanji.Domain.Entities.StringDefinition", b =>
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

                    b.Property<string>("WordId")
                        .HasColumnType("text");

                    b.Property<string>("WordMeaningId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.HasIndex("KanjiMeaningId");

                    b.HasIndex("WordId");

                    b.HasIndex("WordMeaningId");

                    b.ToTable("StringDefinitions");
                });

            modelBuilder.Entity("DanilvarKanji.Domain.Entities.TEST", b =>
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

            modelBuilder.Entity("DanilvarKanji.Domain.Entities.Word", b =>
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

            modelBuilder.Entity("DanilvarKanji.Domain.Entities.WordMeaning", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<float?>("Priority")
                        .HasColumnType("real");

                    b.HasKey("Id");

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

            modelBuilder.Entity("DanilvarKanji.Domain.Entities.AppUser", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", "AppUserRole")
                        .WithMany()
                        .HasForeignKey("AppUserRoleId");

                    b.HasOne("DanilvarKanji.Domain.Entities.Image", "ProfileImage")
                        .WithMany()
                        .HasForeignKey("ProfileImageId");

                    b.Navigation("AppUserRole");

                    b.Navigation("ProfileImage");
                });

            modelBuilder.Entity("DanilvarKanji.Domain.Entities.CharacterLearning", b =>
                {
                    b.HasOne("DanilvarKanji.Domain.Entities.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DanilvarKanji.Domain.Entities.Character", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DanilvarKanji.Domain.Entities.LearningProgress", "LearningProgress")
                        .WithMany()
                        .HasForeignKey("LearningProgressId");

                    b.Navigation("AppUser");

                    b.Navigation("Character");

                    b.Navigation("LearningProgress");
                });

            modelBuilder.Entity("DanilvarKanji.Domain.Entities.Exercises.Exercise", b =>
                {
                    b.HasOne("DanilvarKanji.Domain.Entities.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DanilvarKanji.Domain.Entities.Character", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DanilvarKanji.Domain.Entities.ReviewSession", null)
                        .WithMany("Exercises")
                        .HasForeignKey("ReviewSessionId");

                    b.Navigation("AppUser");

                    b.Navigation("Character");
                });

            modelBuilder.Entity("DanilvarKanji.Domain.Entities.KanjiMeaning", b =>
                {
                    b.HasOne("DanilvarKanji.Domain.Entities.Character", null)
                        .WithMany("KanjiMeanings")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DanilvarKanji.Domain.Entities.Kunyomi", b =>
                {
                    b.HasOne("DanilvarKanji.Domain.Entities.Character", null)
                        .WithMany("Kunyomis")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DanilvarKanji.Domain.Entities.Onyomi", b =>
                {
                    b.HasOne("DanilvarKanji.Domain.Entities.Character", null)
                        .WithMany("Onyomis")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DanilvarKanji.Domain.Entities.ReviewSession", b =>
                {
                    b.HasOne("DanilvarKanji.Domain.Entities.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("DanilvarKanji.Domain.Entities.StringDefinition", b =>
                {
                    b.HasOne("DanilvarKanji.Domain.Entities.Character", null)
                        .WithMany("Mnemonics")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DanilvarKanji.Domain.Entities.KanjiMeaning", null)
                        .WithMany("Definitions")
                        .HasForeignKey("KanjiMeaningId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DanilvarKanji.Domain.Entities.Word", null)
                        .WithMany("WordMeanings")
                        .HasForeignKey("WordId");

                    b.HasOne("DanilvarKanji.Domain.Entities.WordMeaning", null)
                        .WithMany("Definitions")
                        .HasForeignKey("WordMeaningId");
                });

            modelBuilder.Entity("DanilvarKanji.Domain.Entities.Word", b =>
                {
                    b.HasOne("DanilvarKanji.Domain.Entities.Character", null)
                        .WithMany("Words")
                        .HasForeignKey("CharacterId")
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
                    b.HasOne("DanilvarKanji.Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DanilvarKanji.Domain.Entities.AppUser", null)
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

                    b.HasOne("DanilvarKanji.Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("DanilvarKanji.Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DanilvarKanji.Domain.Entities.Character", b =>
                {
                    b.Navigation("KanjiMeanings");

                    b.Navigation("Kunyomis");

                    b.Navigation("Mnemonics");

                    b.Navigation("Onyomis");

                    b.Navigation("Words");
                });

            modelBuilder.Entity("DanilvarKanji.Domain.Entities.KanjiMeaning", b =>
                {
                    b.Navigation("Definitions");
                });

            modelBuilder.Entity("DanilvarKanji.Domain.Entities.ReviewSession", b =>
                {
                    b.Navigation("Exercises");
                });

            modelBuilder.Entity("DanilvarKanji.Domain.Entities.Word", b =>
                {
                    b.Navigation("WordMeanings");
                });

            modelBuilder.Entity("DanilvarKanji.Domain.Entities.WordMeaning", b =>
                {
                    b.Navigation("Definitions");
                });
#pragma warning restore 612, 618
        }
    }
}
