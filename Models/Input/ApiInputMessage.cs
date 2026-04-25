using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Full.Models
{
    public class ApiInputMessage
    {
        /// <summary>
        /// Пльзователь, отправивший сообщение.  Опционально, может отсутствовать для сообщений в каналах.
        /// </summary>
        [JsonPropertyName("sender")]

        public ApiInputUser? Sender { get; init; }

        /// <summary>
        /// Получатель сообщения. Может быть пользователем или чатом
        /// </summary>
        [JsonPropertyName("recipient")]
        public required ApiInputRecipient Recipient { get; init; }

        /// <summary>
        /// Время создания сообщения в формате Unix-time
        /// </summary>
        [JsonPropertyName("timestamp")]
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public required DateTimeOffset TimeStamp { get; init; }

        /// <summary>
        /// Пересланное или ответное сообщение
        /// </summary>
        [JsonPropertyName("link")]
        public ApiInputLinkedMessage? Link { get; init; }

        /// <summary>
        /// Содержимое сообщения. Текст + вложения. Может быть null, если сообщение содержит только пересланное сообщение
        /// </summary>
        [JsonPropertyName("body")]
        public ApiInputMessageBody Body { get; init; }

        /// <summary>
        /// Статистика сообщения.
        /// </summary>
        [JsonPropertyName("stat")]
        public ApiInputMessageStat? Stat { get; init; }

        /// <summary>
        /// Публичная ссылка на пост в канале. Отсутствует для диалогов и групповых чатов
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; init; }
    }
}
