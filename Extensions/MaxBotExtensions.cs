using KmsDev.MaxBot.Full.Requests;
using Microsoft.Extensions.DependencyInjection;

namespace KmsDev.MaxBot.Full
{
    public static partial class MaxBotExtensions
    {
        public static IServiceCollection AddMaxBotSystem(this IServiceCollection serviceCollection, Action<MaxBotRequestsConfigurer>? requestConfigurer = null)
        {
            serviceCollection.AddSingleton<IMaxBotClientBuilder, MaxBotClientBuilder>();
            serviceCollection.AddSingleton<IMaxBotManager, MaxBotManager>();

            {
                var requestsBuilder = new MaxBotRequestsConfigurer();

                requestConfigurer?.Invoke(requestsBuilder);
                requestsBuilder.ConfigureServices(serviceCollection);
            }

            return serviceCollection;
        }

        public static bool IsPresent<T>(this T? value)
        {
            return value != null;
        }
    }
}
