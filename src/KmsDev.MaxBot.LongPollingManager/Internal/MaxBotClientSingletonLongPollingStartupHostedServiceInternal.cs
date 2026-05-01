using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KmsDev.MaxBot.LongPollingManager
{
    internal class MaxBotClientSingletonLongPollingStartupHostedServiceInternal : BackgroundService
    {
        public const string DefaultHandlerPrefix = "default_singleton";

        private readonly IServiceScopeFactory _serviceScopeFactory;

        public MaxBotClientSingletonLongPollingStartupHostedServiceInternal(IServiceScopeFactory serviceScopeFactory)
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

                    var botManager = scope.ServiceProvider.GetService<IMaxBotLongPollingManager>();

                    if (botManager != null)
                    {
                        var botClient = scope.ServiceProvider.GetRequiredService<IMaxBotClient>();

                        await botManager.StartBotAsync(botClient, DefaultHandlerPrefix, stoppingToken);
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
