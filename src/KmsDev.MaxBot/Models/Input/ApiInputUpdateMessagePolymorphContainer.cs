using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Models
{
    [JsonConverter(typeof(MaxBotPolymorphContainerDeserializeJsonConverter<ApiInputUpdateMessagePolymorphContainer>))]
    [MaxBotSourceGeneratePolymorphContainer("update_type")]
    public partial class ApiInputUpdateMessagePolymorphContainer : IMaxBotPolymorphContainer
    {
        [MaxBotSourceGeneratePolymorphItem("message_created")]
        public readonly ApiInputUpdateMessageCreated? MessageCreated;

        [MaxBotSourceGeneratePolymorphItem("message_callback")]
        public readonly ApiInputUpdateMessageCallback? MessageCallback;

        [MaxBotSourceGeneratePolymorphItem("message_edited")]
        public readonly ApiInputUpdateMessageEdited? MessageEdited;

        [MaxBotSourceGeneratePolymorphItem("message_removed")]
        public readonly ApiInputUpdateMessageRemoved? MessageRemoved;

        [MaxBotSourceGeneratePolymorphItem("comment_created")]
        public readonly ApiInputUpdateCommentCreated? CommentCreated;

        [MaxBotSourceGeneratePolymorphItem("comment_edited")]
        public readonly ApiInputUpdateCommentEdited? CommentEdited;

        [MaxBotSourceGeneratePolymorphItem("comment_removed")]
        public readonly ApiInputUpdateCommentRemoved? CommentRemoved;

        [MaxBotSourceGeneratePolymorphItem("bot_added")]
        public readonly ApiInputUpdateBotAdded? BotAdded;

        [MaxBotSourceGeneratePolymorphItem("bot_removed")]
        public readonly ApiInputUpdateBotRemoved? BotRemoved;

        [MaxBotSourceGeneratePolymorphItem("dialog_muted")]
        public readonly ApiInputUpdateDialogMuted? DialogMuted;

        [MaxBotSourceGeneratePolymorphItem("dialog_unmuted")]
        public readonly ApiInputUpdateDialogUnmuted? DialogUnmuted;

        [MaxBotSourceGeneratePolymorphItem("dialog_cleared")]
        public readonly ApiInputUpdateDialogCleared? DialogCleared;

        [MaxBotSourceGeneratePolymorphItem("dialog_removed")]
        public readonly ApiInputUpdateDialogRemoved? DialogRemoved;

        [MaxBotSourceGeneratePolymorphItem("user_added")]
        public readonly ApiInputUpdateUserAdded? UserAdded;

        [MaxBotSourceGeneratePolymorphItem("user_removed")]
        public readonly ApiInputUpdateUserRemoved? UserRemoved;

        [MaxBotSourceGeneratePolymorphItem("bot_started")]
        public readonly ApiInputUpdateBotStarted? BotStarted;

        [MaxBotSourceGeneratePolymorphItem("bot_stopped")]
        public readonly ApiInputUpdateBotStoped? BotStopped;

        [MaxBotSourceGeneratePolymorphItem("chat_title_changed")]
        public readonly ApiInputUpdateChatTitleChanged? ChatTitleChanged;

        [MaxBotSourceGeneratePolymorphItem("bot_admin_permissions_changed")]
        public readonly ApiInputUpdateBotAdminPermissionsChanged? BotAdminPermissionsChanged;

        public string? UnknowData { get; private set; }
    }
}
