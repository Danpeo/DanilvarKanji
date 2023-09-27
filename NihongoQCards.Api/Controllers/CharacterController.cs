using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NihongoQCards.Data;
using NihongoQCards.DTO;

namespace NihongoQCards.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CharacterController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CharacterController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CharacterDto characterDto)
    {
        return Ok();
    }
}