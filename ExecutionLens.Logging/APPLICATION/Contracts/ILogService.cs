using ExecutionLens.Logging.DOMAIN.Models;

namespace ExecutionLens.Logging.APPLICATION.Contracts;

internal interface ILogService
{
    void AddLogEntry(MethodEntry entry);
    void AddLogExit(MethodExit exit);
    void AddInformation(InformationLog log);
    Task<string> Write();
}
