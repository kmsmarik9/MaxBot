namespace KmsDev.MaxBot.Full.Handlers
{
    internal class MaxBotMessageHandlerInMemoryUserStatePersistenceInternal : IMaxBotMessageHandlerUserSatePersistence
    {
        private readonly Dictionary<long, Dictionary<string, (string Route, string Data)>> _data = [];
        private readonly SemaphoreSlim _semaphore = new(1);

        public async Task<(string Route, string Data)?> LoadAsync(string handlersPrefix, long maxUserId)
        {
            await _semaphore.WaitAsync();
            try
            {
                if (_data.TryGetValue(maxUserId, out var userData))
                {
                    if(userData.TryGetValue(handlersPrefix.ToUpperInvariant(), out var data))
                    {
                        return data;
                    }
                }

                return null;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task SaveAsync(string handlersPrefix, long maxUserId, (string Route, string Data) payload)
        {
            await _semaphore.WaitAsync();
            try
            {
                if(!_data.TryGetValue(maxUserId, out var userData))
                {
                    userData = [];
                    _data[maxUserId] = userData;
                }

                userData[handlersPrefix.ToUpperInvariant()] = payload;
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
