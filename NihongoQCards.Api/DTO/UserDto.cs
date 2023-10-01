using DanilvarKanji.Models.Enums;

namespace DanilvarKanji.DTO;

public class UserDto
{
    public string? UserName { get; set; }
    public string Token { get; set; }
    public JlptLevel JlptLevel { get; set; }
}