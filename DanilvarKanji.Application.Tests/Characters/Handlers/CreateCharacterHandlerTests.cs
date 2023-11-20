using AutoMapper;
using DanilvarKanji.Application.Characters.Commands;
using DanilvarKanji.Application.Characters.Handlers;
using DanilvarKanji.Domain.Enumerations;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Infrastructure.Abstractions;
using Moq;

namespace DanilvarKanji.Application.Tests.Characters.Handlers;

public class CreateCharacterHandlerTests
{
    private readonly Mock<ICharacterRepository> _characterRepository;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IMapper> _mapper;

    public CreateCharacterHandlerTests()
    {
        _characterRepository = new Mock<ICharacterRepository>();
        _unitOfWork = new Mock<IUnitOfWork>();
        _mapper = new Mock<IMapper>();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenUnitOfWorkDidNotCompleted()
    {
        //Arrange
        var command = new CreateCharacterCommand("CharacterDef", JlptLevel.N1, CharacterType.Kanji, 3);
        _unitOfWork.Setup(x => x.CompleteAsync()).ReturnsAsync(false);

        var handler = new CreateCharacterHandler(_characterRepository.Object, _mapper.Object, _unitOfWork.Object);

        //Act
        Result result = await handler.Handle(command, default);
        
        //Assert
        Assert.Contains(General.UnProcessableRequest, result.Error);
    }
    
    [Fact]
    public async Task Handle_Should_ReturnIsSuccess_WhenUnitOfWorkCompleted()
    {
        //Arrange
        var command = new CreateCharacterCommand("CharacterDef", JlptLevel.N1, CharacterType.Kanji, 3);
        _unitOfWork.Setup(x => x.CompleteAsync()).ReturnsAsync(true);

        var handler = new CreateCharacterHandler(_characterRepository.Object, _mapper.Object, _unitOfWork.Object);

        //Act
        Result result = await handler.Handle(command, default);
        
        //Assert
        Assert.True(result.IsSuccess);
    }
}