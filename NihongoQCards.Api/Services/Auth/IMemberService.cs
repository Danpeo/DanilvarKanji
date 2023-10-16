using DanilvarKanji.DTO;

namespace DanilvarKanji.Services.Auth;

public interface IMemberService
{
    Task<bool> Exist(string email);
    Task<MemberDto?> GetAsync(string email);
    Task<IEnumerable<MemberDto>> ListAsync();
    Task<bool> AnyExist();
}