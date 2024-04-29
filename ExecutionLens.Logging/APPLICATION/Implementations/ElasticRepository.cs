using ExecutionLens.Logging.APPLICATION.Contracts;
using ExecutionLens.Logging.DOMAIN.Models;
using Nest;

namespace ExecutionLens.Logging.APPLICATION.Implementations;

internal class ElasticRepository(IElasticClient _elasticClient) : ILogRepository
{
    public async Task<string> Insert(MethodLog log)
    {
        var indexResponse = await _elasticClient.IndexDocumentAsync(log);

        var rootId = indexResponse.Id;

        await IndexInteractions(log.Interactions, rootId);

        return rootId;
    }

    private async Task IndexInteractions(List<MethodLog>? interactions, string path)
    {
        if (interactions is null)
        {
            return;
        }

        foreach (var interaction in interactions)
        {
            interaction.NodePath = path;

            var indexResponse = await _elasticClient.IndexDocumentAsync(interaction);

            await IndexInteractions(interaction.Interactions, $"{path}/{indexResponse.Id}");
        }
    }
}
