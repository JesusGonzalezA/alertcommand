using Alert4U.models;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Alert4U.clients;

internal class MicrosoftTeamsBotClient
{
    private readonly HttpClient _httpClient;
    private readonly string _endpoint;

	public MicrosoftTeamsBotClient(HttpClient httpClient, IConfiguration configuration)
	{
		_httpClient = httpClient;
        _endpoint = configuration.GetValue<string>("NotificationEndpoint");
	}

    public async Task<HttpResponseMessage> SendNotification(SendNotificationBody body)
    {
        var serializerOptions = new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
        return await _httpClient.PostAsJsonAsync(_endpoint, body, serializerOptions, default(CancellationToken));
    }
}
