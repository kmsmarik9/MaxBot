using KmsDev.MaxBot.Full.Requests;
using KmsDev.MaxBot.Full.Responses;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Full.Requests
{
    public partial class SetSubscriptionRequest : RequestBase<SetSubscriptionResponse>
    {
        /// <summary>
        /// URL HTTPS-endpoint вашего бота. Должен начинаться с https://
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        /// Список типов обновлений, которые хочет получать ваш бот.
        /// </summary>
        [JsonPropertyName("update_types")]
        public List<MaxBotUpdateType> UpdateTypes { get; set; } = [];

        /// <summary>
        /// Cекрет, который должен быть отправлен в заголовке X-Max-Bot-Api-Secret в каждом запросе Webhook. Разрешены только символы A-Z, a-z, 0-9, и дефис. Заголовок рекомендован, чтобы запрос поступал из установленного веб-узла
        /// </summary>
        [JsonPropertyName("secret")]
        public string? Secret { get; set; }

        public override MaxBotRequestHttpSettings GetRequestHttpSettings()
        {
            return new MaxBotRequestHttpSettings
            {
                Method = HttpMethod.Post,
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

namespace KmsDev.MaxBot.Full
{
    public static partial class MaxBotExtensions
    {
        public static Task<SetSubscriptionResponse> SetSubscriptionAsync(this MaxBotClientApiContainer.SubscriptionsSection subscriptionsSection, SetSubscriptionRequest request, Action<SetSubscriptionRequest.ResilienceDefaultSettings>? resilienceSettingsAction = default, CancellationToken cancellationToken = default)
        {
            var requestResilienceSettings = new SetSubscriptionRequest.ResilienceDefaultSettings();

            resilienceSettingsAction?.Invoke(requestResilienceSettings);

            return subscriptionsSection.MaxBotClient.SendRequestAsync(request, requestResilienceSettings, cancellationToken);
        }
    }
}