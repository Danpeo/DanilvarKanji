using DanilvarKanji.Application.Characters.Commands;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Infrastructure.Data;
using MediatR;

namespace DanilvarKanji.Application.Characters.Handlers;

public class DeleteAllCharactersHandler : IRequestHandler<DeleteAllCharactersCommand, Result>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAllCharactersHandler(ICharacterRepository characterRepository, IUnitOfWork unitOfWork)
    {
        _characterRepository = characterRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteAllCharactersCommand request, CancellationToken cancellationToken)
    {
        _characterRepository.DeleteAll();
        
        if (await _unitOfWork.CompleteAsync())
            return Result.Success();

        return Result.Failure(General.UnProcessableRequest);
    }
}