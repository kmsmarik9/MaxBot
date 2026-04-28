using KmsDev.MaxBot.Handlers;
using OneOf;

namespace KmsDev.MaxBot
{
    public interface IMaxBotManager
    {
        event MaxBotMessageHandlerErrorDelegate? BotMessageHandlerError;
        Task AddBotAsync(IMaxBotClient maxBotClient, string handlersPrefix, CancellationToken cancellationToken = default);
        Task StopBot(OneOf<IMaxBotClient, ulong> target);
    }
}
