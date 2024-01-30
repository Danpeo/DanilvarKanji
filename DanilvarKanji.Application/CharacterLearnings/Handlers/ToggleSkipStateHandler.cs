using DanilvarKanji.Application.CharacterLearnings.Commands;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Infrastructure.Data;
using MediatR;

namespace DanilvarKanji.Application.CharacterLearnings.Handlers;

public class ToggleSkipStateHandler : IRequestHandler<ToggleSkipStateCommand, Result<string>>
{
    private readonly ICharacterLearningRepository _characterLearningRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ToggleSkipStateHandler(ICharacterLearningRepository characterLearningRepository, IUnitOfWork unitOfWork)
    {
        _characterLearningRepository = characterLearningRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(ToggleSkipStateCommand request, CancellationToken cancellationToken)
    {
        await _characterLearningRepository.ToggleSkipStateAsync(request.Id, request.AppUser);
        
        if (await _unitOfWork.CompleteAsync())
            return Result.Success(request.Id);
        
        return Result.Failure<string>(General.UnProcessableRequest);
    }
}