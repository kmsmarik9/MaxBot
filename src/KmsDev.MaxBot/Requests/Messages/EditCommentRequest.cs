using KmsDev.MaxBot.Models;
using KmsDev.MaxBot.Requests;
using KmsDev.MaxBot.Responses;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Requests
{
    public partial class EditCommentRequest : RequestBase<SimpleQueryResultResponse>
    {
        [JsonIgnore]
        public string MessageId { get; set; } = "";

        [JsonIgnore]
        public string CommentId { get; set; } = "";

        /// <summary>
        /// Текст комментария, до 4000 символов
        /// </summary>
        [JsonPropertyName("text")]
        public string? Text { get; set; }

        [JsonPropertyName("link")]
        public ApiNewMessageLink? Link { get; set; }

        [JsonPropertyName("format")]
        public MaxBotTextFormatType? Format { get; set; }

        public override MaxBotRequestHttpSettings GetRequestHttpSettings()
        {
            return new MaxBotRequestHttpSettings
            {
                Method = HttpMethod.Put,
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
        public static Task<SimpleQueryResultResponse> EditCommentAsync(this MaxBotClientApiContainer.MessagesSection messagesSection, EditCommentRequest request, Action<EditCommentRequest.ResilienceDefaultSettings>? resilienceSettingsAction = default, CancellationToken cancellationToken = default)
        {
            var requestResilienceSettings = new EditCommentRequest.ResilienceDefaultSettings();
            resilienceSettingsAction?.Invoke(requestResilienceSettings);
            return messagesSection.MaxBotClient.SendRequestAsync(request, requestResilienceSettings, cancellationToken);
        }
    }
}
