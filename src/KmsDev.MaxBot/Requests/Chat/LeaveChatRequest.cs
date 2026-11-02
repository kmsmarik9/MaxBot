using KmsDev.MaxBot.Requests;
using KmsDev.MaxBot.Responses;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Requests
{
    public partial class LeaveChatRequest : RequestBase<SimpleQueryResultResponse>
    {
        [JsonIgnore]
        public long ChatId { get; set; }

        public override MaxBotRequestHttpSettings GetRequestHttpSettings()
        {
            return new MaxBotRequestHttpSettings
            {
                Method = HttpMethod.Delete,
                Url = MaxBotRequestUrl.WithQuery($"chats/{ChatId}/members/me")
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
        public static Task<SimpleQueryResultResponse> LeaveChatAsync(this MaxBotClientApiContainer.ChatsSection chatsSection, LeaveChatRequest request, Action<LeaveChatRequest.ResilienceDefaultSettings>? resilienceSettingsAction = default, CancellationToken cancellationToken = default)
        {
            var requestResilienceSettings = new LeaveChatRequest.ResilienceDefaultSettings();
            resilienceSettingsAction?.Invoke(requestResilienceSettings);
            return chatsSection.MaxBotClient.SendRequestAsync(request, requestResilienceSettings, cancellationToken);
        }
    }
}
