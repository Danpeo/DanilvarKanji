using Tesseract;

namespace DVar.TextFromImage;

public static class Extractor
{
    public static string ExtractTextFromFile(string filePath, string dataPath, string langMode,
        EngineMode engineMode = EngineMode.Default)
    {
        using var engine = new TesseractEngine(dataPath, langMode, engineMode);
        
        using Pix? pix = Pix.LoadFromFile(filePath);
        return ProcessExtracting(engine, pix);
    }

    public static string ExtractTextFromMemoryStream(MemoryStream stream, string dataPath, string langMode,
        EngineMode engineMode = EngineMode.Default)
    {
        using var engine = new TesseractEngine(dataPath, langMode, engineMode);

        using Pix? pix = Pix.LoadFromMemory(stream.ToArray());
        return ProcessExtracting(engine, pix);
    }

    private static string ProcessExtracting(TesseractEngine engine, Pix pix)
    {
        using Page? page = engine.Process(pix);
        string text = page.GetText();
        return text;
    }
}