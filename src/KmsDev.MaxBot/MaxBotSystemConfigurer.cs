using Microsoft.Extensions.DependencyInjection;

namespace KmsDev.MaxBot
{
    public class MaxBotSystemConfigurer
    {
        public IServiceCollection Services { get; }

        private (string BotToken, string? HashSeed)? _singletonOptions = null;

        public MaxBotSystemConfigurer(IServiceCollection services)
        {
            Services = services;
        }

        /// <summary>
        /// Добавление singleton клиента с интерфейсом IMaxBotClient
        /// <br/>
        /// </summary>
        /// <returns></returns>
        public MaxBotSystemConfigurer AddSingletonClient(string botToken, string? botHashSecretKey)
        {
            _singletonOptions = (botToken, botHashSecretKey);
            return this;
        }

        internal void ConfigureServices()
        {
            if (_singletonOptions.HasValue)
            {
                var options = _singletonOptions.Value;

                Services.AddSingleton<IMaxBotClient>(sp =>
                {
                    var clientBuilder = sp.GetRequiredService<IMaxBotClientBuilder>();
                    return clientBuilder.Build(options.BotToken, options.HashSeed ?? string.Empty);
                });
            }
        }        
    }
}