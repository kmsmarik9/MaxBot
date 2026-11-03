using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    /// <summary>
    /// Тело комментария. Вложений нет.
    /// </summary>
    public class ApiInputCommentMessageBody
    {
        /// <summary>
        /// Уникальный ID сообщения
        /// </summary>
        [JsonPropertyName("mid")]
        public required string MessageId { get; init; }

        /// <summary>
        /// ID последовательности сообщения в чате
        /// </summary>
        [JsonPropertyName("seq")]
        public required long SequenceId { get; init; }

        [JsonPropertyName("text")]
        public string? Text { get; init; }

        /// <summary>
        /// Разметка текста комментария
        /// </summary>
        [JsonPropertyName("markup")]
        public List<ApiInputMarkupElementPolymorphContainer>? Markup { get; init; }
    }
}
