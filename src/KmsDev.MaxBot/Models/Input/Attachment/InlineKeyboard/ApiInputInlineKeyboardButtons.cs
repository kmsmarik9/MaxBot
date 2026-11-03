using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    public class ApiInputInlineKeyboardButton
    {
        /// <summary>
        /// Видимый текст кнопки. От 1 до 128 символов.
        /// </summary>
        [JsonPropertyName("text")]
        public string? Text { get; init; }
    }

    public class ApiInputInlineKeyboardCallbackButton : ApiInputInlineKeyboardButton
    {
        /// <summary>
        /// Токен кнопки. До 1024 символов.
        /// </summary>
        [JsonPropertyName("payload")]
        public string? Payload { get; init; }
    }

    public class ApiInputInlineKeyboardLinkButton : ApiInputInlineKeyboardButton
    {
        /// <summary>
        /// URL. До 2048 символов.
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; init; }
    }

    public class ApiInputInlineKeyboardRequestGeoLocationButton : ApiInputInlineKeyboardButton
    {
        [JsonPropertyName("quick")]
        public bool Quick { get; init; }
    }

    public class ApiInputInlineKeyboardRequestContactButton : ApiInputInlineKeyboardButton
    {
    }

    public class ApiInputInlineKeyboardMessageButton : ApiInputInlineKeyboardButton
    {
    }

    public class ApiInputInlineKeyboardOpenAppButton : ApiInputInlineKeyboardButton
    {
        [JsonPropertyName("web_app")]
        public string? WebApp { get; init; }

        [JsonPropertyName("payload")]
        public string? Payload { get; init; }

        [JsonPropertyName("contact_id")]
        public long? ContactId { get; init; }
    }

    public class ApiInputInlineKeyboardClipboardButton : ApiInputInlineKeyboardButton
    {
        /// <summary>
        /// Текст для буфера. До 1024 символов.
        /// </summary>
        [JsonPropertyName("payload")]
        public string? Payload { get; init; }
    }
}
