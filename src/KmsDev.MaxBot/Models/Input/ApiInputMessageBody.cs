using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    public class ApiInputMessageBody
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

        /// <summary>
        /// Новый текст сообщения
        /// </summary>
        [JsonPropertyName("text")]
        public string? Text { get; set; }


        //TODO attachments
        //TODO markup
    }
}
