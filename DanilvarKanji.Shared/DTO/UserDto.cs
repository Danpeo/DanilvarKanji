using DanilvarKanji.Shared.Entities.Enums;

namespace DanilvarKanji.Shared.DTO;

public class UserDto
{
    public string? Email { get; set; }
    public string? UserName { get; set; }
    public string Token { get; set; }
    public JlptLevel JlptLevel { get; set; }
}