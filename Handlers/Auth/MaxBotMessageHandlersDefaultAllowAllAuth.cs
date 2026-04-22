using KmsDev.MaxBot.Full.Contracts;

namespace KmsDev.MaxBot.Full.Handlers
{
    public class MaxBotMessageHandlersDefaultAllowAllAuth : IMaxBotMessageHandlerAuth
    {
        private readonly static Task<bool> _trueAuth = Task.FromResult(true);

        public Task<bool> AuthAsync(IMaxBotMessageHandlerRequestAccessor messageHandlerRequestAccessor)
        {
            return _trueAuth;
        }
    }
}
