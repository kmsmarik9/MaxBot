using KmsDev.MaxBot.Enums;
using KmsDev.MaxBot.Requests;
using KmsDev.MaxBot.Responses;
using System.Text.Json.Serialization;

namespace KmsDev.MaxBot.Requests
{
    public partial class GetUploadFileUrlRequest : RequestBase<GetUploadFileUrlResponse>
    {
        [JsonIgnore]
        public MaxBotUploadType UploadType { get; set; } = MaxBotUploadType.File;

        public override MaxBotRequestHttpSettings GetRequestHttpSettings()
        {
            return new MaxBotRequestHttpSettings
            {
                Method = HttpMethod.Post,
                Url = $"uploads?type={UploadType}".ToLowerInvariant()
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
        /// <summary>
        /// Получить ссылку для загрузки image, audio, video, file
        /// </summary>
        /// <returns></returns>
        public static Task<GetUploadFileUrlResponse> GetUploadFileUrlAsync(this MaxBotClientApiContainer.UploadSection uploadSection, MaxBotUploadType uploadType, Action<GetUploadFileUrlRequest.ResilienceDefaultSettings>? resilienceSettingsAction = default, CancellationToken cancellationToken = default)
        {
            var getUrlRequest = new GetUploadFileUrlRequest
            {
                UploadType = uploadType
            };

            var requestResilienceSettings = new GetUploadFileUrlRequest.ResilienceDefaultSettings();

            resilienceSettingsAction?.Invoke(requestResilienceSettings);

            return uploadSection.MaxBotClient.SendRequestAsync(getUrlRequest, requestResilienceSettings, cancellationToken);
        }
    }
}