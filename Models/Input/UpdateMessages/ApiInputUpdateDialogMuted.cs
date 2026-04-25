using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Full.Models
{
    public class ApiInputUpdateDialogMuted : IApiInputUpdateItem
    {
        public MaxBotUpdateType UpdateType => MaxBotUpdateType.DialogMuted;

        /// <summary>
        /// Unix-время, когда произошло событие
        /// </summary>
        [JsonPropertyName("timestamp")]
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset Timestamp { get; init; }

        /// <summary>
        /// ID чата, где произошло событие
        /// </summary>
        [JsonPropertyName("chat_id")]
        public long ChatId { get; init; }

        /// <summary>
        /// Пользователь, который отключил уведомления
        /// </summary>
        [JsonPropertyName("user")]
        public ApiInputUser User { get; init; }

        /// <summary>
        /// Время в формате Unix, до наступления которого диалог был отключён
        /// </summary>
        [JsonPropertyName("muted_until")]
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset MutedUntil { get; init; }

        /// <summary>
        /// Текущий язык пользователя в формате IETF BCP 47
        /// </summary>
        [JsonPropertyName("user_locale")]
        public string? UserLocale { get; init; }
    }
}
