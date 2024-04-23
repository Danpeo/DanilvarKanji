using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Domain.Shared.Params;
using MediatR;

namespace DanilvarKanji.Application.Users.Queries;

public record GetUserByIdQuery(string Id) : IRequest<AppUser?>;

public record GetUserLearningSettingsQuery(string Email) : IRequest<LearningSettings?>;

public record ListUsersQuery(PaginationParams? PaginationParams) : IRequest<IEnumerable<AppUser>>;

