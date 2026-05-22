using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    public class ApiInputVideoAttachment
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
        /// Миниатюра видео. URL изображения
        /// </summary>
        [JsonIgnore]
        public string ThumbnailUrl => Thumbnail.Url;

        /// <summary>
        /// Ширина видео
        /// </summary>
        [JsonPropertyName("width")]
        public int? Width { get; init; }

        /// <summary>
        /// Высота видео
        /// </summary>
        [JsonPropertyName("height")]
        public int? Height { get; init; }

        /// <summary>
        /// Длина видео в секундах
        /// </summary>
        [JsonPropertyName("duration")]
        public int? Duration { get; init; }

        [JsonInclude]
        [JsonPropertyName("payload")]
        internal PayloadData Payload { get; } = new();

        [JsonInclude]
        [JsonPropertyName("thumbnail")]
        internal ThumbnailData Thumbnail { get; init; } = new();

        internal class PayloadData
        {
            [JsonPropertyName("token")]
            public string Token { get; set; }

            [JsonPropertyName("url")]
            public string Url { get; set; }
        }

        internal class ThumbnailData
        {
            [JsonPropertyName("url")]
            public string Url { get; set; }
        }
    }
}
