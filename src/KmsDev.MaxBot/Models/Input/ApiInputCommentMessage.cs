using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    /// <summary>
    /// Комментарий. В отличие от сообщения, нет публичного url, а тело не содержит вложений.
    /// </summary>
    public class ApiInputCommentMessage
    {
        [JsonPropertyName("sender")]
        public ApiInputUser? Sender { get; init; }

        [JsonPropertyName("recipient")]
        public required ApiInputRecipient Recipient { get; init; }

        [JsonPropertyName("timestamp")]
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public required DateTimeOffset Timestamp { get; init; }

        [JsonPropertyName("link")]
        public ApiInputCommentLinkedMessage? Link { get; init; }

        [JsonPropertyName("body")]
        public required ApiInputCommentMessageBody Body { get; init; }

        [JsonPropertyName("stat")]
        public ApiInputMessageStat? Stat { get; init; }
    }
}
