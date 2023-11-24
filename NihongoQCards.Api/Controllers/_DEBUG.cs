using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DanilvarKanji.Controllers;


[ApiController]
[Route("api/[controller]")]
public class _DEBUG : ControllerBase
{
    [Authorize]
    [HttpGet("AUTHORIZED")]
    public async Task<IActionResult> Authorized()
    {
        return Ok("AUTHORIZED");
    }
}