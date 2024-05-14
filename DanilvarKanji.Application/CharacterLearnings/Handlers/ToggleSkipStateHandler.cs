using DanilvarKanji.Application.CharacterLearnings.Commands;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Infrastructure.Data;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DanilvarKanji.Application.CharacterLearnings.Handlers;

public class ToggleSkipStateHandler : IRequestHandler<ToggleSkipStateCommand, Result<string>>
{
    private readonly ICharacterLearningRepository _characterLearningRepository;
    private readonly ILogger<ToggleSkipStateHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public ToggleSkipStateHandler(
        ICharacterLearningRepository characterLearningRepository,
        IUnitOfWork unitOfWork,
        ILogger<ToggleSkipStateHandler> logger
    )
    {
        _characterLearningRepository = characterLearningRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<string>> Handle(
        ToggleSkipStateCommand request,
        CancellationToken cancellationToken
    )
    {
        await _characterLearningRepository.ToggleSkipStateAsync(request.Id, request.AppUser);

        if (await _unitOfWork.CompleteAsync())
        {
            _logger.LogInformation("Toggled skip state: {@request}, {@dt}", request, DateTime.UtcNow);
            return Result.Success(request.Id);
        }

        _logger.LogError("Failed to toggle skip state: {@request}, {@dt}", request, DateTime.UtcNow);
        return Result.Failure<string>(General.UnProcessableRequest);
    }
}