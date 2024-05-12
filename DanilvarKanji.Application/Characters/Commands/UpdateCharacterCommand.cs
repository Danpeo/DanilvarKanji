using DanilvarKanji.Domain.Primitives.Result;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Domain.Shared.Enumerations;
using DanilvarKanji.Domain.Shared.ValueObjects;
using MediatR;

namespace DanilvarKanji.Application.Characters.Commands;

public class UpdateCharacterCommand : IRequest<Result<string>>
{
  public string Id { get; set; }
  public string Definition { get; set; }
  public JlptLevel JlptLevel { get; init; }
  public CharacterType CharacterType { get; init; } = CharacterType.Kanji;
  public ICollection<StringDefinition>? Mnemonics { get; init; } = new List<StringDefinition>();

  public int StrokeCount { get; set; }
  public ICollection<KanjiMeaning> KanjiMeanings { get; set; } = new List<KanjiMeaning>();
  public ICollection<Kunyomi>? Kunyomis { get; init; } = new List<Kunyomi>();
  public ICollection<Onyomi>? Onyomis { get; init; } = new List<Onyomi>();
}