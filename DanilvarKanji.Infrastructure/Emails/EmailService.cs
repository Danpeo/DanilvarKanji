using DanilvarKanji.Domain.RepositoryAbstractions;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace DanilvarKanji.Infrastructure.Emails;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    private readonly IUserRepository _userRepository;

    public EmailService(IOptions<EmailSettings> emailSettingsOptions, IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _emailSettings = emailSettingsOptions.Value;
    }

    public async Task SendEmailAsync(string emailTo, string subject, string body)
    {
        var email = new MimeMessage
        {
            From = { new MailboxAddress(_emailSettings.SenderDisplayName, _emailSettings.SenderEmail) },
            To = { MailboxAddress.Parse(emailTo) },
            Subject = subject,
            Body = new TextPart(TextFormat.Text)
            {
                Text = body
            }
        };
        using var smtpClient = new SmtpClient();

        await smtpClient.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, SecureSocketOptions.StartTls);
        await smtpClient.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.SmtpPassword);
        await smtpClient.SendAsync(email);
        await smtpClient.DisconnectAsync(true);
    }

    public async Task CheckConfirmationCodeAsync(string email, string confirmationCode,
        Func<Task> onConfirmedAsync, Action onFailedToConfirm)
    {
        string? expectedCode = await _userRepository.GetRegistrationConfirmationCodeAsync(email);

        if (expectedCode != null)
        {
            bool confirmed = expectedCode == confirmationCode;
            if (confirmed)
            {
                await onConfirmedAsync.Invoke();
            }
            else
            {
                onFailedToConfirm.Invoke();
            }
        }
    }
}