using DanilvarKanji.Domain.Shared.Enumerations;
using Microsoft.AspNetCore.Identity;

namespace DanilvarKanji.Domain.Shared.Entities;

public class AppUser : IdentityUser
{
  public JlptLevel JlptLevel { get; set; }
  public string Role { get; set; } = UserRole.User;
  public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
  public DateTime LastStudied { get; set; } = DateTime.UtcNow;
  public int QtyOfCharsForLearningForDay { get; set; } = 5;
  public int XP { get; set; }
  public string? RefreshToken { get; set; }
  public DateTime RefreshTokenExpiry { get; set; }
}