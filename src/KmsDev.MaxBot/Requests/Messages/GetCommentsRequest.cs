using KmsDev.MaxBot.Requests;
using KmsDev.MaxBot.Responses;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Requests
{
    public partial class GetCommentsRequest : RequestBase<GetCommentsResponse>
    {
        [JsonIgnore]
        public string MessageId { get; set; } = "";

        /// <summary>
        /// Список ID комментариев одной строкой через запятую
        /// </summary>
        [JsonIgnore]
        public IEnumerable<string>? CommentIds { get; set; }

        [JsonIgnore]
        public DateTimeOffset? Before { get; set; }

        [JsonIgnore]
        public DateTimeOffset? After { get; set; }

        /// <summary>
        /// Максимум комментариев в ответе
        /// </summary>
        [JsonIgnore]
        public int? Count { get; set; }

        public override MaxBotRequestHttpSettings GetRequestHttpSettings()
        {
            return new MaxBotRequestHttpSettings
            {
                Method = HttpMethod.Get,
                Url = MaxBotRequestUrl.WithQuery
                (
                    $"messages/{MessageId}/comments",
                    new()
                    {
                        ["comment_ids"] = MaxBotRequestUrl.Join(CommentIds),
                        ["before"] = MaxBotRequestUrl.UnixMilliseconds(Before),
                        ["after"] = MaxBotRequestUrl.UnixMilliseconds(After),
                        ["count"] = MaxBotRequestUrl.Number(Count)
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
        public static Task<GetCommentsResponse> GetCommentsAsync(this MaxBotClientApiContainer.MessagesSection messagesSection, GetCommentsRequest request, Action<GetCommentsRequest.ResilienceDefaultSettings>? resilienceSettingsAction = default, CancellationToken cancellationToken = default)
        {
            var requestResilienceSettings = new GetCommentsRequest.ResilienceDefaultSettings();
            resilienceSettingsAction?.Invoke(requestResilienceSettings);
            return messagesSection.MaxBotClient.SendRequestAsync(request, requestResilienceSettings, cancellationToken);
        }
    }
}
