namespace KmsDev.MaxBot.Handlers
{
    internal interface IMaxBotMessageHandlerRunner
    {
        Task RunAsync(string handlersPrefix);
    }
}
