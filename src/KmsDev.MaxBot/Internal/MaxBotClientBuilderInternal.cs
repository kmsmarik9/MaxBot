namespace KmsDev.MaxBot
{
    internal class MaxBotClientBuilderInternal : IMaxBotClientBuilder
    {
        private readonly IServiceProvider _serviceProvider;

        public MaxBotClientBuilderInternal(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IMaxBotClient Build(string token, string botHashSecretKey = "", CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException("empty", nameof(token));
            }

            return new MaxBotClientInternal(_serviceProvider, token, botHashSecretKey, cancellationToken);
        }
    }
}
