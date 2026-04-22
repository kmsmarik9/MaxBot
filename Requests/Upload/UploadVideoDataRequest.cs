using KmsDev.MaxBot.Full.Enums;
using KmsDev.MaxBot.Full.Exceptions;
using KmsDev.MaxBot.Full.Requests;
using KmsDev.MaxBot.Full.Requests.Upload;
using KmsDev.MaxBot.Full.Responses;
using OneOf;

namespace KmsDev.MaxBot.Full.Requests
{
    public partial class UploadVideoDataRequest : UploadDataBase<UploadVideoResponse>
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
        /// Загрузить видео
        /// </summary>
        /// <returns>
        /// - Union0: (Url, Token, Retval)
        /// <br/>
        /// - Union1: Exception
        /// </returns>
        public static async Task<OneOf<(string Url, string Token, int Retval), MaxBotRequestException>> UploadVideoAsync(this MaxBotClientApiContainer.UploadSection uploadSection, Stream stream, string? fileName = null, Action<UploadVideoDataRequest.ResilienceDefaultSettings>? resilienceSettingsAction = default, CancellationToken cancellationToken = default)
        {
            try
            {
                var uploadFileUrlResponse = await uploadSection.GetUploadFileUrlAsync(MaxBotUploadType.Video, cancellationToken: cancellationToken);

                if(string.IsNullOrWhiteSpace(uploadFileUrlResponse.Url))
                {
                    return new MaxBotRequestException("Url is empty");
                }

                if (string.IsNullOrWhiteSpace(uploadFileUrlResponse.Token))
                {
                    return new MaxBotRequestException("Token is empty");
                }

                var uploadRequest = new UploadVideoDataRequest
                {
                    UploadUrl = uploadFileUrlResponse.Url,
                    Stream = stream,
                    FileName = fileName
                };

                var requestResilienceSettings = new UploadVideoDataRequest.ResilienceDefaultSettings();

                resilienceSettingsAction?.Invoke(requestResilienceSettings);

                var uploadResponse = await uploadSection.MaxBotClient.SendRequestAsync(uploadRequest, requestResilienceSettings, cancellationToken);

                return (uploadFileUrlResponse.Url, uploadFileUrlResponse.Token, uploadResponse.Retval);
            }
            catch(MaxBotRequestException mbre)
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
