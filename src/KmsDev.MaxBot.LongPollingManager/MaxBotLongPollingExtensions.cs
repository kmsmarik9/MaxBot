using KmsDev.MaxBot.Handlers;
using KmsDev.MaxBot.Models;
using KmsDev.MaxBot.Requests;
using Microsoft.Extensions.DependencyInjection;

namespace KmsDev.MaxBot.LongPollingManager
{
    public static class MaxBotLongPollingExtensions
    {
        private static readonly List<ApiInputUpdateMessagePolymorphContainer> _emptyUpdates = [];

        /// <summary>
        /// Запустить бесконечный цикл по обработке update messages
        /// </summary>
        /// <param name="maxBotClient"></param>
        /// <param name="updateHandler"></param>
        /// <param name="errorHandler"></param>
        /// <param name="requestOptions"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task StartLongPollingAsync(
            this IMaxBotClient maxBotClient,
            Func<IMaxBotClient, ApiInputUpdateMessagePolymorphContainer, CancellationToken, Task> updateHandler,
            Func<IMaxBotClient, MaxBotLongPollingErrorType, Exception, CancellationToken, Task>? errorHandler = null,
            (int Limit, long? StartupMarket)? requestOptions = null,
            CancellationToken cancellationToken = default)
        {
            errorHandler ??= (_, _, _, _) => Task.CompletedTask;

            return Task.Run(async () =>
            {
                var request = new GetUpdatesRequest()
                {
                    Limit = requestOptions?.Limit ?? 10,
                    Marker = requestOptions?.StartupMarket
                };

                while (!cancellationToken.IsCancellationRequested)
                {
                    IReadOnlyList<ApiInputUpdateMessagePolymorphContainer> updates = _emptyUpdates;

                    try
                    {
                        var response = await maxBotClient.SendRequestAsync(request, new MaxBotRequestResilienceDefaultSettings
                        {
                            EnableRetryStrategy = false,
                            Timeout = TimeSpan.FromMinutes(5)
                        }, cancellationToken);

                        request.Marker = response.Marker;

                        if (response.Updates.Count == 0)
                        {
                            continue;
                        }

                        updates = response.Updates;
                    }
                    catch (OperationCanceledException)
                    {
                        return;
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            await errorHandler(maxBotClient, MaxBotLongPollingErrorType.GetUpdatesError, ex, cancellationToken);
                        }
                        catch (OperationCanceledException)
                        {
                            return;
                        }
                    }

                    foreach (var update in updates)
                    {
                        try
                        {
                            await updateHandler(maxBotClient, update, cancellationToken);
                        }
                        catch (OperationCanceledException)
                        {
                            return;
                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                await errorHandler(maxBotClient, MaxBotLongPollingErrorType.HandlerError, ex, cancellationToken);
                            }
                            catch (OperationCanceledException)
                            {
                                return;
                            }
                        }
                    }
                }

            }, cancellationToken);
        }

        /// <summary>
        /// Будет добавлен менеджер по управлению ЖЦ LongPolling для каждого бота индивидуально через интерфейс `IMaxBotLongPollingManager`.
        /// </summary>
        /// <param name="maxBotSystemConfigurer"></param>
        /// <returns></returns>
        public static MaxBotSystemConfigurer AddLongPollingManager(this MaxBotSystemConfigurer maxBotSystemConfigurer)
        {
            var services = maxBotSystemConfigurer.Services;

            services.AddSingleton<IMaxBotLongPollingManager, MaxBotLongPollingManagerInternal>();

            return maxBotSystemConfigurer;
        }

        /// <summary>
        /// Рекомендуется использовать если планируете запускать 1 бота на систему.
        /// <br/>
        /// Обработку update message нужно будет добавить отдельно.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="botSettings"></param>
        /// <returns></returns>
        public static IServiceCollection AddMaxBotSingletonWithLongPolling(this IServiceCollection services, (string Token, string? HashSecretKey) botSettings)
        {
            services.AddMaxBotSystem(sc =>
            {
                sc.AddSingletonClient(botSettings.Token, botSettings.HashSecretKey);
                sc.AddLongPollingManager();
            });

            services.AddHostedService<MaxBotClientSingletonLongPollingStartupHostedServiceInternal>();

            return services;
        }

        /// <summary>
        /// Рекомендуется использовать если планируете запускать 1 бота на систему.
        /// <br/>
        /// Можно сразу указать `MaxBotMessageHandlersRouteBuilder` для обработки сообщений.
        /// </summary>
        /// <typeparam name="TUserState"></typeparam>
        /// <typeparam name="TAuth"></typeparam>
        /// <param name="services"></param>
        /// <param name="botSettings"></param>
        /// <param name="handlersBuilder"></param>
        /// <returns></returns>
        public static IServiceCollection AddMaxBotSingletonWithLongPolling<TUserState, TAuth>(this IServiceCollection services, (string Token, string? HashSeed) botSettings, MaxBotMessageHandlersRouteBuilder<TUserState, TAuth> handlersBuilder)
            where TUserState : class, IMaxBotMessageHandlerUserState, new()
            where TAuth : IMaxBotMessageHandlerAuth
        {
            services.AddMaxBotSingletonWithLongPolling(botSettings);

            services.AddMaxBotMessageHandlerSystem(c =>
            {
                c.AddHandler(MaxBotClientSingletonLongPollingStartupHostedServiceInternal.DefaultHandlerPrefix, handlersBuilder);
            });

            return services;
        }
    }
}