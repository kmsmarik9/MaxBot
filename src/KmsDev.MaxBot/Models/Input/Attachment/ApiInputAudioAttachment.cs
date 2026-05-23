using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    public class ApiInputAudioAttachment
    {
        /// <summary>
        /// Используйте token, если вы пытаетесь повторно использовать одно и то же вложение в другом сообщении.
        /// </summary>
        [JsonIgnore]
        public string Token => Payload.Token;

        /// <summary>
        /// URL медиа-вложения. Этот URL будет получен в объекте Update после отправки сообщения в чат.
        /// </summary>
        [JsonIgnore]
        public string Url => Payload.Url;

        /// <summary>
        /// Аудио транскрипция
        /// </summary>
        [JsonPropertyName("transcription")]
        public string? Transcription { get; init; }

        [JsonInclude]
        [JsonPropertyName("payload")]
        internal PayloadData Payload { get; init; } = new();

        internal class PayloadData
        {
            [JsonPropertyName("token")]
            public string Token { get; set; }

            [JsonPropertyName("url")]
            public string Url { get; set; }
        }
    }
}
