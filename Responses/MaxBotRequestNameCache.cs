namespace KmsDev.MaxBot.Full.Responses
{
    public static class MaxBotResponseNameCache<TResponse>
        where TResponse : IMaxBotResponse
    {
        public static readonly string Value = typeof(TResponse).FullName!;
    }
}
