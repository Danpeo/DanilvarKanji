using DanilvarKanji.Application.Users.Queries;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Domain.Shared.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DanilvarKanji.Application.Users.Handlers;

public class ListUsersHandler : IRequestHandler<ListUsersQuery, IEnumerable<AppUser>>
{
    private readonly ILogger<ListUsersHandler> _logger;
    private readonly IUserRepository _userRepository;

    public ListUsersHandler(IUserRepository userRepository, ILogger<ListUsersHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<AppUser>> Handle(
        ListUsersQuery request,
        CancellationToken cancellationToken
    )
    {
        _logger.LogInformation(
            "Processing List Users Query: {@request}, {@dt}",
            request,
            DateTime.UtcNow
        );

        if (await _userRepository.AnyExistAsync()) return await _userRepository.ListAsync(request.PaginationParams);

        _logger.LogInformation("No Users - {@dt}", DateTime.UtcNow);
        return Enumerable.Empty<AppUser>();
    }
}