using KmsDev.MaxBot.Requests;
using KmsDev.MaxBot.Responses;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Requests
{
    public partial class AddChatMembersRequest : RequestBase<ModifyMembersResponse>
    {
        [JsonIgnore]
        public long ChatId { get; set; }

        /// <summary>
        /// Не больше 100 идентификаторов
        /// </summary>
        [JsonPropertyName("user_ids")]
        public List<long> UserIds { get; set; } = [];

        public override MaxBotRequestHttpSettings GetRequestHttpSettings()
        {
            return new MaxBotRequestHttpSettings
            {
                Method = HttpMethod.Post,
                Url = MaxBotRequestUrl.WithQuery($"chats/{ChatId}/members")
            };
        }

        public class ResilienceDefaultSettings : MaxBotRequestResilienceDefaultSettings
        {
            public ResilienceDefaultSettings()
            {
                RetryMaxAttempts = 3;
                RetryDelay = TimeSpan.FromSeconds(30);
            }
        }
    }
}

namespace KmsDev.MaxBot
{
    public static partial class MaxBotExtensions
    {
        public static Task<ModifyMembersResponse> AddMembersAsync(this MaxBotClientApiContainer.ChatsSection chatsSection, AddChatMembersRequest request, Action<AddChatMembersRequest.ResilienceDefaultSettings>? resilienceSettingsAction = default, CancellationToken cancellationToken = default)
        {
            var requestResilienceSettings = new AddChatMembersRequest.ResilienceDefaultSettings();
            resilienceSettingsAction?.Invoke(requestResilienceSettings);
            return chatsSection.MaxBotClient.SendRequestAsync(request, requestResilienceSettings, cancellationToken);
        }
    }
}
