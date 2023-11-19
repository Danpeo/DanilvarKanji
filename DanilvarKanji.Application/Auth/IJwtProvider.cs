using DanilvarKanji.Domain.Entities;

namespace DanilvarKanji.Application.Auth;

public interface IJwtProvider
{
    string Create(AppUser user);
}