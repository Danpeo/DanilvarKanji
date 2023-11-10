using DanilvarKanji.Data;
using DanilvarKanji.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DanilvarKanji.Controllers;

[ApiController]
[Route("Api/[controller]s")]
public class TestController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TestController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] TEST characterDto)
    {
        var character = new TEST
        {
            Name = characterDto.Name
        };
        
        
        _context.Tests.Add(character);
        int saved = _context.SaveChanges();
        return saved > 0 ? Ok() : BadRequest();
    }
}