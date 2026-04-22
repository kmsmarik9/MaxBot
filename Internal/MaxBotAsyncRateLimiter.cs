namespace KmsDev.MaxBot.Full
{
    internal class MaxBotAsyncRateLimiter
    {
        private readonly TimeSpan _interval;
        private readonly SemaphoreSlim _semaphore = new(1, 1);
        private DateTime _lastExecuted = DateTime.MinValue;

        public MaxBotAsyncRateLimiter(TimeSpan interval)
        {
            _interval = interval;
        }

        public async Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> func, CancellationToken cancellationToken = default)
        {
            await _semaphore.WaitAsync(cancellationToken);

            try
            {
                var nowUtc = DateTime.UtcNow;
                var elapsed = nowUtc - _lastExecuted;

                if (elapsed < _interval)
                {
                    await Task.Delay(_interval - elapsed, cancellationToken);
                }

                return await func();
            }
            finally
            {
                _lastExecuted = DateTime.UtcNow;
                _semaphore.Release();
            }
        }
    }
}
