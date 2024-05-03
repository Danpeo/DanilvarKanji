using DanilvarKanji.Client.Models.Responses;
using DanilvarKanji.Domain.Shared.Entities;
using DanilvarKanji.Domain.Shared.Enumerations;
using DanilvarKanji.Domain.Shared.ValueObjects;
using DanilvarKanji.Shared.Requests.Characters;

namespace DanilvarKanji.Client.Mappers;

public static class CharacterMapper
{
    public static CharacterRequest ToCreateCharacterRequest(this GetKanjiResponse_KAD KAD_Kanji)
    {
        var createCharacterRequest = new CharacterRequest
        {
            Definition = KAD_Kanji.Kanji,
            CharacterType = CharacterType.Kanji,
            StrokeCount = KAD_Kanji.Stroke_Count
        };

        createCharacterRequest.FromIntToJlpt(KAD_Kanji.Jlpt);

        foreach (string kunReading in KAD_Kanji.Kun_Readings)
            createCharacterRequest.Kunyomis.Add(new Kunyomi(kunReading));

        foreach (string onReading in KAD_Kanji.On_Readings)
            createCharacterRequest.Onyomis.Add(new Onyomi(onReading));

        foreach (string meaning in KAD_Kanji.Meanings)
        {
            var strDefs = new List<StringDefinition> { new(meaning, Culture.EnUS), new(meaning, Culture.RuRU) };
            createCharacterRequest.KanjiMeanings.Add(new KanjiMeaning(100, strDefs));
        }

        createCharacterRequest.Mnemonics.Add(new StringDefinition("Kanji", Culture.EnUS));
        createCharacterRequest.Mnemonics.Add(new StringDefinition("Кандзи", Culture.RuRU));

        return createCharacterRequest;
    }
}