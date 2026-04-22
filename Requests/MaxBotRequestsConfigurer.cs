using KmsDev.MaxBot.Full.Exceptions;
using KmsDev.MaxBot.Full.Responses;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Polly;
using Polly.Retry;
using Polly.Timeout;
using System.Reflection;
using System.Threading.RateLimiting;

namespace KmsDev.MaxBot.Full.Requests
{
    public sealed class MaxBotRequestsConfigurer
    {
        private readonly Action<ResiliencePipelineBuilder<HttpResponseMessage>> _defaultRpbAction;

        private readonly Dictionary<string, Action<ResiliencePipelineBuilder<HttpResponseMessage>>> _systemRpbActionMap = [];
        private readonly Dictionary<string, Action<ResiliencePipelineBuilder<HttpResponseMessage>>> _customRpbActionMap = [];

        private static readonly HttpRetryStrategyOptions _defaultRetryStrategyOptions = new()
        {
            BackoffType = DelayBackoffType.Exponential,
            Delay = TimeSpan.FromSeconds(1),
            UseJitter = true,
            ShouldHandle = args =>
            {
                if (MaxBotHttpRequestOptionsKeyForResilienceSettingsCache<MaxBotRequestResilienceDefaultSettings>.TryGetFromContext(args.Context, out var resilienceDefaultSettings)
                    && resilienceDefaultSettings.EnableRetryStrategy)
                {
                    if (args.AttemptNumber < resilienceDefaultSettings.RetryMaxAttempts)
                    {
                        return args.Outcome switch
                        {
                            { Result.IsSuccessStatusCode: false } => PredicateResult.True(),
                            { Exception: HttpRequestException } => PredicateResult.True(),
                            { Exception: TimeoutRejectedException } => PredicateResult.True(),
                            _ => PredicateResult.False()
                        };
                    }
                }

                return PredicateResult.False();
            },
            DelayGenerator = static args =>
            {
                if (MaxBotHttpRequestOptionsKeyForResilienceSettingsCache<MaxBotRequestResilienceDefaultSettings>.TryGetFromContext(args.Context, out var resilienceDefaultSettings)
                    && resilienceDefaultSettings.EnableRetryStrategy)
                {
                    return ValueTask.FromResult<TimeSpan?>(resilienceDefaultSettings.RetryDelay);
                }

                return ValueTask.FromResult<TimeSpan?>(null);
            }
        };

        private static readonly HttpTimeoutStrategyOptions _defaultTimeoutStrategyOptions = new()
        {
            Timeout = TimeSpan.FromSeconds(30),
            TimeoutGenerator = static args =>
            {
                if (MaxBotHttpRequestOptionsKeyForResilienceSettingsCache<MaxBotRequestResilienceDefaultSettings>.TryGetFromContext(args.Context, out var resilienceDefaultSettings))
                {
                    return ValueTask.FromResult(resilienceDefaultSettings.Timeout);
                }

                return ValueTask.FromResult(TimeSpan.FromSeconds(30));
            }
        };

        internal MaxBotRequestsConfigurer()
        {
            _defaultRpbAction = rpb =>
            {
                rpb.AddRetry(_defaultRetryStrategyOptions);

                rpb.AddTimeout(_defaultTimeoutStrategyOptions);
            };

            InitSystemRpb();
        }

        internal void ConfigureServices(IServiceCollection serviceCollection)
        {
            {
                var defaultUri = new Uri("https://platform-api.max.ru/");

                var allClientsRateLimiterPipeline = new ResiliencePipelineBuilder()
                    .AddRateLimiter(new SlidingWindowRateLimiter(new SlidingWindowRateLimiterOptions
                    {
                        PermitLimit = 30,
                        QueueLimit = 0, //не копим сообщения, перекладываем на Retry если он установлен
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        Window = TimeSpan.FromSeconds(1),
                        SegmentsPerWindow = 5
                    }))
                    .Build();


                var requestTypes = AppDomain.CurrentDomain
                    .GetAssemblies()
                    .SelectMany(s=>s.GetTypes())
                    .Where(p => p.IsAssignableTo(typeof(IMaxBotRequest)) && !p.IsGenericType && p.IsClass)
                    .ToList();

                foreach (var t in requestTypes)
                {
                    var requestName = typeof(MaxBotRequestNameCache<>)
                        .MakeGenericType(t)
                        .GetField(nameof(MaxBotRequestNameCache<IMaxBotRequest>.Value), BindingFlags.Public | BindingFlags.Static)!
                        .GetValue(null)!
                        .ToString()!;

                    var httpClientBuilder = serviceCollection.AddHttpClient(requestName, c =>
                    {
                        c.BaseAddress = defaultUri;
                        c.Timeout = Timeout.InfiniteTimeSpan;
                    });

                    {
                        Action<ResiliencePipelineBuilder<HttpResponseMessage>> finalRpbAction = _defaultRpbAction;

                        if (_customRpbActionMap.TryGetValue(requestName, out var customAction))
                        {
                            finalRpbAction = customAction;
                        }
                        else if (_systemRpbActionMap.TryGetValue(requestName, out var systemAction))
                        {
                            finalRpbAction = systemAction;
                        }

                        httpClientBuilder.AddResilienceHandler($"maxbot_resilience_handler:{requestName}", rpb =>
                        {
                            rpb.AddPipeline(allClientsRateLimiterPipeline);
                            finalRpbAction(rpb);
                        });
                    }
                }
            }

            {
                var responseTypes = AppDomain.CurrentDomain
                    .GetAssemblies()
                    .SelectMany(s=>s.GetTypes())
                    .Where(p => p.IsAssignableTo(typeof(IMaxBotResponse)) && !p.IsGenericType && p.IsClass)
                    .ToList();

                foreach(var t in responseTypes)
                {
                    var responseName = typeof(MaxBotResponseNameCache<>)
                        .MakeGenericType(t)
                        .GetField(nameof(MaxBotResponseNameCache<IMaxBotResponse>.Value), BindingFlags.Public | BindingFlags.Static)!
                        .GetValue(null)!
                        .ToString()!;

                    if (t.IsAssignableTo(typeof(IMaxBotJsonResponse)))
                    {
                        serviceCollection.AddKeyedSingleton(typeof(IMaxBotResponseParser), responseName, typeof(MaxBotJsonResponseParser));
                    }
                    else if (t.IsAssignableTo(typeof(IMaxBotXmlResponse)))
                    {
                        serviceCollection.AddKeyedSingleton(typeof(IMaxBotResponseParser), responseName, typeof(MaxBotXmlResponseParser));
                    }
                    else
                    {
                        throw new Exception($"Unknow parser type: {t.FullName}");
                    }
                }
            }
        }

