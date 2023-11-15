using AutoMapper;
using DanilvarKanji.Application.Characters.Commands;
using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Infrastructure.Abstractions;
using MediatR;

namespace DanilvarKanji.Application.Characters.Handlers;

public class CreateCharacterHandler : IRequestHandler<CreateCharacterCommand, Result>
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

    public async Task<Result> Handle(CreateCharacterCommand request, CancellationToken cancellationToken)
    {
        var character = _mapper.Map<Character>(request);
        
        _characterRepository.CreateAsync(character);
        if (await _unitOfWork.CompleteAsync())
            return Result.Success();

        return Result.Failure(General.UnProcessableRequest);
    }
}