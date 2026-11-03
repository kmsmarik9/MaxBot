using KmsDev.MaxBot.Requests;
using KmsDev.MaxBot.Responses;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Requests
{
    public partial class PinMessageRequest : RequestBase<SimpleQueryResultResponse>
    {
        [JsonIgnore]
        public long ChatId { get; set; }

        [JsonPropertyName("message_id")]
        public string MessageId { get; set; } = "";

        /// <summary>
        /// Уведомить участников системным сообщением
        /// </summary>
        [JsonPropertyName("notify")]
        public bool? Notify { get; set; }

        public override MaxBotRequestHttpSettings GetRequestHttpSettings()
        {
            return new MaxBotRequestHttpSettings
            {
                Method = HttpMethod.Put,
                Url = MaxBotRequestUrl.WithQuery($"chats/{ChatId}/pin")
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
        public static Task<SimpleQueryResultResponse> PinMessageAsync(this MaxBotClientApiContainer.ChatsSection chatsSection, PinMessageRequest request, Action<PinMessageRequest.ResilienceDefaultSettings>? resilienceSettingsAction = default, CancellationToken cancellationToken = default)
        {
            var requestResilienceSettings = new PinMessageRequest.ResilienceDefaultSettings();
            resilienceSettingsAction?.Invoke(requestResilienceSettings);
            return chatsSection.MaxBotClient.SendRequestAsync(request, requestResilienceSettings, cancellationToken);
        }
    }
}
