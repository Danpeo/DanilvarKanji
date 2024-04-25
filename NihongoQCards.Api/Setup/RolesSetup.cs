using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Domain.Shared.Enumerations;
using Microsoft.AspNetCore.Identity;

namespace DanilvarKanji.Setup;

public static class RolesSetup
{
    public static async Task AddRoles(RoleManager<AppUserRole> roleManager)
    {
        foreach (string role in UserRole.Roles)
        {
            bool roleExists = await roleManager.RoleExistsAsync(role);
            if (!roleExists)
            {
                await roleManager.CreateAsync(new AppUserRole(role));
            }
        }
    }
}