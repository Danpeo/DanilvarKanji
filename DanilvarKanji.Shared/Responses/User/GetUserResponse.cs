using DanilvarKanji.Domain.Enumerations;

namespace DanilvarKanji.Shared.Responses.User;

public class GetUserResponse
{
    public string Id { get; set; }
    public string? Email { get; set; }
    public string? UserName { get; set; }
    public JlptLevel JlptLevel { get; set; }
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    public DateTime LastStudied { get; set; } = DateTime.UtcNow;
}