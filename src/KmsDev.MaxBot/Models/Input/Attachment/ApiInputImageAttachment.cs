using OneOf;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    public class ApiInputImageAttachment
    {
        /// <summary>
        /// Уникальный ID этого изображения
        /// </summary>
        [JsonIgnore]
        public long PhotoId => Payload.PhotoId;

        /// <summary>
        /// Token
        /// </summary>
        [JsonIgnore]
        public string Token => Payload.Token;

        /// <summary>
        /// URL изображения
        /// </summary>
        [JsonIgnore]
        public string Url => Payload.Url;

        [JsonInclude]
        [JsonPropertyName("payload")]
        internal PayloadData Payload { get; init; } = new();

        internal class PayloadData
        {
            [JsonPropertyName("photo_id")]
            public long PhotoId { get; set; }

            [JsonPropertyName("token")]
            public string Token { get; set; }

            [JsonPropertyName("url")]
            public string Url { get; set; }
        }
    }
}
