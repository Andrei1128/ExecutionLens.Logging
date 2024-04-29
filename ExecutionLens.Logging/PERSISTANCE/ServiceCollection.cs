using ExecutionLens.Logging.PERSISTANCE.Contracts;
using ExecutionLens.Logging.PERSISTANCE.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace ExecutionLens.Logging.PERSISTANCE;

public static partial class ServiceCollection
{
    public static void AddElasticClient(this IServiceCollection services, IElasticClient client, string index)
    {
        services.AddSingleton<ILogRepository>(new ElasticSearch(client, index));
    }
}