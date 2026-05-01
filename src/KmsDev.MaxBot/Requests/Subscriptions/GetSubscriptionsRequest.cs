using KmsDev.MaxBot.Requests;
using KmsDev.MaxBot.Responses;

namespace KmsDev.MaxBot.Requests
{
    public partial class GetSubscriptionsRequest : RequestBase<GetSubscriptionsResponse>
    {
        public static readonly GetSubscriptionsRequest STATIC = new();

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

namespace KmsDev.MaxBot
{
    public static partial class MaxBotExtensions
    {
        public static Task<GetSubscriptionsResponse> GetSubscriptionsAsync(this MaxBotClientApiContainer.SubscriptionsSection subscriptionsSection, Action<GetSubscriptionsRequest.ResilienceDefaultSettings>? resilienceSettingsAction = default, CancellationToken cancellationToken = default)
        {
            var requestResilienceSettings = new GetSubscriptionsRequest.ResilienceDefaultSettings();

            resilienceSettingsAction?.Invoke(requestResilienceSettings);

            return subscriptionsSection.MaxBotClient.SendRequestAsync(GetSubscriptionsRequest.STATIC, requestResilienceSettings, cancellationToken);
        }
    }
}