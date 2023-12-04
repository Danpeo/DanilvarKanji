using DanilvarKanji.Domain.Enumerations;
using Microsoft.AspNetCore.Identity;

namespace DanilvarKanji.Domain.Entities;

public class AppUser : IdentityUser
{
    public Image? ProfileImage { get; set; }
    public JlptLevel JlptLevel { get; set; }
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    public DateTime LastStudied { get; set; } = DateTime.UtcNow;
    //public ICollection<CharacterLearning> CharacterLearnings { get; set; }
    
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiry { get; set; }
}