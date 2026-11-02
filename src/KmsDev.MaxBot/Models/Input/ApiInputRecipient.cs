using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    public class ApiInputRecipient
    {
        /// <summary>
        /// ID чата
        /// </summary>
        [JsonPropertyName("chat_id")]
        public required long ChatId { get; init; }

        /// <summary>
        /// Тип чата
        /// </summary>
        [JsonPropertyName("chat_type")]
        public required MaxBotChatType ChatType { get; init; }

        /// <summary>
        /// ID пользователя, если сообщение было отправлено пользователю
        /// </summary>
        [JsonPropertyName("user_id")]
        public long? UserId { get; init; }

        /// <summary>
        /// ID поста, если сообщение относится к комментариям
        /// </summary>
        [JsonPropertyName("post_id")]
        public string? PostId { get; init; }
    }
}
