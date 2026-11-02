using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    public class ApiInputUpdateCommentRemoved : IApiInputUpdateItem
    {
        public MaxBotUpdateType UpdateType => MaxBotUpdateType.CommentRemoved;

        /// <summary>
        /// Unix-время, когда произошло событие
        /// </summary>
        [JsonPropertyName("timestamp")]
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset Timestamp { get; init; }

        /// <summary>
        /// ID удалённого комментария
        /// </summary>
        [JsonPropertyName("message_id")]
        public required string MessageId { get; init; }

        /// <summary>
        /// ID чата, где комментарий был удалён
        /// </summary>
        [JsonPropertyName("chat_id")]
        public long ChatId { get; init; }

        /// <summary>
        /// Пользователь, удаливший комментарий
        /// </summary>
        [JsonPropertyName("user_id")]
        public long UserId { get; init; }

        /// <summary>
        /// ID поста
        /// </summary>
        [JsonPropertyName("post_id")]
        public required string PostId { get; init; }
    }
}
