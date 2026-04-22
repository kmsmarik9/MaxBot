using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Full.Models
{
    public class ApiInputLinkedMessage
    {
        /// <summary>
        /// Тип связанного сообщения
        /// </summary>
        [JsonPropertyName("type")]
        public required MaxBotMessageLinkType LinkType { get; init; }

        /// <summary>
        /// Пользователь, отправивший сообщение
        /// </summary>
        [JsonPropertyName("sender")]
        public ApiInputUser? Sender { get; init; }

        /// <summary>
        /// Чат, в котором сообщение было изначально опубликовано. Только для пересланных сообщений
        /// </summary>
        [JsonPropertyName("chat_id")]
        public long? ChatId { get; init; }

        /// <summary>
        /// Схема, представляющая тело сообщения
        /// </summary>
        [JsonPropertyName("message")]
        public required ApiInputMessageBody MessageBody { get; init; }
    }
}
