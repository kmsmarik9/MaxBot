using KmsDev.MaxBot.Requests;
using KmsDev.MaxBot.Responses;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Requests
{
    public partial class DeleteMessageRequest : RequestBase<SimpleQueryResultResponse>
    {
        [JsonIgnore]
        public string MessageId { get; set; } = "";

        public override MaxBotRequestHttpSettings GetRequestHttpSettings()
        {
            return new MaxBotRequestHttpSettings
            {
                Method = HttpMethod.Delete,
                Url = MaxBotRequestUrl.WithQuery
                (
                    "messages",
                    new()
                    {
                        ["message_id"] = MessageId
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
        public static Task<SimpleQueryResultResponse> DeleteMessageAsync(this MaxBotClientApiContainer.MessagesSection messagesSection, DeleteMessageRequest request, Action<DeleteMessageRequest.ResilienceDefaultSettings>? resilienceSettingsAction = default, CancellationToken cancellationToken = default)
        {
            var requestResilienceSettings = new DeleteMessageRequest.ResilienceDefaultSettings();
            resilienceSettingsAction?.Invoke(requestResilienceSettings);
            return messagesSection.MaxBotClient.SendRequestAsync(request, requestResilienceSettings, cancellationToken);
        }
    }
}
