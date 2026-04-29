using KmsDev.MaxBot.Exceptions;
using KmsDev.MaxBot.Models;
using KmsDev.MaxBot.Requests;
using KmsDev.MaxBot.Responses;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Web;

namespace KmsDev.MaxBot
{
    internal class MaxBotClientInternal : IMaxBotClient
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly string _token;

        public CancellationToken SelfCancellationToken { get; }
        public string BotHash { get; }

        public MaxBotClientInternal(IServiceProvider serviceProvider, string token, string botHashSecretKey = "", CancellationToken cancellationToken = default)
        {
            _serviceProvider = serviceProvider;
            _token = token;
            SelfCancellationToken = cancellationToken;

            {
                var secretKeyBytes = Encoding.UTF8.GetBytes(botHashSecretKey ?? string.Empty);
                using var hmac = new HMACSHA256(secretKeyBytes);
                var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(token));

                BotHash = Convert.ToBase64String(hashBytes)
                    .Replace("+", "")
                    .Replace("/", "")
                    .Replace("=", "");
            }

            Api = new(this);
        }

        public MaxBotClientApiContainer Api { get; }

        private readonly static JsonSerializerOptions _defaultWebappInitDataValidatorJsonOptions = new() { PropertyNameCaseInsensitive = true };
        public (bool IsValid, MaxBotWebappUserDataModel? User, string? Error) ValidateWebappInitData(string initData)
        {
            try
            {
                var queryParams = HttpUtility.ParseQueryString(initData);

                var sourceHash = queryParams.Get("hash");
                if (string.IsNullOrWhiteSpace(sourceHash))
                {
                    return (false, null, "hash not found");
                }

                var userRaw = queryParams.Get("user");
                if (string.IsNullOrEmpty(userRaw))
                {
                    return (false, null, "user not found");
                }

                queryParams.Remove("hash");

                {
                    var list = new List<string>(queryParams.Count);
                    foreach (var key in queryParams.AllKeys)
                    {
                        list.Add($"{key}={queryParams.Get(key)}"!);
                    }

                    list.Sort((a, b) => a.CompareTo(b));

                    using var secretHmac = new HMACSHA256(Encoding.UTF8.GetBytes("WebAppData"));
                    var secketKey = secretHmac.ComputeHash(Encoding.UTF8.GetBytes(_token));

                    using var initDataHmac = new HMACSHA256(secketKey);
                    var calculatedHash = BitConverter.ToString(initDataHmac.ComputeHash(Encoding.UTF8.GetBytes(string.Join("\n", list)))).Replace("-", "");

                    var isHashEquals = calculatedHash.Equals(sourceHash, StringComparison.OrdinalIgnoreCase);

                    if (!isHashEquals)
                    {
                        return (false, null, "hash invalid");
                    }
                }

                return (true, JsonSerializer.Deserialize<MaxBotWebappUserDataModel>(userRaw!, _defaultWebappInitDataValidatorJsonOptions), null);
            }
            catch (Exception ex)
            {
                return (false, null, "system error");
            }
        }

        public async Task<TResponse> SendRequestAsync<TResponse, TResilienceSettings>(IMaxBotRequest<TResponse> request, TResilienceSettings resilienceSettings, CancellationToken cancellationToken = default)
            where TResponse : IMaxBotResponse
            where TResilienceSettings: IMaxBotRequestResilienceSettings
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            using var cts = CancellationTokenSource.CreateLinkedTokenSource(SelfCancellationToken, cancellationToken);
            var requestSettings = request.GetRequestHttpSettings();

            using var httpContent = request.GetRequestHttpContent()!;

            var q = await httpContent.ReadAsStringAsync(cancellationToken);

            using var httpRequestMessage = new HttpRequestMessage(requestSettings.Method, requestSettings.Url)
            {
                Content = httpContent
            };


            if (requestSettings.IncludeAuthorizationToken)
            {
                httpRequestMessage.Headers.TryAddWithoutValidation("Authorization", _token);
            }

            try
            {
                MaxBotHttpRequestOptionsKeyForResilienceSettingsCache<TResilienceSettings>.SetSettings(httpRequestMessage.Options, resilienceSettings);

                using var httpResponse = await _serviceProvider
                    .GetRequiredService<IHttpClientFactory>()
                    .CreateClient(request.GetRequestName())
                    .SendAsync(httpRequestMessage, cts.Token);

                if (httpResponse.IsSuccessStatusCode)
                {
                    var responseParser = _serviceProvider.GetRequiredKeyedService<IMaxBotResponseParser>(MaxBotResponseNameCache<TResponse>.Value);

                    var result = await responseParser.ParseAsync<TResponse>(httpResponse, cancellationToken);
                    return result!;
                }
                else
                {
                    var contentErrorMessage = await httpResponse.Content.ReadAsStringAsync(cts.Token);
                    throw new Exception(contentErrorMessage);
                }
            }
            catch(MaxBotRequestException rex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MaxBotRequestException($"MaxBot request fail. {request.GetType().Name}: {ex.Message}", ex);
            }
        }

        public async Task UploadFile(CancellationToken cancellationToken = default)
        {

        }
    }
}