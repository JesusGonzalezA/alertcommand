
namespace mteams.DTO
{
    public class IncomingPostNotification
    {
        public string BotId { get; set; }
        public string ConversationId { get; set; }
        public string ServiceUrl { get; set; }
        public string Message { get; set; }
        public string ChannelId { get; set; }
    }
}
