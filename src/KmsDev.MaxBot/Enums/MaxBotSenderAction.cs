using System.Text.Json.Serialization;

namespace KmsDev.MaxBot
{
    [JsonConverter(typeof(EnumByStringConverter<MaxBotSenderAction>))]
    public enum MaxBotSenderAction
    {
        [EnumValueByString("typing_on")]
        TypingOn = 10,

        [EnumValueByString("sending_photo")]
        SendingPhoto = 20,

        [EnumValueByString("sending_video")]
        SendingVideo = 30,

        [EnumValueByString("sending_audio")]
        SendingAudio = 40,

        [EnumValueByString("sending_file")]
        SendingFile = 50,

        [EnumValueByString("mark_seen")]
        MarkSeen = 60
    }
}
