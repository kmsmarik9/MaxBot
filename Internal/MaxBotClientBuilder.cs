namespace KmsDev.MaxBot.Full
{
    internal class MaxBotClientBuilder : IMaxBotClientBuilder
    {
        private readonly IServiceProvider _serviceProvider;

        public MaxBotClientBuilder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IMaxBotClient Build(string token, string botHashSeed = "", CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException("empty", nameof(token));
            }

            return new MaxBotClient(_serviceProvider, token, botHashSeed, cancellationToken);
        }
    }
}
