using DanilvarKanji.Application.Users.Commands;
using DanilvarKanji.Domain.Errors;
using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Infrastructure.Data;
using MediatR;

namespace DanilvarKanji.Application.Users.Handlers;

public class UpdateUserLearningSettingsHandler : IRequestHandler<UpdateUserLearningSettingsCommand, Result<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserLearningSettingsHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(UpdateUserLearningSettingsCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.UpdateUserLearningSettingsAsync(request.Email, request.LearningSettings);

        if (await _unitOfWork.CompleteAsync())
            return Result.Success(request.Email);
        
        return Result.Failure<string>(General.UnProcessableRequest);
    }
}