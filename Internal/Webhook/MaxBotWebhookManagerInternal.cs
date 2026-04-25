using KmsDev.MaxBot.Full.Handlers;
using OneOf;

namespace KmsDev.MaxBot.Full
{
    internal class MaxBotWebhookManagerInternal : IMaxBotManager
    {
        public event MaxBotMessageHandlerErrorDelegate? BotMessageHandlerError;

        public Task AddBotAsync(IMaxBotClient maxBotClient, string handlersPrefix, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task StopBot(OneOf<IMaxBotClient, ulong> target)
        {
            throw new NotImplementedException();
        }
    }
}
