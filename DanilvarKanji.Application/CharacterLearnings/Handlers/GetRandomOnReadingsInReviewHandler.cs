using DanilvarKanji.Application.CharacterLearnings.Queries;
using DanilvarKanji.Domain.RepositoryAbstractions;
using MediatR;

namespace DanilvarKanji.Application.CharacterLearnings.Handlers;

public class GetRandomOnReadingsInReviewHandler : IRequestHandler<GetRandomOnReadingsInReviewQuery, (List<string>
    random, string correct)>
{
    private readonly ICharacterLearningRepository _charLearningRepository;
    private readonly ICharacterRepository _characterRepository;
    private readonly Random _random;

    public GetRandomOnReadingsInReviewHandler(ICharacterLearningRepository charLearningRepository,
        ICharacterRepository characterRepository)
    {
        _charLearningRepository = charLearningRepository;
        _characterRepository = characterRepository;
        _random = new Random();
    }

    public async Task<(List<string> random, string correct)> Handle(GetRandomOnReadingsInReviewQuery request, CancellationToken cancellationToken)
    {
        string? randomKunyomiFromCharacter =
            _characterRepository.GetRandomOnyomiFromCharacter(request.CharacterId);
        
        int characterQty = randomKunyomiFromCharacter != null ? request.Qty - 1 : request.Qty;

        List<string> charLearningsFromReview =
            await _charLearningRepository.GetRandomOnyomisInReviewQueueAsync(
                request.CharacterId,
                request.AppUser,
                characterQty);
        
        if (charLearningsFromReview.Count + 1 < request.Qty)
        {
            return await GetFromReviewAndLearnQueueAsync(request, characterQty, charLearningsFromReview,
                randomKunyomiFromCharacter);
        }

        if (randomKunyomiFromCharacter != null)
        {
            charLearningsFromReview.Add(randomKunyomiFromCharacter);
            charLearningsFromReview = charLearningsFromReview.OrderBy(_ => _random.Next()).ToList();
        }

        return (charLearningsFromReview, randomKunyomiFromCharacter);
    }

    private async Task<(List<string> random, string correct)> GetFromReviewAndLearnQueueAsync(
        GetRandomOnReadingsInReviewQuery request, int characterQty,
        IEnumerable<string?> charLearningsFromReview, string? randomMeaningFromReviewedChar)
    {
        var charLearningsToLearn =
            await _characterRepository.GetRandomOnReadingsInLearnQueueAsync(request.AppUser, characterQty);

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