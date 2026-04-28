using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(ApiOutputInlineKeyboardAttachment), "inline_keyboard")]
    [JsonDerivedType(typeof(ApiOutputLocationAttachment), "location")]
    [JsonDerivedType(typeof(ApiOutputShareAttachment), "share")]
    [JsonDerivedType(typeof(ApiOutputContactAttachment), "contact")]
    [JsonDerivedType(typeof(ApiOutputStickerAttachment), "sticker")]
    [JsonDerivedType(typeof(ApiOutputFileAttachment), "file")]
    [JsonDerivedType(typeof(ApiOutputAudioAttachment), "audio")]
    [JsonDerivedType(typeof(ApiOutputVideoAttachment), "video")]
    [JsonDerivedType(typeof(ApiOutputImageAttachment), "image")]
    public abstract class ApiOutputAttachmentBase
    {

    }
}
