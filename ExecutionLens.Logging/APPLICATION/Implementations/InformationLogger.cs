using ExecutionLens.Logging.APPLICATION.Contracts;
using ExecutionLens.Logging.DOMAIN.Configurations;
using ExecutionLens.Logging.DOMAIN.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ExecutionLens.Logging.APPLICATION.Implementations;

internal class InformationLogger(IOptionsMonitor<LoggerConfiguration> config,
                                 ILogService _logService,
                                 IEmailService _emailService) : IInformationLogger
{
    private readonly LoggerConfiguration _config = config.CurrentValue;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (IsEnabled(logLevel))
        {
            InformationLog log = new()
            {
                LogLevel = logLevel.ToString(),
                Message = formatter(state, exception),
                Exception = exception
            };

            _logService.AddInformation(log);

            if (IsEmailEnabled(logLevel))
            {
                Task.Run(async () => await _emailService.SendEmail($"[{logLevel}]", $"{formatter(state, exception)}\n{exception}"));
            }

            if (_config.LogToConsole)
            {
                Console.WriteLine($"[{logLevel}] {formatter(state, exception)}\n{exception}");
            }
        }
    }

    public bool IsEnabled(LogLevel logLevel) => logLevel >= _config.MinimumLogLevel;
    public bool IsEmailEnabled(LogLevel logLevel) => logLevel >= _config.MinimumEmailLevel;

    public IDisposable BeginScope<TState>(TState state) where TState : notnull => default!;
}
