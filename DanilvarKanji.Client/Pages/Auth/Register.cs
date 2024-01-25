using System.Security.Cryptography;
using Danilvar.Random;
using DanilvarKanji.Client.JsWrapper;
using DanilvarKanji.Client.Services.Auth;
using DanilvarKanji.Shared.Domain.Enumerations;
using DanilvarKanji.Shared.Requests.Auth;
using Microsoft.AspNetCore.Components;

namespace DanilvarKanji.Client.Pages.Auth;

public partial class Register
{
    [Inject] public IAuthService AuthService { get; set; } = default!;

    [Inject] public required Js Js { get; set; }
    
    private const string PasswordElId = "password";
    private const string UsernameElid = "userName";
    private const string EmailElId = "email";
    
    private RegisterUserRequest _registerUserRequest = new ();
    private bool _submitSuccessful;
    private string? _errorMessage;
    
    private async Task HandleSubmitAsync()
    {
        RegisterUserRequest? request = await AuthService.RegisterUserAsync(_registerUserRequest);

        if (request is not null)
        {
            _submitSuccessful = true;
        }
    }
    
    private void HandleInvalidSubmit()
    {
        _submitSuccessful = false;
        _errorMessage = "There was a problem";
    }

    private async Task GenerateCredentials()
    {
        await Js.Dom.ChangeElementValue(PasswordElId, RandomGenerator.Password());
        _registerUserRequest.PasswordRepeat = _registerUserRequest.Password;

        await Js.Dom.ChangeElementValue(UsernameElid, RandomGenerator.Username());
        await Js.Dom.ChangeElementValue(EmailElId, RandomGenerator.Email());
        _registerUserRequest.JlptLevel = (JlptLevel)RandomNumberGenerator.GetInt32(4);
    }
}