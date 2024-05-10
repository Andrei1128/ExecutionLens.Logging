using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using ExecutionLens.Logging.DOMAIN.Configurations;
using ExecutionLens.Logging.APPLICATION.Contracts;

namespace ExecutionLens.Logging.APPLICATION.Implementations;

internal class EmailService(IOptionsMonitor<LoggerConfiguration> config) : IEmailService
{
    private readonly EmailConfiguration? _config = config.CurrentValue.EmailConfiguration;

    public async Task SendEmail(string subject, string body)
    {
        if (_config is null)
        {
            return;
        }

        var message = new MailMessage()
        {
            From = new MailAddress(_config.Address),
            Subject = subject,
            Body = body,
            IsBodyHtml = false,
        };

        foreach (var email in _config.To)
        {
            message.To.Add(email);
        }

        var smtpClient = new SmtpClient(_config.Client)
        {
            Port = _config.Port,
            Credentials = new NetworkCredential(_config.Address, _config.Password),
            EnableSsl = true
        };

        await smtpClient.SendMailAsync(message);
    }
}
