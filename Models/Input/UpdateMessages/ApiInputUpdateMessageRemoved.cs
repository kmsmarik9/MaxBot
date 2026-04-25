using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Full.Models
{
    public class ApiInputUpdateMessageRemoved : IApiInputUpdateItem
    {
        public MaxBotUpdateType UpdateType => MaxBotUpdateType.MessageRemoved;

        /// <summary>
        /// Unix-время, когда произошло событие
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTimeOffset Timestamp { get; init; }

        /// <summary>
        /// ID удаленного сообщения
        /// </summary>
        [JsonPropertyName("message_id")]
        public string MessageId { get; init; }

        /// <summary>
        /// ID чата, где сообщение было удалено
        /// </summary>
        [JsonPropertyName("chat_id")]
        public long ChatId { get; init; }

        /// <summary>
        /// Пользователь, удаливший сообщение
        /// </summary>
        [JsonPropertyName("user_id")]
        public long UserId { get; init; }
    }
}
