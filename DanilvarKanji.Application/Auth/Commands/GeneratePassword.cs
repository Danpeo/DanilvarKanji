using MediatR;

namespace DanilvarKanji.Application.Auth.Commands;

public class GeneratePasswordCommand : IRequest<string>
{
    public int Length { get; set; } = 8;

    public bool RequireLowercase { get; set; } = true;

    public bool RequierUppercase { get; set; } = true;

    public bool RequireNonAlphanumeric { get; set; } = true;

    public GeneratePasswordCommand()
    {
        
    }

    public GeneratePasswordCommand(int length, bool requireLowercase, bool requierUppercase, bool requireNonAlphanumeric)
    {
        if (length < 6)
            Length = 6;
        
        Length = length;
        RequireLowercase = requireLowercase;
        RequierUppercase = requierUppercase;
        RequireNonAlphanumeric = requireNonAlphanumeric;
    }
}