using AutoMapper;
using AutoMapper.QueryableExtensions;
using DanilvarKanji.Data;
using DanilvarKanji.DTO;
using DanilvarKanji.Models;
using DanilvarKanji.Services.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DanilvarKanji.Services.Auth;

public class MemberService : Service<ApplicationDbContext>, IMemberService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public MemberService(ApplicationDbContext context, UserManager<AppUser> userManager, IMapper mapper) : base(context)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<MemberDto?> GetAsync(string email)
    {
        return await Context.Users
            .Where(x => x.UserName == email)
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<MemberDto>> ListAsync()
    {
        List<AppUser> members = await Context.AppUsers
            .Include(x => x.CharacterLearnings)
            .ToListAsync();
        
        return _mapper.Map<IEnumerable<MemberDto>>(members);
    }

    public async Task<bool> Exist(string email) =>
        await _userManager.Users.AnyAsync(x => x.UserName == email.ToLower());
    
    public async Task<bool> AnyExist() =>
        await _userManager.Users.AnyAsync();
}