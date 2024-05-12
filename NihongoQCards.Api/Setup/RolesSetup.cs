using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Domain.Shared.Enumerations;
using Microsoft.AspNetCore.Identity;

namespace DanilvarKanji.Setup;

public static class RolesSetup
{
  public static async Task AddRoles(RoleManager<AppUserRole> roleManager)
  {
    foreach (var role in UserRole.Roles)
    {
      var roleExists = await roleManager.RoleExistsAsync(role);
      if (!roleExists) await roleManager.CreateAsync(new AppUserRole(role));
    }
  }
}