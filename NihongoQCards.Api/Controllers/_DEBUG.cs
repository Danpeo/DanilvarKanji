using DanilvarKanji.Domain.RepositoryAbstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DanilvarKanji.Controllers;

[ApiController]
[Route("api/[controller]")]
public class _DEBUG : ControllerBase
{
    private readonly ICharacterLearningRepository _characterLearningRepository;

    public _DEBUG(ICharacterLearningRepository characterLearningRepository)
    {
        _characterLearningRepository = characterLearningRepository;
    }

    [Authorize]
    [HttpGet("AUTHORIZED")]
    public async Task<IActionResult> Authorized()
    {
        return Ok("AUTHORIZED");
    }

    [HttpGet("TEST_CHARACTER_LEARNING_SETTINGS")]
    public IActionResult TestCharacterLearningSettings()
    {
        return Ok(_characterLearningRepository.TestLearningSettings("ahah"));
    }
}