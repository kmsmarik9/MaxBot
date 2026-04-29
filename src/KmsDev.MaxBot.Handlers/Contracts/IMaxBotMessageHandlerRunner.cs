using KmsDev.MaxBot.Models;

namespace KmsDev.MaxBot.Handlers
{
    public interface IMaxBotMessageHandlerRunner
    {
        Task RunAsync(IMaxBotClient maxBotClient, string handlersPrefix, ApiInputUpdateMessagePolymorphContainer updateMessage, CancellationToken cancellationToken = default);
    }
}
