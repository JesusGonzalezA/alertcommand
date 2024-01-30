using mteams.Models;
using System.Threading.Tasks;

namespace mteams.Services;

public interface IConversationService
{
    public Task SaveConversationReference(string tenantId, ConversationReferenceEntity conversationReferenceEntity);
}
