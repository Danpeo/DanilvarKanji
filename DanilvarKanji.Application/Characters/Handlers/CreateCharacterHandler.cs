using AutoMapper;
using DanilvarKanji.Application.Characters.Commands;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Infrastructure.Data;
using MediatR;

namespace DanilvarKanji.Application.Characters.Handlers;

// ReSharper disable once UnusedType.Global
public class CreateCharacterHandler : IRequestHandler<CreateCharacterCommand, Result<string>>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCharacterHandler(ICharacterRepository characterRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _characterRepository = characterRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(CreateCharacterCommand request, CancellationToken cancellationToken)
    {
        var character = _mapper.Map<Character>(request);

        _characterRepository.Create(character);
        if (await _unitOfWork.CompleteAsync())
            return Result.Success(character.Id);

        return Result.Failure<string>(General.UnProcessableRequest);
    }
}