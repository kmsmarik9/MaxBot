namespace KmsDev.MaxBot.Full.Requests
{
    public static class MaxBotRequestNameCache<TReqeust>
        where TReqeust : IMaxBotRequest
    {
        public static readonly string Value = typeof(TReqeust).FullName!;
    }
}
