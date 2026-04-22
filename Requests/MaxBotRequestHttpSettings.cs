namespace KmsDev.MaxBot.Full.Requests
{
    public class MaxBotRequestHttpSettings
    {
        public required HttpMethod Method { get; init; }
        public required string Url { get; init; }
        public bool IncludeAuthorizationToken { get; init; } = true;
    }
}
