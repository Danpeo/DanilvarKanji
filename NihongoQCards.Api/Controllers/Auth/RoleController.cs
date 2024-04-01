using DanilvarKanji.Shared.Domain.Entities;
using DanilvarKanji.Shared.Domain.Enumerations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DanilvarKanji.Controllers.Auth;

[Route("api/[controller]s")]
public class RoleController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<AppUser> _userManager;

    public RoleController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    [HttpPost("Initialiaze")]
    public async Task<IActionResult> InitialiazeRolesAsync()
    {
        foreach (string role in UserRole.Roles)
        {
            IdentityRole? foundRole = await _roleManager.FindByNameAsync(role);

            if (foundRole == null)
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(role));
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
            }
        }

        return Ok("Roles are initialized!");
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            IdentityRole? foundRole = await _roleManager.FindByNameAsync(name);

            if (foundRole != null)
                return Ok("Role is already exists!");

            IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
            if (result.Succeeded)
                return Ok("Role is successfully created!");

            return BadRequest("There was an error creating the role!");
        }

        return BadRequest("Name cannot be empty!");
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("All")]
    public IActionResult ListAsync()
    {
        return Ok(_roleManager.Roles.ToString());
    }
    
    [HttpPost("AssignRoleToUser")]
    public async Task<IActionResult> AssignRoleToUser(string roleName, string userEmail)
    {
        IdentityRole? foundRole = await _roleManager.FindByNameAsync(roleName);
        AppUser? user = await _userManager.FindByEmailAsync(userEmail);

        if (foundRole != null && user != null)
        {
            bool isInRole = await _userManager.IsInRoleAsync(user, foundRole.Name);

            if (!isInRole)
            {
                IdentityResult result = await _userManager.AddToRoleAsync(user, foundRole.Name);

                if (result.Succeeded)
                {
                    return Ok($"Role '{roleName}' is successfully added to user '{user.UserName}' - {user.Email}");
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            else
            {
                return BadRequest($"User '{user.UserName}' is already in role '{roleName}'");
            }
        }

        return BadRequest("Role or user not found");
    }

}