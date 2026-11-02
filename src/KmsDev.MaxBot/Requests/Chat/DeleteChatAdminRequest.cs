using KmsDev.MaxBot.Requests;
using KmsDev.MaxBot.Responses;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Requests
{
    public partial class DeleteChatAdminRequest : RequestBase<SimpleQueryResultResponse>
    {
        [JsonIgnore]
        public long ChatId { get; set; }

        [JsonIgnore]
        public long UserId { get; set; }

        public override MaxBotRequestHttpSettings GetRequestHttpSettings()
        {
            return new MaxBotRequestHttpSettings
            {
                Method = HttpMethod.Delete,
                Url = MaxBotRequestUrl.WithQuery($"chats/{ChatId}/members/admins/{UserId}")
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
        public static Task<SimpleQueryResultResponse> DeleteAdminAsync(this MaxBotClientApiContainer.ChatsSection chatsSection, DeleteChatAdminRequest request, Action<DeleteChatAdminRequest.ResilienceDefaultSettings>? resilienceSettingsAction = default, CancellationToken cancellationToken = default)
        {
            var requestResilienceSettings = new DeleteChatAdminRequest.ResilienceDefaultSettings();
            resilienceSettingsAction?.Invoke(requestResilienceSettings);
            return chatsSection.MaxBotClient.SendRequestAsync(request, requestResilienceSettings, cancellationToken);
        }
    }
}
