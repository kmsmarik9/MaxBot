using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    /// <summary>
    /// Полезная нагрузка изображения. Поля взаимоисключающие.
    /// </summary>
    public class ApiPhotoAttachmentRequestPayload
    {
        [JsonPropertyName("url")]
        public string? Url { get; set; }

        [JsonPropertyName("token")]
        public string? Token { get; set; }

        /// <summary>
        /// Токены загруженных изображений. Ключ — идентификатор фото.
        /// </summary>
        [JsonPropertyName("photos")]
        public Dictionary<string, ApiPhotoToken>? Photos { get; set; }
    }

    public class ApiPhotoToken
    {
        [JsonPropertyName("token")]
        public string Token { get; set; } = "";
    }
}
