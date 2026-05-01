using OneOf;

namespace KmsDev.MaxBot.LongPollingManager
{
    public interface IMaxBotLongPollingManager
    {
        event MaxBotMessageHandlerErrorDelegate? BotMessageHandlerError;
        Task StartBotAsync(IMaxBotClient maxBotClient, string handlersPrefix, CancellationToken cancellationToken = default);
        Task StopBotAsync(OneOf<IMaxBotClient, string> target);
    }
}
