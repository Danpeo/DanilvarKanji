﻿namespace DanilvarKanji.Client.Models.Responses.JMdict;

public class Kana
{
    public Guid Id { get; set; }

    public bool? Common { get; set; }

    public string? Text { get; set; }

    public List<string>? Tags { get; set; }

    public List<string>? AppliesToKanji { get; set; }

    public string? WordId { get; set; }

    //public virtual Word? Word { get; set; }
}
