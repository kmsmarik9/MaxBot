using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    public class ApiInputStickerAttachment
    {
        /// <summary>
        /// ID стикера
        /// </summary>
        [JsonIgnore]
        public string Code => Payload.Code;

        /// <summary>
        /// URL медиа-вложения. Этот URL будет получен в объекте Update после отправки сообщения в чат.
        /// </summary>
        [JsonIgnore]
        public string Url => Payload.Url;

        /// <summary>
        /// Ширина стикера
        /// </summary>
        [JsonPropertyName("width")]
        public int Width { get; init; }

        /// <summary>
        /// Высота стикера
        /// </summary>
        [JsonPropertyName("height")]
        public int Height { get; init; }

        [JsonInclude]
        [JsonPropertyName("payload")]
        internal PayloadData Payload { get; init; } = new();

        internal class PayloadData
        {
            [JsonPropertyName("code")]
            public string Code { get; set; }

            [JsonPropertyName("url")]
            public string Url { get; set; }
        }
    }
}
