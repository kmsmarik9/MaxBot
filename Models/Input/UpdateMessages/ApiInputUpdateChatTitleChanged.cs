using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Full.Models
{
    public class ApiInputUpdateChatTitleChanged : IApiInputUpdateItem
    {
        public MaxBotUpdateType UpdateType => MaxBotUpdateType.ChatTitleChanged;

        /// <summary>
        /// Unix-время, когда произошло событие
        /// </summary>
        [JsonPropertyName("timestamp")]
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset TimeStamp { get; set; }

        /// <summary>
        /// ID чата, где произошло событие
        /// </summary>
        [JsonPropertyName("chat_id")]
        public long ChatId { get; set; }

        /// <summary>
        /// Пользователь, который изменил название
        /// </summary>
        [JsonPropertyName("user")]
        public required ApiInputUser User { get; set; }

        /// <summary>
        /// Новое название
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; init; }
    }
}
