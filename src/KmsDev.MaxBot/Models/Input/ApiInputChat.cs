using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    public class ApiInputChat
    {
        [JsonPropertyName("chat_id")]
        public long ChatId { get; init; }

        [JsonPropertyName("type")]
        public MaxBotChatType Type { get; init; }

        [JsonPropertyName("status")]
        public MaxBotChatStatus Status { get; init; }

        /// <summary>
        /// Заголовок. Для диалогов может быть null.
        /// </summary>
        [JsonPropertyName("title")]
        public string? Title { get; init; }

        [JsonPropertyName("icon")]
        public ApiInputImage? Icon { get; init; }

        [JsonPropertyName("last_event_time")]
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset LastEventTime { get; init; }

        [JsonPropertyName("participants_count")]
        public int ParticipantsCount { get; init; }

        /// <summary>
        /// ID владельца. Виден только администраторам.
        /// </summary>
        [JsonPropertyName("owner_id")]
        public long? OwnerId { get; init; }

        /// <summary>
        /// Участники и время последней активности в миллисекундах Unix. Ключ — ID пользователя.
        /// </summary>
        [JsonPropertyName("participants")]
        public Dictionary<long, long>? Participants { get; init; }

        [JsonPropertyName("is_public")]
        public bool IsPublic { get; init; }

        [JsonPropertyName("link")]
        public string? Link { get; init; }

        [JsonPropertyName("description")]
        public string? Description { get; init; }

        [JsonPropertyName("dialog_with_user")]
        public ApiInputUserWithPhoto? DialogWithUser { get; init; }

        /// <summary>
        /// Число сообщений. Для диалогов отсутствует.
        /// </summary>
        [JsonPropertyName("messages_count")]
        public int? MessagesCount { get; init; }

        /// <summary>
        /// Закреплённое сообщение. Приходит только при запросе одного чата.
        /// </summary>
        [JsonPropertyName("pinned_message")]
        public ApiInputMessage? PinnedMessage { get; init; }
    }
}
