using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Full.Models
{
    public class ApiInputUpdateMessageCreated : IApiInputUpdateItem
    {
        public MaxBotUpdateType UpdateType => MaxBotUpdateType.MessageCreated;

        /// <summary>
        /// Unix-время, когда произошло событие
        /// </summary>
        [JsonPropertyName("timestamp")]
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset Timestamp { get; init; }

        /// <summary>
        /// Новое созданное сообщение
        /// </summary>
        [JsonPropertyName("message")]
        public required ApiInputMessage Message { get; init; }

        /// <summary>
        /// Текущий язык пользователя
        /// </summary>
        [JsonPropertyName("user_locale")]
        public string? UserLocale { get; init; }
    }
}
