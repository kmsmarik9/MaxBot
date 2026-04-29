using KmsDev.MaxBot.Requests;
using Microsoft.Extensions.DependencyInjection;

namespace KmsDev.MaxBot
{
    public static partial class MaxBotExtensions
    {
        public static IServiceCollection AddMaxBotSystem(this IServiceCollection serviceCollection, Action<MaxBotSystemConfigurer>? systemConfigurer = null) //, Action<MaxBotRequestsConfigurer>? requestConfigurer = null)
        {
            serviceCollection.AddSingleton<IMaxBotClientBuilder, MaxBotClientBuilderInternal>();

            {
                var conf = new MaxBotSystemConfigurer(serviceCollection);
                systemConfigurer?.Invoke(conf);
                conf.ConfigureServices();
            }

            {
                var conf = new MaxBotRequestsConfigurer(serviceCollection);
                //requestConfigurer?.Invoke(requestsBuilder);
                conf.ConfigureServices();
            }

            return serviceCollection;
        }
    }
}
