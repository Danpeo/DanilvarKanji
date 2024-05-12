using System.Text.Json.Serialization;

namespace DanilvarKanji.Client.Validation.Errors;

public class RegisterError : IError
{
  [JsonPropertyName("Identity.EmailNotUnique")]
  public List<string> EmailNotUniqueErrors { get; set; }

  [JsonPropertyName("Identity.ConcurrencyFailure")]
  public List<string> ConcurrencyFailure { get; set; }

  [JsonPropertyName("Identity.DefaultError")]
  public List<string> DefaultError { get; set; }

  [JsonPropertyName("Identity.DuplicateEmail")]
  public List<string> DuplicateEmail { get; set; }

  [JsonPropertyName("Identity.DuplicateRoleName")]
  public List<string> DuplicateRoleName { get; set; }

  [JsonPropertyName("Identity.DuplicateUserName")]
  public List<string> DuplicateUserName { get; set; }

  [JsonPropertyName("DuplicateUserName")]
  public List<string> DuplicateUserName2 { get; set; }

  [JsonPropertyName("Identity.InvalidEmail")]
  public List<string> InvalidEmail { get; set; }

  [JsonPropertyName("Identity.InvalidRoleName")]
  public List<string> InvalidRoleName { get; set; }

  [JsonPropertyName("Identity.InvalidToken")]
  public List<string> InvalidToken { get; set; }

  [JsonPropertyName("Identity.InvalidUserName")]
  public List<string> InvalidUserName { get; set; }

  [JsonPropertyName("Identity.LoginAlreadyAssociated")]
  public List<string> LoginAlreadyAssociated { get; set; }

  [JsonPropertyName("Identity.PasswordMismatch")]
  public List<string> PasswordMismatch { get; set; }

  [JsonPropertyName("Identity.PasswordRequiresDigit")]
  public List<string> PasswordRequiresDigit { get; set; }

  [JsonPropertyName("Identity.PasswordRequiresLower")]
  public List<string> PasswordRequiresLower { get; set; }

  [JsonPropertyName("Identity.PasswordRequiresNonAlphanumeric")]
  public List<string> PasswordRequiresNonAlphanumeric { get; set; }

  [JsonPropertyName("Identity.PasswordRequiresUniqueChars")]
  public List<string> PasswordRequiresUniqueChars { get; set; }

  [JsonPropertyName("Identity.PasswordRequiresUpper")]
  public List<string> PasswordRequiresUpper { get; set; }

  [JsonPropertyName("Identity.PasswordTooShort")]
  public List<string> PasswordTooShort { get; set; }

  [JsonPropertyName("Identity.RecoveryCodeRedemptionFailed")]
  public List<string> RecoveryCodeRedemptionFailed { get; set; }

  [JsonPropertyName("Identity.UserAlreadyHasPassword")]
  public List<string> UserAlreadyHasPassword { get; set; }

  [JsonPropertyName("Identity.UserAlreadyInRole")]
  public List<string> UserAlreadyInRole { get; set; }

  [JsonPropertyName("Identity.UserLockoutNotEnabled")]
  public List<string> UserLockoutNotEnabled { get; set; }

  [JsonPropertyName("Identity.UserNotInRole")]
  public List<string> UserNotInRole { get; set; }
}