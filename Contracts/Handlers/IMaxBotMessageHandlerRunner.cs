namespace KmsDev.MaxBot.Full.Contracts
{
    internal interface IMaxBotMessageHandlerRunner
    {
        Task RunAsync(string handlersPrefix);
    }
}
