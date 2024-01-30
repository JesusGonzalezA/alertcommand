using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Teams;
using Microsoft.Bot.Schema;
using mteams.Models;
using mteams.Services;
using System.Threading;
using System.Threading.Tasks;

namespace NotifyBot.Bots;

public class NotifyBot : TeamsActivityHandler
{
    private readonly IConversationService _conversationService;

    public NotifyBot(IConversationService conversationService)
    {
        _conversationService = conversationService;
    }

    protected override async Task<Task> OnConversationUpdateActivityAsync(ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
    {
        var conversationReference = (turnContext.Activity as Activity).GetConversationReference();
        var conversationReferenceEntity = new ConversationReferenceEntity()
        {
            UserId = turnContext.Activity.From.Id,
            BotId = conversationReference.Bot.Id,
            ServiceUrl = conversationReference.ServiceUrl,
            ConversationId = conversationReference.Conversation.Id
        };
        await _conversationService.SaveConversationReference(turnContext.Activity.Conversation.TenantId, conversationReferenceEntity);
        await turnContext.SendActivityAsync(MessageFactory.Text("Hello there! I am a helpful tool that can send you some custom notifications!"));

        return Task.CompletedTask;
    }

    protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
    {
        var conversationReference = (turnContext.Activity as Activity).GetConversationReference();
        var conversationReferenceEntity = new ConversationReferenceEntity()
        {
            UserId = turnContext.Activity.From.Id,
            BotId = conversationReference.Bot.Id,
            ServiceUrl = conversationReference.ServiceUrl,
            ConversationId = conversationReference.Conversation.Id,
            ChannelId = conversationReference.ChannelId
        };
        string conversationReferenceEntityAsString = $"ChannelId: {conversationReferenceEntity.ChannelId} UserId: {conversationReferenceEntity.UserId} \n BotId: {conversationReferenceEntity.BotId} \n ConversationId: {conversationReferenceEntity.ConversationId} \n ServiceUrl: {conversationReferenceEntity.ServiceUrl}";
        await turnContext.SendActivityAsync(MessageFactory.Text(conversationReferenceEntityAsString), cancellationToken);
    }
}
