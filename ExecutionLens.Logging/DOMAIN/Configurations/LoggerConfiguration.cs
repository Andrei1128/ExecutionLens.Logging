using Microsoft.Extensions.Logging;

namespace ExecutionLens.Logging.DOMAIN.Configurations;

public class LoggerConfiguration
{
    public string ElasticUri { get; set; } = string.Empty;
    public string Index { get; set; } = "logs";
    public bool LogOnlyOnException { get; set; } = false;
    public bool LogToConsole { get; set; } = false;
    public LogLevel MinimumLogLevel { get; set; } = LogLevel.Information;
    public LogLevel MinimumEmailLevel { get; set; } = LogLevel.Error;
    public EmailConfiguration? EmailConfiguration { get; set; } = null;
}

public class EmailConfiguration
{
    public string Address { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Client { get; set; } = string.Empty;
    public int Port { get; set; } = 587;
    public string[] To { get; set; } = [];
}