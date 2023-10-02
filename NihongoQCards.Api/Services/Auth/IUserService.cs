namespace DanilvarKanji.Services.Auth;

public interface IUserService
{
    Task<bool> Exist(string userName);
}