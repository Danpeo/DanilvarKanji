namespace DanilvarKanji.Infrastructure.Emails;

public class EmailSettings
{
    public string SenderDisplayName { get; init; }

    public string SenderEmail { get; init; }

    public string SmtpPassword { get; init; }

    public string SmtpServer { get; init; }

    public int SmtpPort { get; init; }
}