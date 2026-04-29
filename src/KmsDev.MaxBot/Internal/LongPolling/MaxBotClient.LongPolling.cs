using KmsDev.MaxBot.Models;
using KmsDev.MaxBot.Requests;

namespace KmsDev.MaxBot
{
    internal static class MaxBotClientLongPollingExtensions
    {
        private static readonly List<ApiInputUpdateMessagePolymorphContainer> _emptyUpdates = []; 

        public static Task StartLongPollingAsync(
            this IMaxBotClient maxBotClient, 
            Func<IMaxBotClient, ApiInputUpdateMessagePolymorphContainer, CancellationToken, Task> updateHandler,
            Func<IMaxBotClient, MaxBotLongPollingErrorType, Exception, CancellationToken, Task>? errorHandler = null,
            CancellationToken cancellationToken = default)
        {
            errorHandler ??= (_, _, _, _) => Task.CompletedTask;

            return Task.Run(async () =>
            {
                var request = new GetUpdatesRequest()
                {
                    Limit = 10
                };

                while (!cancellationToken.IsCancellationRequested)
                {
                    IReadOnlyList<ApiInputUpdateMessagePolymorphContainer> updates = _emptyUpdates;

                    try
                    {
                        var response = await maxBotClient.SendRequestAsync(request, new MaxBotRequestResilienceDefaultSettings
                        {
                            EnableRetryStrategy = false,
                            Timeout = TimeSpan.FromHours(1)
                        }, cancellationToken);

                        request.Marker = response.Marker;

                        if(response.Updates.Count == 0)
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

                    foreach(var update in updates)
                    {
                        try
                        {
                            await updateHandler(maxBotClient, update, cancellationToken);
                        }
                        catch(OperationCanceledException)
                        {
                            return;
                        }
                        catch(Exception ex)
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
    }
}
