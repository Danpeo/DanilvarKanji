using DanilvarKanji.Shared.Models.Enums;

namespace DanilvarKanji.Shared.DTO;

public class RegisterDto
{
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public JlptLevel JlptLevel { get; set; }
}