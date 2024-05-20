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
    private const string PasswordElId = "password";
    private const string UsernameElid = "userName";
    private const string EmailElId = "email";
    private string? _errorMessage;

    private RegisterUserRequest _registerUserRequest = new();
    private bool _submitSuccessful;
    private bool _showLoading;

    [Inject] public IAuthService AuthService { get; set; } = default!;

    [Inject] public required Js Js { get; set; }

    private async Task HandleSubmitAsync()
    {
        try
        {
            _showLoading = true;
            RegisterUserRequest? request = await AuthService.RegisterUserAsync(_registerUserRequest);
            if (request is not null)
            {
                _submitSuccessful = true;
            }

            _showLoading = false;
        }
        catch (HttpRequestException e)
        {
            _errorMessage = e.Message;
        }
    }

    private void HandleInvalidSubmit()
    {
        _submitSuccessful = false;
        _showLoading = false;
    }

    private async Task GenerateCredentials()
    {
        var passwordOptions = new PasswordOptions(
            8,
            true,
            true,
            true
        );

        var userNameOptions = new UsernameOptions("naruto", 6);

        var emailOptions = new EmailOptions(
            "naruto",
            new[] { "mail.ru", "gmail.com" },
            4
        );

        await Js.Dom.ChangeElementValueAsync(PasswordElId, RandGen.Password(passwordOptions));
        _registerUserRequest.PasswordRepeat = _registerUserRequest.Password;
        await Js.Dom.ChangeElementValueAsync(UsernameElid, RandGen.Username(userNameOptions));
        await Js.Dom.ChangeElementValueAsync(EmailElId, RandGen.EmailDefault(emailOptions));
        _registerUserRequest.JlptLevel = (JlptLevel)RandomNumberGenerator.GetInt32(4);
    }
}