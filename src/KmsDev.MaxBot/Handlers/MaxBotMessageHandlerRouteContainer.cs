namespace KmsDev.MaxBot.Handlers
{
    public sealed class MaxBotMessageHandlerRouteContainer
    {
        private string _fullRoutePath = string.Empty;
        private readonly List<string> _routePaths = [];

        public string FullRoutePath => _fullRoutePath;

        public void Init(string fullRoutePath)
        {
            _routePaths.AddRange(fullRoutePath.Split('#', StringSplitOptions.RemoveEmptyEntries));

            ReFillFullRoutePath();
        }

        private void ReFillFullRoutePath()
        {
            _fullRoutePath = string.Join("#", _routePaths);
        }

        public void PushRoutePath(string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                _routePaths.Add(path);
                ReFillFullRoutePath();
            }
        }

        public void ForceSetRoutePaths(params string[] paths)
        {
            if (paths.Length > 0)
            {
                ClearPath();
            }

            _routePaths.AddRange(paths);
            ReFillFullRoutePath();
        }

        public string PopRoutePath()
        {
            var result = string.Empty;

            if (_routePaths.Count > 0)
            {
                var removeIndex = _routePaths.Count - 1;

                result = _routePaths[removeIndex];

                _routePaths.RemoveAt(removeIndex);

                ReFillFullRoutePath();
            }

            return result;
        }

        public void ClearPath()
        {
            _routePaths.Clear();
            _routePaths.Capacity = 0;

            ReFillFullRoutePath();
        }
    }
}
