namespace DanilvarKanji.Infrastructure.Emails;

public interface IEmailService
{
  Task SendEmailAsync(string emailTo, string subject, string body);

  Task CheckConfirmationCodeAsync(
    string email,
    string confirmationCode,
    Func<Task> onConfirmedAsync,
    Action onFailedToConfirm
  );
}