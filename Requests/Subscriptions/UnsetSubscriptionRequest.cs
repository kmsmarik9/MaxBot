using KmsDev.MaxBot.Full.Requests;
using KmsDev.MaxBot.Full.Responses;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Full.Requests
{
    //TODO not-tested

    public partial class UnsetSubscriptionRequest : RequestBase<UnsetSubscriptionResponse>
    {
        [JsonIgnore]
        public string Url { get; set; }

        public override MaxBotRequestHttpSettings GetRequestHttpSettings()
        {
            return new MaxBotRequestHttpSettings
            {
                Method = HttpMethod.Delete,
                Url = $"subscriptions?url={Url}"
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
        public static Task<UnsetSubscriptionResponse> UnsetSubscriptionAsync(this MaxBotClientApiContainer.MessagesSection messagesSection, UnsetSubscriptionRequest messageRequest, Action<UnsetSubscriptionRequest.ResilienceDefaultSettings>? resilienceSettingsAction = default, CancellationToken cancellationToken = default)
        {
            var requestResilienceSettings = new UnsetSubscriptionRequest.ResilienceDefaultSettings();

            resilienceSettingsAction?.Invoke(requestResilienceSettings);

            return messagesSection.MaxBotClient.SendRequestAsync(messageRequest, requestResilienceSettings, cancellationToken);
        }
    }
}