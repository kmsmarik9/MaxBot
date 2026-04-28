using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(ApiOutputInlineKeyboardCallbackButton), "callback")]
    [JsonDerivedType(typeof(ApiOutputInlineKeyboardLinkButton), "link")]
    [JsonDerivedType(typeof(ApiOutputInlineKeyboardOpenAppButton), "open_app")]
    [JsonDerivedType(typeof(ApiOutputInlineKeyboardRequestGeoLocationButton), "request_geo_location")]
    [JsonDerivedType(typeof(ApiOutputInlineKeyboardRequestContactButton), "request_contact")]
    [JsonDerivedType(typeof(ApiOutputInlineKeyboardMessageButton), "message")]
    [JsonDerivedType(typeof(ApiOutputInlineKeyboardClipboardButton), "clipboard")]
    public abstract class ApiOutputInlineKeyboardButtonBase
    {
        /// <summary>
        /// Видимый текст кнопки. от 1 до 128 символов
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
