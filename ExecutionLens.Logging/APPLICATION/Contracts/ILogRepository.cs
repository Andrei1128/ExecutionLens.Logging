using ExecutionLens.Logging.DOMAIN.Models;

namespace ExecutionLens.Logging.APPLICATION.Contracts;

public interface ILogRepository
{
    Task<string> Insert(MethodLog log);
}
