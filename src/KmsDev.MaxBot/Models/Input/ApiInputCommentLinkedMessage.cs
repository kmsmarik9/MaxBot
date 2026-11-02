using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    public class ApiInputCommentLinkedMessage
    {
        [JsonPropertyName("type")]
        public required MaxBotMessageLinkType LinkType { get; init; }

        [JsonPropertyName("sender")]
        public ApiInputUser? Sender { get; init; }

        [JsonPropertyName("chat_id")]
        public long? ChatId { get; init; }

        [JsonPropertyName("message")]
        public required ApiInputCommentMessageBody MessageBody { get; init; }
    }
}
