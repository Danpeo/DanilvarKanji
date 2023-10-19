using DanilvarKanji.DTO;
using DanilvarKanji.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

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

    [EnableQuery]
    [HttpGet("{email}")]
    public async Task<IActionResult> GetAsync(string email)
    {
        if (!await _memberService.Exist(email))
            return NotFound("Member with this email was not found");

        MemberDto? member = await _memberService.GetAsync(email);

        return Ok(member);
    }

    [HttpGet("{email}/Partial")]
    public async Task<IActionResult> GetPartialAsync(string email, [FromQuery] IEnumerable<string> fields)
    {
        if (!await _memberService.Exist(email))
            return NotFound("Character with this email was not found");

        MemberDto? member = await _memberService.GetPartialAsync(email, fields);

        if (member != null)
        {
            Dictionary<string,object?> nonNullFields = member.GetType()
                .GetProperties()
                .Where(prop => prop.GetValue(member) != null)
                .ToDictionary(prop => prop.Name, prop => prop.GetValue(member));

            return Ok(nonNullFields);
        }

        return NotFound("Partial data was not found");
    }

    [EnableQuery]
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