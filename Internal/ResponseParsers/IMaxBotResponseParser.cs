using KmsDev.MaxBot.Full.Responses;

namespace KmsDev.MaxBot.Full
{
    internal interface IMaxBotResponseParser
    {
        internal Task<TResponse> ParseAsync<TResponse>(HttpResponseMessage response, CancellationToken cancellationToken = default)
            where TResponse : IMaxBotResponse;
    }
}
