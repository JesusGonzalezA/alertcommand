using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Configuration;
using mteams.DTO;
using System.Threading;
using System.Threading.Tasks;

namespace NotifyBot.Controllers;

[Route("api")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly IBot _bot;
    private readonly IBotFrameworkHttpAdapter _adapter;
    private readonly string _appId;

    public NotificationController(IBotFrameworkHttpAdapter adapter, IBot bot, IConfiguration configuration)
    {
        _bot = bot;
        _adapter = adapter;
        _appId = configuration["MicrosoftAppId"] ?? string.Empty;
    }

    [HttpOptions("messages")]
    [HttpPost("messages")]
    public async Task PostMessageAsync()
    {
        await _adapter.ProcessAsync(Request, Response, _bot);
    }

    [HttpPost("notification")]
    public async Task<IActionResult> PostNotificationAsync([FromBody] IncomingPostNotification incomingPostNotification)
    {
        var conversationReference = new ConversationReference()
        {
            Bot = new ChannelAccount() { Id = incomingPostNotification.BotId },
            Conversation = new ConversationAccount() { Id = incomingPostNotification.ConversationId },
            ServiceUrl = incomingPostNotification.ServiceUrl,
            ChannelId = incomingPostNotification.ChannelId
        };

        await ((BotAdapter)_adapter).ContinueConversationAsync(_appId, conversationReference, (turnContext, cancellationToken) => NotifyCallBack(turnContext, cancellationToken, incomingPostNotification.Message), default(CancellationToken));
        return Ok();
    }

    private async Task NotifyCallBack(ITurnContext turnContext, CancellationToken cancellationToken, string message)
    {
        await turnContext.SendActivityAsync(message, cancellationToken: cancellationToken);
    }
}
