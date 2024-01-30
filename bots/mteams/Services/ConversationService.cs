using Microsoft.Azure.Cosmos;
using mteams.Configuration;
using mteams.Models;
using System.Threading.Tasks;

namespace mteams.Services;

public class ConversationService : IConversationService
{
    private readonly Container _container;

    public ConversationService(CosmosClient cosmosClient, DocumentDBConfiguration documentDBConfiguration)
    {
        _container = cosmosClient.GetContainer(documentDBConfiguration.DocumentDatabaseName, documentDBConfiguration.CosmosCollectionNameList.ConversationsCollectionName);
    }

    public async Task SaveConversationReference(string tenantId, ConversationReferenceEntity conversationReferenceEntity)
    {
        await _container.CreateItemAsync(conversationReferenceEntity);
    }
}
