using KmsDev.MaxBot.Handlers;
using KmsDev.MaxBot.Models;
using Metalama.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using OneOf;

namespace KmsDev.MaxBot.LongPollingManager
{
    internal partial class MaxBotLongPollingManagerInternal : IMaxBotLongPollingManager
    {
        [Dependency]
        private readonly IServiceProvider _serviceProvider;

        private readonly SemaphoreSlim _managementSemaphore = new(1);
        private readonly Dictionary<string, MaxBotStoreItem> _bots = [];

        public event MaxBotMessageHandlerErrorDelegate? BotMessageHandlerError;

        /// <summary>
        /// Начать LongPolling подписку
        /// </summary>
        /// <param name="maxBotClient"></param>
        /// <param name="handlersPrefix"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StartBotAsync(IMaxBotClient maxBotClient, string handlersPrefix, CancellationToken cancellationToken = default)
        {
            if (_bots.ContainsKey(maxBotClient.BotHash))
            {
                //TODO
            }
            else
            {
                var internalCts = CancellationTokenSource.CreateLinkedTokenSource(maxBotClient.SelfCancellationToken, cancellationToken);
                var meResponse = await maxBotClient.Api.Bots.GetMeAsync(cancellationToken: internalCts.Token);

                await _managementSemaphore.WaitAsync();

                try
                {
                    _bots[maxBotClient.BotHash] = new MaxBotStoreItem
                    {
                        MaxBotClient = maxBotClient,
                        BotName = meResponse.UserName!,
                        CancellationTokenSource = internalCts,
                        PollingReceiveTask = maxBotClient.StartLongPollingAsync
                        (
                            updateHandler: (_, updateMessage, ct) => HandleUpdateMessage(maxBotClient, handlersPrefix, updateMessage, ct),
                            errorHandler: (_, errorType, ex, ct) => HandleError(maxBotClient, errorType, ex, ct),
                            cancellationToken: internalCts.Token
                        )
                    };
                }
                finally
                {
                    _managementSemaphore.Release();
                }
            }
        }

        /// <summary>
        /// Остановить подписку
        /// </summary>
        /// <param name="target">Можно передать MaxBotClient или BotHash</param>
        /// <returns></returns>
        public async Task StopBotAsync(OneOf<IMaxBotClient, string> target)
        {
            var botHash = target.Match
            (
                bc=>bc.BotHash,
                botHash => botHash
            );

            await _managementSemaphore.WaitAsync();

            try
            {
                if (_bots.TryGetValue(botHash, out var item))
                {
                    await item.CancellationTokenSource.CancelAsync();
                    try
                    {
                        await item.PollingReceiveTask;
                    }
                    catch (OperationCanceledException)
                    {
                        //ignore
                    }
                    catch (Exception ex)
                    {
                        //TODO
                    }
                }
            }
            finally
            {
                _managementSemaphore.Release();
            }
        }

        private async Task HandleUpdateMessage(IMaxBotClient maxBotClient, string handlersPrefix, ApiInputUpdateMessagePolymorphContainer updateMessage, CancellationToken cancellationToken)
        {
            var runner = _serviceProvider.GetRequiredService<IMaxBotMessageHandlerRunner>();

            await runner.RunAsync(maxBotClient, handlersPrefix, updateMessage, cancellationToken);
        }

        private async Task HandleError(IMaxBotClient maxBotClient, MaxBotLongPollingErrorType errorType, Exception ex, CancellationToken cancellationToken)
        {
            BotMessageHandlerError?.Invoke(maxBotClient, errorType, ex, cancellationToken);
        }

        private class MaxBotStoreItem
        {
            public required string BotName { get; init; }
            public required IMaxBotClient MaxBotClient { get; init; }
            public required CancellationTokenSource CancellationTokenSource { get; init; } 
            public required Task PollingReceiveTask { get; init; }
        }
    }
}
