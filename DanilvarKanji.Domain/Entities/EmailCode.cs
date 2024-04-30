using Danilvar.Entity;

namespace DanilvarKanji.Domain.Entities;

public class EmailCode : Entity
{
    public string Email { get; set; } = string.Empty;

    public string? GeneratedCode { get; set; } = string.Empty;

    public EmailCode()
    {
        
    }
    
    public EmailCode(string email, string? generatedCode)
    {
        Email = email;
        GeneratedCode = generatedCode;
    }
}