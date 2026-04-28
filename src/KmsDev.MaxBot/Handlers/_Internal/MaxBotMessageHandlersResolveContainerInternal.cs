namespace KmsDev.MaxBot.Handlers
{
    internal class MaxBotMessageHandlersResolveContainerInternal
    {
        private readonly IReadOnlyList<(Func<MaxBotMessageHandlerRequestData, bool> Predicate, (string ServiceKey, string Route) Result)> _items = [];

        public MaxBotMessageHandlersResolveContainerInternal(IReadOnlyList<(Func<MaxBotMessageHandlerRequestData, bool> Predicate, (string ServiceKey, string Route) Result)> items)
        {
            _items = items;
        }

        public (string ServiceKey, string Route)? Match(MaxBotMessageHandlerRequestData requestInfo)
        {
            return _items
                .Where(p => p.Predicate(requestInfo))
                .Select(s => s.Result)
                .FirstOrDefault();
        }

        public static string GenerateContainerName(string path)
        {
            return $"{nameof(MaxBotMessageHandlersResolveContainerInternal)}_{path}";
        }
    }
}
