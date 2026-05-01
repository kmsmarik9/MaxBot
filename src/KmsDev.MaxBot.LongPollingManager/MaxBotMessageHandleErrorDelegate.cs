namespace KmsDev.MaxBot.LongPollingManager
{
    public delegate Task MaxBotMessageHandlerErrorDelegate(IMaxBotClient botClient, MaxBotLongPollingErrorType errorType, Exception ex, CancellationToken cancellationToken);
}
