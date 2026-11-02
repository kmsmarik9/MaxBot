using KmsDev.MaxBot.Models;
using KmsDev.MaxBot.Requests;
using KmsDev.MaxBot.Responses;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Requests
{
    public partial class EditMyCommandsRequest : RequestBase<BotCommandsResponse>
    {
        /// <summary>
        /// Команды бота. Не больше 32.
        /// </summary>
        [JsonPropertyName("commands")]
        public List<ApiBotCommand>? Commands { get; set; }

        public override MaxBotRequestHttpSettings GetRequestHttpSettings()
        {
            return new MaxBotRequestHttpSettings
            {
                Method = HttpMethod.Patch,
                Url = MaxBotRequestUrl.WithQuery("me/commands")
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
        public static Task<BotCommandsResponse> EditMyCommandsAsync(this MaxBotClientApiContainer.BotsSection botsSection, EditMyCommandsRequest request, Action<EditMyCommandsRequest.ResilienceDefaultSettings>? resilienceSettingsAction = default, CancellationToken cancellationToken = default)
        {
            var requestResilienceSettings = new EditMyCommandsRequest.ResilienceDefaultSettings();
            resilienceSettingsAction?.Invoke(requestResilienceSettings);
            return botsSection.MaxBotClient.SendRequestAsync(request, requestResilienceSettings, cancellationToken);
        }
    }
}
