using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Full
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
