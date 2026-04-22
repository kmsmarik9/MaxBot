namespace KmsDev.MaxBot.Full.Handlers
{
    internal class MaxBotMessageHandlersSqliteUserStateModelInternal
    {
        public long Id { get; set; }
        public required string HandlersPrefix { get; set; }
        public required long MaxUserId { get; set; }
        public required string StatePath { get; set; }
        public required string Data { get; set; }
    }
}
