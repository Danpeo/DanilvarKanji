using DanilvarKanji.Shared.Domain.Entities;
using DanilvarKanji.Shared.Domain.Params;
using MediatR;

namespace DanilvarKanji.Application.Users.Queries;

public record GetUserByIdQuery(string Id) : IRequest<AppUser?>;

public record GetUserLearningSettingsQuery(string Email) : IRequest<LearningSettings?>;

