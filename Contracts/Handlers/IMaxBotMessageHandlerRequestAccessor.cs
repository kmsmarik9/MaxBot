using KmsDev.MaxBot.Full.Handlers;
using KmsDev.MaxBot.Full.Models;

namespace KmsDev.MaxBot.Full.Handlers
{
    public interface IMaxBotMessageHandlerRequestAccessor
    {
        MaxBotMessageHandlerRequestData RequestData { get; }   
        long MaxUserId { get; }
        MaxBotMessageHandlerRouteContainer RouteContainer { get; }
        internal Task<bool> InitAsync(IMaxBotClient maxBotClient, string handlersPrefix, ApiInputUpdateMessagePolymorphContainer updateMessage);
    }

    public interface IMaxBotMessageHandlerRequestAccessor<TUserState> : IMaxBotMessageHandlerRequestAccessor
        where TUserState : class, IMaxBotMessageHandlerUserState, new()
    {
        TUserState UserState { get; }
        Task SaveStateChangesAsync();
    }
}
