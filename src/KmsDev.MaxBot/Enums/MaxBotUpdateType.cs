using System.Text.Json.Serialization;

namespace KmsDev.MaxBot
{
    [JsonConverter(typeof(EnumByStringConverter<MaxBotUpdateType>))]
    public enum MaxBotUpdateType
    {
        [EnumValueByString("message_created")]
        MessageCreated = 10,

        [EnumValueByString("message_callback")]
        MessageCallback = 20,

        [EnumValueByString("message_edited")]
        MessageEdited = 30,

        [EnumValueByString("message_removed")]
        MessageRemoved = 40,

        [EnumValueByString("bot_added")]
        BotAdded = 50,

        [EnumValueByString("bot_removed")]
        BotRemoved = 60,

        [EnumValueByString("dialog_muted")]
        DialogMuted = 70,

        [EnumValueByString("dialog_unmuted")]
        DialogUnmuted = 80,

        [EnumValueByString("dialog_cleared")]
        DialogCleared = 90,

        [EnumValueByString("dialog_removed")]
        DialogRemoved = 100,

        [EnumValueByString("user_added")]
        UserAdded = 110,

        [EnumValueByString("user_removed")]
        UserRemoved = 120,

        [EnumValueByString("bot_started")]
        BotStarted = 130,

        [EnumValueByString("bot_stopped")]
        BotStopped = 140,

        [EnumValueByString("chat_title_changed")]
        ChatTitleChanged = 150
    }
}
