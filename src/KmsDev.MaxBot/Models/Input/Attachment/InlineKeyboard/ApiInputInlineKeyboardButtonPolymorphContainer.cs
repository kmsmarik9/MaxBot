using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    [JsonConverter(typeof(MaxBotPolymorphContainerDeserializeJsonConverter<ApiInputInlineKeyboardButtonPolymorphContainer>))]
    [MaxBotSourceGeneratePolymorphContainer("type")]
    public partial class ApiInputInlineKeyboardButtonPolymorphContainer : IMaxBotPolymorphContainer
    {
        [MaxBotSourceGeneratePolymorphItem("callback")]
        public readonly ApiInputInlineKeyboardCallbackButton? Callback;

        [MaxBotSourceGeneratePolymorphItem("link")]
        public readonly ApiInputInlineKeyboardLinkButton? Link;

        [MaxBotSourceGeneratePolymorphItem("request_geo_location")]
        public readonly ApiInputInlineKeyboardRequestGeoLocationButton? RequestGeoLocation;

        [MaxBotSourceGeneratePolymorphItem("request_contact")]
        public readonly ApiInputInlineKeyboardRequestContactButton? RequestContact;

        [MaxBotSourceGeneratePolymorphItem("message")]
        public readonly ApiInputInlineKeyboardMessageButton? Message;

        [MaxBotSourceGeneratePolymorphItem("open_app")]
        public readonly ApiInputInlineKeyboardOpenAppButton? OpenApp;

        [MaxBotSourceGeneratePolymorphItem("clipboard")]
        public readonly ApiInputInlineKeyboardClipboardButton? Clipboard;

        public string? UnknowData { get; private set; }
    }
}
