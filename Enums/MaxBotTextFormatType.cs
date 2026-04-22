using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Full
{
    [JsonConverter(typeof(EnumByStringConverter<MaxBotTextFormatType>))]
    public enum MaxBotTextFormatType
    {
        [EnumValueByString("markdown")]
        Markdown = 10,

        [EnumValueByString("html")]
        html = 20
    }
}
