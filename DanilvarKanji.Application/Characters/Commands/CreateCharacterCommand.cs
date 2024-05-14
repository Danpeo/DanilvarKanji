using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Domain.Shared.Enumerations;
using DanilvarKanji.Domain.Shared.ValueObjects;
using MediatR;

namespace DanilvarKanji.Application.Characters.Commands;

public class CreateCharacterCommand : IRequest<Result<string>>
{
    public CreateCharacterCommand(
        string? definition,
        JlptLevel jlptLevel,
        CharacterType characterType,
        int? strokeCount
    )
    {
        Definition = definition;
        JlptLevel = jlptLevel;
        CharacterType = characterType;
        StrokeCount = strokeCount;
    }

    public string? Definition { get; set; }

    public JlptLevel JlptLevel { get; set; }

    public CharacterType CharacterType { get; set; } = CharacterType.Kanji;

    public ICollection<StringDefinition>? Mnemonics { get; set; } = new List<StringDefinition>();

    public int? StrokeCount { get; set; }

    public ICollection<KanjiMeaning>? KanjiMeanings { get; set; } = new List<KanjiMeaning>();

    public ICollection<Kunyomi>? Kunyomis { get; set; } = new List<Kunyomi>();

    public ICollection<Onyomi>? Onyomis { get; set; } = new List<Onyomi>();

    public List<string>? ChildCharacterIds { get; set; }
}