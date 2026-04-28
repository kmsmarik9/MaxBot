using KmsDev.MaxBot.Requests;
using KmsDev.MaxBot.Responses;
using OneOf;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Requests
{
    public partial class GetMessagesRequest : RequestBase<GetMessagesResponse>
    {
        /// <summary>
        /// ID чата или Список ID сообщений
        /// </summary>
        [JsonIgnore]
        public OneOf<long, string[]> Target { get; set; }

        [JsonIgnore]
        public DateTimeOffset? From { get; set; }

        [JsonIgnore]
        public DateTimeOffset? To { get; set; }

        [JsonIgnore]
        public int? Count { get; set; }


        public override MaxBotRequestHttpSettings GetRequestHttpSettings()
        {
            var queryParams = new List<string>(4);

            Target.Switch
            (
                chatId => queryParams.Add($"chat_id={chatId}"),
                messageIds => queryParams.Add($"message_ids={string.Join(',', messageIds)}")
            );

            if (From.HasValue)
            {
                queryParams.Add($"from={From.Value.ToUnixTimeMilliseconds()}");
            }

            if (To.HasValue)
            {
                queryParams.Add($"to={To.Value.ToUnixTimeMilliseconds()}");
            }

            if (Count.HasValue)
            {
                queryParams.Add($"count={Count}");
            }

            return new MaxBotRequestHttpSettings
            {
                Method = HttpMethod.Get,
                Url = $"messages?{string.Join('&', queryParams)}"
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
        public static Task<GetMessagesResponse> GetMessagesAsync(this MaxBotClientApiContainer.MessagesSection messagesSection, GetMessagesRequest messageRequest, Action<GetMessagesRequest.ResilienceDefaultSettings>? resilienceSettingsAction = default, CancellationToken cancellationToken = default)
        {
            var requestResilienceSettings = new GetMessagesRequest.ResilienceDefaultSettings();

            resilienceSettingsAction?.Invoke(requestResilienceSettings);

            return messagesSection.MaxBotClient.SendRequestAsync(messageRequest, requestResilienceSettings, cancellationToken);
        }
    }
}