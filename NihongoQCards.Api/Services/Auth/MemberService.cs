using System.Reflection;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DanilvarKanji.Data;
using DanilvarKanji.Services.Common;
using DanilvarKanji.Domain.DTO;
using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Infrastructure.Data;
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

    public async Task<IEnumerable<MemberDto>> ListAsync()
    {
        List<AppUser> members = await Context.AppUsers
            .Include(x => x.CharacterLearnings)
            .ToListAsync();

        return _mapper.Map<IEnumerable<MemberDto>>(members);
    }

    public async Task<MemberDto?> GetAsync(string email)
    {
        return await Context.AppUsers
            .Where(x => x.Email == email)
            .Include(x => x.CharacterLearnings)
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }

    public async Task<MemberDto?> GetPartialAsync(string email, IEnumerable<string> fields)
    {
        AppUser? member = await Context.AppUsers
            .Where(x => x.Email == email)
            .Include(x => x.CharacterLearnings)
            .FirstOrDefaultAsync();

        MemberDto? memberDto = _mapper.Map<MemberDto>(member);

        if (fields.Any())
        {
            PropertyInfo[] propertyInfos = typeof(MemberDto).GetProperties();
            IEnumerable<PropertyInfo> includedProperties =
                propertyInfos.Where(p => fields.Contains(p.Name, StringComparer.OrdinalIgnoreCase));

            IEnumerable<PropertyInfo> excludedProperties = propertyInfos.Except(includedProperties);

            foreach (PropertyInfo property in excludedProperties) 
                property.SetValue(memberDto, null);
        }

        return memberDto;
    }

    public async Task<bool> Exist(string email) =>
        await _userManager.Users.AnyAsync(x => x.Email == email.ToLower());

    public async Task<bool> AnyExist() =>
        await _userManager.Users.AnyAsync();
}