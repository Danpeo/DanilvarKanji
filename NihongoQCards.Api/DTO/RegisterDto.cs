using DanilvarKanji.Models.Enums;

namespace DanilvarKanji.DTO;

public class RegisterDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public JlptLevel JlptLevel { get; set; }
}