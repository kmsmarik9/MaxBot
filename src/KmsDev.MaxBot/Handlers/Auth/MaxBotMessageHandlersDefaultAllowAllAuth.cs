namespace KmsDev.MaxBot.Handlers
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
