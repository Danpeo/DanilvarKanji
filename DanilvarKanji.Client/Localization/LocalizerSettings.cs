namespace DanilvarKanji.Client.Localization;

public static class LocalizerSettings
{
    public static readonly CultureWithName? NeutralCulture = new("English", "en-US");

    public static readonly List<CultureWithName> SupportedCulturesWithName =
        new()
        {
            new CultureWithName("English", "en-US"),
            new CultureWithName("русский", "ru-RU")
        };
}