using DanilvarKanji.Application.CharacterLearnings.Queries;
using DanilvarKanji.Domain.RepositoryAbstractions;
using MediatR;

namespace DanilvarKanji.Application.CharacterLearnings.Handlers;

public class GetRandomMeaningsInReviewHandler
    : IRequestHandler<GetRandomMeaningsInReviewQuery, RandomItemsInReview?>
{
    private readonly ICharacterRepository _characterRepository;
    private readonly ICharacterLearningRepository _charLearningRepository;
    private readonly Random _random;

    public GetRandomMeaningsInReviewHandler(
        ICharacterLearningRepository charLearningRepository,
        ICharacterRepository characterRepository
    )
    {
        _charLearningRepository = charLearningRepository;
        _characterRepository = characterRepository;
        _random = new Random();
    }

    public async Task<RandomItemsInReview?> Handle(
        GetRandomMeaningsInReviewQuery request,
        CancellationToken cancellationToken
    )
    {
        var randomMeaningFromReviewedChar = _characterRepository.GetRandomMeaningFromCharacter(
            request.CharacterId,
            request.Culture
        );

        if (string.IsNullOrEmpty(randomMeaningFromReviewedChar))
            return null;

        var characterQty = request.Qty - 1;

        var charLearningsFromReview = await _charLearningRepository.GetRandomMeaningsInReviewQueueAsync(
            request.CharacterId,
            request.AppUser,
            request.Culture,
            characterQty
        );

        if (charLearningsFromReview.Count + 1 < request.Qty)
            return await GetFromReviewAndLearnQueueAsync(
                request,
                characterQty,
                charLearningsFromReview,
                randomMeaningFromReviewedChar
            );

        charLearningsFromReview.Add(randomMeaningFromReviewedChar);
        charLearningsFromReview = charLearningsFromReview.OrderBy(_ => _random.Next()).ToList();

        return new RandomItemsInReview(charLearningsFromReview, randomMeaningFromReviewedChar);
    }

    private async Task<RandomItemsInReview?> GetFromReviewAndLearnQueueAsync(
        GetRandomMeaningsInReviewQuery request,
        int characterQty,
        IEnumerable<string?> charLearningsFromReview,
        string? randomMeaningFromReviewedChar
    )
    {
        var charLearningsToLearn = await _characterRepository.GetRandomMeaningsInLearnQueueAsync(
            request.AppUser,
            request.Culture,
            characterQty
        );

        var combinedLearnings = charLearningsFromReview.Concat(charLearningsToLearn).ToList();

        var result = combinedLearnings.OrderBy(_ => _random.Next()).Take(characterQty).ToList();

        if (randomMeaningFromReviewedChar != null)
        {
            result.Add(randomMeaningFromReviewedChar);
            result = result.OrderBy(_ => _random.Next()).ToList();
        }

        return new RandomItemsInReview(result, randomMeaningFromReviewedChar);
    }
}