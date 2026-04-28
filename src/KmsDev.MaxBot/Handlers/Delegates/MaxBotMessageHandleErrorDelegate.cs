namespace KmsDev.MaxBot.Handlers
{
    public delegate Task MaxBotMessageHandlerErrorDelegate(IMaxBotClient botClient, MaxBotGetUpdatesErrorType errorType, Exception ex, CancellationToken cancellationToken);
}
