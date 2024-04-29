using ExecutionLens.Logging.DOMAIN.Models;

namespace ExecutionLens.Logging.PERSISTANCE.Contracts;

public interface ILogRepository
{
    Task<string> Add(MethodLog log);
    Task<MethodLog> Get(string id);
}
