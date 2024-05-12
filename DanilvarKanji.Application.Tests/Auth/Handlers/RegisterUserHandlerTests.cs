/*using AutoMapper;
using DanilvarKanji.Application.Auth.Commands;
using DanilvarKanji.Application.Auth.Handlers;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Domain.Shared.Enumerations;
using DanilvarKanji.Infrastructure.Emails;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace DanilvarKanji.Application.Tests.Auth.Handlers;

public class RegisterUserHandlerTests
{
    private readonly Mock<IUserRepository> _userRepoMock;
    private readonly Mock<UserManager<AppUser>> _userManagerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IEmailService> _emailServiceMock;

    public RegisterUserHandlerTests()
    {
        _userRepoMock = new Mock<IUserRepository>();

        _userManagerMock = new Mock<UserManager<AppUser>>(
            Mock.Of<IUserStore<AppUser>>(),
            null, null, null, null, null, null, null, null);

        _mapperMock = new Mock<IMapper>();
        _emailServiceMock = new Mock<IEmailService>();
    }

    [Fact]
    public async Task Handle_Should_ContainEmailNotUniqueError_WhenEmailIsNotUnique()
    {
        //Arrange
        var command =
            new RegisterUserCommand("PubesEater", JlptLevel.N5, "pubeseater@mail.com", "123456_Qq", "123456_Qq");

        _userRepoMock.Setup(x => x.ExistByEmail(It.IsAny<string>())).ReturnsAsync(true);

        var handler = new RegisterUserHandler(_userRepoMock.Object, _mapperMock.Object, _userManagerMock.Object,
            _emailServiceMock.Object);

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
            new RegisterUserCommand("PubesEater", JlptLevel.N5, "pubeseater@mail.com", "123456_Qq", "123456_QQQ");
        var handler = new RegisterUserHandler(_userRepoMock.Object, _mapperMock.Object, _userManagerMock.Object, _emailServiceMock.Object);

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
            new RegisterUserCommand("PubesEater", JlptLevel.N5, "pubeseater@mail.com", "123456_Qq", "123456_Qq");
        var handler = new RegisterUserHandler(_userRepoMock.Object, _mapperMock.Object, _userManagerMock.Object, _emailServiceMock.Object);

        //Act
        await handler.Handle(command, default);

        //Assert
        _userManagerMock.Verify(x => x
            .CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()), Times.Once);
    }
}*/