        //TODO
        //public MaxBotRequestsConfigurer CustomRequestSettings<TRequest>(Action<ResiliencePipelineBuilder<HttpResponseMessage>> resiliencePipelineBuilder)
        //    where TRequest: IMaxBotRequest
        //{
        //    var requestName = MaxBotRequestNameCache<TRequest>.Value;

        //    _customRpbActionMap[requestName] = resiliencePipelineBuilder;

        //    return this;
        //}

        private void InitSystemRpb()
        {
            #region MaxBotSendMessageRequest
            _systemRpbActionMap[MaxBotRequestNameCache<SendMessageRequest>.Value] = rpb =>
            {
                rpb.AddRetry(new RetryStrategyOptions<HttpResponseMessage>
                {
                    BackoffType = DelayBackoffType.Exponential,
                    Delay = TimeSpan.FromSeconds(1),
                    UseJitter = true,
                    ShouldHandle = static args =>
                    {
                        if (MaxBotHttpRequestOptionsKeyForResilienceSettingsCache<SendMessageRequest.ResilienceDefaultSettings>.TryGetFromContext(args.Context, out var resilienceSettings)
                            && resilienceSettings.EnableRetryStrategy)
                        {
                            if (args.AttemptNumber < resilienceSettings.RetryMaxAttempts)
                            {
                                return args.Outcome switch
                                {
                                    { Result.IsSuccessStatusCode: false } => PredicateResult.True(),
                                    { Exception: HttpRequestException } => PredicateResult.True(),
                                    { Exception: TimeoutRejectedException } => PredicateResult.True(),
                                    _ => PredicateResult.False()
                                };
                            }
                        }

                        return PredicateResult.False();
                    },
                    DelayGenerator = static args =>
                    {
                        if (MaxBotHttpRequestOptionsKeyForResilienceSettingsCache<SendMessageRequest.ResilienceDefaultSettings>.TryGetFromContext(args.Context, out var resilienceSettings)
                            && resilienceSettings.EnableRetryStrategy)
                        {
                            return ValueTask.FromResult<TimeSpan?>(resilienceSettings.RetryDelay);
                        }

                        //иначе стратегия сама вычисляет задержку
                        return ValueTask.FromResult<TimeSpan?>(null);
                    }
                });

                #region только для "attachment.not.ready"
                rpb.AddRetry(new RetryStrategyOptions<HttpResponseMessage> 
                { 
                    BackoffType = DelayBackoffType.Exponential,
                    Delay = TimeSpan.FromSeconds(30),
                    UseJitter = true,
                    ShouldHandle = static args =>
                    {
                        if (MaxBotHttpRequestOptionsKeyForResilienceSettingsCache<SendMessageRequest.ResilienceDefaultSettings>.TryGetFromContext(args.Context, out var resilienceSettings)
                            && resilienceSettings.EnableRetryStrategy)
                        {
                            if(args.AttemptNumber < resilienceSettings.RetryMaxAttemptsForAttachmentNotReady)
                            {
                                return args.Outcome switch
                                {
                                    { Exception: MaxBotAttachmentNotReadyException } => PredicateResult.True(),
                                    _ => PredicateResult.False()
                                };
                            }
                        }

                        return PredicateResult.False();
                    },
                    DelayGenerator = static args =>
                    {
                        if (MaxBotHttpRequestOptionsKeyForResilienceSettingsCache<SendMessageRequest.ResilienceDefaultSettings>.TryGetFromContext(args.Context, out var resilienceSettings)
                            && resilienceSettings.EnableRetryStrategy)
                        {
                            return ValueTask.FromResult<TimeSpan?>(resilienceSettings.RetryDelayForAttachmentNotReady);
                        }

                        //иначе стратегия сама вычисляет задержку
                        return ValueTask.FromResult<TimeSpan?>(null);
                    }
                });

                rpb.AddStrategy(context => new SendMessageRequest.ThrowIfAttachmentNotReadyStrategy());
                #endregion

                rpb.AddTimeout(_defaultTimeoutStrategyOptions);
            };
            #endregion

            #region MaxBotUploadImageRequest
            _systemRpbActionMap[MaxBotRequestNameCache<UploadImageDataRequest>.Value] = rpb =>
            {
                //кастомная стратегия для 200 статуса
                //обработка до retry, т.к. это мы отправили кривой файл
                rpb.AddStrategy(context => new UploadImageDataRequest.ThrowIf200StatusWithErrorCodeStrategy());

                rpb.AddRetry(_defaultRetryStrategyOptions);

                rpb.AddTimeout(_defaultTimeoutStrategyOptions);
            };
            #endregion
        }
    }
}