using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Full.Models
{
    public class ApiInputUpdateBotAdded : IApiInputUpdateItem
    {
        public MaxBotUpdateType UpdateType => MaxBotUpdateType.BotAdded;

        /// <summary>
        /// Unix-время, когда произошло событие
        /// </summary>
        [JsonPropertyName("timestamp")]
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset Timestamp { get; init; }

        /// <summary>
        /// ID чата, куда был добавлен бот
        /// </summary>
        [JsonPropertyName("chat_id")]
        public long ChatId { get; init; }

        /// <summary>
        /// Пользователь, добавивший бота в чат
        /// </summary>
        [JsonPropertyName("user")]
        public ApiInputUser User { get; init; }

        /// <summary>
        /// Указывает, был ли бот добавлен в канал или нет
        /// </summary>
        [JsonPropertyName("is_channel")]
        public bool IsChannel { get; init; }
    }
}
