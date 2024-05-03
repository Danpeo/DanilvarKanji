using DanilvarKanji.Application.Characters.Commands;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Infrastructure.Data;
using MediatR;

namespace DanilvarKanji.Application.Characters.Handlers;

public class UpdateCharacterHandler : IRequestHandler<UpdateCharacterCommand, Result<string>>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCharacterHandler(ICharacterRepository characterRepository, IUnitOfWork unitOfWork)
    {
        _characterRepository = characterRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(UpdateCharacterCommand request, CancellationToken cancellationToken)
    {
        var character = await _characterRepository.GetAsync(request.Id);

        if (character != null)
        {
            character.Definition = request.Definition;
            character.StrokeCount = request.StrokeCount;
            character.JlptLevel = request.JlptLevel;
            character.CharacterType = request.CharacterType;
            character.KanjiMeanings = request.KanjiMeanings;
            character.Kunyomis = request.Kunyomis;
            character.Onyomis = request.Onyomis;
            character.Mnemonics = request.Mnemonics;
        }

        await _characterRepository.UpdateAsync(request.Id, character);

        if (await _unitOfWork.CompleteAsync())
            return Result.Success(character.Id);

        return Result.Failure<string>(General.UnProcessableRequest);
    }
}