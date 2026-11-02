using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    public class ApiInputUpdateCommentCreated : IApiInputUpdateItem
    {
        public MaxBotUpdateType UpdateType => MaxBotUpdateType.CommentCreated;

        /// <summary>
        /// Unix-время, когда произошло событие
        /// </summary>
        [JsonPropertyName("timestamp")]
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset Timestamp { get; init; }

        /// <summary>
        /// Созданный комментарий. Схема события ссылается на Message.
        /// </summary>
        [JsonPropertyName("message")]
        public required ApiInputMessage Message { get; init; }
    }
}
