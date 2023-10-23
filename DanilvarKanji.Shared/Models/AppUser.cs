using DanilvarKanji.Shared.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace DanilvarKanji.Shared.Models;

public class AppUser : IdentityUser
{
    public Image? ProfileImage { get; set; }
    public JlptLevel JlptLevel { get; set; }
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    public DateTime LastStudied { get; set; } = DateTime.UtcNow;
    public ICollection<CharacterLearning> CharacterLearnings { get; set; }
}