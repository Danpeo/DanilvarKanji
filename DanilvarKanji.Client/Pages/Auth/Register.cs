using DanilvarKanji.Client.Services.Auth;
using DanilvarKanji.Shared.Requests.Auth;
using Microsoft.AspNetCore.Components;

namespace DanilvarKanji.Client.Pages.Auth;

public partial class Register
{
    [Inject] public IAuthService AuthService { get; set; } = default!;
    
    private RegisterUserRequest _registerUserRequest = new ();
    private bool _submitSuccessful;
    private string? _errorMessage;
    
    private async Task HandleSubmit()
    {
        RegisterUserRequest? character = await AuthService.RegisterUserAsync(_registerUserRequest);

        if (character is not null)
        {
            _submitSuccessful = true;
        }
    }
    
    private void HandleInvalidSubmit()
    {
        _submitSuccessful = false;
        _errorMessage = "There was a problem";
    }
}