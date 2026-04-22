namespace KmsDev.MaxBot.Full.Contracts
{
    public interface IMaxBotMessageHandlerUserSatePersistence
    {
        Task<(string Route, string Data)?> LoadAsync(string handlersPrefix, long maxUserId);
        Task SaveAsync(string handlersPrefix, long maxUserId, (string Route, string Data) payload);
    }
}
