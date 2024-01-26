using DanilvarKanji.Application.Users.Queries;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Shared.Domain.Params;
using MediatR;

namespace DanilvarKanji.Application.Users.Handlers;

public class GetUserLearningSettingsQueryHandler : IRequestHandler<GetUserLearningSettingsQuery, LearningSettings?>
{
    private readonly IUserRepository _userRepository;

    public GetUserLearningSettingsQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<LearningSettings?> Handle(GetUserLearningSettingsQuery request, CancellationToken cancellationToken)
    {
        if (await _userRepository.ExistByEmail(request.Email))
        {
            return await _userRepository.GetUserLearningSettingsAsync(request.Email);
        }
        
        return null;
    }
}