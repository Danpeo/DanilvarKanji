using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace DanilvarKanji.Infrastructure.Emails;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> emailSettingsOptions)
    {
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
}