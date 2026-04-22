using KmsDev.MaxBot.Full.Models;

namespace KmsDev.MaxBot.Full.Handlers
{
    public class MaxBotMessageHandlerRequestData
    {
        public required IMaxBotClient MaxBotClient { get; init; }
        public required string HandlersPrefix { get; init; }
        public required ApiInputUpdateMessagePolymorphContainer UpdateMessageContainer { get; init; }
        public required long MaxUserId { get; init; }
    }
}
