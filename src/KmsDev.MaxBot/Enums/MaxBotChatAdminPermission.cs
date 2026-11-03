using System.Text.Json.Serialization;

namespace KmsDev.MaxBot
{
    [JsonConverter(typeof(EnumByStringConverter<MaxBotChatAdminPermission>))]
    public enum MaxBotChatAdminPermission
    {
        [EnumValueByString("read_all_messages")]
        ReadAllMessages = 10,

        [EnumValueByString("add_remove_members")]
        AddRemoveMembers = 20,

        [EnumValueByString("add_admins")]
        AddAdmins = 30,

        [EnumValueByString("change_chat_info")]
        ChangeChatInfo = 40,

        [EnumValueByString("pin_message")]
        PinMessage = 50,

        [EnumValueByString("edit_link")]
        EditLink = 60,

        [EnumValueByString("write")]
        Write = 70,

        [EnumValueByString("edit")]
        Edit = 80,

        [EnumValueByString("delete")]
        Delete = 90,

        [EnumValueByString("can_call")]
        CanCall = 100,

        [EnumValueByString("view_stats")]
        ViewStats = 110
    }
}
