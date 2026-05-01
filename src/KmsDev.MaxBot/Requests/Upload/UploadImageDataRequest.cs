using KmsDev.MaxBot.Enums;
using KmsDev.MaxBot.Exceptions;
using KmsDev.MaxBot.Requests;
using KmsDev.MaxBot.Requests.Upload;
using KmsDev.MaxBot.Responses;
using OneOf;
using Polly;
using System.Text.Json;

namespace KmsDev.MaxBot.Requests
{
    public partial class UploadImageDataRequest : UploadDataBase<UploadImageResponse>
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


        /// <summary>
        /// Кастомная стратегия для MaxBotUploadImageRequest. (возможно это баг Api или сделали специально, под вопросом)
        /// <br/>
        /// Если на сервер отправлять не изображение, то он может ответить 200 статусом, но в body будет json с ошибкой
        /// </summary>
        internal class ThrowIf200StatusWithErrorCodeStrategy : ResilienceStrategy<HttpResponseMessage>
        {
            protected override async ValueTask<Outcome<HttpResponseMessage>> ExecuteCore<TState>(Func<ResilienceContext, TState, ValueTask<Outcome<HttpResponseMessage>>> callback, ResilienceContext context, TState state)
            {
                var outcome = await callback(context, state);

                if (outcome.Result?.IsSuccessStatusCode ?? false)
                {
                    var response = outcome.Result;

                    if (response.Content.Headers.ContentType?.MediaType?.Contains("application/json") ?? false)
                    {
                        var content = await response.Content.ReadAsStringAsync();

                        using var jsonDocument = JsonDocument.Parse(content);

                        if (jsonDocument.RootElement.TryGetProperty("error_code", out _))
                        {
                            var errorData = "unknow error";

                            if (jsonDocument.RootElement.TryGetProperty("error_data", out var jErrorData))
                            {
                                errorData = jErrorData.GetString() ?? errorData;
                            }

                            return Outcome.FromException<HttpResponseMessage>(new MaxBotRequestException(errorData));
                        }
                    }
                }

                return outcome;
            }
        }
    }
}

namespace KmsDev.MaxBot
{
    public static partial class MaxBotExtensions
    {
        /// <summary>
        /// Загрузить изображение
        /// </summary>
        /// <returns>
        /// - Union0: (Url, Token)
        /// <br/>
        /// - Union1: Exception
        /// </returns>
        public static async Task<OneOf<(string Url, string Token), MaxBotRequestException>> UploadImageAsync(this MaxBotClientApiContainer.UploadSection uploadSection, Stream stream, string? fileName = null, Action<UploadImageDataRequest.ResilienceDefaultSettings>? resilienceSettingsAction = default, CancellationToken cancellationToken = default)
        {
            try
            {
                var uploadFileUrlResponse = await uploadSection.GetUploadFileUrlAsync(MaxBotUploadType.Image, cancellationToken: cancellationToken);

                if (string.IsNullOrWhiteSpace(uploadFileUrlResponse.Url))
                {
                    return new MaxBotRequestException("Url is empty");
                }

                var uploadRequest = new UploadImageDataRequest
                {
                    UploadUrl = uploadFileUrlResponse.Url,
                    Stream = stream,
                    FileName = fileName
                };

                var requestResilienceSettings = new UploadImageDataRequest.ResilienceDefaultSettings();

                resilienceSettingsAction?.Invoke(requestResilienceSettings);

                var uploadResponse = await uploadSection.MaxBotClient.SendRequestAsync(uploadRequest, requestResilienceSettings, cancellationToken);

                return (uploadFileUrlResponse.Url, uploadResponse.Photos.Values.First().Token);
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
