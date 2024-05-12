namespace DanilvarKanji.Shared.Requests.Auth;

public class GeneratePasswordRequest
{
  private int _length = 8;

  public int Length
  {
    get => _length;
    set
    {
      _length = value;
      if (_length < 6)
        _length = 6;
    }
  }

  public bool RequireLowercase { get; set; } = true;
  public bool RequierUppercase { get; set; } = true;
  public bool RequireNonAlphanumeric { get; set; } = true;
}