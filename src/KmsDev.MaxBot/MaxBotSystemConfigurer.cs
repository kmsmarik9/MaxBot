using Microsoft.Extensions.DependencyInjection;

namespace KmsDev.MaxBot
{
    public class MaxBotSystemConfigurer
    {
        public static readonly Uri DefaultBaseAddress = new("https://platform-api2.max.ru/");

        public IServiceCollection Services { get; }

        /// <summary>
        /// Базовый адрес API. По умолчанию <see cref="DefaultBaseAddress"/>.
        /// </summary>
        public Uri BaseAddress { get; private set; } = DefaultBaseAddress;

        /// <summary>
        /// Только для тестов, не использовать на проде. <br/> Включить игнорирование проблем с сертификатом на сервере. 
        /// </summary>
        public bool UseDangerousAcceptAnyServerCertificateValidator { get; set; } = false;

        private (string BotToken, string? HashSeed)? _singletonOptions = null;

        public MaxBotSystemConfigurer(IServiceCollection services)
        {
            Services = services;
        }

        /// <summary>
        /// Базовый URL для запросов к API. Относительные пути методов дописываются к нему.
        /// <br/>
        /// Если завершающего <c>/</c> нет, он будет добавлен.
        /// </summary>
        public MaxBotSystemConfigurer SetBaseAddress(string baseAddress)
        {
            if (string.IsNullOrWhiteSpace(baseAddress))
            {
                throw new ArgumentException("empty", nameof(baseAddress));
            }

            var normalized = baseAddress.Trim();
            if (!normalized.EndsWith('/'))
            {
                normalized += "/";
            }

            if (!Uri.TryCreate(normalized, UriKind.Absolute, out var uri)
                || (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
            {
                throw new ArgumentException("absolute http or https uri required", nameof(baseAddress));
            }

            BaseAddress = uri;
            return this;
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