using KmsDev.MaxBot.Full.Exceptions;
using KmsDev.MaxBot.Full.Models;
using KmsDev.MaxBot.Full.Requests;
using KmsDev.MaxBot.Full.Responses;
using Polly;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Full.Requests
{
    public partial class SendMessageRequest : RequestBase<SendMessageResponse>, IMaxBotMessageRequestWithAttachments
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public (TargetType TargetType, long Id) Target { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public bool DisableLinkPreview { get; set; }

        /// <summary>
        /// Новый текст сообщения, до 4000 символов
        /// </summary>
        [JsonPropertyName("text")]
        public string? Text { get; set; }

        /// <summary>
        /// Если false, участники чата не будут уведомлены (по умолчанию true)
        /// </summary>
        [JsonPropertyName("notify")]
        public bool Notify { get; set; } = true;

        /// <summary>
        /// Если установлен, текст сообщения будет форматирован данным способом. 
        /// Для подробной информации загляните в раздел <see href="https://dev.max.ru/docs-api#%D0%A4%D0%BE%D1%80%D0%BC%D0%B0%D1%82%D0%B8%D1%80%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D0%B5%20%D1%82%D0%B5%D0%BA%D1%81%D1%82%D0%B0">Форматирование </see>
        /// </summary>
        [JsonPropertyName("format")]
        public MaxBotTextFormatType? Format { get; set; }

        /// <summary>
        /// Вложения сообщения. Если пусто, все вложения будут удалены
        /// </summary>
        [JsonPropertyName("attachments")]
        public List<ApiOutputAttachmentBase>? Attachments { get; set; }

        public override MaxBotRequestHttpSettings GetRequestHttpSettings()
        {
            var requestUrlBuilder = new StringBuilder("messages");

            switch (Target.TargetType)
            {
                case TargetType.User:
                    {
                        requestUrlBuilder.Append($"?user_id={Target.Id}");
                        break;
                    }
                case TargetType.Chat:
                    {
                        requestUrlBuilder.Append($"?chat_id={Target.Id}");
                        break;
                    }
                default:
                    throw new ArgumentException("TargetType invalid");
            }

            if (DisableLinkPreview)
            {
                requestUrlBuilder.Append("&disable_link_preview=true");
            }

            return new MaxBotRequestHttpSettings
            {
                Method = HttpMethod.Post,
                Url = requestUrlBuilder.ToString()
            };
        }

        public enum TargetType
        {
            User = 1,
            Chat = 2
        }

        public class ResilienceDefaultSettings : MaxBotRequestResilienceDefaultSettings
        {
            public int RetryMaxAttemptsForAttachmentNotReady { get; set; } = 10;
            public TimeSpan RetryDelayForAttachmentNotReady { get; set; } = TimeSpan.FromMinutes(1);

            public ResilienceDefaultSettings()
            {
                EnableRetryStrategy = true;

                RetryMaxAttempts = 3;
                RetryDelay = TimeSpan.FromSeconds(3);

                RetryMaxAttemptsForAttachmentNotReady = 10;
                RetryDelayForAttachmentNotReady = TimeSpan.FromSeconds(30);
            }
        }

        internal class ThrowIfAttachmentNotReadyStrategy : ResilienceStrategy<HttpResponseMessage>
        {
            private static readonly string _attachmentNotReadyUpperValue = "attachment.not.ready".ToUpperInvariant();

            protected override async ValueTask<Outcome<HttpResponseMessage>> ExecuteCore<TState>(Func<ResilienceContext, TState, ValueTask<Outcome<HttpResponseMessage>>> callback, ResilienceContext context, TState state)
            {
                var outcome = await callback(context, state);

                if(outcome.Exception != null)
                {
                    return outcome;
                }

                if (outcome.Result != null && !outcome.Result.IsSuccessStatusCode)
                {
                    var responseText = await outcome.Result.Content.ReadAsStringAsync();

                    using var jsonDocument = JsonDocument.Parse(responseText);
                    if (jsonDocument.RootElement.TryGetProperty("code", out var codeElement))
                    {
                        var codeValue = codeElement.GetString()?.ToUpperInvariant();

                        if (codeValue == _attachmentNotReadyUpperValue)
                        {
                            return Outcome.FromException<HttpResponseMessage>(new MaxBotAttachmentNotReadyException());
                        }
                    }
                }

                return outcome;
            }
        }
    }
}

namespace KmsDev.MaxBot.Full
{
    public static partial class MaxBotExtensions
    {
        public static Task<SendMessageResponse> SendMessageAsync(this MaxBotClientApiContainer.MessagesSection messagesSection, SendMessageRequest messageRequest, Action<SendMessageRequest.ResilienceDefaultSettings>? resilienceSettingsAction = default, CancellationToken cancellationToken = default)
        {
            var requestResilienceSettings = new SendMessageRequest.ResilienceDefaultSettings();

            resilienceSettingsAction?.Invoke(requestResilienceSettings);

            return messagesSection.MaxBotClient.SendRequestAsync(messageRequest, requestResilienceSettings, cancellationToken);
        }
    }
}