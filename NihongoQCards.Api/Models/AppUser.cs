using DanilvarKanji.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace DanilvarKanji.Models;

public class AppUser : IdentityUser
{
    public string? Photo { get; set; }
    public JlptLevel JlptLevel { get; set; }
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    public DateTime LastStudied { get; set; } = DateTime.UtcNow;
    public ICollection<CharacterLearning> CharacterLearnings { get; set; }
}