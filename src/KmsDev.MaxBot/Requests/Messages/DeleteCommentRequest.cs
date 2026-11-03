using KmsDev.MaxBot.Requests;
using KmsDev.MaxBot.Responses;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Requests
{
    public partial class DeleteCommentRequest : RequestBase<SimpleQueryResultResponse>
    {
        [JsonIgnore]
        public string MessageId { get; set; } = "";

        [JsonIgnore]
        public string CommentId { get; set; } = "";

        public override MaxBotRequestHttpSettings GetRequestHttpSettings()
        {
            return new MaxBotRequestHttpSettings
            {
                Method = HttpMethod.Delete,
                Url = MaxBotRequestUrl.WithQuery
                (
                    $"messages/{MessageId}/comments",
                    new()
                    {
                        ["comment_id"] = CommentId
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
        public static Task<SimpleQueryResultResponse> DeleteCommentAsync(this MaxBotClientApiContainer.MessagesSection messagesSection, DeleteCommentRequest request, Action<DeleteCommentRequest.ResilienceDefaultSettings>? resilienceSettingsAction = default, CancellationToken cancellationToken = default)
        {
            var requestResilienceSettings = new DeleteCommentRequest.ResilienceDefaultSettings();
            resilienceSettingsAction?.Invoke(requestResilienceSettings);
            return messagesSection.MaxBotClient.SendRequestAsync(request, requestResilienceSettings, cancellationToken);
        }
    }
}
