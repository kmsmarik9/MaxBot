using KmsDev.MaxBot.Requests;
using KmsDev.MaxBot.Responses;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Requests
{
    public partial class RemoveChatMemberRequest : RequestBase<SimpleQueryResultResponse>
    {
        [JsonIgnore]
        public long ChatId { get; set; }

        [JsonIgnore]
        public long UserId { get; set; }

        /// <summary>
        /// Заблокировать пользователя в чате со ссылкой. Для остальных чатов сервер игнорирует флаг.
        /// </summary>
        [JsonIgnore]
        public bool Block { get; set; }

        public override MaxBotRequestHttpSettings GetRequestHttpSettings()
        {
            return new MaxBotRequestHttpSettings
            {
                Method = HttpMethod.Delete,
                Url = MaxBotRequestUrl.WithQuery
                (
                    $"chats/{ChatId}/members",
                    new()
                    {
                        ["user_id"] = UserId.ToString(),
                        ["block"] = MaxBotRequestUrl.Flag(Block)
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
        public static Task<SimpleQueryResultResponse> RemoveMemberAsync(this MaxBotClientApiContainer.ChatsSection chatsSection, RemoveChatMemberRequest request, Action<RemoveChatMemberRequest.ResilienceDefaultSettings>? resilienceSettingsAction = default, CancellationToken cancellationToken = default)
        {
            var requestResilienceSettings = new RemoveChatMemberRequest.ResilienceDefaultSettings();
            resilienceSettingsAction?.Invoke(requestResilienceSettings);
            return chatsSection.MaxBotClient.SendRequestAsync(request, requestResilienceSettings, cancellationToken);
        }
    }
}
