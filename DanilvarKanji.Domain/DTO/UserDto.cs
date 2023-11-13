using DanilvarKanji.Domain.Enumerations;

namespace DanilvarKanji.Domain.DTO;

public class UserDto
{
    public string? Email { get; set; }
    public string? UserName { get; set; }
    public string Token { get; set; }
    public JlptLevel JlptLevel { get; set; }
}