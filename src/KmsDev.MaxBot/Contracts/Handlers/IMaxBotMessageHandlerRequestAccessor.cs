using KmsDev.MaxBot.Handlers;
using KmsDev.MaxBot.Models;

namespace KmsDev.MaxBot.Handlers
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
