using System.Text.Json.Serialization;

namespace KmsDev.MaxBot
{
    [JsonConverter(typeof(EnumByStringConverter<MaxBotMessageLinkType>))]
    public enum MaxBotMessageLinkType
    {
        [EnumValueByString("forward")]
        Forward = 10,

        [EnumValueByString("reply")]
        Reply = 20
    }
}
