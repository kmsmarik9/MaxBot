using System.Text.Json.Serialization;

namespace KmsDev.MaxBot
{
    [JsonConverter(typeof(EnumByStringConverter<MaxBotChatStatus>))]
    public enum MaxBotChatStatus
    {
        [EnumValueByString("active")]
        Active = 10,

        [EnumValueByString("removed")]
        Removed = 20,

        [EnumValueByString("left")]
        Left = 30,

        [EnumValueByString("closed")]
        Closed = 40,

        [EnumValueByString("suspended")]
        Suspended = 50
    }
}
