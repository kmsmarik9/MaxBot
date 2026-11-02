using KmsDev.MaxBot.Models;
using KmsDev.MaxBot.Requests;
using KmsDev.MaxBot.Responses;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Requests
{
    public partial class EditChatRequest : RequestBase<ChatResponse>
    {
        [JsonIgnore]
        public long ChatId { get; set; }

        [JsonPropertyName("icon")]
        public ApiPhotoAttachmentRequestPayload? Icon { get; set; }

        /// <summary>
        /// Заголовок, от 1 до 200 символов
        /// </summary>
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        /// <summary>
        /// Описание, до 16000 символов. Пустая строка удаляет описание.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// ID сообщения, которое нужно закрепить
        /// </summary>
        [JsonPropertyName("pin")]
        public string? Pin { get; set; }

        /// <summary>
        /// Уведомить участников системным сообщением. По умолчанию на сервере true.
        /// </summary>
        [JsonPropertyName("notify")]
        public bool? Notify { get; set; }

        public override MaxBotRequestHttpSettings GetRequestHttpSettings()
        {
            return new MaxBotRequestHttpSettings
            {
                Method = HttpMethod.Patch,
                Url = MaxBotRequestUrl.WithQuery($"chats/{ChatId}")
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
        public static Task<ChatResponse> EditChatAsync(this MaxBotClientApiContainer.ChatsSection chatsSection, EditChatRequest request, Action<EditChatRequest.ResilienceDefaultSettings>? resilienceSettingsAction = default, CancellationToken cancellationToken = default)
        {
            var requestResilienceSettings = new EditChatRequest.ResilienceDefaultSettings();
            resilienceSettingsAction?.Invoke(requestResilienceSettings);
            return chatsSection.MaxBotClient.SendRequestAsync(request, requestResilienceSettings, cancellationToken);
        }
    }
}
