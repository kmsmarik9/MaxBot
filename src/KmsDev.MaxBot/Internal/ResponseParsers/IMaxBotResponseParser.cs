using KmsDev.MaxBot.Responses;

namespace KmsDev.MaxBot
{
    internal interface IMaxBotResponseParser
    {
        internal Task<TResponse> ParseAsync<TResponse>(HttpResponseMessage response, CancellationToken cancellationToken = default)
            where TResponse : IMaxBotResponse;
    }
}
