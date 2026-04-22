using KmsDev.MaxBot.Full.Requests;
using KmsDev.MaxBot.Full.Responses;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Full.Requests
{
    //TODO not-tested

    public partial class GetMessageRequest : RequestBase<GetMessageResponse>
    {
        [JsonIgnore]
        public string MessageId { get; set; }

        public override MaxBotRequestHttpSettings GetRequestHttpSettings()
        {
            return new MaxBotRequestHttpSettings
            {
                Method = HttpMethod.Get,
                Url = $"messages/{MessageId}"
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

namespace KmsDev.MaxBot.Full
{
    public static partial class MaxBotExtensions
    {
        public static Task<GetMessageResponse> GetMessageAsync(this MaxBotClientApiContainer.MessagesSection messagesSection, GetMessageRequest messageRequest, Action<GetMessageRequest.ResilienceDefaultSettings>? resilienceSettingsAction = default, CancellationToken cancellationToken = default)
        {
            var requestResilienceSettings = new GetMessageRequest.ResilienceDefaultSettings();

            resilienceSettingsAction?.Invoke(requestResilienceSettings);

            return messagesSection.MaxBotClient.SendRequestAsync(messageRequest, requestResilienceSettings, cancellationToken);
        }
    }
}