using KmsDev.MaxBot.Requests;
using KmsDev.MaxBot.Responses;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Requests
{
    public partial class GetChatMembersRequest : RequestBase<ChatMembersListResponse>
    {
        [JsonIgnore]
        public long ChatId { get; set; }

        /// <summary>
        /// ID пользователей одной строкой через запятую. Если задан, count и marker игнорируются сервером.
        /// </summary>
        [JsonIgnore]
        public IEnumerable<long>? UserIds { get; set; }

        [JsonIgnore]
        public long? Marker { get; set; }

        /// <summary>
        /// От 1 до 100. По умолчанию на сервере 20.
        /// </summary>
        [JsonIgnore]
        public int? Count { get; set; }

        public override MaxBotRequestHttpSettings GetRequestHttpSettings()
        {
            return new MaxBotRequestHttpSettings
            {
                Method = HttpMethod.Get,
                Url = MaxBotRequestUrl.WithQuery
                (
                    $"chats/{ChatId}/members",
                    new()
                    {
                        ["user_ids"] = MaxBotRequestUrl.Join(UserIds),
                        ["marker"] = MaxBotRequestUrl.Number(Marker),
                        ["count"] = MaxBotRequestUrl.Number(Count)
                    }
                )
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
        public static Task<ChatMembersListResponse> GetMembersAsync(this MaxBotClientApiContainer.ChatsSection chatsSection, GetChatMembersRequest request, Action<GetChatMembersRequest.ResilienceDefaultSettings>? resilienceSettingsAction = default, CancellationToken cancellationToken = default)
        {
            var requestResilienceSettings = new GetChatMembersRequest.ResilienceDefaultSettings();
            resilienceSettingsAction?.Invoke(requestResilienceSettings);
            return chatsSection.MaxBotClient.SendRequestAsync(request, requestResilienceSettings, cancellationToken);
        }
    }
}
