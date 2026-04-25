namespace KmsDev.MaxBot.Full.Handlers
{
    public interface IMaxBotMessageHandlerAuth
    {
        Task<bool> AuthAsync(IMaxBotMessageHandlerRequestAccessor messageHandlerRequestAccessor);
    }
}
