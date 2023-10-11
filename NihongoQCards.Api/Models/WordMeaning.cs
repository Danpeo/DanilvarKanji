using System.ComponentModel.DataAnnotations;

namespace DanilvarKanji.Models;

public class WordMeaning
{
    [Key]
    public Guid Id { get; set; }
    public string Definition { get; set; }
    public float? Priority { get; set; }
}