using DanilvarKanji.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DanilvarKanji.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<KanjiMeaning> KanjiMeanings { get; set; }
    public DbSet<WordMeaning> WordMeanings { get; set; }
    public DbSet<Word> Words { get; set; }
    public DbSet<Kunyomi> Kunyomis { get; set; }
    public DbSet<Onyomi> Onyomis { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
    }
}