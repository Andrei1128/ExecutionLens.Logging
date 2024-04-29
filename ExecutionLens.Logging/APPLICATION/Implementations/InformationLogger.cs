﻿using ExecutionLens.Logging.APPLICATION.Contracts;
using ExecutionLens.Logging.DOMAIN.Configurations;
using ExecutionLens.Logging.DOMAIN.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ExecutionLens.Logging.APPLICATION.Implementations;

internal class InformationLogger(IOptionsMonitor<LoggerConfiguration> config, ILogService _logService) : IInformationLogger
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

            if (_config.LogToConsole)
            {
                Console.WriteLine($"[{logLevel}] {formatter(state, exception)}");

                if (exception is not null)
                {
                    Console.WriteLine(exception);
                }
            }
        }
    }

    public bool IsEnabled(LogLevel logLevel) => logLevel >= _config.MinimumLogLevel;

    public IDisposable BeginScope<TState>(TState state) where TState : notnull => default!;
}
