using KmsDev.MaxBot.Full.Requests;
using KmsDev.MaxBot.Full.Responses;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Full.Requests
{
    //TODO not-tested

    public partial class GetVideoInfoRequest : RequestBase<GetVideoInfoResponse>
    {
        [JsonIgnore]
        public string Token { get; set; }

        public override MaxBotRequestHttpSettings GetRequestHttpSettings()
        {
            return new MaxBotRequestHttpSettings
            {
                Method = HttpMethod.Get,
                Url = $"videos/{Token}"
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
        public static Task<GetVideoInfoResponse> GetVideoInfoAsync(this MaxBotClientApiContainer.MessagesSection messagesSection, GetVideoInfoRequest videoRequest, Action<GetVideoInfoRequest.ResilienceDefaultSettings>? resilienceSettingsAction = default, CancellationToken cancellationToken = default)
        {
            var requestResilienceSettings = new GetVideoInfoRequest.ResilienceDefaultSettings();

            resilienceSettingsAction?.Invoke(requestResilienceSettings);

            return messagesSection.MaxBotClient.SendRequestAsync(videoRequest, requestResilienceSettings, cancellationToken);
        }
    }
}