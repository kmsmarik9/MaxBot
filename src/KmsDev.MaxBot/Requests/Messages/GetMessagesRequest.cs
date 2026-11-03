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

        [Obsolete("Параметр from устарел в схеме MAX. Используйте After.")]
        [JsonIgnore]
        public DateTimeOffset? From { get; set; }

        [Obsolete("Параметр to устарел в схеме MAX. Используйте Before.")]
        [JsonIgnore]
        public DateTimeOffset? To { get; set; }

        /// <summary>
        /// Сообщения до этой отметки времени
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset? Before { get; set; }

        /// <summary>
        /// Сообщения после этой отметки времени
        /// </summary>
        [JsonIgnore]
        public DateTimeOffset? After { get; set; }

        [JsonIgnore]
        public int? Count { get; set; }


        public override MaxBotRequestHttpSettings GetRequestHttpSettings()
        {
            string? chatId = null;
            string? messageIds = null;

            Target.Switch
            (
                id => chatId = id.ToString(),
                ids => messageIds = MaxBotRequestUrl.Join(ids)
            );

#pragma warning disable CS0618
            var url = MaxBotRequestUrl.WithQuery
            (
                "messages",
                new()
                {
                    ["chat_id"] = chatId,
                    ["message_ids"] = messageIds,
                    ["from"] = MaxBotRequestUrl.UnixMilliseconds(From),
                    ["to"] = MaxBotRequestUrl.UnixMilliseconds(To),
                    ["before"] = MaxBotRequestUrl.UnixMilliseconds(Before),
                    ["after"] = MaxBotRequestUrl.UnixMilliseconds(After),
                    ["count"] = MaxBotRequestUrl.Number(Count)
                }
            );
#pragma warning restore CS0618

            return new MaxBotRequestHttpSettings
            {
                Method = HttpMethod.Get,
                Url = url
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