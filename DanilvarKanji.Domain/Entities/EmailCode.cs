using System.ComponentModel.DataAnnotations;
using Danilvar.Entity;

namespace DanilvarKanji.Domain.Entities;

public class EmailCode : Entity
{
    public EmailCode()
    {
    }

    public EmailCode(string email, string? generatedCode)
    {
        Email = email;
        GeneratedCode = generatedCode;
    }

    [MaxLength(100)] public string Email { get; } = string.Empty;

    [MaxLength(15)] public string? GeneratedCode { get; } = string.Empty;
}