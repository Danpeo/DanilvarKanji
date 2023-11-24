using DanilvarKanji.Domain.Entities;

namespace DanilvarKanji.Infrastructure.Auth;

public interface IJwtProvider
{
    string Create(AppUser user);
}