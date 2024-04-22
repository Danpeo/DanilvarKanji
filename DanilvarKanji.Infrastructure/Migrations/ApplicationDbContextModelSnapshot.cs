﻿// <auto-generated />
using System;
using DanilvarKanji.Infrastructure.Data;
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

            modelBuilder.Entity("DanilvarKanji.Shared.Domain.Entities.AppUser", b =>
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

                    b.Property<int>("QtyOfCharsForLearningForDay")
                        .HasColumnType("integer");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime>("RefreshTokenExpiry")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<int>("XP")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Domain.Entities.Character", b =>
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

            modelBuilder.Entity("DanilvarKanji.Shared.Domain.Entities.CharacterLearning", b =>
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

                    b.Property<bool>("LastReviewWasCorrect")
                        .HasColumnType("boolean");

                    b.Property<int>("LearnedCount")
                        .HasColumnType("integer");

                    b.Property<int>("LearningLevel")
                        .HasColumnType("integer");

                    b.Property<float>("LearningLevelValue")
                        .HasColumnType("real");

                    b.Property<int>("LearningState")
                        .HasColumnType("integer");

                    b.Property<DateTime>("NextReviewDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("CharacterId");

                    b.ToTable("CharacterLearnings");
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Domain.Entities.Exercise", b =>
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

                    b.Property<int>("ExerciseSubject")
                        .HasColumnType("integer");

                    b.Property<int>("ExerciseType")
                        .HasColumnType("integer");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("boolean");

                    b.Property<string>("ReviewSessionId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("CharacterId");

                    b.HasIndex("ReviewSessionId");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Domain.Entities.Flashcards.Flashcard", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Back")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("DoRemember")
                        .HasColumnType("boolean");

                    b.Property<string>("FlashcardCollectionId")
                        .HasColumnType("text");

                    b.Property<string>("Main")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RememberedInARow")
                        .HasColumnType("integer");

                    b.Property<string>("Sub")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FlashcardCollectionId");

                    b.ToTable("Flashcards");
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Domain.Entities.Flashcards.FlashcardCollection", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("AppUserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.ToTable("FlashcardCollections");
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Domain.Entities.KanjiMeaning", b =>
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

            modelBuilder.Entity("DanilvarKanji.Shared.Domain.Entities.Kunyomi", b =>
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

            modelBuilder.Entity("DanilvarKanji.Shared.Domain.Entities.Onyomi", b =>
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

            modelBuilder.Entity("DanilvarKanji.Shared.Domain.Entities.ReviewSession", b =>
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

            modelBuilder.Entity("DanilvarKanji.Shared.Domain.Entities.StringDefinition", b =>
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

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.HasIndex("KanjiMeaningId");

                    b.ToTable("StringDefinitions");
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Domain.Entities.Test", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("A")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Tests");
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

            modelBuilder.Entity("DanilvarKanji.Shared.Domain.Entities.CharacterLearning", b =>
                {
                    b.HasOne("DanilvarKanji.Shared.Domain.Entities.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DanilvarKanji.Shared.Domain.Entities.Character", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Character");
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Domain.Entities.Exercise", b =>
                {
                    b.HasOne("DanilvarKanji.Shared.Domain.Entities.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DanilvarKanji.Shared.Domain.Entities.Character", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DanilvarKanji.Shared.Domain.Entities.ReviewSession", null)
                        .WithMany("Exercises")
                        .HasForeignKey("ReviewSessionId");

                    b.Navigation("AppUser");

                    b.Navigation("Character");
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Domain.Entities.Flashcards.Flashcard", b =>
                {
                    b.HasOne("DanilvarKanji.Shared.Domain.Entities.Flashcards.FlashcardCollection", null)
                        .WithMany("Flashcards")
                        .HasForeignKey("FlashcardCollectionId");
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Domain.Entities.Flashcards.FlashcardCollection", b =>
                {
                    b.HasOne("DanilvarKanji.Shared.Domain.Entities.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Domain.Entities.KanjiMeaning", b =>
                {
                    b.HasOne("DanilvarKanji.Shared.Domain.Entities.Character", null)
                        .WithMany("KanjiMeanings")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Domain.Entities.Kunyomi", b =>
                {
                    b.HasOne("DanilvarKanji.Shared.Domain.Entities.Character", null)
                        .WithMany("Kunyomis")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Domain.Entities.Onyomi", b =>
                {
                    b.HasOne("DanilvarKanji.Shared.Domain.Entities.Character", null)
                        .WithMany("Onyomis")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Domain.Entities.ReviewSession", b =>
                {
                    b.HasOne("DanilvarKanji.Shared.Domain.Entities.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Domain.Entities.StringDefinition", b =>
                {
                    b.HasOne("DanilvarKanji.Shared.Domain.Entities.Character", null)
                        .WithMany("Mnemonics")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DanilvarKanji.Shared.Domain.Entities.KanjiMeaning", null)
                        .WithMany("Definitions")
                        .HasForeignKey("KanjiMeaningId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Domain.Entities.Test", b =>
                {
                    b.OwnsOne("DanilvarKanji.Shared.Domain.Entities.InTest", "InTest", b1 =>
                        {
                            b1.Property<string>("TestId")
                                .HasColumnType("text");

                            b1.Property<int>("B")
                                .HasColumnType("integer");

                            b1.HasKey("TestId");

                            b1.ToTable("Tests");

                            b1.WithOwner()
                                .HasForeignKey("TestId");
                        });

                    b.Navigation("InTest")
                        .IsRequired();
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
                    b.HasOne("DanilvarKanji.Shared.Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DanilvarKanji.Shared.Domain.Entities.AppUser", null)
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

                    b.HasOne("DanilvarKanji.Shared.Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("DanilvarKanji.Shared.Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Domain.Entities.Character", b =>
                {
                    b.Navigation("KanjiMeanings");

                    b.Navigation("Kunyomis");

                    b.Navigation("Mnemonics");

                    b.Navigation("Onyomis");
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Domain.Entities.Flashcards.FlashcardCollection", b =>
                {
                    b.Navigation("Flashcards");
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Domain.Entities.KanjiMeaning", b =>
                {
                    b.Navigation("Definitions");
                });

            modelBuilder.Entity("DanilvarKanji.Shared.Domain.Entities.ReviewSession", b =>
                {
                    b.Navigation("Exercises");
                });
#pragma warning restore 612, 618
        }
    }
}
