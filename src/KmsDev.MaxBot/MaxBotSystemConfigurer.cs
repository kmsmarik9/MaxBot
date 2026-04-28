using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OneOf;

namespace KmsDev.MaxBot
{
    public class MaxBotSystemConfigurer
    {
        private (string BotToken, string? HashSeed, string? HandlersPrefix)? _singletonOptions = null;

        private bool _addLongPollongManager;
        private bool _addWebhookManager;

        /// <summary>
        /// Добавление singleton клиента с интерфейсом IMaxBotClient
        /// <br/>
        /// Можно задать HandlersPrefix и ManagerOptions для автоматического запуска системы обработчиков
        /// </summary>
        /// <param name="botOptions"></param>
        /// <param name="handlersOptions"></param>
        /// <returns></returns>
        public MaxBotSystemConfigurer AddSingletonClient((string BotToken, string? HashSeed) botOptions, (string HandlersPrefix, OneOf<LongPollingManagerOptions> ManagerOptions)? handlersOptions = null)
        {
            _singletonOptions = (botOptions.BotToken, botOptions.HashSeed, handlersOptions?.HandlersPrefix);

            if(handlersOptions.HasValue)
            {
                handlersOptions.Value.ManagerOptions.Switch
                (
                    lpmo =>
                    {
                        AddLongPollingManager();
                    }
                );
            }

            return this;
        }

        public MaxBotSystemConfigurer AddLongPollingManager()
        {
            _addWebhookManager = false;
            _addLongPollongManager = true;
            return this;
        }

        //TODO not impl
        //public MaxBotSystemConfigurer AddWebhookManager()
        //{
        //    _addLongPollongManager = false;
        //    _addWebhookManager = true;
        //    return this;
        //}

        public class LongPollingManagerOptions
        {

        }

        ///TODO
        //public class WebHookManagerOptions
        //{

        //}

        internal void ConfigureServices(IServiceCollection serviceCollection)
        {
            if (_singletonOptions.HasValue)
            {
                var options = _singletonOptions.Value;

                serviceCollection.AddSingleton<IMaxBotClient>(sp =>
                {
                    var clientBuilder = sp.GetRequiredService<IMaxBotClientBuilder>();
                    return clientBuilder.Build(options.BotToken, options.HashSeed ?? string.Empty);
                });

                if (!string.IsNullOrWhiteSpace(options.HandlersPrefix))
                {
                    serviceCollection.AddSingleton(new MaxBotClientSingletonStartupOptionsInternal
                    {
                        HandlersPrefix = options.HandlersPrefix
                    });

                    serviceCollection.AddHostedService<MaxBotClientSingletonStartupHostedServiceInternal>();
                }
            }

            if (_addLongPollongManager)
            {
                serviceCollection.AddSingleton<IMaxBotManager, MaxBotLongPollingManagerInternal>();
            }
            else if (_addWebhookManager)
            {
                serviceCollection.AddSingleton<IMaxBotManager, MaxBotWebhookManagerInternal>();
            }
        }

        internal class MaxBotClientSingletonStartupOptionsInternal
        {
            public required string HandlersPrefix { get; init; }
        }

        internal class MaxBotClientSingletonStartupHostedServiceInternal : BackgroundService
        {
            private readonly IServiceScopeFactory _serviceScopeFactory;

            public MaxBotClientSingletonStartupHostedServiceInternal(IServiceScopeFactory serviceScopeFactory)
            {
                _serviceScopeFactory = serviceScopeFactory;
            }

            protected override Task ExecuteAsync(CancellationToken stoppingToken)
            {
                return Task.Run(async () =>
                {
                    try
                    {
                        using var scope = _serviceScopeFactory.CreateScope();

                        var botManager = scope.ServiceProvider.GetService<IMaxBotManager>();

                        if (botManager != null)
                        {
                            var options = scope.ServiceProvider.GetRequiredService<MaxBotClientSingletonStartupOptionsInternal>();
                            var botClient = scope.ServiceProvider.GetRequiredService<IMaxBotClient>();

                            await botManager.AddBotAsync(botClient, options.HandlersPrefix, stoppingToken);
                        }
                        else
                        {
                            //TODO
                        }
                    }
                    catch (Exception ex)
                    {
                        //TODO
                    }
                }, stoppingToken);
            }
        }
    }
}
