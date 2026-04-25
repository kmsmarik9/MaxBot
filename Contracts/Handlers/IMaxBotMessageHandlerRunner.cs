namespace KmsDev.MaxBot.Full.Handlers
{
    internal interface IMaxBotMessageHandlerRunner
    {
        Task RunAsync(string handlersPrefix);
    }
}
