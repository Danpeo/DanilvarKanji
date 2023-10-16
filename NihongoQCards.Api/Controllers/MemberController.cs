using DanilvarKanji.DTO;
using DanilvarKanji.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DanilvarKanji.Controllers;

[Authorize]
[ApiController]
[Route("Api/[controller]s")]
public class MemberController : ControllerBase
{
    private readonly IMemberService _memberService;

    public MemberController(IMemberService memberService)
    {
        _memberService = memberService;
    }

    [HttpGet]
    public async Task<IActionResult> ListAsync()
    {
        if (await _memberService.AnyExist())
        {
            IEnumerable<MemberDto> members = await _memberService.ListAsync();
            return Ok(members);
        } 
        
        return NotFound("No members");
    }
}