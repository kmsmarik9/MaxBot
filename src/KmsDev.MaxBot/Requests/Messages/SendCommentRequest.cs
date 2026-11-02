using KmsDev.MaxBot.Models;
using KmsDev.MaxBot.Requests;
using KmsDev.MaxBot.Responses;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Requests
{
    public partial class SendCommentRequest : RequestBase<SendCommentResponse>
    {
        [JsonIgnore]
        public string MessageId { get; set; } = "";

        [JsonIgnore]
        public bool DisableLinkPreview { get; set; }

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
                Method = HttpMethod.Post,
                Url = MaxBotRequestUrl.WithQuery
                (
                    $"messages/{MessageId}/comments",
                    new()
                    {
                        ["disable_link_preview"] = MaxBotRequestUrl.Flag(DisableLinkPreview)
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
        public static Task<SendCommentResponse> SendCommentAsync(this MaxBotClientApiContainer.MessagesSection messagesSection, SendCommentRequest request, Action<SendCommentRequest.ResilienceDefaultSettings>? resilienceSettingsAction = default, CancellationToken cancellationToken = default)
        {
            var requestResilienceSettings = new SendCommentRequest.ResilienceDefaultSettings();
            resilienceSettingsAction?.Invoke(requestResilienceSettings);
            return messagesSection.MaxBotClient.SendRequestAsync(request, requestResilienceSettings, cancellationToken);
        }
    }
}
