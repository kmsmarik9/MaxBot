using KmsDev.MaxBot.Requests;
using KmsDev.MaxBot.Responses;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Requests
{
    public partial class GetCommentRequest : RequestBase<GetCommentResponse>
    {
        [JsonIgnore]
        public string MessageId { get; set; } = "";

        [JsonIgnore]
        public string CommentId { get; set; } = "";

        public override MaxBotRequestHttpSettings GetRequestHttpSettings()
        {
            return new MaxBotRequestHttpSettings
            {
                Method = HttpMethod.Get,
                Url = MaxBotRequestUrl.WithQuery($"messages/{MessageId}/comments/{CommentId}")
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
        public static Task<GetCommentResponse> GetCommentAsync(this MaxBotClientApiContainer.MessagesSection messagesSection, GetCommentRequest request, Action<GetCommentRequest.ResilienceDefaultSettings>? resilienceSettingsAction = default, CancellationToken cancellationToken = default)
        {
            var requestResilienceSettings = new GetCommentRequest.ResilienceDefaultSettings();
            resilienceSettingsAction?.Invoke(requestResilienceSettings);
            return messagesSection.MaxBotClient.SendRequestAsync(request, requestResilienceSettings, cancellationToken);
        }
    }
}
