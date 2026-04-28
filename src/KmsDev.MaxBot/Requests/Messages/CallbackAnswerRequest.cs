using KmsDev.MaxBot.Models;
using KmsDev.MaxBot.Requests;
using KmsDev.MaxBot.Responses;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Requests
{
    /// <summary>
    /// Этот метод используется для отправки ответа после того, как пользователь нажал на кнопку. Ответом может быть обновленное сообщение и/или одноразовое уведомление для пользователя
    /// <br/>
    /// Обязательно должно быть заполнено поле Message или Notification
    /// </summary>
    public partial class CallbackAnswerRequest : RequestBase<CallbackAnswerResponse>
    {
        [JsonIgnore]
        public string CallbackId { get; set; }

        /// <summary>
        /// Заполните это, если хотите изменить текущее сообщение
        /// </summary>
        [JsonPropertyName("message")]
        public MessageBody? Message { get; set; }

        /// <summary>
        /// Заполните это, если хотите просто отправить одноразовое уведомление пользователю
        /// </summary>
        [JsonPropertyName("notification")]
        public bool? Notification { get; set; }


        public override MaxBotRequestHttpSettings GetRequestHttpSettings()
        {
            return new MaxBotRequestHttpSettings
            {
                Method = HttpMethod.Post,
                Url = $"answers?callback_id={CallbackId}"
            };
        }

        public class MessageBody
        {
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
        public static Task<CallbackAnswerResponse> CallbackAnswerAsync(this MaxBotClientApiContainer.MessagesSection messagesSection, CallbackAnswerRequest callbackAnswerRequest, Action<CallbackAnswerRequest.ResilienceDefaultSettings>? resilienceSettingsAction = default, CancellationToken cancellationToken = default)
        {
            var requestResilienceSettings = new CallbackAnswerRequest.ResilienceDefaultSettings();

            resilienceSettingsAction?.Invoke(requestResilienceSettings);

            return messagesSection.MaxBotClient.SendRequestAsync(callbackAnswerRequest, requestResilienceSettings, cancellationToken);
        }
    }
}