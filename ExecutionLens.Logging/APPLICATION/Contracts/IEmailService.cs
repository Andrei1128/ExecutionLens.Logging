namespace ExecutionLens.Logging.APPLICATION.Contracts;

internal interface IEmailService
{
    Task SendEmail(string subject, string body);
}
