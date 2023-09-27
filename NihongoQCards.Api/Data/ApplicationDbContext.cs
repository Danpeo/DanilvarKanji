using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NihongoQCards.Models;

namespace NihongoQCards.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<Meaning> Meanings { get; set; }
    public DbSet<Word> Words { get; set; }
    
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
}