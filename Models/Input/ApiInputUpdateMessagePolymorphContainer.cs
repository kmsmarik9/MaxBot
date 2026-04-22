using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Full.Models
{
    [JsonConverter(typeof(MaxBotPolymorphContainerDeserializeJsonConverter<ApiInputUpdateMessagePolymorphContainer>))]
    [MaxBotSourceGeneratePolymorphContainer("update_type")]
    public partial class ApiInputUpdateMessagePolymorphContainer
    {
        [MaxBotSourceGeneratePolymorphItem("message_created")]
        public readonly ApiInputUpdateMessageCreated? MessageCreated;

        [MaxBotSourceGeneratePolymorphItem("message_callback")]
        public readonly ApiInputUpdateMessageCallback? MessageCallback;

        [MaxBotSourceGeneratePolymorphItem("bot_started")]
        public readonly ApiInputUpdateBotStarted? BotStarted;

        [MaxBotSourceGeneratePolymorphItem("bot_stopped")]
        public readonly ApiInputUpdateBotStoped? BotStopped;
    }
}
