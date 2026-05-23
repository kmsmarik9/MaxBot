using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    public class ApiInputFileAttachment
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
        /// Имя загруженного файла
        /// </summary>
        [JsonPropertyName("filename")]
        public string FileName { get; init; }

        /// <summary>
        /// Размер файла в байтах
        /// </summary>
        [JsonPropertyName("size")]
        public long FileSize { get; init; }

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
