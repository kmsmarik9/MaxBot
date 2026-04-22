namespace KmsDev.MaxBot.Full.Handlers
{
    public delegate Task MaxBotMessageHandlerErrorDelegate(IMaxBotClient botClient, MaxBotGetUpdatesErrorType errorType, Exception ex, CancellationToken cancellationToken);
}
