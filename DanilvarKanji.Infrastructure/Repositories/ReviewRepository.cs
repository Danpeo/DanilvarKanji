using DanilvarKanji.Domain.RepositoryAbstractions;
using DanilvarKanji.Infrastructure.Data;
using DanilvarKanji.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DanilvarKanji.Infrastructure.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly ApplicationDbContext _context;

    public ReviewRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Create(ReviewSession session) =>
        _context.ReviewSessions.Add(session);

    public async Task<ReviewSession?> GetAsync(string id, AppUser user) =>
        await GetReviewSessionsWithRelatedData()
            .FirstOrDefaultAsync(rs => rs.Id == id && rs.AppUser == user);

    public async ValueTask<bool> ExistAsync(string id, AppUser user) => 
        await _context.ReviewSessions.AnyAsync(s => s.Id == id && s.AppUser == user);

    private IQueryable<ReviewSession> GetReviewSessionsWithRelatedData()
    {
        var reviewSessions = _context.ReviewSessions
            .AsSplitQuery()
            .Include(rs => rs.Exercises)
            .ThenInclude(e => e.Character);

        return reviewSessions.OrderByDescending(rs => rs.ReviewDataTime);
    }
}