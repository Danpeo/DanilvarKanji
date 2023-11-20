using AutoMapper;
using DanilvarKanji.Application.Auth.Commands;
using DanilvarKanji.Application.Auth.Handlers;
using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Enumerations;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.RepositoryAbstractions;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace DanilvarKanji.Application.Tests.Auth.Handlers;

public class RegisterUserHandlerTests
{
    private readonly Mock<IUserRepository> _userRepoMock;
    private readonly Mock<UserManager<AppUser>> _userManagerMock;
    private readonly Mock<IMapper> _mapperMock;

    public RegisterUserHandlerTests()
    {
        _userRepoMock = new Mock<IUserRepository>();

        _userManagerMock = new Mock<UserManager<AppUser>>(
            Mock.Of<IUserStore<AppUser>>(),
            null, null, null, null, null, null, null, null);

        _mapperMock = new Mock<IMapper>();
    }

    [Fact]
    public async Task Handle_Should_ContainEmailNotUniqueError_WhenEmailIsNotUnique()
    {
        //Arrange
        var command =
            new RegisterUserCommand("PubesEater", "pubeseater@mail.com", "123456_Qq", "123456_Qq", JlptLevel.N5);
        
        _userRepoMock.Setup(x => x.ExistByEmail(It.IsAny<string>())).ReturnsAsync(true);
        
        var handler = new RegisterUserHandler(_userRepoMock.Object, _mapperMock.Object, _userManagerMock.Object);

        //Act
        IdentityResult result = await handler.Handle(command, default);

        //Assert
        Assert.Contains(Identity.EmailNotUnique.Code, result.Errors.Select(error => error.Code));
    }

    [Fact]
    public async Task Handle_Should_ContainPasswordsDontMatch_WhenPasswordsNotMatch()
    {
        //Arrange
        var command =
            new RegisterUserCommand("PubesEater", "pubeseater@mail.com", "123456_Qq", "123456_QQQ", JlptLevel.N5);
        var handler = new RegisterUserHandler(_userRepoMock.Object, _mapperMock.Object, _userManagerMock.Object);

        //Act
        IdentityResult result = await handler.Handle(command, default);

        //Assert        
        Assert.Contains(Identity.PasswordsDontMatch.Code, result.Errors.Select(error => error.Code));
    }

    [Fact]
    public async Task Handle_Should_CallCreateOnUserManager_WhenAllInputsAreValid()
    {
        //Arrange
        var command =
            new RegisterUserCommand("PubesEater", "pubeseater@mail.com", "123456_Qq", "123456_Qq", JlptLevel.N5);
        var handler = new RegisterUserHandler(_userRepoMock.Object, _mapperMock.Object, _userManagerMock.Object);

        //Act
        await handler.Handle(command, default);

        //Assert
        _userManagerMock.Verify(x => x
            .CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()), Times.Once);
    }
}