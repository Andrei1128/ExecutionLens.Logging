using Microsoft.Extensions.Logging;

namespace ExecutionLens.Logging.DOMAIN.Configurations;

public class LoggerConfiguration
{
    public string ElasticUri { get; set; } = string.Empty;
    public string Index {  get; set; } = "logs";
    public bool LogOnlyOnException { get; set; } = false;
    public bool LogToConsole { get; set; } = false;
    public LogLevel MinimumLogLevel { get; set; } = LogLevel.Information;
}
