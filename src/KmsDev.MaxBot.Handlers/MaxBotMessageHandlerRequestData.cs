using KmsDev.MaxBot.Models;

namespace KmsDev.MaxBot.Handlers
{
    public class MaxBotMessageHandlerRequestData
    {
        public required IMaxBotClient MaxBotClient { get; init; }
        public required string HandlersPrefix { get; init; }
        public required ApiInputUpdateMessagePolymorphContainer UpdateMessageContainer { get; init; }
        public required long MaxUserId { get; init; }
    }
}
