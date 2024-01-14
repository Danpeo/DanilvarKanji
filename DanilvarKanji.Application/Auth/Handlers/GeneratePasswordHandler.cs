using DanilvarKanji.Application.Auth.Commands;
using MediatR;

namespace DanilvarKanji.Application.Auth.Handlers;

public class GeneratePasswordHandler : IRequestHandler<GeneratePasswordCommand, string>
{
    public Task<string> Handle(GeneratePasswordCommand request, CancellationToken cancellationToken)
    {
        const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
        const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string numericChars = "0123456789";
        const string specialChars = "!@#$%^&*()_-+=<>?";
        const string allChars = lowerChars + upperChars + numericChars + specialChars;

        var password = new char[request.Length];
        var random = new Random();

        for (int i = 0; i < request.Length; i++)
        {
            string currentChars = i switch
            {
                0 when request.RequireLowercase => lowerChars,
                1 when request.RequierUppercase => upperChars,
                2 when request.RequireNonAlphanumeric => specialChars,
                _ => allChars
            };

            password[i] = currentChars[random.Next(currentChars.Length)];
        }

        password = password.OrderBy(c => random.Next()).ToArray();

        return Task.FromResult(new string(password));
    }
}
