using Alert4U.clients;
using Alert4U.models;
using Microsoft.Extensions.Configuration;

namespace Alert4U.services;

internal class MessageOptionService
{
    private readonly MicrosoftTeamsBotClient _microsoftTeamsBotClient;
    private readonly IConfiguration _configuration;

    public MessageOptionService(MicrosoftTeamsBotClient microsoftTeamsBotClient, IConfiguration configuration)
    {
        _microsoftTeamsBotClient = microsoftTeamsBotClient;
        _configuration = configuration;
    }

    public async Task Invoke(string message)
    {
        await _microsoftTeamsBotClient.SendNotification(new SendNotificationBody()
        {
            BotId = _configuration.GetValue<string>("BotId"),
            ConversationId = _configuration.GetValue<string>("ConversationId"),
            ServiceUrl = _configuration.GetValue<string>("ServiceUrl"),
            Message = message
        });
    }
}
