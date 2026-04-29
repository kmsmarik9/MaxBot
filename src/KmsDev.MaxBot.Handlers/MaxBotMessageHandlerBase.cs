using Metalama.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace KmsDev.MaxBot.Handlers
{
    public abstract partial class MaxBotMessageHandlerBase<TUserState> : IMaxBotMessageHandler<TUserState>
        where TUserState : class, IMaxBotMessageHandlerUserState, new()
    {
        [Dependency(IsRequired = true)]
        protected IServiceProvider ServiceProvider { get; init; }

        protected IMaxBotMessageHandlerRequestAccessor<TUserState> RequestAccessor { get; init; }

        public MaxBotMessageHandlerBase()
        {
            RequestAccessor = ServiceProvider!.GetRequiredService<IMaxBotMessageHandlerRequestAccessor<TUserState>>();
        }

        public abstract Task RunAsync(string triggeredRoute);
    }
}
