using DanilvarKanji.Shared.Domain.Entities;
using MediatR;

namespace DanilvarKanji.Application.Reviews.Queries;

public record GetReviewSessionQuery(string Id, AppUser AppUser) : IRequest<ReviewSession?>;