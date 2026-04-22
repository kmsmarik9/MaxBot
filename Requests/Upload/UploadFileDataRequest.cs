using KmsDev.MaxBot.Full.Enums;
using KmsDev.MaxBot.Full.Exceptions;
using KmsDev.MaxBot.Full.Requests;
using KmsDev.MaxBot.Full.Requests.Upload;
using KmsDev.MaxBot.Full.Responses;
using OneOf;

namespace KmsDev.MaxBot.Full.Requests
{
    public partial class UploadFileDataRequest : UploadDataBase<UploadFileResponse>
    {
        public class ResilienceDefaultSettings : MaxBotRequestResilienceDefaultSettings
        {
            public ResilienceDefaultSettings()
            {
                RetryMaxAttempts = 5;
                RetryDelay = TimeSpan.FromSeconds(30);
                Timeout = TimeSpan.FromMinutes(10);
            }
        }
    }
}

namespace KmsDev.MaxBot.Full
{
    public static partial class MaxBotExtensions
    {
        /// <summary>
        /// Загрузить файл
        /// </summary>
        /// <returns>
        /// - Union0: (Url, Token)
        /// <br/>
        /// - Union1: Exception
        /// </returns>
        public static async Task<OneOf<(string Url, string Token), MaxBotRequestException>> UploadFileAsync(this MaxBotClientApiContainer.UploadSection uploadSection, Stream stream, string? fileName = null, Action<UploadFileDataRequest.ResilienceDefaultSettings>? resilienceSettingsAction = default, CancellationToken cancellationToken = default)
        {
            try
            {
                var uploadFileUrlResponse = await uploadSection.GetUploadFileUrlAsync(MaxBotUploadType.File, cancellationToken: cancellationToken);

                if (string.IsNullOrWhiteSpace(uploadFileUrlResponse.Url))
                {
                    return new MaxBotRequestException("Url is empty");
                }

                var uploadRequest = new UploadFileDataRequest
                {
                    UploadUrl = uploadFileUrlResponse.Url,
                    Stream = stream,
                    FileName = fileName
                };

                var requestResilienceSettings = new UploadFileDataRequest.ResilienceDefaultSettings();

                resilienceSettingsAction?.Invoke(requestResilienceSettings);

                var uploadResponse = await uploadSection.MaxBotClient.SendRequestAsync(uploadRequest, requestResilienceSettings, cancellationToken);

                return (uploadFileUrlResponse.Url, uploadResponse.Token);
            }
            catch (MaxBotRequestException mbre)
            {
                return mbre;
            }
            catch (Exception ex)
            {
                return new MaxBotRequestException(ex.Message);
            }
        }
    }
}
