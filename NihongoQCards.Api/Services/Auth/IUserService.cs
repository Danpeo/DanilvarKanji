namespace DanilvarKanji.Services.Auth;

public interface IUserService
{
    Task<bool> Exists(string userName);
}