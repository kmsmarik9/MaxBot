using KmsDev.MaxBot.Handlers;
using KmsDev.MaxBot.Models;
using Metalama.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using OneOf;

namespace KmsDev.MaxBot
{
    internal partial class MaxBotLongPollingManagerInternal : IMaxBotManager
    {
        [Dependency]
        private readonly IServiceScopeFactory _serviceScopeFactory;

        private readonly SemaphoreSlim _managementSemaphore = new(1);
        private readonly Dictionary<ulong, MaxBotStoreItem> _bots = [];

        public event MaxBotMessageHandlerErrorDelegate? BotMessageHandlerError;

        public async Task AddBotAsync(IMaxBotClient maxBotClient, string handlersPrefix, CancellationToken cancellationToken = default)
        {
            await _managementSemaphore.WaitAsync();

            try
            {
                if (_bots.ContainsKey(maxBotClient.BotHash))
                {
                    //TODO
                }
                else
                {
                    var internalCts = CancellationTokenSource.CreateLinkedTokenSource(maxBotClient.SelfCancellationToken, cancellationToken);

                    var meResponse = await maxBotClient.Api.Bots.GetMeAsync(cancellationToken: internalCts.Token);

                    _bots[maxBotClient.BotHash] = new MaxBotStoreItem
                    {
                        MaxBotClient = maxBotClient,
                        BotName = meResponse.UserName!,
                        CancellationTokenSource = internalCts,
                        PollingReceiveTask = maxBotClient.StartLongPollingAsync
                        (
                            updateHandler: (_,updateMessage, ct) => HandleUpdateMessage(maxBotClient, handlersPrefix, updateMessage, ct),
                            errorHandler: (_, errorType, ex, ct) => HandleError(maxBotClient, errorType, ex, ct),
                            cancellationToken: internalCts.Token
                        )
                    };
                }
            }
            finally
            {
                _managementSemaphore.Release();
            }
        }

        public async Task StopBot(OneOf<IMaxBotClient, ulong> target)
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
            using var scope = _serviceScopeFactory.CreateScope();

            var requestAccessor = scope.ServiceProvider.GetRequiredKeyedService<IMaxBotMessageHandlerRequestAccessor>(handlersPrefix);

            var initResult = await requestAccessor.InitAsync(maxBotClient, handlersPrefix, updateMessage);
            if (!initResult)
            {
                return;
            }

            var runner = scope.ServiceProvider.GetRequiredService<IMaxBotMessageHandlerRunner>();

            await runner.RunAsync(handlersPrefix);
        }

        private async Task HandleError(IMaxBotClient maxBotClient, MaxBotGetUpdatesErrorType errorType, Exception ex, CancellationToken cancellationToken)
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
