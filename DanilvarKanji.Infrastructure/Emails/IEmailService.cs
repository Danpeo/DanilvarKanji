namespace DanilvarKanji.Infrastructure.Emails;

public interface IEmailService
{
    Task SendEmailAsync(string emailTo, string subject, string body);
}