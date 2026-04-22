namespace KmsDev.MaxBot.Full.Contracts
{
    public interface IMaxBotMessageHandlerAuth
    {
        Task<bool> AuthAsync(IMaxBotMessageHandlerRequestAccessor messageHandlerRequestAccessor);
    }
}
