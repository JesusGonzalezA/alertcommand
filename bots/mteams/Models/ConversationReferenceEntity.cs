namespace mteams.Models;

public class ConversationReferenceEntity : Entity
{
    public string UserId { get; set; }
    public string ServiceUrl { get; set; }
    public string BotId { get; set; }
    public string ConversationId { get; set; }
    public string ChannelId { get; set; }
}
