using System.Net.Http.Headers;
using DanilvarKanji.Client.Services.Auth;

namespace DanilvarKanji.Client.Handlers;

public class AuthHandler : DelegatingHandler
{
    private readonly IAuthService _authService;

    private readonly IConfiguration _configuration;

    public AuthHandler(IAuthService authService, IConfiguration configuration)
    {
        _authService = authService;
        _configuration = configuration;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        string jwt = await _authService.GetJwtAsync();
        bool isToServer = request.RequestUri?.AbsoluteUri.StartsWith(_configuration["ServerUrl"] ?? "") ?? false;

        if (isToServer && !string.IsNullOrEmpty(jwt))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

        HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

        return response;
    }
}