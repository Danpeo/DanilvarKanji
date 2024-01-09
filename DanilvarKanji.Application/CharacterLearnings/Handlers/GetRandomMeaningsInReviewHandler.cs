using DanilvarKanji.Application.CharacterLearnings.Queries;
using DanilvarKanji.Domain.RepositoryAbstractions;
using MediatR;

namespace DanilvarKanji.Application.CharacterLearnings.Handlers;

public class GetRandomMeaningsInReviewHandler : IRequestHandler<GetRandomMeaningsInReviewQuery, (List<string>
    random, string correct)>
{
    private readonly ICharacterLearningRepository _charLearningRepository;
    private readonly ICharacterRepository _characterRepository;
    private readonly Random _random;

    public GetRandomMeaningsInReviewHandler(ICharacterLearningRepository charLearningRepository,
        ICharacterRepository characterRepository)
    {
        _charLearningRepository = charLearningRepository;
        _characterRepository = characterRepository;
        _random = new Random();
    }

    public async Task<(List<string> random, string correct)> Handle(GetRandomMeaningsInReviewQuery request,
        CancellationToken cancellationToken)
    {
        string? randomMeaningFromReviewedChar =
            _characterRepository.GetRandomMeaningFromCharacter(request.CharacterId, request.Culture);

        int characterQty = randomMeaningFromReviewedChar != null ? request.Qty - 1 : request.Qty;

        List<string> charLearningsFromReview =
            await _charLearningRepository.GetRandomMeaningsInReviewQueueAsync(
                request.CharacterId,
                request.AppUser,
                request.Culture,
                characterQty);

        if (charLearningsFromReview.Count + 1 < request.Qty)
        {
            return await GetFromReviewAndLearnQueueAsync(request, characterQty, charLearningsFromReview,
                randomMeaningFromReviewedChar);
        }

        if (randomMeaningFromReviewedChar != null)
        {
            charLearningsFromReview.Add(randomMeaningFromReviewedChar);
            charLearningsFromReview = charLearningsFromReview.OrderBy(_ => _random.Next()).ToList();
        }

        return (charLearningsFromReview, randomMeaningFromReviewedChar);
    }

    private async Task<(List<string> random, string correct)> GetFromReviewAndLearnQueueAsync(
        GetRandomMeaningsInReviewQuery request, int characterQty,
        IEnumerable<string?> charLearningsFromReview, string? randomMeaningFromReviewedChar)
    {
        var charLearningsToLearn =
            await _characterRepository.GetRandomMeaningsInLearnQueueAsync(request.AppUser, request.Culture,
                characterQty);

        var combinedLearnings = charLearningsFromReview.Concat(charLearningsToLearn).ToList();

        var result = combinedLearnings
            .OrderBy(_ => _random.Next())
            .Take(characterQty)
            .ToList();

        if (randomMeaningFromReviewedChar != null)
        {
            result.Add(randomMeaningFromReviewedChar);
            result = result.OrderBy(_ => _random.Next()).ToList();
        }

        return (result, randomMeaningFromReviewedChar);
    }
}