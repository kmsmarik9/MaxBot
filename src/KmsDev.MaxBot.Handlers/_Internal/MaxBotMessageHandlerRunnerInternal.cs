using KmsDev.MaxBot.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace KmsDev.MaxBot.Handlers
{
    internal class MaxBotMessageHandlerRunnerInternal : IMaxBotMessageHandlerRunner
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public MaxBotMessageHandlerRunnerInternal(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task RunAsync(IMaxBotClient maxBotClient, string handlersPrefix, ApiInputUpdateMessagePolymorphContainer updateMessage, CancellationToken cancellationToken = default)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            var requestAccessor = serviceProvider.GetRequiredKeyedService<IMaxBotMessageHandlerRequestAccessor>(handlersPrefix);

            if(!await requestAccessor.InitAsync(maxBotClient, handlersPrefix, updateMessage))
            {
                return;
            }

            var serviceKey = string.Empty;

            {
                var routePathBuilder = new StringBuilder(handlersPrefix);

                if (requestAccessor.RouteContainer.FullRoutePath.Length > 0)
                {
                    routePathBuilder.Append('#');
                    routePathBuilder.Append(requestAccessor.RouteContainer.FullRoutePath);
                }

                serviceKey = routePathBuilder.ToString();
            }

            var handlersResolveResult = serviceProvider.GetKeyedService<MaxBotMessageHandlersResolveContainerInternal>(MaxBotMessageHandlersResolveContainerInternal.GenerateContainerName(serviceKey))
                ?.Match(requestAccessor.RequestData);

            if(handlersResolveResult.HasValue)
            {
                var handlerServiceKey = handlersResolveResult.Value.ServiceKey;
                var handlerRoute = handlersResolveResult.Value.Route;

                var authService = serviceProvider.GetKeyedService<IMaxBotMessageHandlerAuth>(handlerServiceKey);

                if(authService != null && await authService.AuthAsync(requestAccessor))
                {
                    try
                    {
                        var handler = serviceProvider.GetKeyedService<IMaxBotMessageHandler>(handlerServiceKey);

                        if (handler != null)
                        {
                            await handler.RunAsync(handlerRoute);
                        }
                    }
                    catch(Exception ex)
                    {
                        //TODO
                    }
                }
            }
        }
    }
}