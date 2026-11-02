using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    public class ApiInputUpdateBotAdminPermissionsChanged : IApiInputUpdateItem
    {
        public MaxBotUpdateType UpdateType => MaxBotUpdateType.BotAdminPermissionsChanged;

        /// <summary>
        /// Unix-время, когда произошло событие
        /// </summary>
        [JsonPropertyName("timestamp")]
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset Timestamp { get; init; }

        /// <summary>
        /// ID чата, где изменились права
        /// </summary>
        [JsonPropertyName("chat_id")]
        public long ChatId { get; init; }

        /// <summary>
        /// Пользователь или бот, изменивший права
        /// </summary>
        [JsonPropertyName("user_id")]
        public long UserId { get; init; }

        /// <summary>
        /// Бот, чьи права изменились
        /// </summary>
        [JsonPropertyName("bot_id")]
        public long BotId { get; init; }

        /// <summary>
        /// Событие произошло в канале
        /// </summary>
        [JsonPropertyName("is_channel")]
        public bool IsChannel { get; init; }

        /// <summary>
        /// Бот остаётся администратором чата или канала
        /// </summary>
        [JsonPropertyName("is_admin")]
        public bool IsAdmin { get; init; }

        /// <summary>
        /// Права администратора. null, если бот больше не администратор.
        /// </summary>
        [JsonPropertyName("permissions")]
        public List<MaxBotChatAdminPermission>? Permissions { get; init; }
    }
}
