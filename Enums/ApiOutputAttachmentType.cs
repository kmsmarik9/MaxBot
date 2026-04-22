using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Full
{
    [JsonConverter(typeof(EnumByStringConverter<ApiOutputAttachmentType>))]
    public enum ApiOutputAttachmentType
    {
        [EnumValueByString("image")]
        Image = 10,

        [EnumValueByString("video")]
        Video = 20,

        [EnumValueByString("audio")]
        Audio = 30,

        [EnumValueByString("file")]
        File = 40,

        [EnumValueByString("sticker")]
        Sticker = 50,

        [EnumValueByString("contact")]
        Contact = 60,

        //[EnumValueByString("inline_keyboard")]
        //InlineKeyboard = 70,

        [EnumValueByString("location")]
        Location = 80,

        [EnumValueByString("share")]
        Share = 90
    }
}
