using DanilvarKanji.Application.Users.Queries;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Shared.Domain.Entities;
using MediatR;

namespace DanilvarKanji.Application.Users.Handlers;

// ReSharper disable once UnusedType.Global
public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, AppUser?>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<AppUser?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        if (await _userRepository.ExistById(request.Id))
            return await _userRepository.GetByIdAsync(request.Id);

        return null;
    }
}