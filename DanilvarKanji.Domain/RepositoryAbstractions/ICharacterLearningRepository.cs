using DanilvarKanji.Domain.Entities;
using DanilvarKanji.Domain.Enumerations;
using DanilvarKanji.Domain.Params;

namespace DanilvarKanji.Domain.RepositoryAbstractions;

public interface ICharacterLearningRepository
{
    Task<IEnumerable<CharacterLearning>> ListLearnQueueAsync(PaginationParams? paginationParams,
        AppUser user,
        JlptLevel jlptLevel = JlptLevel.N5);

    Task<bool> AnyExist();
    void Create(CharacterLearning characterLearning);
}