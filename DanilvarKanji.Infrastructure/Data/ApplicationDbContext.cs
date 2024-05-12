using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Domain.Shared.Entities.Flashcards;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DanilvarKanji.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
  public ApplicationDbContext(DbContextOptions options)
    : base(options)
  {
  }

  public DbSet<AppUser> AppUsers { get; set; }
  public DbSet<Character> Characters { get; set; }

  public DbSet<CharacterLearning> CharacterLearnings { get; set; }

  /*
  public DbSet<LearningProgress> LearningProgresses { get; set; }
  */
  public DbSet<KanjiMeaning> KanjiMeanings { get; set; }

  /*
  public DbSet<WordMeaning> WordMeanings { get; set; }
  */
  /*
  public DbSet<Word> Words { get; set; }
  */
  public DbSet<Kunyomi> Kunyomis { get; set; }
  public DbSet<Onyomi> Onyomis { get; set; }
  public DbSet<Exercise> Exercises { get; set; }

  public DbSet<ReviewSession> ReviewSessions { get; set; }

  /*
  public DbSet<StringDefinition> StringDefinitions { get; set; }
  */
  public DbSet<FlashcardCollection> FlashcardCollections { get; set; }
  public DbSet<Flashcard> Flashcards { get; set; }
  public DbSet<EmailCode> EmailCodes { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder
      .Entity<CharacterLearning>()
      .HasOne(cl => cl.Character)
      .WithMany()
      .OnDelete(DeleteBehavior.Cascade);

    modelBuilder
      .Entity<Character>()
      .HasMany(x => x.Kunyomis)
      .WithOne()
      .OnDelete(DeleteBehavior.Cascade);

    modelBuilder
      .Entity<Character>()
      .HasMany(x => x.Onyomis)
      .WithOne()
      .OnDelete(DeleteBehavior.Cascade);

    modelBuilder
      .Entity<Character>()
      .HasMany(x => x.KanjiMeanings)
      .WithOne()
      .OnDelete(DeleteBehavior.Cascade);

    modelBuilder
      .Entity<Character>()
      .HasMany(x => x.Mnemonics)
      .WithOne()
      .OnDelete(DeleteBehavior.Cascade);

    /*modelBuilder.Entity<Character>()
        .HasMany(x => x.Words)
        .WithOne()
        .OnDelete(DeleteBehavior.Cascade);*/

    modelBuilder
      .Entity<KanjiMeaning>()
      .HasMany(x => x.Definitions)
      .WithOne()
      .OnDelete(DeleteBehavior.Cascade);
  }
}