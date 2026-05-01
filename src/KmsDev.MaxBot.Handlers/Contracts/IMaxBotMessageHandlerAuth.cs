namespace KmsDev.MaxBot.Handlers
{
    public interface IMaxBotMessageHandlerAuth
    {
        Task<bool> AuthAsync(IMaxBotMessageHandlerRequestAccessor messageHandlerRequestAccessor);
    }
}
