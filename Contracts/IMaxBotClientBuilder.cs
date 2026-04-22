namespace KmsDev.MaxBot.Full
{
    public interface IMaxBotClientBuilder
    {
        /// <summary>
        /// BotHash создается через <see cref="System.IO.Hashing.XxHash3.HashToUInt64"/> на основе `token`. 
        /// <br/>
        /// Используйте `secretKey` чтобы разбавить хэш
        /// </summary>
        /// <param name="token"></param>
        /// <param name="botHashSeed"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        IMaxBotClient Build(string token, string botHashSeed = "", CancellationToken cancellationToken = default);
    }
}
