using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    public class ApiInputShareAttachment
    {
        /// <summary>
        /// Токен вложения
        /// </summary>
        [JsonIgnore]
        public string Token => Payload.Token;

        /// <summary>
        /// URL, прикрепленный к сообщению в качестве предпросмотра медиа
        /// </summary>
        [JsonIgnore]
        public string Url => Payload.Url;

        /// <summary>
        /// Заголовок предпросмотра ссылки.
        /// </summary>
        [JsonPropertyName("title")]
        public string? Title { get; init; }

        /// <summary>
        /// Описание предпросмотра ссылки
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; init; }

        /// <summary>
        /// Изображение предпросмотра ссылки
        /// </summary>
        [JsonPropertyName("image_url")]
        public string? ImageUrl { get; init; }

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
