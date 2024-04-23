using System.Security.Cryptography;
using DanilvarKanji.Client.JsWrapper;
using DanilvarKanji.Client.Services.Auth;
using DanilvarKanji.Domain.Shared.Enumerations;
using DanilvarKanji.Shared.Requests.Auth;
using DVar.RandCreds;
using DVar.RandCreds.Options;
using Microsoft.AspNetCore.Components;

namespace DanilvarKanji.Client.Pages.Auth;

public partial class Register
{
    [Inject] public IAuthService AuthService { get; set; } = default!;

    [Inject] public required Js Js { get; set; }

    private const string PasswordElId = "password";
    private const string UsernameElid = "userName";
    private const string EmailElId = "email";

    private RegisterUserRequest _registerUserRequest = new();
    private bool _submitSuccessful;
    private string? _errorMessage;

    private async Task HandleSubmitAsync()
    {
        try
        {
            RegisterUserRequest? request = await AuthService.RegisterUserAsync(_registerUserRequest);
            if (request is not null)
            {
                _submitSuccessful = true;
            }
        }
        catch (HttpRequestException e)
        {
            _errorMessage = e.Message;
        }
    }

    private void HandleInvalidSubmit()
    {
        _submitSuccessful = false;
        /*
        _errorMessage = "There was a problem";
    */
    }

    private async Task GenerateCredentials()
    {
        var passwordOptions = new PasswordOptions
        (
            length: 8,
            lowercase: true,
            uppercase: true,
            nonAlpha: true
        );

        var userNameOptions = new UsernameOptions
        (
            prefix: "naruto",
            postfixLength: 6
        );

        var emailOptions = new EmailOptions
        (
            prefix: "naruto",
            domains: new[] { "mail.ru", "gmail.com" },
            randomCharLength: 6
        );

        await Js.Dom.ChangeElementValueAsync(PasswordElId, RandGen.Password(passwordOptions));
        _registerUserRequest.PasswordRepeat = _registerUserRequest.Password;
        await Js.Dom.ChangeElementValueAsync(UsernameElid, RandGen.Username(userNameOptions));
        await Js.Dom.ChangeElementValueAsync(EmailElId, RandGen.EmailDefault(emailOptions));
        _registerUserRequest.JlptLevel = (JlptLevel)RandomNumberGenerator.GetInt32(4);
    }
}