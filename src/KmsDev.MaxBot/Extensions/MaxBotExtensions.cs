using KmsDev.MaxBot.Requests;
using Microsoft.Extensions.DependencyInjection;

namespace KmsDev.MaxBot
{
    public static partial class MaxBotExtensions
    {
        public static IServiceCollection AddMaxBotSystem(this IServiceCollection serviceCollection, Action<MaxBotSystemConfigurer>? systemConfigurer = null) //, Action<MaxBotRequestsConfigurer>? requestConfigurer = null)
        {
            serviceCollection.AddSingleton<IMaxBotClientBuilder, MaxBotClientBuilderInternal>();
            serviceCollection.AddSingleton<IMaxBotManager, MaxBotLongPollingManagerInternal>();

            {
                var conf = new MaxBotSystemConfigurer();
                systemConfigurer?.Invoke(conf);
                conf.ConfigureServices(serviceCollection);
            }

            {
                var conf = new MaxBotRequestsConfigurer();
                //requestConfigurer?.Invoke(requestsBuilder);
                conf.ConfigureServices(serviceCollection);
            }

            return serviceCollection;
        }

        internal static bool IsPresent<T>(this T? value)
        {
            return value != null;
        }
    }
}
