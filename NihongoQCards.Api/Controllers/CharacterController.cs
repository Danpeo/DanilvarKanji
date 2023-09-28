using DanilvarKanji.Attributes;
using DanilvarKanji.Data;
using DanilvarKanji.DTO;
using DanilvarKanji.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DanilvarKanji.Controllers;

[Todo("Add Authorize later")]
[Route("api/[controller]")]
[ApiController]
public class CharacterController : ControllerBase
{
    private readonly ICharacterService _characterService;

    public CharacterController(ApplicationDbContext context, ICharacterService characterService)
    {
        _characterService = characterService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CharacterDto characterDto)
    {
        bool result = await _characterService.CreateAsync(characterDto);
        return result ? Ok() : NotFound();
    }
}