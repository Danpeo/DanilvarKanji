using DanilvarKanji.Domain.Entities;
using MediatR;

namespace DanilvarKanji.Application.Users.Queries;

public record GetUserByIdQuery(string Id) : IRequest<AppUser?>;

