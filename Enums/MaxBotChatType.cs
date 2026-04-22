using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Full
{
    [JsonConverter(typeof(EnumByStringConverter<MaxBotChatType>))]
    public enum MaxBotChatType
    {
        [EnumValueByString("chat")]
        Chat = 10,

        [EnumValueByString("dialog")]
        Dialog = 20,

        [EnumValueByString("channel")]
        Channel = 30
    }
}
