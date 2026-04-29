namespace KmsDev.MaxBot
{
    public interface IMaxBotClientBuilder
    {
        /// <summary>
        /// BotHash создается через HMACSHA256 на основе `token`. 
        /// <br/>
        /// Используйте `botHashSecretKey` чтобы разбавить хэш
        /// </summary>
        /// <param name="token"></param>
        /// <param name="botHashSecretKey"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        IMaxBotClient Build(string token, string botHashSecretKey = "", CancellationToken cancellationToken = default);
    }
}
