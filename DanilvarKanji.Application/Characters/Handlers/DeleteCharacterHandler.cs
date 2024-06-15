using DanilvarKanji.Application.Characters.Commands;
using DanilvarKanji.Domain.Enumerations;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Persistance;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Infrastructure.Data;
using MediatR;

namespace DanilvarKanji.Application.Characters.Handlers;

// ReSharper disable once UnusedType.Global
public class DeleteCharacterHandler : IRequestHandler<DeleteCharacterCommand, Result>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCharacterHandler(ICharacterRepository characterRepository, IUnitOfWork unitOfWork)
    {
        _characterRepository = characterRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        DeleteCharacterCommand request,
        CancellationToken cancellationToken
    )
    {
        if (await _characterRepository.ExistAsync(request.Id))
        {
            await _characterRepository.DeleteAsync(request.Id);

            if (await _unitOfWork.CompleteAsync())
                return Result.Success();
        }

        return Result.Failure(CharacterErr.NotFound(request.Culture));
    }
}