using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    [JsonConverter(typeof(MaxBotPolymorphContainerDeserializeJsonConverter<ApiInputMessageAttachmentPolymorphContainer>))]
    [MaxBotSourceGeneratePolymorphContainer("type")]
    public partial class ApiInputMessageAttachmentPolymorphContainer : IMaxBotPolymorphContainer
    {
        [MaxBotSourceGeneratePolymorphItem("image")]
        public readonly ApiInputImageAttachment? Image;

        [MaxBotSourceGeneratePolymorphItem("video")]
        public readonly ApiInputVideoAttachment? Video;

        [MaxBotSourceGeneratePolymorphItem("audio")]
        public readonly ApiInputAudioAttachment? Audio;

        [MaxBotSourceGeneratePolymorphItem("file")]
        public readonly ApiInputFileAttachment? File;

        [MaxBotSourceGeneratePolymorphItem("sticker")]
        public readonly ApiInputStickerAttachment? Sticker;

        [MaxBotSourceGeneratePolymorphItem("contact")]
        public readonly ApiInputContactAttachment? Contact;

        [MaxBotSourceGeneratePolymorphItem("share")]
        public readonly ApiInputShareAttachment? Share;

        [MaxBotSourceGeneratePolymorphItem("location")]
        public readonly ApiInputLocationAttachment? Location;

        //TODO inline_keyboard

        public string? UnknowData { get; private set; }
    }
}