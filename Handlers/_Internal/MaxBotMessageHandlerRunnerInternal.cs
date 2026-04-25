using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace KmsDev.MaxBot.Full.Handlers
{
    internal class MaxBotMessageHandlerRunnerInternal : IMaxBotMessageHandlerRunner
    {
        private readonly IServiceProvider _serviceProvider;

        public MaxBotMessageHandlerRunnerInternal(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task RunAsync(string handlersPrefix)
        {
            var requestAccessor = _serviceProvider.GetRequiredKeyedService<IMaxBotMessageHandlerRequestAccessor>(handlersPrefix);

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

            //var triggeredMessageHandlerStateContainer = _serviceProvider.GetRequiredService<TriggeredMessageHandlerStateContainer>();

            var handlersResolveResult = _serviceProvider.GetKeyedService<MaxBotMessageHandlersResolveContainerInternal>(MaxBotMessageHandlersResolveContainerInternal.GenerateContainerName(serviceKey))
                ?.Match(requestAccessor.RequestData);

            if(handlersResolveResult.HasValue)
            {
                var handlerServiceKey = handlersResolveResult.Value.ServiceKey;
                var handlerRoute = handlersResolveResult.Value.Route;

                var authService = _serviceProvider.GetKeyedService<IMaxBotMessageHandlerAuth>(handlerServiceKey);

                if(authService != null && await authService.AuthAsync(requestAccessor))
                {
                    try
                    {
                        var handler = _serviceProvider.GetKeyedService<IMaxBotMessageHandler>(handlerServiceKey);

                        if (handler != null)
                        {
                            await handler.RunAsync(handlerRoute);
                        }
                    }
                    catch(Exception ex)
                    {

                    }
                }
            }
        }
    }
}