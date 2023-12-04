using AutoMapper;
using DanilvarKanji.Application.CharacterLearnings.Commands;
using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Infrastructure.Data;
using MediatR;

namespace DanilvarKanji.Application.CharacterLearnings.Handlers;

// ReSharper disable once UnusedType.Global
public class CreateCharacterLearningHandler : IRequestHandler<CreateCharacterLearningCommand, Result>
{
    private readonly ICharacterLearningRepository _characterLearningRepository;
    private readonly ICharacterRepository _characterRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCharacterLearningHandler(ICharacterLearningRepository characterLearningRepository, IMapper mapper,
        IUnitOfWork unitOfWork, ICharacterRepository characterRepository)
    {
        _characterLearningRepository = characterLearningRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _characterRepository = characterRepository;
    }

    public async Task<Result> Handle(CreateCharacterLearningCommand request, CancellationToken cancellationToken)
    {
        /*
        var character = _mapper.Map<CharacterLearning>(request);
        */

        Character? character = await _characterRepository.GetAsync(request.CharacterId);
        
        var characterLearning = new CharacterLearning()
        {
            AppUser = request.AppUser,
            Character = character ?? new Character(),
            LearningProgress = new LearningProgress(),
            LearningState = request.LearningState
        };
        
        _characterLearningRepository.Create(characterLearning);
        if (await _unitOfWork.CompleteAsync())
            return Result.Success();

        return Result.Failure(General.UnProcessableRequest);
    }
}