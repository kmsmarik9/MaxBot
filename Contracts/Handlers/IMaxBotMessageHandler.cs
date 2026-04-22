namespace KmsDev.MaxBot.Full.Contracts
{
    public interface IMaxBotMessageHandler
    {
        Task RunAsync(string triggeredRoute);
    }

    public interface IMaxBotMessageHandler<TUserState> : IMaxBotMessageHandler
        where TUserState : IMaxBotMessageHandlerUserState
    {

    }
}
