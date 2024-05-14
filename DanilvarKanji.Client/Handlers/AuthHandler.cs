using System.Net;
using System.Net.Http.Headers;
using DanilvarKanji.Client.Services.Auth;

namespace DanilvarKanji.Client.Handlers;

public class AuthHandler : DelegatingHandler
{
    private readonly IAuthService _authService;
    private readonly IConfiguration _configuration;
    private bool _refreshing;

    public AuthHandler(IAuthService authService, IConfiguration configuration)
    {
        _authService = authService;
        _configuration = configuration;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    )
    {
        var jwt = await _authService.GetJwtAsync();
        var isToServer =
            request.RequestUri?.AbsoluteUri.StartsWith(_configuration["ServerUrl"] ?? "") ?? false;

        if (isToServer && !string.IsNullOrEmpty(jwt))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

        HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

        if (
            !_refreshing
            && !string.IsNullOrEmpty(jwt)
            && response.StatusCode == HttpStatusCode.Unauthorized
        )
            try
            {
                _refreshing = true;

                if (await _authService.RefreshAsync())
                {
                    jwt = await _authService.GetJwtAsync();

                    if (isToServer && !string.IsNullOrEmpty(jwt))
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

                    response = await base.SendAsync(request, cancellationToken);
                }
            }
            finally
            {
                _refreshing = false;
            }

        return response;
    }
}