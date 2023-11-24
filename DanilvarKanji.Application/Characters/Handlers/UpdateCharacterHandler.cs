using AutoMapper;
using DanilvarKanji.Application.Characters.Commands;
using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Infrastructure.Data;
using MediatR;

namespace DanilvarKanji.Application.Characters.Handlers;

public class UpdateCharacterHandler : IRequestHandler<UpdateCharacterCommand, Result>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCharacterHandler(ICharacterRepository characterRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _characterRepository = characterRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result> Handle(UpdateCharacterCommand request, CancellationToken cancellationToken)
    {
        var character = new Character()
        {
            Definition = request.Definition,
            StrokeCount = request.StrokeCount,
            JlptLevel = request.JlptLevel,
            CharacterType = request.CharacterType,
            ChildCharacterIds = request.ChildCharacterIds,
            KanjiMeanings = request.KanjiMeanings,
            Kunyomis = request.Kunyomis,
            Onyomis = request.Onyomis,
            Mnemonics = request.Mnemonics,
            Words = request.Words
        };

        await _characterRepository.UpdateAsync(request.Id, character);

        if (await _unitOfWork.CompleteAsync())
            return Result.Success();

        return Result.Failure(General.UnProcessableRequest);
    }
}