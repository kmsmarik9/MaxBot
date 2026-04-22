using KmsDev.MaxBot.Full.Requests;
using KmsDev.MaxBot.Full.Responses;

namespace KmsDev.MaxBot.Full.Requests
{
    //TODO not-tested

    public partial class GetSubscriptionsRequest : RequestBase<GetSubscriptionsResponse>
    {
        public override MaxBotRequestHttpSettings GetRequestHttpSettings()
        {
            return new MaxBotRequestHttpSettings
            {
                Method = HttpMethod.Get,
                Url = "subscriptions"
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
        public static Task<GetSubscriptionsResponse> GetSubscriptionsAsync(this MaxBotClientApiContainer.MessagesSection messagesSection, GetSubscriptionsRequest request, Action<GetSubscriptionsRequest.ResilienceDefaultSettings>? resilienceSettingsAction = default, CancellationToken cancellationToken = default)
        {
            var requestResilienceSettings = new GetSubscriptionsRequest.ResilienceDefaultSettings();

            resilienceSettingsAction?.Invoke(requestResilienceSettings);

            return messagesSection.MaxBotClient.SendRequestAsync(request, requestResilienceSettings, cancellationToken);
        }
    }
}