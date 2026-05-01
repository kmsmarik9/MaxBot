using KmsDev.MaxBot.Requests;
using KmsDev.MaxBot.Responses;

namespace KmsDev.MaxBot.Requests
{
    public partial class GetMeRequest : RequestBase<GetMeResponse>
    {
        internal static readonly GetMeRequest STATIC = new();

        private static readonly MaxBotRequestHttpSettings _requestSettings = new()
        {
            Method = HttpMethod.Get,
            Url = "me"
        };

        public override MaxBotRequestHttpSettings GetRequestHttpSettings()
        {
            return _requestSettings;
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
        public static Task<GetMeResponse> GetMeAsync(this MaxBotClientApiContainer.BotsSection botsSection, Action<GetMeRequest.ResilienceDefaultSettings>? resilienceSettingsAction = default, CancellationToken cancellationToken = default)
        {
            var requestResilienceSettings = new GetMeRequest.ResilienceDefaultSettings();

            resilienceSettingsAction?.Invoke(requestResilienceSettings);

            return botsSection.MaxBotClient.SendRequestAsync(GetMeRequest.STATIC, requestResilienceSettings, cancellationToken);
        }
    }
}