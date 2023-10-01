using DanilvarKanji.Data;
using DanilvarKanji.Models;
using DanilvarKanji.Services.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DanilvarKanji.Services.Auth;

public class UserService : Service<ApplicationDbContext>, IUserService
{
    private UserManager<AppUser> _userManager;

    public UserService(ApplicationDbContext context, UserManager<AppUser> userManager) : base(context)
    {
        _userManager = userManager;
    }

    public async Task<bool> Exists(string userName) =>
        await _userManager.Users.AnyAsync(x => x.UserName == userName.ToLower());
}